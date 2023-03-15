using GymTrainingDiary.DataAccess.Interfaces;
using GymTrainingDiary.DataHandling;
using GymTrainingDiary.DTO;
using GymTrainingDiary.Mapping.EntityToDto;
using GymTrainingDiary.Mapping.ModelToEntity;
using GymTrainingDiary.Model;
using GymTrainingDiary.Utilities.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace GymTrainingDiaryAPI.Controllers.v1
{
    [Area("api")]
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EquipmentController : ControllerBase
    {
        private readonly IExerciseRepository exerciseRepository;
        private readonly IEquipmentRepository equipmentRepository;
        private readonly LinkGenerator linkGenerator;

        public Dictionary<string, string> Methods = new Dictionary<string, string>
        {
            [nameof(DefaultApiConventions.Get)] = nameof(GetItemById),
            [nameof(DefaultApiConventions.Post)] = nameof(AddItem),
            [nameof(DefaultApiConventions.Put)] = nameof(UpdateItem),
            [nameof(DefaultApiConventions.Delete)] = nameof(DeleteItem),
        };


        public EquipmentController(
            IExerciseRepository  exerciseRepository,
            IEquipmentRepository equipmentRepository,
            LinkGenerator linkGenerator)
        {
            this.exerciseRepository = exerciseRepository;
            this.equipmentRepository = equipmentRepository;
            this.linkGenerator = linkGenerator;
        }

        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<EquipmentDTO>> GetAllItems()
        {
            return Ok(this.equipmentRepository.GetAllItems().Select(x => x.MapEquipmentToDTO()));
        }

        [HttpGet]
        [Route("{id:int:min(1)}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<EquipmentDTO> GetItemById([FromRoute] int id)
        {
            EquipmentDTO result = null;

            var item =  this.equipmentRepository.GetItemById(id);

            if (item == null) return NotFound();

            result = item.MapEquipmentToDTO();
            result.Links = this.linkGenerator.CreateLinksForResource(HttpContext, id, this.Methods);

            return Ok(result);
        }

        [HttpDelete("{id:int:min(1)}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Delete))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult DeleteItem([FromRoute] int id)
        {
            var itemToDelete = this.equipmentRepository.GetItemById(id);

            if (itemToDelete == null) return BadRequest("Item not found");

            this.equipmentRepository.DeleteItem(id);

            return Ok();
        }

        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EquipmentDTO> AddItem([FromBody] EquipmentModel model)
        {
            if (model.Id != 0)
            {
                ModelState.AddModelError("Id", "Id should not be specified while adding new Equipment");
            }

            if (this.equipmentRepository.IsEqipmentExist(model.Name))
            {
                ModelState.AddModelError("duplicate", "Equipment with this name already exist");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState.FormatErrors());

            EquipmentDTO result = new EquipmentDTO();

            var add =  this.equipmentRepository.AddItem(model.MapEquipmentModelToEntity());

            result = add.MapEquipmentToDTO();
            result.Links = this.linkGenerator.CreateLinksForResource(this.HttpContext, add.Id, this.Methods);

            return Ok(result);
        }

        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<EquipmentDTO> UpdateItem([FromBody] EquipmentModel model)
        {
            if (model.Id == 0)
            {
                ModelState.AddModelError("Id", "Id value is needed to perform an update of the Equipment");
            }

            if (!ModelState.IsValid) return BadRequest(ModelState.FormatErrors());

            var updated =  this.equipmentRepository.UpdateItem(model.MapEquipmentModelToEntity());

            var result = updated.MapEquipmentToDTO();
            result.Links = this.linkGenerator.CreateLinksForResource(this.HttpContext, result.Id, this.Methods);
            return Ok(result);
        }

        [HttpGet("{id:int:min(1)}/Exercises")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<string> GetExercisesForEquipment([FromRoute] int id)
        {
            var res =  this.exerciseRepository.GetItemsByCondtion(x => x.RequiredEquipmentId == id);

            if (!res.Any()) return Ok("Nothing found");

            return Ok(res.Select(x => x.Name));
        }
    }
}
