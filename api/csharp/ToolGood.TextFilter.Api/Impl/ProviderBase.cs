using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ToolGood.TextFilter.Api.Impl
{
    public abstract class ProviderBase
    {
        private readonly TextFilterConfig _textFilterConfig;
        public ProviderBase(TextFilterConfig textFilterConfig)
        {
            _textFilterConfig = textFilterConfig;
        }

        protected internal T PostContent<T>(string url, string json) where T : class
        {
            var u = _textFilterConfig.TextFilterHost.TrimEnd('/') + url;
            var requestMessage = new HttpRequestMessage() {
                RequestUri = new Uri(u),
                Method = new HttpMethod("POST"),
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };


            var httpClient = _textFilterConfig.CreateHttpClient();
#if NETSTANDARD
            var task = httpClient.SendAsync(requestMessage);
            task.Wait();
            var response = task.Result;
            var msTask = response.Content.ReadAsStreamAsync();
            msTask.Wait();
            var ms = msTask.Result;
#else
            var response = httpClient.Send(requestMessage);
            var ms = response.Content.ReadAsStream();
#endif
            StreamReader sr = new StreamReader(ms);
            var content = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            response.Dispose();
            httpClient.Dispose();

            return JsonConvert.DeserializeObject<T>(content);
        }

        protected internal async Task<T> PostContentAsync<T>(string url, string json) where T : class
        {
            var u = _textFilterConfig.TextFilterHost.TrimEnd('/') + url;
            var requestMessage = new HttpRequestMessage() {
                RequestUri = new Uri(u),
                Method = new HttpMethod("POST"),
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };

            var httpClient = _textFilterConfig.CreateHttpClient();
            var response = await httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();
            response.Dispose();
            httpClient.Dispose();

            return JsonConvert.DeserializeObject<T>(content);
        }


        protected internal T GetContent<T>(string url) where T : class
        {
            var u = _textFilterConfig.TextFilterHost.TrimEnd('/') + url;
            var requestMessage = new HttpRequestMessage() {
                RequestUri = new Uri(u),
                Method = new HttpMethod("GET"),
            };

            var httpClient = _textFilterConfig.CreateHttpClient();
#if NETSTANDARD
            var task = httpClient.SendAsync(requestMessage);
            task.Wait();
            var response = task.Result;
            var msTask = response.Content.ReadAsStreamAsync();
            msTask.Wait();
            var ms = msTask.Result;
#else
            var response = httpClient.Send(requestMessage);
            var ms = response.Content.ReadAsStream();
#endif

            StreamReader sr = new StreamReader(ms);
            var content = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            response.Dispose();
            httpClient.Dispose();

            return JsonConvert.DeserializeObject<T>(content);

        }

        protected internal async Task<T> GetContentAsync<T>(string url) where T : class
        {
            var u = _textFilterConfig.TextFilterHost.TrimEnd('/') + url;
            var requestMessage = new HttpRequestMessage() {
                RequestUri = new Uri(u),
                Method = new HttpMethod("GET"),
            };

            var httpClient = _textFilterConfig.CreateHttpClient();
            var response = await httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();
            response.Dispose();
            httpClient.Dispose();
            return JsonConvert.DeserializeObject<T>(content);
        }


        protected internal T PostFile<T>(string url, string filePath) where T : class
        {
            var u = _textFilterConfig.TextFilterHost.TrimEnd('/') + url;

            var requestMessage = new HttpRequestMessage() {
                RequestUri = new Uri(u),
                Method = new HttpMethod("POST"),
            };
            var formDataContent = new MultipartFormDataContent();
            var bytes = File.ReadAllBytes(filePath);
            formDataContent.Add(new ByteArrayContent(bytes));
            requestMessage.Content = formDataContent;


            var httpClient = _textFilterConfig.CreateHttpClient();
#if NETSTANDARD
            var task = httpClient.SendAsync(requestMessage);
            task.Wait();
            var response = task.Result;
            var msTask = response.Content.ReadAsStreamAsync();
            msTask.Wait();
            var ms = msTask.Result;
#else
            var response = httpClient.Send(requestMessage);
            var ms = response.Content.ReadAsStream();
#endif
            StreamReader sr = new StreamReader(ms);
            var content = sr.ReadToEnd();
            sr.Close();
            ms.Close();
            response.Dispose();
            httpClient.Dispose();
            return JsonConvert.DeserializeObject<T>(content);
        }

        protected internal async Task<T> PostFileAsync<T>(string url, string filePath) where T : class
        {
            var u = _textFilterConfig.TextFilterHost.TrimEnd('/') + url;
            var requestMessage = new HttpRequestMessage() {
                RequestUri = new Uri(u),
                Method = new HttpMethod("POST"),
            };
            var formDataContent = new MultipartFormDataContent();
#if NETSTANDARD2_0
            var bytes = File.ReadAllBytes(filePath);
#else
            var bytes =await File.ReadAllBytesAsync(filePath);
#endif
            formDataContent.Add(new ByteArrayContent(bytes));
            requestMessage.Content = formDataContent;


            var httpClient = _textFilterConfig.CreateHttpClient();
            var response = await httpClient.SendAsync(requestMessage);
            var content = await response.Content.ReadAsStringAsync();
            response.Dispose();
            httpClient.Dispose();
            return JsonConvert.DeserializeObject<T>(content);

        }
    }
}
