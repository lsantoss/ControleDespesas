using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using System.Collections.Generic;
using System.Linq;

namespace ControleDespesas.Infra.Commands
{
    public class CommandResult : ICommandResult<Notificacao>
    {
        public int StatusCode { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }
        public IReadOnlyCollection<Notificacao> Erros { get; set; }

        public CommandResult(int statusCode, string mensagem, object dados)
        {
            StatusCode = statusCode;
            Sucesso = true;
            Mensagem = mensagem;
            Dados = dados;
            Erros = null;
        }

        public CommandResult(int statusCode, string mensagem, IEnumerable<Notificacao> erros)
        {
            StatusCode = statusCode;
            Sucesso = false;
            Mensagem = mensagem;
            Dados = null;
            Erros = erros.ToList();
        }

        public CommandResult(int statusCode, string mensagem, string propriedade, string notificacaoMensagem)
        {
            StatusCode = statusCode;
            Sucesso = false;
            Mensagem = mensagem;
            Dados = null;
            Erros = new List<Notificacao>() { new Notificacao(propriedade, notificacaoMensagem) };
        }
    }
}