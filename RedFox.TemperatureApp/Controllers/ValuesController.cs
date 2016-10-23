using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Linq;

namespace RedFox.TemperatureApp.Controllers
{
	[Route("api/[controller]")]
    public class ValuesController : Controller
    {
		public ValuesController(IOptions<AppOptions> config)
		{
			Configuration = config;
		}

        public IOptions<AppOptions> Configuration { get; }

		// GET api/values
		[HttpGet]
        public IActionResult Get()
        {
			return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
		}

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
			return "values";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
