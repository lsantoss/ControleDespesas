using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Dominio.Commands.Pagamento.Input
{
    public class AdicionarPagamentoCommand : Notificadora, CommandPadrao
    {
        public int IdTipoPagamento { get; set; }
        public int IdEmpresa { get; set; }
        public int IdPessoa { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime DataPagamento { get; set; }
        public DateTime DataVencimento { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                AddNotificacao(new ContratoValidacao().EhMaior(IdTipoPagamento, 0, "Id Tipo Pagamento", "Id Tipo Pagamento não é valido"));

                AddNotificacao(new ContratoValidacao().EhMaior(IdEmpresa, 0, "Id Empresa", "Id Empresa não é valido"));

                AddNotificacao(new ContratoValidacao().EhMaior(IdPessoa, 0, "Id Pessoa", "Id Pessoa não é valido"));

                AddNotificacao(new ContratoValidacao().TamanhoMinimo(Descricao, 1, "Descrição", "Descrição é um campo obrigatório"));
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