using System.Collections.Generic;
using System.Threading.Tasks;
using Application.Readings;
using Domain;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController
    {
        private readonly IMediator _mediator;

        public TestController(IMediator mediator)
        {
            _mediator = mediator;
        } 

        [HttpGet("test")]
        public ActionResult<string> Test()
        {
            return("ok");
        }

    }
}