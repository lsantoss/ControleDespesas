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
                AddNotificacao(new ContratoValidacao().EhMaior(Id, 0, "Id", "Id não é valido"));

                AddNotificacao(new ContratoValidacao().EhMaior(IdTipoPagamento, 0, "Id Tipo Pagamento", "Id Tipo Pagamento não é valido"));

                AddNotificacao(new ContratoValidacao().EhMaior(IdEmpresa, 0, "Id Empresa", "Id Empresa não é valido"));

                AddNotificacao(new ContratoValidacao().EhMaior(IdPessoa, 0, "Id Pessoa", "Id Pessoa não é valido"));

                AddNotificacao(new ContratoValidacao().NaoEhNuloOuVazio(Descricao, "Descrição", "Descrição é um campo obrigatório"));
                AddNotificacao(new ContratoValidacao().TamanhoMaximo(Descricao, 250, "Descrição", "Descrição maior que o esperado"));

                AddNotificacao(new ContratoValidacao().EhMaior(Valor, 0, "Valor", "Valor não é valido"));

                return Valido;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}