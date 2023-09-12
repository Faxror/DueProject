using DaireYonetimAPI.Business.Abstrack;
using DaireYonetimAPI.Business.Concrete;
using DaireYonetimAPI.DataAccess;
using DaireYönetimAPI.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DaireYonetimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BakiyeController : ControllerBase
    {
        private readonly IBakiyeService bakiyeService;

        private readonly DaireDbContext _dbContext;

        public BakiyeController(IBakiyeService bakiyeService, DaireDbContext dbContext)
        {
            this.bakiyeService = bakiyeService;
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("{action}")]
        public IActionResult List()
        {
            return Ok(bakiyeService.GetAllBakiye());
        }

        [HttpGet]
        [Route("debt/{apartmentno}/{paymnet}")]
        public IActionResult Debt(int apartmentno, decimal paymnet)
        {
            var updatedBakiye = bakiyeService.CalculateCurrentDebt(apartmentno, paymnet);                
            return Ok(updatedBakiye);
        }

        [HttpGet("debt")]
        public IActionResult Debt()
        {
            var debt = bakiyeService.GetBakiyeler(true);
            return Ok(debt);
        }

        [HttpGet("no-debt")]
        public IActionResult nodebt()
        {
            var nodebt = bakiyeService.GetBakiyeler(false);
            return Ok(nodebt);
        }

        [HttpGet("paymentstatus")]
        public IActionResult PaymentStatus(decimal borcBakiye)
        {
            var bakiyestatus = bakiyeService.PaymentStatus(borcBakiye);
            return Ok(bakiyestatus);
        }

        [HttpGet("payment")]
        public IActionResult Paymentsİnfo(int daireId)
        {
            var paymentsinfo = bakiyeService.Payment(daireId);
            return Ok(paymentsinfo);
        }
    }
}
