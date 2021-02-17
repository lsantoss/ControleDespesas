using ControleDespesas.Domain.Commands.Empresa.Input;
using ControleDespesas.Domain.Helpers;
using ControleDespesas.Domain.Interfaces.Handlers;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Infra.Commands;
using LSCode.Facilitador.Api.Interfaces.Commands;
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
                if (command == null)
                    return CommandHelper.Result(StatusCodes.Status400BadRequest, 
                                                "Parâmentros inválidos", 
                                                "Parâmetros de entrada", 
                                                "Parâmetros de entrada estão nulos");

                if (!command.ValidarCommand())
                    return CommandHelper.Result(StatusCodes.Status422UnprocessableEntity, 
                                                "Parâmentros inválidos", 
                                                command.Notificacoes);

                var empresa = EmpresaHelper.GerarEntidade(command);

                AddNotificacao(empresa.Notificacoes);

                if (Invalido)
                    return CommandHelper.Result(StatusCodes.Status422UnprocessableEntity, 
                                                "Inconsistência(s) no(s) dado(s)", 
                                                Notificacoes);

                empresa = _repository.Salvar(empresa);

                var dadosRetorno = EmpresaHelper.GerarDadosRetornoInsert(empresa);

                return CommandHelper.Result(StatusCodes.Status201Created, 
                                            "Empresa gravada com sucesso!", 
                                            dadosRetorno);
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
                    return CommandHelper.Result(StatusCodes.Status400BadRequest, 
                                                "Parâmetros de entrada", 
                                                "Parâmetros de entrada", 
                                                "Parâmetros de entrada estão nulos");

                command.Id = id;

                if (!command.ValidarCommand())
                    return CommandHelper.Result(StatusCodes.Status422UnprocessableEntity, 
                                                "Parâmentros inválidos", 
                                                command.Notificacoes);

                var empresa = EmpresaHelper.GerarEntidade(command);

                AddNotificacao(empresa.Notificacoes);

                if (Invalido)
                    return CommandHelper.Result(StatusCodes.Status422UnprocessableEntity, 
                                                "Inconsistência(s) no(s) dado(s)", 
                                                Notificacoes);

                if (!_repository.CheckId(empresa.Id))
                    return CommandHelper.Result(StatusCodes.Status422UnprocessableEntity, 
                                                "Inconsistência(s) no(s) dado(s)", 
                                                "Id", 
                                                "Id inválido. Este id não está cadastrado!");

                _repository.Atualizar(empresa);

                var dadosRetorno = EmpresaHelper.GerarDadosRetornoUpdate(empresa);

                return CommandHelper.Result(StatusCodes.Status200OK, 
                                            "Empresa atualizada com sucesso!", 
                                            dadosRetorno);
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
                    return CommandHelper.Result(StatusCodes.Status422UnprocessableEntity, 
                                                "Inconsistência(s) no(s) dado(s)", 
                                                "Id", 
                                                "Id inválido. Este id não está cadastrado!");

                _repository.Deletar(id);

                var dadosRetorno = EmpresaHelper.GerarDadosRetornoDelete(id);

                return CommandHelper.Result(StatusCodes.Status200OK, 
                                            "Empresa excluída com sucesso!", 
                                            dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}