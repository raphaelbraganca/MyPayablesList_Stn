using System;
using System.Collections.Generic;

namespace MyPayablesList_Stn.Models
{
    public partial class FinLancamento
    {
        public int LanLancamentoId { get; set; }
        public Guid LanOrgOrganizacaoId { get; set; }
        public string LanFormaPagamento { get; set; }
        public decimal? LanValorLancamento { get; set; }
        public string LanMoeda { get; set; }
        public DateTime? LanDataLancamento { get; set; }
        public Guid? LanPesPessoaId { get; set; }
    }
}
