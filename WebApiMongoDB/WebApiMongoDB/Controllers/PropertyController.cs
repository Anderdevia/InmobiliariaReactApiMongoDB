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
    public class PropertyController : ControllerBase
    {
        private readonly PropertyService _propertyService;

        // Inyección de dependencias (PropertyService)
        public PropertyController(PropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtener todas las propiedades", Description = "Obtiene todas las propiedades registradas en el sistema.")]
        public ActionResult<List<Property>> GetProperties()
        {
            try
            {
                var properties = _propertyService.GetProperties();
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener propiedades: {ex.Message}");
            }
        }

        [HttpGet("{id:length(24)}")]
        [SwaggerOperation(Summary = "Obtener propiedad por ID", Description = "Obtiene una propiedad específica por su ID.")]
        public ActionResult<Property> GetProperty(string id)
        {
            try
            {
                var property = _propertyService.GetProperty(id);
                if (property == null)
                {
                    return NotFound("Propiedad no encontrada.");
                }
                return Ok(property);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener propiedad: {ex.Message}");
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crear propiedad", Description = "Crea una nueva propiedad en el sistema.")]
        public ActionResult<Property> PostProperty([FromBody] Property property)
        {
            try
            {
                if (property == null)
                {
                    return BadRequest("Los datos no son correctos. La propiedad no puede ser nula.");
                }

                var createdProperty = _propertyService.PostProperty(property);
                return CreatedAtAction(nameof(GetProperty), new { id = createdProperty.IdProperty }, createdProperty);
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
        [SwaggerOperation(Summary = "Actualizar propiedad", Description = "Actualiza una propiedad existente en el sistema.")]
        public ActionResult<Property> PutProperty(string id, [FromBody] Property property)
        {
            try
            {
                var updatedProperty = _propertyService.PutProperty(id, property);
                if (updatedProperty == null)
                {
                    return NotFound("Propiedad no encontrada.");
                }
                return Ok(updatedProperty);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar propiedad: {ex.Message}");
            }
        }

        [HttpDelete("{id:length(24)}")]
        [SwaggerOperation(Summary = "Eliminar propiedad", Description = "Elimina una propiedad del sistema.")]
        public ActionResult<Property> DeleteProperty(string id)
        {
            try
            {
                var deletedProperty = _propertyService.DeleteProperty(id);
                if (deletedProperty == null)
                {
                    return NotFound("Propiedad no encontrada.");
                }
                return Ok(deletedProperty);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar propiedad: {ex.Message}");
            }
        }
    }
}
