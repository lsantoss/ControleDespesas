using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Domain.Commands.Pagamento.Input
{
    public class AdicionarPagamentoCommand : Notificadora, CommandPadrao
    {
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
            try
            {
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
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}