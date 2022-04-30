using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Medicure_Mvc
{
    public static class Utilities
    {
        public async static Task<T> GetResponseFromApi<T>
            (this Controller controller,
            string baseUri,
            string requestUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                
                var responce = await client.GetAsync(requestUrl);

                if (responce.IsSuccessStatusCode)
                {
                    var result = JsonSerializer.Deserialize<T>(await responce.Content.ReadAsStringAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web));
                    return result;
                }
                else
                    return default(T);
            }
        }

        public async static Task<T> SendDataToApi<T>(
           this Controller controller,
           string baseUri,
           string requestUrl,
           T model)
        {

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
                
                var response = await client.PostAsync(
                    requestUri: requestUrl,
                    content: JsonContent.Create(
                            inputValue: model,
                            inputType: typeof(T),
                            mediaType: new System.Net.Http.Headers.MediaTypeHeaderValue("application/json"),
                            options: new JsonSerializerOptions(JsonSerializerDefaults.Web)
                    ));


            }
            return default(T);
        }
        public async static Task<TResult> SendDataToApi<TInput, TResult>(
            this Controller controller,
            string baseUri,
            string requestUrl,
            TInput model)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUri);
              
                var response = await client.PostAsJsonAsync(requestUrl, model);
                //response.EnsureSuccessStatusCode();
                var i = response.IsSuccessStatusCode;
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<TResult>();
                    return result;
                }

                return default(TResult);
            }

        }
    }
}
