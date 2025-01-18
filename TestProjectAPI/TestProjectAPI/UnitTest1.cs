using System.Text;
using System.Text.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProjectAPI
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public async Task Test_Insert_Owner()
        {
            // Arrange
            HttpClient httpClient = new HttpClient();
            string endpointUrl = "http://backapimongodb.somee.com/api/Owner";  

            var owner = new
            {
                name = "Anderson Devia",
                address = "Calle 69",
                photo = "234rddf",
                birthday = "1992-01-15T05:55:22.463Z"
            };

            string jsonData = System.Text.Json.JsonSerializer.Serialize(owner);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await httpClient.PostAsync(endpointUrl, content);

            // Leer la respuesta
            string responseContent = await response.Content.ReadAsStringAsync();

            // Assert: Verificar si la respuesta fue exitosa
            Assert.IsTrue(response.IsSuccessStatusCode, $"Error en la solicitud: {responseContent}");
            Assert.IsTrue(responseContent.Contains("idOwner"), "La respuesta no contiene idOwner.");
        }


        [TestMethod]
        public async Task Test_Get_Data_From_Endpoint()
        {
            // Arrange
            HttpClient httpClient = new HttpClient();
            string endpointUrl = "http://backapimongodb.somee.com/api/Owner";

            // Act
            HttpResponseMessage response = await httpClient.GetAsync(endpointUrl);
            string responseContent = await response.Content.ReadAsStringAsync();

            // Parse the JSON response
            JsonDocument jsonDoc = JsonDocument.Parse(responseContent);
            JsonElement root = jsonDoc.RootElement;

            if (root.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement element in root.EnumerateArray())
                {
                    var idOwner = element.GetProperty("idOwner").GetString();
                    var name = element.GetProperty("name").GetString();
                    var address = element.GetProperty("address").GetString();
                    var photo = element.GetProperty("photo").GetString();
                    DateTime? birthday = element.GetProperty("birthday").GetDateTime();

                    // Assert
                    Assert.IsInstanceOfType(idOwner, typeof(string));
                    Assert.IsInstanceOfType(name, typeof(string));
                    Assert.IsInstanceOfType(address, typeof(string));
                    Assert.IsInstanceOfType(photo, typeof(string));
                    Assert.IsInstanceOfType(birthday, typeof(DateTime?));

                    // Additional assertions to verify data correctness
                    Assert.IsTrue(!string.IsNullOrEmpty(name), "Name should not be null or empty.");
                    Assert.IsTrue(!string.IsNullOrEmpty(address), "Address should not be null or empty.");
                }
            }
            else
            {
                Assert.Fail("Expected an array but got a different response.");
            }
        }

        [TestMethod]
        public async Task Test_Get_Properties_From_Endpoint()
        {
            // Arrange
            HttpClient httpClient = new HttpClient();
            string endpointUrl = "http://backapimongodb.somee.com/api/Property";  

            // Act
            HttpResponseMessage response = await httpClient.GetAsync(endpointUrl);
            string responseContent = await response.Content.ReadAsStringAsync();

            JsonDocument jsonDoc = JsonDocument.Parse(responseContent);
            JsonElement root = jsonDoc.RootElement;

            // Handle the array response
            if (root.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement element in root.EnumerateArray())
                {
                    var idProperty = element.GetProperty("idProperty").GetString();
                    var name = element.GetProperty("name").GetString();
                    var address = element.GetProperty("address").GetString();
                    var price = element.GetProperty("price").GetDecimal();
                    var codeInternal = element.GetProperty("codeInternal").GetString();
                    var year = element.GetProperty("year").GetInt32();
                    var idOwner = element.GetProperty("idOwner").GetString();

                    // Assert
                    Assert.IsInstanceOfType(idProperty, typeof(string));
                    Assert.IsInstanceOfType(name, typeof(string));
                    Assert.IsInstanceOfType(address, typeof(string));
                    Assert.IsInstanceOfType(price, typeof(decimal));
                    Assert.IsInstanceOfType(codeInternal, typeof(string));
                    Assert.IsInstanceOfType(year, typeof(int));
                    Assert.IsInstanceOfType(idOwner, typeof(string));

                    // Additional assertions to verify data correctness
                    Assert.IsTrue(!string.IsNullOrEmpty(name), "Name should not be null or empty.");
                    Assert.IsTrue(!string.IsNullOrEmpty(address), "Address should not be null or empty.");
                    Assert.IsTrue(price > 0, "Price should be greater than 0.");
                    Assert.IsTrue(year > 0, "Year should be a positive value.");
                }
            }
            else
            {
                Assert.Fail("Expected an array but got a different response.");
            }
        }

        [TestMethod]
        public async Task Test_Get_PropertyImages_From_Endpoint()
        {
            // Arrange
            HttpClient httpClient = new HttpClient();
            string endpointUrl = "http://backapimongodb.somee.com/api/PropertyImage";  

            // Act
            HttpResponseMessage response = await httpClient.GetAsync(endpointUrl);
            string responseContent = await response.Content.ReadAsStringAsync();

            JsonDocument jsonDoc = JsonDocument.Parse(responseContent);
            JsonElement root = jsonDoc.RootElement;

            // Handle the array response
            if (root.ValueKind == JsonValueKind.Array)
            {
                foreach (JsonElement element in root.EnumerateArray())
                {
                    element.TryGetProperty("idPropertyImage", out JsonElement idPropertyImageElement);
                    element.TryGetProperty("idProperty", out JsonElement idPropertyElement);
                    element.TryGetProperty("file", out JsonElement fileElement);
                    element.TryGetProperty("enabled", out JsonElement enabledElement);

                    var idPropertyImage = idPropertyImageElement.GetString();
                    var idProperty = idPropertyElement.GetString();
                    var file = fileElement.GetString();
                    var enabled = enabledElement.GetBoolean();

                    // Assert
                    Assert.IsInstanceOfType(idPropertyImage, typeof(string));
                    Assert.IsInstanceOfType(idProperty, typeof(string));
                    Assert.IsInstanceOfType(file, typeof(string));
                    Assert.IsInstanceOfType(enabled, typeof(bool));

                    // Additional assertions to verify data correctness
                    Assert.IsTrue(!string.IsNullOrEmpty(idPropertyImage), "IdPropertyImage should not be null or empty.");
                    Assert.IsTrue(!string.IsNullOrEmpty(idProperty), "IdProperty should not be null or empty.");
                    Assert.IsTrue(!string.IsNullOrEmpty(file), "File should not be null or empty.");
                    Assert.IsTrue(enabled == true || enabled == false, "Enabled should be either true or false.");
                }
            }
            else
            {
                Assert.Fail("Expected an array but got a different response.");
            }
        }

        [TestMethod]
        public async Task Test_Get_PropertyTrace_From_Endpoint()
        {
            // Arrange
            HttpClient httpClient = new HttpClient();
            string endpointUrl = "http://backapimongodb.somee.com/api/PropertyTrace"; 

            // Act
            HttpResponseMessage response = await httpClient.GetAsync(endpointUrl);
            string responseContent = await response.Content.ReadAsStringAsync();

            JsonDocument jsonDoc = JsonDocument.Parse(responseContent);
            JsonElement root = jsonDoc.RootElement;

            if (root.ValueKind == JsonValueKind.Array)
            {
                // Loop through each item in the array
                foreach (JsonElement element in root.EnumerateArray())
                {
                    var idPropertyTrace = element.GetProperty("idPropertyTrace").GetString();
                    var dateSale = element.GetProperty("dateSale").GetDateTime();
                    var name = element.GetProperty("name").GetString();
                    var value = element.GetProperty("value").GetDecimal();
                    var tax = element.GetProperty("tax").GetDecimal();
                    var idProperty = element.GetProperty("idProperty").GetString();

                    // Assert
                    Assert.IsInstanceOfType(idPropertyTrace, typeof(string));
                    Assert.IsInstanceOfType(name, typeof(string));
                    Assert.IsInstanceOfType(value, typeof(decimal));
                    Assert.IsInstanceOfType(tax, typeof(decimal));
                    Assert.IsInstanceOfType(idProperty, typeof(string));
                    Assert.IsInstanceOfType(dateSale, typeof(DateTime));

                    // Additional assertions to verify data correctness
                    Assert.IsTrue(!string.IsNullOrEmpty(idPropertyTrace), "idPropertyTrace should not be null or empty.");
                    Assert.IsTrue(!string.IsNullOrEmpty(name), "Name should not be null or empty.");
                    Assert.IsTrue(value >= 0, "Value should be a positive number or zero.");
                    Assert.IsTrue(tax >= 0, "Tax should be a positive number or zero.");
                    Assert.IsTrue(!string.IsNullOrEmpty(idProperty), "idProperty should not be null or empty.");
                }
            }
            else
            {
                Assert.Fail("Expected an array but got a different response.");
            }
        }

        
            [TestMethod]
            public async Task Test_Insert_PropertyTrace()
            {
                // Datos de la solicitud (ejemplo)
                var propertyTrace = new
                {
                 
                    dateSale = "2025-01-18T14:31:17.193Z",
                    name = "prueba propertyy trance",
                    value = 0,
                    tax = 0,
                    idProperty = "678b3997306938b75bf5aca0"
                };

                string jsonData = System.Text.Json.JsonSerializer.Serialize(propertyTrace);

                HttpClient httpClient = new HttpClient();
                string endpointUrl = "http://backapimongodb.somee.com/api/PropertyTrace"; 

                var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await httpClient.PostAsync(endpointUrl, content);

                string responseContent = await response.Content.ReadAsStringAsync();

                // Assert: Verificar si la respuesta fue exitosa
                Assert.IsTrue(response.IsSuccessStatusCode, $"Error en la solicitud: {responseContent}");
                Assert.IsTrue(responseContent.Contains("idPropertyTrace"), "La respuesta no contiene idPropertyTrace.");
            }
        
    }
}
