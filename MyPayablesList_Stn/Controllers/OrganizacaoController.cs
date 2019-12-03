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
    #pragma warning disable CS1591
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class OrganizacaoController : ControllerBase
    {

        private readonly PayablesAPIContext _context;

        public OrganizacaoController(PayablesAPIContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Consultar estabelecimento 
        /// </summary>
        /// <returns>Retorna a linha recém criada</returns>
        /// <response code="200">Retorna linhas caso encontrar</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CadOrganizacao>>> GetOrgItem()
        {
            return await _context.CadOrganizacaoItens.ToListAsync();
        }

        /// <summary>
        /// Cadastrar estabelecimento 
        /// </summary>
        /// <returns>Retorna a linha recém criada</returns>
        /// <response code="201">Retorna a linha recém criada</response>
        /// <response code="400">Caso algum parâmetro esteja faltando ou a forma de pagamento esteja fora de formatação</response> 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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