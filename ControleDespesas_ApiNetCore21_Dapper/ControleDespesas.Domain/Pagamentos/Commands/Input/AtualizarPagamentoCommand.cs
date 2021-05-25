using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using Newtonsoft.Json;
using System;

namespace ControleDespesas.Domain.Pagamentos.Commands.Input
{
    public class AtualizarPagamentoCommand : Notificadora, CommandPadrao
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int IdTipoPagamento { get; set; }
        public int IdEmpresa { get; set; }
        public int IdPessoa { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public string ArquivoPagamento { get; set; }
        public string ArquivoComprovante { get; set; }

        public bool ValidarCommand()
        {
            if (Id <= 0)
                AddNotificacao("Id", "Id não é valido");

            if (IdTipoPagamento <= 0)
                AddNotificacao("IdTipoPagamento", "IdTipoPagamento não é valido");

            if (IdEmpresa <= 0)
                AddNotificacao("IdEmpresa", "IdEmpresa não é valido");

            if (IdPessoa <= 0)
                AddNotificacao("IdPessoa", "IdPessoa não é valido");

            if (string.IsNullOrEmpty(Descricao))
                AddNotificacao("Descricao", "Descricao é um campo obrigatório");
            else if (Descricao.Length > 250)
                AddNotificacao("Descricao", "Descricao maior que o esperado");

            if (Valor <= 0)
                AddNotificacao("Valor", "Valor não é valido");

            return Valido;
        }
    }
}