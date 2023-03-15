using GymTrainingDiary.DataAccess.Interfaces;
using GymTrainingDiary.DataHandling;
using GymTrainingDiary.DTO;
using GymTrainingDiary.Mapping.EntityToDto;
using GymTrainingDiary.Mapping.ModelToEntity;
using GymTrainingDiary.Model;
using GymTrainingDiary.Utilities.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using System.Text.Json;

namespace GymTrainingDiaryAPI.Controllers.v1
{
    [Area("api")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [Produces(MediaTypeNames.Application.Json, MediaTypeNames.Application.Xml)]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IWorkoutRepository workoutRepository;
        private readonly LinkGenerator linkGenerator;

        public Dictionary<string, string> Methods = new Dictionary<string, string>
        {
            [nameof(DefaultApiConventions.Get)] = nameof(GetUserById),
            [nameof(DefaultApiConventions.Post)] = nameof(AddNewUser),
            [nameof(DefaultApiConventions.Put)] = nameof(UpdateUser),
            [nameof(DefaultApiConventions.Delete)] = nameof(DeleteUser),
        };

        public UsersController(
            IUserRepository userRepository, 
            LinkGenerator linkGenerator, 
            IWorkoutRepository workoutRepository)
        {
            this.userRepository = userRepository;
            this.linkGenerator = linkGenerator;
            this.workoutRepository = workoutRepository;
        }

        [HttpGet(Name = nameof(GetAllUsers))]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UserDTO>> GetAllUsers()
        {
            ListDTO<UserDTO> result = new ListDTO<UserDTO>();

            var dbResults =  this.userRepository.GetAllItems();

            if (!dbResults.Any()) return Ok("No records found");

            result.Items = dbResults.Select(x => x.MapUserToDto()).ToList();
            result.TotalCount = dbResults.Count();

            foreach (var item in result.Items)
            {
                item.Links = this.linkGenerator.CreateLinksForResource(this.HttpContext, item.Id, this.Methods);
            }

            return Ok(result);
        }

        [HttpGet("{userId:int:min(1)}", Name = nameof(GetUserById))]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        public ActionResult<UserDTO> GetUserById([FromRoute] int userId)
        {
            var foundUser =  this.userRepository.GetItemById(userId);

            if (foundUser == null) return NotFound("User not found");

            var result = foundUser.MapUserToDto();

            result.Links = this.linkGenerator.CreateLinksForResource(this.HttpContext, userId, this.Methods);

            return Ok(result);
        }

        [HttpGet("Paged",Name = nameof(GetPagedUsers))]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        public ActionResult<ListDTO<UserDTO>> GetPagedUsers(int page, int perPage)
        {
            if (page == 0 || perPage == 0)
            {
                ModelState.AddModelError("Params", "Parameters values cannot be zero");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState.FormatErrors());
            }

            ListDTO<UserDTO> result = new ListDTO<UserDTO>();

            var dbResults =  this.userRepository.GetPagedItems(page, perPage);

            if (dbResults.Items?.Any() == false) return Ok("Nothing found, check page and/or perPage parameters");
            
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
            result.Items = dbResults.Items?.Select(EntitiesToDtoMapper.MapUserToDto).ToList();

            foreach (var item in result.Items)
            {
                item.Links = this.linkGenerator.CreateLinksForResource(this.HttpContext, item.Id, this.Methods);
            }

            return Ok(result);
        }

        [HttpPost(Name = nameof(AddNewUser))]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        [ProducesResponseType(typeof(WorkoutDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> AddNewUser(UserModel model)
        {
            if (model.Id != 0)
            {
                ModelState.AddModelError("Id", "Id should not be specified for a new User");
            }

            if (this.userRepository.IsUserExist(model.FirstName.ToUpper(), model.LastName.ToUpper()))
            {
                ModelState.AddModelError("Duplicate", "User with specified first and last names already exist");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState.FormatErrors());

            var result =  this.userRepository.AddItem(model.MapUserModelToEntity());

            return CreatedAtAction(nameof(AddNewUser), result.Id, result.MapUserToDto());
        }

        [HttpGet("{userId:int:min(1)}/Workouts")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(typeof(ListDTO<WorkoutDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ListDTO<WorkoutDTO>> GetWorkoutsForUser(int userId)
        {
            var result = new ListDTO<WorkoutDTO>();

            var dbResults = ( this.workoutRepository.GetItemsByCondtion(x => x.UserId == userId)).ToList();

            if (!dbResults.Any()) return NotFound("No records found");

            result.Items = dbResults.Select(x => x.MapWorkoutToDto()).ToList();
            result.TotalCount = result.Items.Count();


            ////TODO resource links
            //foreach (var item in result.Items)
            //{
            //    item.Links = 
            //}


            return Ok(result);
        }

        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDTO> UpdateUser([FromBody] UserModel userModel)
        {
            if (userModel.Id == 0)
            {
                ModelState.AddModelError("Id", "Id field should be specified for User in order to update it");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState.FormatErrors());

            var result =  this.userRepository.UpdateItem(userModel.MapUserModelToEntity());

            return Ok(result.MapUserToDto());
        }

        [HttpDelete("{userId:int:min(1)}", Name = nameof(DeleteUser))]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteUser([FromRoute] int userId)
        {
            var result =  this.userRepository.DeleteItem(userId);

            return result ? Ok() : BadRequest();
        }
    }
}
