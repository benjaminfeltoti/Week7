using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Week7.Models;

namespace Week7.Data
{
    public class RestService
    {
        HttpClient client;
        string grant_type = "password";

        public RestService()
        {
            client = new HttpClient();
            client.MaxResponseContentBufferSize = 250000;
            client.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
        }

        public async Task<Token> login(User user)
        {
            List<KeyValuePair<string, string>> postData = new List<KeyValuePair<string, string>>();

            postData.Add(new KeyValuePair<string, string>("grant_type", grant_type));
            postData.Add(new KeyValuePair<string, string>("username", user.Username));
            postData.Add(new KeyValuePair<string, string>("password", user.Password));

            FormUrlEncodedContent content = new FormUrlEncodedContent(postData);
            Token response = await postResponseLogin<Token>(Constants.LoginUrl, content);

            DateTime date = new DateTime();
            date = DateTime.Today;
            response.expireDate = date.AddSeconds(response.expireIn);
            return response;
        }

        public async Task<T> postResponseLogin<T>(string weburl, FormUrlEncodedContent content) where T : class
        {
            HttpResponseMessage response = await client.PostAsync(weburl, content);
            string result = response.Content.ReadAsStringAsync().Result;
            T deserialized = JsonConvert.DeserializeObject<T>(result);
            return deserialized;
        }

        public async Task<T> postResponse<T>(string weburl, string jsonstring) where T : class 
        {
            Token token = App.TokenDatabase.getToken();
            string contentType = "application/json";
            client.DefaultRequestHeaders.Authorization =
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.accessToken);

            try
            {
                HttpResponseMessage result =
                    await client.PostAsync(weburl, new StringContent(jsonstring, Encoding.UTF8, contentType));

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsontResult = result.Content.ReadAsStringAsync().Result;
                    try
                    {
                        return JsonConvert.DeserializeObject<T>(jsontResult);
                    }
                    catch
                    {
                        return null;                        
                    }
                }
            }
            catch
            {
                return null;
            }

            return null;
        }

        public async Task<T> getResponse<T>(string weburl) where T : class
        {
            Token token = App.TokenDatabase.getToken();
            client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token.accessToken);

            try
            {
                HttpResponseMessage response = await client.GetAsync(weburl);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string jsonResult = response.Content.ReadAsStringAsync().Result;
                    try
                    {
                        return JsonConvert.DeserializeObject<T>(jsonResult);
                    }
                    catch 
                    {
                        return null;
                    }
                }
            }
            catch 
            {
                return null;
            }

            return null;
        }
    }
}
