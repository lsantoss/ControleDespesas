using ControleDespesas.Domain.Pagamentos.Query.Results;
using System.Collections.Generic;

namespace ControleDespesas.Domain.Pessoas.Query.Results
{
    public class PessoaQueryResult
    {
        public long Id { get; set; }
        public long IdUsuario { get; set; }
        public string Nome { get; set; }
        public string ImagemPerfil { get; set; }
        public List<PagamentoQueryResult> Pagamentos { get; set; } = new List<PagamentoQueryResult>();
    }
}