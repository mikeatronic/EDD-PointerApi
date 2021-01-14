﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PointerApi.Data;
using PointerApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PointerApi.Controllers
{
    [ApiVersion("1.0")]
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class PointerController : ControllerBase
    {
        private readonly PointerContext _context;

        public PointerController(PointerContext context)
        {
            _context = context;
        }

        [HttpGet("PostCodeSearch/{postcode}", Name = "Get")]
        [Produces("application/json")]
        public async Task<ActionResult<IEnumerable<PointerModel>>> GetPointerByPostCode(string postcode)
        {
            var pointerModel = await _context.Pointer.Where(i => i.Postcode == postcode.ToUpper() && i.Building_Status == "BUILT" && i.Address_Status == "APPROVED").ToListAsync();

            if (pointerModel.Count == 0)
            {
                return NoContent();
            }

            return Ok(pointerModel);
        }
    }
}