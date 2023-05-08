using Microsoft.AspNetCore.SignalR;

namespace SignalRExample.Hubs
{
    public class ProgressHub : Hub
    {
        public async Task ProcessJob()
        {
            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(500);
                await Clients.Caller.SendAsync("Progress", $"{i}").ConfigureAwait(false);
            }

            await Clients.Caller.SendAsync("Progress", "100").ConfigureAwait(false);
        }
    }
}
