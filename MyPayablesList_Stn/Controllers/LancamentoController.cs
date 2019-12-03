using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPayablesList_Stn.Models;

namespace MyPayablesList_Stn.Controllers
{
    #pragma warning disable CS1591
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class LancamentoController : ControllerBase
    {

        private readonly PayablesAPIContext _context;
        public LancamentoController(PayablesAPIContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Consultar lançamentos 
        /// </summary>
        /// <response code="200">Retorna linhas caso encontrar</response>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinLancamento>>> GetLanItens()
        {
            return await _context.FinLancamentoItens.ToListAsync();
        }

        /// <summary>
        /// Cadastrar lançamentos
        /// </summary>
        /// <remarks>
        /// Modelo:
        ///
        ///     POST 
        ///     {
        ///        "lanOrgOrganizacaoId": "01ccbfdc-cbfc-45dc-8669-2dd5fbab3dfc",
        ///        "lanFormaPagamento": "Debito",
        ///        "lanValorLancamento": 20.12,
        ///        "lanDataLancamento": "2019-11-29"
        ///     }
        ///
        /// </remarks>
        /// <returns>Retorna a linha recém criada</returns>
        /// <response code="201">Retorna a linha recém criada</response>
        /// <response code="400">Caso algum parâmetro esteja faltando ou a forma de pagamento esteja fora de formatação</response> 
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Consumes(MediaTypeNames.Application.Json)]
        [HttpPost]
        public async Task<ActionResult<FinLancamento>> PostLanItem(FinLancamento lancamento)
        {
            _context.FinLancamentoItens.Add(lancamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLanItens", new FinLancamento { LanLancamentoId = lancamento.LanLancamentoId }, lancamento);
        }


        /// <summary>
        /// Consultar lançamentos filtrados por período e agrupados por Categoria 
        /// </summary>
        /// <remarks>
        /// Modelo:
        ///     
        ///     dataInicio: AAAA-MM-DD
        ///     dataFim: AAAA-MM-DD
        ///
        /// </remarks>
        /// <response code="200">Retorna linhas caso encontrar</response>
        /// <response code="404">Retorna caso não encontre nenhuma linha</response> 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet, Route("grupo")]
        public async Task<ActionResult<IAsyncEnumerable<FinLancamentoQueryable>>> GetLanItensCategoria(DateTime? dataInicio, DateTime? dataFim)
        {
            if(dataFim == null)
            {
                dataFim = DateTime.Now;
            }

            if(dataInicio == null)
            {
                dataInicio = DateTime.MinValue;
            }

            string query = "SELECT lan_data_lancamento, org_categoria, lan_forma_pagamento, lan_moeda, sum(lan_valor_lancamento) as lan_valor_total FROM fin_lancamento " 
            + "inner join cad_organizacao on lan_org_organizacao_id = org_organizacao_id "
            + "where lan_data_lancamento between {0} and {1} "
            + "group by lan_data_lancamento, org_categoria, lan_forma_pagamento, lan_moeda "
            + "order by lan_data_lancamento desc";
            var lancamento = await _context.FinLancamentoItensRetorno
                .FromSqlRaw(query, dataInicio, dataFim)
                .ToListAsync();

            if (lancamento == null)
            {
                return NotFound();
            }

            return Ok(lancamento);

        }

    }
}