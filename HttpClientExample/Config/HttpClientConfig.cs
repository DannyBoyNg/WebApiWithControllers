namespace HttpClientExample.Config
{
    public static class HttpClientConfig
    {
        public static Action<HttpClient> Configuration
        {
            get
            {
                return httpClient =>
                {
                    httpClient.BaseAddress = new Uri("https://location-of-webapi-to-call.com/");
                    httpClient.DefaultRequestHeaders.Add("ApiKey", "c74199e7-7b4a-485a-975c-ade01bf752b8");
                };
            }
        }
    }
}
