using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BookingApp.LocalTrafficRouterService.Client
{
    public interface IRegistryServiceClient
    {
        Task<string[]> GetServiceUrlsByTag(string serviceTag);
    }

    public class RegistryServiceClient : IRegistryServiceClient
    {
        private readonly string _serviceUrl;

        public RegistryServiceClient(string serviceUrl)
        {
            _serviceUrl = serviceUrl;
        }

        public async Task<string[]> GetServiceUrlsByTag(string serviceTag)
        {
            using (var httpClient = new HttpClient() { BaseAddress = new Uri(_serviceUrl) })
            {
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var httpResponse = await httpClient.GetAsync($"/registry/{serviceTag}");
                httpResponse.EnsureSuccessStatusCode();

                var content = await httpResponse.Content.ReadAsStringAsync();
                var response = JsonConvert.DeserializeObject<string[]>(content);
                return response;
            }
        }
    }
}
