﻿using ControleDespesas.Domain.Commands.Empresa.Input;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Interfaces.Handlers
{
    public interface IEmpresaHandler
    {
        ICommandResult<Notificacao> Handler(AdicionarEmpresaCommand command);
        ICommandResult<Notificacao> Handler(int id, AtualizarEmpresaCommand command);
        ICommandResult<Notificacao> Handler(int id);
    }
}