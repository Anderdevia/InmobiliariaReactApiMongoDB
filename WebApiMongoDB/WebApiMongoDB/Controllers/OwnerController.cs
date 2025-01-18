using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Swashbuckle.AspNetCore.Annotations;
using WebApiMongoDB.Data;
using WebApiMongoDB.Entities;

namespace WebApiMongoDB.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OwnerController : ControllerBase
    {
        private readonly OwnerService _ownerService;

        // Inyección de dependencias (OwnerService)
        public OwnerController(OwnerService ownerService)
        {
            _ownerService = ownerService;
        }

        [HttpGet]
        public ActionResult<List<Owner>> GetOwners()
        {
            try
            {
                var owners = _ownerService.GetOwners();
                return Ok(owners);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener propietarios: {ex.Message}");
            }
        }

        [HttpGet("{id:length(24)}")]
        public ActionResult<Owner> GetOwner(string id)
        {
            try
            {
                var owner = _ownerService.GetOwner(id);
                if (owner == null)
                {
                    return NotFound("Propietario no encontrado.");
                }
                return Ok(owner);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener propietario: {ex.Message}");
            }
        }

        [HttpPost]
        [SwaggerResponse(201, "Propietario creado exitosamente", typeof(Owner))]
        [SwaggerResponse(400, "Datos incorrectos. Verifique los campos.")]
        public ActionResult<Owner> PostOwner([FromBody] Owner owner)
        {
            try
            {
                if (owner == null)
                {
                    return BadRequest("Los datos no son correctos. El propietario no puede ser nulo.");
                }

                if (string.IsNullOrEmpty(owner.Name) || string.IsNullOrEmpty(owner.Address))
                {
                    return BadRequest("Los datos no son correctos. El nombre y la dirección no pueden estar vacíos.");
                }

                var createdOwner = _ownerService.PostOwner(owner);
                return CreatedAtAction(nameof(PostOwner), new { id = owner.IdOwner }, owner);
            }
            catch (MongoDB.Bson.BsonSerializationException ex)
            {
                return BadRequest($"Error de serialización en MongoDB: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error inesperado: {ex.Message}");
            }
        }

        [HttpPut("{id:length(24)}")]
        [SwaggerOperation(Summary = "Actualizar propietario", Description = "Actualiza un propietario existente en el sistema.")]
        public ActionResult<Owner> PutOwner(string id, [FromBody] Owner owner)
        {
            try
            {
                var updatedOwner = _ownerService.PutOwner(id, owner);
                if (updatedOwner == null)
                {
                    return NotFound("El propietario no fue encontrado.");
                }
                return Ok(updatedOwner);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar propietario: {ex.Message}");
            }
        }

        [HttpDelete("{id:length(24)}")]
        [SwaggerOperation(Summary = "Eliminar propietario", Description = "Elimina un propietario del sistema.")]
        public ActionResult<Owner> DeleteOwner(string id)
        {
            try
            {
                var deletedOwner = _ownerService.DeleteOwner(id);
                if (deletedOwner == null)
                {
                    return NotFound("El propietario no fue encontrado.");
                }
                return Ok(deletedOwner);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar propietario: {ex.Message}");
            }
        }
    }
}
