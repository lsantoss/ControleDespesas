using ControleDespesas.Dominio.Query.Empresa;
using System.Collections.Generic;

namespace ControleDespesas.Dominio.Query
{
    public class CommandResult2 : ICommandResult2<List<EmpresaQueryResult>>
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public List<EmpresaQueryResult> Dados { get; set; }

        public CommandResult2(bool sucesso, string mensagem, List<EmpresaQueryResult> dados)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
        }
    }
}