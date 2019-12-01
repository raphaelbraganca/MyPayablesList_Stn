using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPayablesList_Stn.Models
{
    public partial class CadOrganizacao
    {
        public Guid OrgOrganizacaoId { get; set; }

        [StringLength(65, MinimumLength = 3)]
        public string OrgNome { get; set; }

        [StringLength(35)]
        public string OrgCategoria { get; set; }
    }
}
