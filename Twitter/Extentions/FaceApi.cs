using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using Twitter.Models;

namespace Twitter.Extentions
{
    public static class FaceApi
    {

        public static async Task<FaceDetection[]> MakeDetectRequest(string imageFilePath)
        {
            var client = new HttpClient();

            // Request headers - replace this example key with your valid key.
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "b604f2c63f1344e1a6de365fa31e287f");

            // Request parameters and URI string.
            string queryString = "returnFaceId=false&returnFaceLandmarks=false&returnFaceAttributes=gender";
            string uri = "https://westeurope.api.cognitive.microsoft.com/face/v1.0/detect?" + queryString;

            HttpResponseMessage response;
            string responseContent;
            var param = JsonConvert.SerializeObject(new { url = imageFilePath});

            using (var content = new StringContent(param))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                response = await client.PostAsync(uri, content);
                responseContent = response.Content.ReadAsStringAsync().Result;
                if (!responseContent.Contains("[]"))
                {
                    
                    var faces = JsonConvert.DeserializeObject<FaceDetection[]>(responseContent);
                    return faces;
                }
            }
            return null;

        }
    }
}