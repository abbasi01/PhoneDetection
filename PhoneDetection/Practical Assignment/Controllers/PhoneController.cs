using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Practical_Assignment.Interface;

namespace Practical_Assignment.Controllers
{
    [Route("api/Phone")]
    [ApiController]
    public class PhoneController : ControllerBase
    {
        private IPhone _Phone;
        public PhoneController(IPhone phone) 
        { 
            _Phone = phone;
        }
       
        [HttpGet("PhoneDetect")]
        public IActionResult PhoneDetect(string phone)
        {
            try
            {
                if (phone != null)
                {
                    var data = _Phone.DetectPhone(phone);
                    if (data.Count ==0)
                    {
                        return Ok("Please Input Valid string");
                    }
                    return Ok(data);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
               
            }
            return StatusCode(500, "An error occurred while processing the request.");
        }
    }
}
