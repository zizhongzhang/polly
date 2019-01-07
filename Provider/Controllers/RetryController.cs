using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Provider.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RetryController :ControllerBase
    {
        [HttpGet]
        [Route("statuscode")]
        public async Task<IActionResult> InternalServerError()
        {
            await Task.CompletedTask;
            return new StatusCodeResult(500);
        }

        [HttpGet]
        [Route("httprequestexception")]
        public async Task<IActionResult> HttpRequestException()
        {
            await Task.CompletedTask;
            throw new HttpRequestException("A network error has occured");
        }

        [HttpGet]
        [Route("exception")]
        public async Task<IActionResult> Exception()
        {
            await Task.CompletedTask;
            throw new ArgumentException("some generic error occured");
        }

        [HttpGet]
        [Route("timeout")]
        public async Task<IActionResult> Timeout()
        {
            await Task.Delay(TimeSpan.FromSeconds(10));
            return Ok("Success");
        }
    }
}
