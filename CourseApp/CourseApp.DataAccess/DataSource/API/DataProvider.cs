using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CourseApp.DataAccess.DataSource.API
{
    public class DataProvider : IDisposable
    {
        private readonly HttpClient httpClient;

        private bool disposedValue = false;

        public DataProvider(Uri hostUri)
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

            this.httpClient = new HttpClient(httpClientHandler, disposeHandler: true);
            this.httpClient.BaseAddress = hostUri;
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetAsync<T>(string relativeUri)
        {
            HttpResponseMessage response = await this.PerformRequest(async () => await this.httpClient.GetAsync(relativeUri));

            return await this.GetDataFromResponse<T>(response);
        }

        public async Task<TIdentifier> PostAsync<T, TIdentifier>(string relativeUri, T model)
        {
            var content = this.ToStringContent<T>(model);
            HttpResponseMessage response = await this.PerformRequest(async () => await this.httpClient.PostAsync(relativeUri, content));

            return await this.GetDataFromResponse<TIdentifier>(response);
        }

        public async Task PutAsync<T>(string relativeUri, T model)
        {
            var content = this.ToStringContent<T>(model);
            HttpResponseMessage response = await this.PerformRequest(async () => await this.httpClient.PutAsync(relativeUri, content));
        }

        public async Task DeleteAsync<TIdentifier>(string relativeUri, TIdentifier id)
        {
            relativeUri = string.Format(relativeUri, id);
            HttpResponseMessage response = await this.PerformRequest(async () => await this.httpClient.DeleteAsync(relativeUri));
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.httpClient.Dispose();
                }

                disposedValue = true;
            }
        }

        private async Task<HttpResponseMessage> PerformRequest(Func<Task<HttpResponseMessage>> requestFunc)
        {
            try
            {
                HttpResponseMessage response = await requestFunc();

                response.EnsureSuccessStatusCode();

                return response;
            }
            catch (HttpRequestException ex)
            {
                throw ex;
            }
        }

        private StringContent ToStringContent<T>(T source)
        {
            string rawModel = JsonConvert.SerializeObject(source);
            var content = new StringContent(rawModel);

            return content;
        }

        private async Task<T> GetDataFromResponse<T>(HttpResponseMessage response)
        {
            string rawResponse = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(rawResponse);
        }
    }
}
