using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using System.Collections.Generic;

namespace ControleDespesas.Infra.Commands
{
    public static class CommandHelper
    {
        public static ICommandResult<Notificacao> Result(int statusCode, string mensagem, string propriedade, string notificacaoMensagem)
        {
            var notificacoes = new List<Notificacao>();
            var notificacao = new Notificacao(propriedade, notificacaoMensagem);
            notificacoes.Add(notificacao);
            return new CommandResult<Notificacao>(statusCode, mensagem, notificacoes);
        }

        public static ICommandResult<Notificacao> Result(int statusCode, string mensagem, IReadOnlyCollection<Notificacao> notificacoes)
        {
            return new CommandResult<Notificacao>(statusCode, mensagem, notificacoes);
        }

        public static ICommandResult<Notificacao> Result(int statusCode, string mensagem, object dados)
        {
            return new CommandResult<Notificacao>(statusCode, mensagem, dados);
        }
    }
}