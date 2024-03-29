﻿using ControleDespesas.Domain.Usuarios.Commands.Input;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;

namespace ControleDespesas.Domain.Usuarios.Interfaces.Handlers
{
    public interface IUsuarioHandler
    {
        ICommandResult<Notificacao> Handler(AdicionarUsuarioCommand command);
        ICommandResult<Notificacao> Handler(int id, AtualizarUsuarioCommand command);
        ICommandResult<Notificacao> Handler(int id);
        ICommandResult<Notificacao> Handler(LoginUsuarioCommand command);
    }
}