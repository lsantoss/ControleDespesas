using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Dominio.Commands.TipoPagamento.Input
{
    public class AtualizarTipoPagamentoCommand : Notificadora, CommandPadrao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        public bool ValidarCommand()
        {
            AddNotificacao(new ContratoValidacao().EhMaior(Id, 0, "Id", "Id não é valido"));

            AddNotificacao(new ContratoValidacao().TamanhoMinimo(Descricao, 1, "Descrição", "Descrição é um campo obrigatório"));
            AddNotificacao(new ContratoValidacao().TamanhoMaximo(Descricao, 250, "Descrição", "Descrição maior que o esperado"));

            return Valido;
        }
    }
}