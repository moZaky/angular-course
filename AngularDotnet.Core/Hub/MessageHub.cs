using AngularDotnet.Core.DTOs;
using Microsoft.AspNetCore.SignalR;

namespace AngularDotnet.Core.Hub
{
    public class MessageHub : Hub<IMessageHubClient>
    {
        public async Task NotifyAboutNewMovie(MovieDTO movie)
        {
            await Clients.All.NotifyAboutNewMovie(movie);
        }
    }
}
