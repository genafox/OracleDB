using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CourseApp.DataAccess.DataSource.API
{
    public class ServiceProxy : IDisposable
    {
        private readonly HttpClient httpClient;
        private readonly IList<MediaTypeFormatter> formatters;

        private bool disposedValue = false;

        public ServiceProxy(Uri hostUri)
        {
            this.formatters = new List<MediaTypeFormatter>
            {
                new JsonMediaTypeFormatter(),
                new XmlMediaTypeFormatter()
            };

            this.httpClient = new HttpClient();
            this.httpClient.BaseAddress = hostUri;
            this.httpClient.DefaultRequestHeaders.Accept.Clear();
            this.httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<T> GetAsync<T>(string relativeUri)
        {
            HttpResponseMessage response = await this.httpClient.GetAsync(relativeUri);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<T>(this.formatters);
        }

        public async Task<TIdentifier> PostAsync<T, TIdentifier>(string relativeUri, T model)
        {
            HttpResponseMessage response = await this.httpClient.PostAsJsonAsync(relativeUri, model);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsAsync<TIdentifier>();
        }

        public async Task PutAsync<T>(string relativeUri, T model)
        {
            HttpResponseMessage response = await this.httpClient.PutAsJsonAsync(relativeUri, model);

            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteAsync<TIdentifier>(string relativeUri, TIdentifier id)
        {
            relativeUri = string.Format(relativeUri, id);
            HttpResponseMessage response = await this.httpClient.DeleteAsync(relativeUri);

            response.EnsureSuccessStatusCode();
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

        public void Dispose()
        {
            this.Dispose(true);
        }
    }
}
