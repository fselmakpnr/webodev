using Microsoft.AspNetCore.Mvc;
using webodev.Models;



namespace webodev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciApiController : ControllerBase
    {
        Context k = new Context();
       
        [HttpGet]
        public List<Kullanicilar> Get()
        {
            var kullanici = k.Kullanicilars.ToList();
            return kullanici;
        }

       
        [HttpGet("{id}")]
        public ActionResult<Kullanicilar> Get(int id)
        {
            var kullanici = k.Kullanicilars.FirstOrDefault(x => x.KullanicilarID == id);
            if (kullanici == null)
            {
                return NotFound();
            }
            return kullanici;

        }
       
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

       
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
