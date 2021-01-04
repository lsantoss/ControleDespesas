using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDespesas.Dominio.Commands.Pagamento.Input
{
    public class AtualizarPagamentoCommand : Notificadora, CommandPadrao
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int IdTipoPagamento { get; set; }

        [Required]
        public int IdEmpresa { get; set; }

        [Required]
        public int IdPessoa { get; set; }

        [Required]
        public string Descricao { get; set; }

        [Required]
        public double Valor { get; set; }

        [Required]
        public DateTime DataVencimento { get; set; }

        public DateTime? DataPagamento { get; set; }

        public string ArquivoPagamento { get; set; }

        public string ArquivoComprovante { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                if (Id <= 0)
                    AddNotificacao("Id", "Id não é valido");

                if (IdTipoPagamento <= 0)
                    AddNotificacao("Id Tipo Pagamento", "Id Tipo Pagamento não é valido");

                if (IdEmpresa <= 0)
                    AddNotificacao("Id Empresa", "Id Empresa não é valido");

                if (IdPessoa <= 0)
                    AddNotificacao("Id Pessoa", "Id Pessoa não é valido");

                if (string.IsNullOrEmpty(Descricao))
                    AddNotificacao("Descrição", "Descrição é um campo obrigatório");
                else if (Descricao.Length > 250)
                    AddNotificacao("Descrição", "Descrição maior que o esperado");

                if (Valor <= 0)
                    AddNotificacao("Valor", "Valor não é valido");

                return Valido;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}