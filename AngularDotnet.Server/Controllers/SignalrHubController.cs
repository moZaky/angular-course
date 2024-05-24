using AngularDotnet.Core;
using AngularDotnet.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace AngularDotnet.Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SignalrHubController : ControllerBase
    {
        private readonly IHubContext<ImportNotificationHub, INotificationHub> _importAppointmentsNotification;

        public SignalrHubController(IHubContext<ImportNotificationHub, INotificationHub> hubContext)
        {
            _importAppointmentsNotification = hubContext;
        }
        [HttpGet]
        public async Task<ActionResult> ImportAppointmentsNotification(Notification notification)
        {
            try
            {
                await _importAppointmentsNotification.Clients.Group(notification.Name).SendMessage(notification);
                return Ok(true);
            }
            catch (Exception ex)
            {
                return Ok(false);
            }
        }
    }
}
