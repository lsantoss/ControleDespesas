using LSCode.Validador.ValidacoesNotificacoes;
using System.Collections.Generic;

namespace ControleDespesas.Infra.Response
{
    public class ApiResponse<TDados> where TDados : class
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public TDados Dados { get; set; }
        public List<Notificacao> Erros { get; set; }

        public ApiResponse() { }

        public ApiResponse(bool sucesso, string mensagem, TDados dados, List<Notificacao> erros)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
            Erros = erros;
        }

        public ApiResponse(string mensagem, TDados dados)
        {
            Sucesso = true;
            Mensagem = mensagem;
            Dados = dados;
            Erros = null;
        }

        public ApiResponse(string mensagem, List<Notificacao> erros)
        {
            Sucesso = false;
            Mensagem = mensagem;
            Dados = null;
            Erros = erros;
        }
    }
}