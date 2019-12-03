using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPayablesList_Stn.Models
{
    #pragma warning disable CS1591
    public partial class CadOrganizacao
    {
        [Key]
        public Guid OrgOrganizacaoId { get; set; }
        [Required]
        [StringLength(65, MinimumLength = 3)]
        public string OrgNome { get; set; }

        [StringLength(35)]
        public string OrgCategoria { get; set; }
    }
}
