using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDespesas.Dominio.Commands.TipoPagamento.Input
{
    public class AdicionarTipoPagamentoCommand : Notificadora, CommandPadrao
    {
        [Required]
        public string Descricao { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                AddNotificacao(new ContratoValidacao().NaoEhNuloOuVazio(Descricao, "Descrição", "Descrição é um campo obrigatório"));
                AddNotificacao(new ContratoValidacao().TamanhoMaximo(Descricao, 250, "Descrição", "Descrição maior que o esperado"));

                return Valido;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}