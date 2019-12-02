using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyPayablesList_Stn.Models;
using Microsoft.EntityFrameworkCore;

namespace MyPayablesList_Stn.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizacaoController : ControllerBase
    {

        private readonly PayablesAPIContext _context;

        public OrganizacaoController(PayablesAPIContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CadOrganizacao>>> GetOrgItem()
        {
            return await _context.CadOrganizacaoItens.ToListAsync();
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [HttpPost]
        public async Task<ActionResult<CadOrganizacao>> PostOrgItem(CadOrganizacao organizacao)
        {
            _context.CadOrganizacaoItens.Add(organizacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetOrgItem", new CadOrganizacao { OrgOrganizacaoId = organizacao.OrgOrganizacaoId }, organizacao);
        }
    }
}