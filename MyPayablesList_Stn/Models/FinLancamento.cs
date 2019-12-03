using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyPayablesList_Stn.Models
{
    #pragma warning disable CS1591
    public partial class FinLancamento
    {
        public int LanLancamentoId { get; set; }
        [Required]
        public Guid LanOrgOrganizacaoId { get; set; }
        [Required]
        [StringLength(15)]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string LanFormaPagamento { get; set; }
        public decimal? LanValorLancamento { get; set; }
        public string LanMoeda { get; set; }
        [DataType(DataType.Date)]
        public DateTime? LanDataLancamento { get; set; }
        public Guid? LanPesPessoaId { get; set; }
    }

    public partial class FinLancamentoQueryable
    {
        public DateTime? LanDataLancamento { get; set; }
        public string OrgCategoria { get; set; }
        public string LanFormaPagamento { get; set; }
        public decimal? LanValorTotal { get; set; }
        public string LanMoeda { get; set; }

    }
}
