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
    public class PropertyImageController : ControllerBase
    {
        private readonly PropertyImageService _propertyImageService;

        // Inyección de dependencias (PropertyImageService)
        public PropertyImageController(PropertyImageService propertyImageService)
        {
            _propertyImageService = propertyImageService;
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtener todas las imágenes de propiedad", Description = "Obtiene todas las imágenes de propiedad registradas en el sistema.")]
        public ActionResult<List<PropertyImage>> GetPropertyImages()
        {
            try
            {
                var propertyImages = _propertyImageService.GetPropertyImages();
                return Ok(propertyImages);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener imágenes de propiedad: {ex.Message}");
            }
        }

        [HttpGet("{id:length(24)}")]
        [SwaggerOperation(Summary = "Obtener imagen de propiedad por ID", Description = "Obtiene una imagen de propiedad específica por su ID.")]
        public ActionResult<PropertyImage> GetPropertyImage(string id)
        {
            try
            {
                var propertyImage = _propertyImageService.GetPropertyImage(id);
                if (propertyImage == null)
                {
                    return NotFound("Imagen de propiedad no encontrada.");
                }
                return Ok(propertyImage);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al obtener imagen de propiedad: {ex.Message}");
            }
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Crear imagen de propiedad", Description = "Crea una nueva imagen de propiedad en el sistema.")]
        public ActionResult<PropertyImage> PostPropertyImage([FromBody] PropertyImage propertyImage)
        {
            try
            {
                if (propertyImage == null)
                {
                    return BadRequest("Los datos no son correctos. La imagen de propiedad no puede ser nula.");
                }

                var createdPropertyImage = _propertyImageService.PostPropertyImage(propertyImage);
                return CreatedAtAction(nameof(GetPropertyImage), new { id = createdPropertyImage.IdPropertyImage }, createdPropertyImage);
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
        [SwaggerOperation(Summary = "Actualizar imagen de propiedad", Description = "Actualiza una imagen de propiedad existente en el sistema.")]
        public ActionResult<PropertyImage> PutPropertyImage(string id, [FromBody] PropertyImage propertyImage)
        {
            try
            {
                var updatedPropertyImage = _propertyImageService.PutPropertyImage(id, propertyImage);
                if (updatedPropertyImage == null)
                {
                    return NotFound("Imagen de propiedad no encontrada.");
                }
                return Ok(updatedPropertyImage);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar imagen de propiedad: {ex.Message}");
            }
        }

        [HttpDelete("{id:length(24)}")]
        [SwaggerOperation(Summary = "Eliminar imagen de propiedad", Description = "Elimina una imagen de propiedad del sistema.")]
        public ActionResult<PropertyImage> DeletePropertyImage(string id)
        {
            try
            {
                var deletedPropertyImage = _propertyImageService.DeletePropertyImage(id);
                if (deletedPropertyImage == null)
                {
                    return NotFound("Imagen de propiedad no encontrada.");
                }
                return Ok(deletedPropertyImage);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar imagen de propiedad: {ex.Message}");
            }
        }
    }
}
