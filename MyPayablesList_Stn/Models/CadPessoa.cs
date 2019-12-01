using System;
using System.Collections.Generic;

namespace MyPayablesList_Stn.Models
{
    public partial class CadPessoa
    {
        public Guid PesPessoaId { get; set; }
        public string PesNome { get; set; }
        public string PesUsuario { get; set; }
        public string PesSenha { get; set; }
    }
}
