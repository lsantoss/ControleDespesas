﻿using ControleDespesas.Domain.Empresas.Commands.Input;
using ControleDespesas.Domain.Empresas.Helpers;
using ControleDespesas.Domain.Empresas.Interfaces.Handlers;
using ControleDespesas.Domain.Empresas.Interfaces.Repositories;
using ControleDespesas.Infra.Commands;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Http;

namespace ControleDespesas.Domain.Empresas.Handlers
{
    public class EmpresaHandler : Notificadora, IEmpresaHandler
    {
        private readonly IEmpresaRepository _repository;

        public EmpresaHandler(IEmpresaRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult<Notificacao> Handler(AdicionarEmpresaCommand command)
        {
            if (command == null)
                return new CommandResult(StatusCodes.Status400BadRequest, "Parâmentros inválidos", "Parâmetros de entrada", "Parâmetros de entrada estão nulos");

            if (!command.ValidarCommand())
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Parâmentros inválidos", command.Notificacoes);

            var empresa = EmpresaHelper.GerarEntidade(command);

            if (empresa.Invalido)
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", empresa.Notificacoes);

            var id = _repository.Salvar(empresa);
            empresa.DefinirId(id);
            var dadosRetorno = EmpresaHelper.GerarDadosRetornoInsert(empresa);
            return new CommandResult(StatusCodes.Status201Created, "Empresa gravada com sucesso!", dadosRetorno);
        }

        public ICommandResult<Notificacao> Handler(int id, AtualizarEmpresaCommand command)
        {
            if (command == null)
                return new CommandResult(StatusCodes.Status400BadRequest, "Parâmetros de entrada", "Parâmetros de entrada", "Parâmetros de entrada estão nulos");

            command.Id = id;

            if (!command.ValidarCommand())
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Parâmentros inválidos", command.Notificacoes);

            var empresa = EmpresaHelper.GerarEntidade(command);

            if (empresa.Invalido)
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", empresa.Notificacoes);

            if (!_repository.CheckId(empresa.Id))
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", "Id", "Id inválido. Este id não está cadastrado!");

            _repository.Atualizar(empresa);
            var dadosRetorno = EmpresaHelper.GerarDadosRetornoUpdate(empresa);
            return new CommandResult(StatusCodes.Status200OK, "Empresa atualizada com sucesso!", dadosRetorno);
        }

        public ICommandResult<Notificacao> Handler(int id)
        {
            if (!_repository.CheckId(id))
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", "Id", "Id inválido. Este id não está cadastrado!");

            _repository.Deletar(id);
            var dadosRetorno = EmpresaHelper.GerarDadosRetornoDelete(id);
            return new CommandResult(StatusCodes.Status200OK, "Empresa excluída com sucesso!", dadosRetorno);
        }
    }
}