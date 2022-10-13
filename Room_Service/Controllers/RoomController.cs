using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Room_Service.Data;
using Room_Service.DTO;
using Room_Service.Entities;
using Room_Service.Models;

namespace Room_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly DBContext _context;

        public RoomController(DBContext context)
        {
            _context = context;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Workspace>), 200)]
        public async Task<ActionResult<IEnumerable<Workspace>>> GetProducts()
        {
            return Ok();
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Workspace), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Workspace>> GetProductById(string id)
        {
            var product = id;

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [Route("category/{category}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Workspace>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Workspace>>> GetProductsByCategory(string category)
        {
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(Workspace), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Workspace>> CreateProduct([FromBody] Workspace product)
        {
            return Ok();
        }
    }
}
