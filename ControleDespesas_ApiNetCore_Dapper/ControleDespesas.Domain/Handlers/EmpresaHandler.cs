using ControleDespesas.Domain.Commands.Empresa.Input;
using ControleDespesas.Domain.Commands.Empresa.Output;
using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Helpers;
using ControleDespesas.Domain.Interfaces.Handlers;
using ControleDespesas.Domain.Interfaces.Repositories;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Http;
using System;

namespace ControleDespesas.Domain.Handlers
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
            try
            {
                Empresa empresa = EmpresaHelper.GerarEntidade(command);

                AddNotificacao(empresa.Notificacoes);

                if (Invalido)
                    return new CommandResult<Notificacao>(422, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                empresa = _repository.Salvar(empresa);

                AdicionarEmpresaCommandOutput dadosRetorno = EmpresaHelper.GerarDadosRetornoInsert(empresa);

                return new CommandResult<Notificacao>(201, "Empresa gravada com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ICommandResult<Notificacao> Handler(int id, AtualizarEmpresaCommand command)
        {
            try
            {
                if (command == null)
                {
                    AddNotificacao("Parâmetros de entrada", "Parâmetros de entrada estão nulos");
                    return new CommandResult<Notificacao>(StatusCodes.Status400BadRequest, "Parâmetros de entrada", Notificacoes);
                }                    

                command.Id = id;

                if (!command.ValidarCommand())
                    return new CommandResult<Notificacao>(StatusCodes.Status422UnprocessableEntity, "Parâmentros inválidos", command.Notificacoes);

                Empresa empresa = EmpresaHelper.GerarEntidade(command);

                AddNotificacao(empresa.Notificacoes);

                if (Invalido)
                    return new CommandResult<Notificacao>(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                if (!_repository.CheckId(empresa.Id))
                {
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");
                    return new CommandResult<Notificacao>(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", Notificacoes);
                }

                _repository.Atualizar(empresa);

                AtualizarEmpresaCommandOutput dadosRetorno = EmpresaHelper.GerarDadosRetornoUpdate(empresa);

                return new CommandResult<Notificacao>(StatusCodes.Status200OK, "Empresa atualizada com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ICommandResult<Notificacao> Handler(int id)
        {
            try
            {
                if (!_repository.CheckId(id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult<Notificacao>(422, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Deletar(id);

                ApagarEmpresaCommandOutput dadosRetorno = EmpresaHelper.GerarDadosRetornoDelete(id);

                return new CommandResult<Notificacao>(200, "Empresa excluída com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}