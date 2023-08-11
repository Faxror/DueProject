using DaireYonetimAPI.Business.Abstrack;
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
        public BakiyeController(IBakiyeService bakiyeService)
        {
            this.bakiyeService = bakiyeService;
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
            if (paymnet <= 0)
            {
                return BadRequest("Geçersiz yapilanOdeme değeri. Lütfen pozitif bir değer girin.");
            }

            Bakiye bakiye = bakiyeService.calculatecurrentdebt(apartmentno, paymnet);

            if (bakiye == null)
            {
                return NotFound();
            }

            return Ok(bakiye.TotalDebt);
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


        [HttpPut]
        [Route("{id}")] 
        public IActionResult AddDebt([FromBody] Bakiye model, int id)
        {

            int ApartmentNo = model.ApartmentNo;
            decimal BalanceDue = model.BalanceDue;

            var bakiye = bakiyeService.AddDebt(ApartmentNo, BalanceDue, id);

            if (bakiye == null)
            {
                return BadRequest("Daire bulunamadı!");
            }

            return Ok(bakiye);
        }

        [HttpGet("paymentstatus")]
        public IActionResult PaymentStatus(decimal borcBakiye)
        {
            var bakiyedurum = bakiyeService.PaymentStatus(borcBakiye);
            return Ok(bakiyedurum);
        }

        [HttpGet("payment")]
        public IActionResult Paymentsİnfo (int daireId)
        {
            var paymentsinfo = bakiyeService.Payment(daireId);
            return Ok(paymentsinfo);
        }
    }
}
