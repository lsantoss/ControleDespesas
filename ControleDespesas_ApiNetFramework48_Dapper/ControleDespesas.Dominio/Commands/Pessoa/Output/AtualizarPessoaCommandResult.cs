﻿using LSCode.Facilitador.Api.InterfacesCommand;

namespace ControleDespesas.Dominio.Commands.Pessoa.Output
{
    public class AtualizarPessoaCommandResult : ICommandResult
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }

        public AtualizarPessoaCommandResult(bool sucesso, string mensagem, object dados)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
        }
    }
}