﻿using LSCode.Facilitador.Api.InterfacesCommand;

namespace ControleDespesas.Dominio.Commands.Empresa.Output
{
    public class AtualizarEmpresaCommandResult : ICommandResult
    {
        public bool Sucesso { get; set; }
        public string Mensagem { get; set; }
        public object Dados { get; set; }

        public AtualizarEmpresaCommandResult(bool sucesso, string mensagem, object dados)
        {
            Sucesso = sucesso;
            Mensagem = mensagem;
            Dados = dados;
        }
    }
}