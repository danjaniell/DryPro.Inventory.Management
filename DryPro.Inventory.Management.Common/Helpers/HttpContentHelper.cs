using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DryPro.Inventory.Management.Common.Helpers
{
    public class HttpContentHelper
    {
        public static HttpContent CreateRequest(object request)
        {
            var jsonRequest = JsonConvert.SerializeObject(request);
            var buffer = System.Text.Encoding.UTF8.GetBytes(jsonRequest);
            var byteContent = new ByteArrayContent(buffer);
            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return byteContent;
        }
    }
}
