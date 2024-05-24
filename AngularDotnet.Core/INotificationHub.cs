using AngularDotnet.Core.DTOs;

namespace AngularDotnet.Core
{
    public interface INotificationHub
    {
        public Task SendMessage(Notification notification);

    }
}
