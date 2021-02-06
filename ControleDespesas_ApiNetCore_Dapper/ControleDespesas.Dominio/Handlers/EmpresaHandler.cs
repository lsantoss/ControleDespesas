using ControleDespesas.Domain.Commands.Empresa.Input;
using ControleDespesas.Domain.Commands.Empresa.Output;
using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Helpers;
using ControleDespesas.Domain.Interfaces.Repositorio;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Domain.Handlers
{
    public class EmpresaHandler : Notificadora, ICommandHandler<AdicionarEmpresaCommand, Notificacao>,
                                                ICommandHandler<AtualizarEmpresaCommand, Notificacao>,
                                                ICommandHandler<ApagarEmpresaCommand, Notificacao>
    {
        private readonly IEmpresaRepositorio _repository;

        public EmpresaHandler(IEmpresaRepositorio repository)
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
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                empresa = _repository.Salvar(empresa);

                AdicionarEmpresaCommandOutput dadosRetorno = EmpresaHelper.GerarDadosRetornoInsert(empresa);

                return new CommandResult<Notificacao>("Empresa gravada com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ICommandResult<Notificacao> Handler(AtualizarEmpresaCommand command)
        {
            try
            {
                Empresa empresa = EmpresaHelper.GerarEntidade(command);

                AddNotificacao(empresa.Notificacoes);

                if (!_repository.CheckId(empresa.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Atualizar(empresa);

                AtualizarEmpresaCommandOutput dadosRetorno = EmpresaHelper.GerarDadosRetornoUpdate(empresa);

                return new CommandResult<Notificacao>("Empresa atualizada com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ICommandResult<Notificacao> Handler(ApagarEmpresaCommand command)
        {
            try
            {
                if (!_repository.CheckId(command.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Deletar(command.Id);

                ApagarEmpresaCommandOutput dadosRetorno = EmpresaHelper.GerarDadosRetornoDelete(command.Id);

                return new CommandResult<Notificacao>("Empresa excluída com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}