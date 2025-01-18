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
    public class PropertyTraceController : ControllerBase
    {
        private readonly PropertyTraceService _propertyTraceService;

        // Inyección de dependencias (PropertyTraceService)
        public PropertyTraceController(PropertyTraceService propertyTraceService)
        {
            _propertyTraceService = propertyTraceService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtener todos los registros de PropertyTrace", Description = "Obtiene todos los registros de PropertyTrace registrados en el sistema.")]
        public ActionResult<List<PropertyTrace>> GetPropertyTraces()
        {
            try
            {
                var propertyTraces = _propertyTraceService.GetPropertyTraces();
                return Ok(propertyTraces);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener registros de PropertyTrace: {ex.Message}");
            }
        }

        [HttpGet("{id:length(24)}")]
        [SwaggerOperation(Summary = "Obtener registro de PropertyTrace por ID", Description = "Obtiene un registro de PropertyTrace específico por su ID.")]
        public ActionResult<PropertyTrace> GetPropertyTrace(string id)
        {
            try
            {
                var propertyTrace = _propertyTraceService.GetPropertyTrace(id);
                if (propertyTrace == null)
                {
                    return NotFound("Registro de PropertyTrace no encontrado.");
                }
                return Ok(propertyTrace);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener registro de PropertyTrace: {ex.Message}");
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crear registro de PropertyTrace", Description = "Crea un nuevo registro de PropertyTrace en el sistema.")]
        public ActionResult<PropertyTrace> PostPropertyTrace([FromBody] PropertyTrace propertyTrace)
        {
            try
            {
                if (propertyTrace == null)
                {
                    return BadRequest("Los datos no son correctos. El registro de PropertyTrace no puede ser nulo.");
                }

                var createdPropertyTrace = _propertyTraceService.PostPropertyTrace(propertyTrace);
                return CreatedAtAction(nameof(GetPropertyTrace), new { id = createdPropertyTrace.IdPropertyTrace }, createdPropertyTrace);
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
        [SwaggerOperation(Summary = "Actualizar registro de PropertyTrace", Description = "Actualiza un registro de PropertyTrace existente en el sistema.")]
        public ActionResult<PropertyTrace> PutPropertyTrace(string id, [FromBody] PropertyTrace propertyTrace)
        {
            try
            {
                var updatedPropertyTrace = _propertyTraceService.PutPropertyTrace(id, propertyTrace);
                if (updatedPropertyTrace == null)
                {
                    return NotFound("Registro de PropertyTrace no encontrado.");
                }
                return Ok(updatedPropertyTrace);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar registro de PropertyTrace: {ex.Message}");
            }
        }

        [HttpDelete("{id:length(24)}")]
        [SwaggerOperation(Summary = "Eliminar registro de PropertyTrace", Description = "Elimina un registro de PropertyTrace del sistema.")]
        public ActionResult<PropertyTrace> DeletePropertyTrace(string id)
        {
            try
            {
                var deletedPropertyTrace = _propertyTraceService.DeletePropertyTrace(id);
                if (deletedPropertyTrace == null)
                {
                    return NotFound("Registro de PropertyTrace no encontrado.");
                }
                return Ok(deletedPropertyTrace);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar registro de PropertyTrace: {ex.Message}");
            }
        }
    }
}
