using Microsoft.AspNetCore.SignalR;

namespace AngularDotnet.Core
{
    public class ImportNotificationHub : Hub<INotificationHub>
    {
        public Task SubscribeToUser(string Name)
        {
            return this.Groups.AddToGroupAsync(Context.ConnectionId, Name);
        }
    }
}
