using Microsoft.AspNetCore.SignalR;

namespace SignalRExample.Config
{
    public static class SignalRConfig
    {
        public static Action<HubOptions> Options { 
            get 
            {
                return options =>
                {
                    options.MaximumReceiveMessageSize = 131072;
                };
            } 
        }
    }
}
