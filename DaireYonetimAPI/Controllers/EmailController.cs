using DaireYonetimAPI.Business.Abstrack;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaireYonetimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly EmailSenderService _emailSenderService;
        private Timer _timer;

        public EmailController(EmailSenderService emailSenderService)
        {
            this._emailSenderService = emailSenderService;
            StartScheduler();
        }

        private void StartScheduler()
        {
            var now = DateTime.Now;
            var targetTime = new DateTime(now.Year, now.Month, now.Day, 21, 0, 0);
            if (now > targetTime)
            {
                targetTime = targetTime.AddDays(1);
            }

            var dueTime = targetTime - now;

            _timer = new Timer(SendEmailTask, null, dueTime, TimeSpan.FromDays(1));
        }

        private void SendEmailTask(object state)
        {
            _emailSenderService.SendEmails(state);
        }
        [HttpPost("start")]
        public ActionResult SendEmailController(object state) 
        {

            _emailSenderService.SendEmails(state);
            return Ok("başaralı");

        }
    }
}
