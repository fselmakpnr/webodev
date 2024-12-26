using Microsoft.AspNetCore.Mvc;
using webodev.Models;



namespace webodev.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KullaniciApiController : ControllerBase
    {
        Context k = new Context();
        // GET: api/<KullaniciApiController>
        [HttpGet]
        public List<Kullanicilar> Get()
        {
            var kullanici = k.Kullanicilars.ToList();
            return kullanici;
        }

        // GET api/<KullaniciApiController>/5
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
        // POST api/<KullaniciApiController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<KullaniciApiController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<KullaniciApiController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
