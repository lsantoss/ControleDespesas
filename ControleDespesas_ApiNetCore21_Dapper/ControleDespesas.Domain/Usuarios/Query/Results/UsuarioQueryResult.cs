using ControleDespesas.Domain.Pessoas.Query.Results;
using ControleDespesas.Domain.Usuarios.Enums;
using System.Collections.Generic;

namespace ControleDespesas.Domain.Usuarios.Query.Results
{
    public class UsuarioQueryResult
    {
        public long Id { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
        public EPrivilegioUsuario Privilegio { get; set; }
        public List<PessoaQueryResult> Pessoas { get; set; } = new List<PessoaQueryResult>();
    }
}