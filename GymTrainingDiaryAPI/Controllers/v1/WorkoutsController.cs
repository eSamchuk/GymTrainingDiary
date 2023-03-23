using GymTrainingDiary.Caching;
using GymTrainingDiary.DataAccess.Interfaces;
using GymTrainingDiary.DataHandling;
using GymTrainingDiary.DTO;
using GymTrainingDiary.Mapping.EntityToDto;
using GymTrainingDiary.Mapping.ModelToEntity;
using GymTrainingDiary.Model;
using GymTrainingDiary.Utilities.Abstractions;
using GymTrainingDiary.Utilities.ActionFifters;
using GymTrainingDiary.Validation.ModelValidation.Workout;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System.Diagnostics.Metrics;
using System.Text;
using System.Text.Json;

namespace GymTrainingDiaryAPI.Controllers.v1
{
    [Area("api")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    //[ServiceFilter(typeof(ExecutionTimeMetrics))]
    public class WorkoutsController : ControllerBase
    {
        private readonly IWorkoutRepository workoutRepo;
        private readonly IWorkoutExerciseRepository workoutExerciseRepo;
        private readonly IUserRepository userRepository;
        private readonly IDistributedCache distributedCache;


        private readonly LinkGenerator linkGenerator;

        private readonly Dictionary<string, string> methodNames = new Dictionary<string, string>
        {
            [nameof(DefaultApiConventions.Get)] = nameof(GetWorkoutById),
            [nameof(DefaultApiConventions.Post)] = nameof(AddNewWorkout),
            [nameof(DefaultApiConventions.Put)] = nameof(UpdateWorkout),
            [nameof(DefaultApiConventions.Delete)] = nameof(DeleteWorkout),
        };

        public WorkoutsController(
            IWorkoutRepository workoutRepo,
            IWorkoutExerciseRepository workoutExerciseRepo,
            LinkGenerator linkGenerator,
            IUserRepository userRepository,
            IDistributedCache distributedCache)
        {
            this.workoutRepo = workoutRepo;
            this.workoutExerciseRepo = workoutExerciseRepo;
            this.linkGenerator = linkGenerator;
            this.userRepository = userRepository;
            this.distributedCache = distributedCache;
        }

        [HttpGet("{id:int:min(1)}", Name = nameof(GetWorkoutById))]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public ActionResult<WorkoutDTO> GetWorkoutById(int id, bool includeExercises = false)
        {
            var foundItem = this.workoutRepo.GetItemById(id);

            if (foundItem == null) return NotFound();

            var result = foundItem.MapWorkoutToDto();

            if (includeExercises)
            {
                var exercises = (this.workoutExerciseRepo.GetItemsByCondtion(x => x.WorkoutId == id)).ToList();
                result.Exercises = exercises.Select(x => x.MapWorkoutExerciseToDto()).ToList();
            }

            result.Links = this.linkGenerator.CreateLinksForResource(this.HttpContext, foundItem.Id, this.methodNames);

            return result;
        }

        [HttpGet()]
        [Cached(60)]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public ActionResult<ListDTO<WorkoutDTO>> GetAllWorkouts()
        {
            ListDTO<WorkoutDTO> result = new ListDTO<WorkoutDTO>();

            var dbResults = this.workoutRepo.GetAllItems();
            if (!dbResults.Any()) return Ok("No records found");

            result.TotalCount = dbResults.Count();
            result.Items = dbResults.Select(x => EntitiesToDtoMapper.MapWorkoutToDto(x)).ToList();

            return Ok(result);
        }


        [HttpGet("Paged")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public ActionResult<ListDTO<WorkoutDTO>> GetAllWorkoutsPaged(int page, int perPage)
        {
            if (page == 0 || perPage == 0)
            {
                ModelState.AddModelError("Params", "Parameters values cannot be zero");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.FormatErrors());
            }

            ListDTO<WorkoutDTO> result = new ListDTO<WorkoutDTO>();

            var dbResults = this.workoutRepo.GetPagedItems(page, perPage);

            if (dbResults.Items?.Any() == false) return Ok("Nothing found, check parameters");

            var metadata = new
            {
                dbResults.TotalCount,
                dbResults.TotalPages,
                dbResults.CurrentPage,
                dbResults.PageSize,
                dbResults.HasNext,
                dbResults.HasPrevious
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(metadata));
            result.Items = dbResults.Items?.Select(EntitiesToDtoMapper.MapWorkoutToDto).ToList();

            return Ok(result);
        }

        [HttpPost(Name = nameof(AddNewWorkout))]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public ActionResult<WorkoutDTO> AddNewWorkout(WorkoutModel model)
        {
            var validator = new WorkoutAddValidator();
            var validationResult = validator.Validate(model);

            if (!validationResult.IsValid)
            {
                ModelState.AddModelError("Validation errors", string.Join(", ", validationResult.Errors.Select(x => x.ErrorMessage)));
            }

            var validUser = this.userRepository.IsItemsExistForCondition(x => x.Id == model.UserId);

            if (!validUser)
            {
                ModelState.AddModelError("User", "User with specified Id doesn't exist");
            }

            var existingWorkout = (this.workoutRepo.GetItemsByCondtion(x => x.UserId == model.UserId && x.WorkoutStart == model.WorkoutStart)).FirstOrDefault();

            if (existingWorkout != null)
            {
                ModelState.AddModelError("Duplicate", "Workout for this user at given time already exist");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState.FormatErrors());

            var addedItem = this.workoutRepo.AddItem(model.MapWorkoutModelToEntity());

            return CreatedAtRoute(
                nameof(GetWorkoutById),
                new { addedItem.Id },
                addedItem.MapWorkoutToDto());
        }

        [HttpPut(Name = nameof(UpdateWorkout))]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public ActionResult<WorkoutDTO> UpdateWorkout(WorkoutModel model)
        {
            if (model.Id == 0)
            {
                ModelState.AddModelError("Id", "Id field should be specified for Workout in order to update it");
            }

            var existingWorkout = this.workoutRepo.GetItemById(model.Id);

            if (existingWorkout == null)
            {
                ModelState.AddModelError("Not found", "Resource with specified Id was not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.FormatErrors());
            }

            this.workoutRepo.UpdateItem(model.MapWorkoutModelToEntity());

            return Ok();
        }

        [HttpDelete("{id:int:min(1)}", Name = nameof(DeleteWorkout))]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        public ActionResult DeleteWorkout(int Id)
        {
            var existingWorkout = this.workoutRepo.GetItemById(Id);

            if (existingWorkout == null)
            {
                ModelState.AddModelError("Not found", "Resource with specified Id was not found");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.FormatErrors());
            }

            var result = this.workoutRepo.DeleteItem(Id);

            return result ? Ok() : BadRequest();
        }
    }
}