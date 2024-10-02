using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Errors;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        private readonly StoreContext _context;

        public BuggyController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("testauth")]
        [Authorize]
        public ActionResult<string> GetSecretText()
        {
            return "your-64-character-super-secure-key-1234567890abcdefghijklmnoqrstuvwxyz";
        }


        [HttpGet("notfound")]
        public ActionResult GetNotFoundRequest()
        {
            var Id = _context.Products.Find(552); // 552 is product id since we dont have productid 552 for test only  
            if (Id == null)
            {
                return NotFound( new ApiResponse (404));
            }

            return Ok();
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError()
        {
            var Id = _context.Products.Find(552);
            var thingToReturn = Id.ToString();
            return Ok();
        }


        [HttpGet("badrequest")]
        public ActionResult GetBadRequest( )
        {
            return BadRequest( new ApiResponse(400));
        }


        [HttpGet("badrequest/{id}")]
        public ActionResult GetNotFoundRequest(int id)
        {
            return Ok();
        }

    }
}