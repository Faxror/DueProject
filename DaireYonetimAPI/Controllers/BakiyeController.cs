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

            var updatedBakiye = bakiyeService.calculatecurrentdebt(apartmentno, paymnet);

            if (updatedBakiye == null)
            {
                return BadRequest();
            }
                
            return Ok(updatedBakiye);
        }

        [HttpGet("debt")]
        public IActionResult Debt()
        {
            var borclu = bakiyeService.GetBakiyeler(true);
            return Ok(borclu);
        }

        [HttpGet("no-debt")]
        public IActionResult nodebt()
        {
            var olmayn = bakiyeService.GetBakiyeler(false);
            return Ok(olmayn);
        }



        [HttpGet("paymentstatus")]
        public IActionResult PaymentStatus(decimal borcBakiye)
        {
            var bakiyedurum = bakiyeService.PaymentStatus(borcBakiye);
            return Ok(bakiyedurum);
        }

        [HttpGet("payment")]
        public IActionResult Paymentsİnfo(int daireId)
        {
            var paymentsinfo = bakiyeService.Payment(daireId);
            return Ok(paymentsinfo);
        }
    }
}
