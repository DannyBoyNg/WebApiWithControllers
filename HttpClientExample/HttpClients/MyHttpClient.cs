using HttpClientExample.HttpClients.Dtos;

namespace HttpClientExample.HttpClients
{
    public class MyHttpClient
    {
        private readonly HttpClient httpClient;

        public MyHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        //SendAsync example
        public async Task<string?> Send()
        {
            try
            {
                var url = $"api/Get";
                using var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                var response = await httpClient.SendAsync(requestMessage);
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return null;
            }
        }

        //GetAsync example
        public async Task<string?> Get()
        {
            try
            {
                var url = $"api/Get";
                var response = await httpClient.GetAsync(url);
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return null;
            }
        }

        //GetStringAsync example
        public async Task<string?> GetString()
        {
            try
            {
                var url = $"api/GetString";
                return await httpClient.GetStringAsync(url);
            }
            catch
            {
                return null;
            }
        }

        //GetByteArrayAsync example
        public async Task<byte[]?> GetByteArray()
        {
            try
            {
                var url = $"api/GetBytes";
                return await httpClient.GetByteArrayAsync(url);
            }
            catch
            {
                return null;
            }
        }

        //GetStreamAsync example
        public async Task<Stream?> GetStream()
        {
            try
            {
                var url = $"api/GetStream";
                return await httpClient.GetStreamAsync(url);
            }
            catch
            {
                return null;
            }
        }

        //GetFromJsonAsync example
        public async Task<PersonDto?> GetFromJson()
        {
            try
            {
                var url = $"api/GetJson";
                return await httpClient.GetFromJsonAsync<PersonDto>(url);
            }
            catch
            {
                return null;
            }
        }

        //PostAsync example
        public async Task<string?> PostData()
        {
            try
            {
                var url = $"api/PostData";
                //Send data as FormUrlEncoded
                HttpContent content = new FormUrlEncodedContent(new[]
                {
                    new KeyValuePair<string, string>("key1", "value1"),
                    new KeyValuePair<string, string>("key2", "value2")
                });
                //Or send data as String
                content = new StringContent("My Data");
                var response = await httpClient.PostAsync(url, content);
                return await response.Content.ReadAsStringAsync();
            }
            catch
            {
                return null;
            }
        }

        //PostAsJsonAsync example
        public async Task<bool> PostDataAsJson()
        {
            try
            {
                var url = $"api/PostData";
                var person = new PersonDto { Name = "John", Age = 23 };
                var response = await httpClient.PostAsJsonAsync(url, person);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
