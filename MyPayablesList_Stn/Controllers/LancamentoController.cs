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
    [Route("api/[controller]")]
    [ApiController]
    public class LancamentoController : ControllerBase
    {

        private readonly PayablesAPIContext _context;
        public LancamentoController(PayablesAPIContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<FinLancamento>>> GetLanItens()
        {
            return await _context.FinLancamentoItens.ToListAsync();
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [HttpPost]
        public async Task<ActionResult<FinLancamento>> PostLanItem(FinLancamento lancamento)
        {
            _context.FinLancamentoItens.Add(lancamento);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLanItens", new FinLancamento { LanLancamentoId = lancamento.LanLancamentoId }, lancamento);
        }

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