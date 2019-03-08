using System.Linq;
using System.Net.Http;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;

namespace notam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotamController : ControllerBase
    {
        private readonly HttpClient _httpClient;

        public NotamController()
        {
            _httpClient = new HttpClient();
        }
        
        // GET api/notam/enva
        [HttpGet("{icaoCode}")]
        public ActionResult<string> Get(string icaoCode)
        {
            var url =
                $"https://www.aviationweather.gov/adds/dataserver_current/httpparam?dataSource=metars&requestType=retrieve&format=xml&hoursBeforeNow=3&mostRecentForEachStation=true&stationString={icaoCode}";
            var notam = _httpClient.GetStringAsync(url).Result;
            return XElement.Parse(notam).Descendants("raw_text").Select(x => x.Value).First();
        }
    }
}