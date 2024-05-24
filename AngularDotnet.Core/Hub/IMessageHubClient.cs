using AngularDotnet.Core.DTOs;

namespace AngularDotnet.Core.Hub
{
    public interface IMessageHubClient
    {
        Task NotifyAboutNewMovie(MovieDTO movie);
    }
}
