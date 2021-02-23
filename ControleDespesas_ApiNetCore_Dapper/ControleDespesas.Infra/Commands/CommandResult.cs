using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using System.Collections.Generic;

namespace ControleDespesas.Infra.Commands
{
    public class CommandResult : ICommandResult<Notificacao>
    {
        public int StatusCode { get; set; }
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }
        public IReadOnlyCollection<Notificacao> Erros { get; set; }

        /// <summary>Construtor da classe CommandResult.</summary>
        /// <param name="statusCode">Indica o status code gerado na requisição.</param>
        /// <param name="sucesso">Indica se a requisição foi realizada com sucesso.</param>
        /// <param name="mensagem">Mensagem retornada da requisição.</param>
        /// <param name="dados">Principais dados da requisição.</param>
        /// <param name="erros">Notificações da requisição.</param>
        /// <returns> Cria uma instância da classe CommandResult.</returns>
        public CommandResult(int statusCode, bool sucesso, string mensagem, object dados, List<Notificacao> erros)
        {
            StatusCode = statusCode;
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
            Erros = erros;
        }

        /// <summary>Construtor da classe CommandResult.</summary>
        /// <param name="statusCode">Indica o status code gerado na requisição.</param>
        /// <param name="mensagem">Mensagem retornada da requisição.</param>
        /// <param name="propriedade">Propriedade que será notificada.</param>
        /// <param name="notificacaoMensagem">Mensagem de notificação.</param>
        /// <returns> Cria uma instância da classe CommandResult.</returns>
        public CommandResult(int statusCode, string mensagem, string propriedade, string notificacaoMensagem)
        {
            StatusCode = statusCode;
            Sucesso = false;
            Mensagem = mensagem;
            Dados = null;
            Erros = new List<Notificacao>() { new Notificacao(propriedade, notificacaoMensagem) };
        }

        /// <summary>Construtor da classe CommandResult para requisições com sucesso.</summary>
        /// <param name="statusCode">Indica o status code gerado na requisição.</param>
        /// <param name="mensagem">Mensagem retornada da requisição.</param>
        /// <param name="dados">Principais dados da requisição.</param>
        /// <returns> Cria uma instância da classe CommandResult.</returns>
        public CommandResult(int statusCode, string mensagem, object dados)
        {
            StatusCode = statusCode;
            Sucesso = true;
            Mensagem = mensagem;
            Dados = dados;
            Erros = null;
        }

        /// <summary>Construtor da classe CommandResult para requisições sem sucesso.</summary>
        /// <param name="statusCode">Indica o status code gerado na requisição.</param>
        /// <param name="mensagem">Mensagem retornada da requisição.</param>
        /// <param name="erros">Notificações da requisição.</param>
        /// <returns> Cria uma instância da classe CommandResult.</returns>
        public CommandResult(int statusCode, string mensagem, List<Notificacao> erros)
        {
            StatusCode = statusCode;
            Sucesso = false;
            Mensagem = mensagem;
            Dados = null;
            Erros = erros;
        }
    }
}
