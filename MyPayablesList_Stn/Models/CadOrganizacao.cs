using System;
using System.Collections.Generic;

namespace MyPayablesList_Stn.Models
{
    public partial class CadOrganizacao
    {
        public Guid OrgOrganizacaoId { get; set; }
        public string OrgNome { get; set; }
        public string OrgCategoria { get; set; }
    }
}
