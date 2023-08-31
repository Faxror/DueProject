using DaireYonetimAPI.Business.Abstrack;
using DaireYonetimAPI.DataAccess;
using DaireYönetimAPI.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace DaireYonetimAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DaireController : ControllerBase
    {
        private readonly IDaireService daireService;
        private readonly DaireDbContext _daire;

        public DaireController(IDaireService daireService, DaireDbContext daire)
        {
            this.daireService = daireService;
            _daire = daire;
        }

        [HttpGet]
        [Route("{action}")]
        public IActionResult List()
        {
            return Ok(daireService.GetAllDaires());
        }

        //[HttpGet]
        //[Route("{action}")]
        //public List<Daire> aylıkodenenler()
        //{
        //    var ödenenler = _daire.Daires.Where(d => d.payduestatus).ToList();
        //    return ödenenler;
        //}

        //[HttpGet]
        //[Route("{action}")]
        //public List<Daire> ödenmeyenler()
        //{
        //    var ödenmeyenler = _daire.Daires.Where(d => !d.payduestatus).ToList();
        //    return ödenmeyenler;
        //}

        [HttpPut]
        [Route("{action}/{id}")]
        public IActionResult Put([FromBody] Daire dai)
        {
            return Ok(daireService.UpdateDaire(dai));
        }

        [HttpPost]
        [Route("{action}")]
        public IActionResult Post([FromBody] Daire daireInput)
        {
            var createdDaire = daireService.CreateDaires(daireInput);

            if (createdDaire != null)
            {       
                return Ok(createdDaire);
            }
            else
            {
                return BadRequest("Daire oluşturulurken bir hata oluştu.");
            }
        }

        [HttpPut]
        [Route("{action}")]
        public IActionResult UpdateDue(string newdue, DateTime newUpdate)
        {
            var updatedue = daireService.UpdateConfigDue(newdue, newUpdate);

            if (updatedue != null)
            {
                return Ok("Başarılı şekilde değiştirili");  
            }
            else
            {
                return BadRequest("hata");
            }



                
        }
    }
}