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

        }

    }
