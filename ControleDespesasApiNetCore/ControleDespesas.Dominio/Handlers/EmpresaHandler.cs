using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Helpers;
using ControleDespesas.Dominio.Interfaces;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Facilitador.Api.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using System;
using System.Collections.Generic;

namespace ControleDespesas.Dominio.Handlers
{
    public class EmpresaHandler : Notificadora, ICommandHandler<AdicionarEmpresaCommand>,
                                                ICommandHandler<AtualizarEmpresaCommand>,
                                                ICommandHandler<ApagarEmpresaCommand>
    {
        private readonly IEmpresaRepositorio _repository;

        public EmpresaHandler(IEmpresaRepositorio repository)
        {
            _repository = repository;
        }

        public ICommandResult Handler(AdicionarEmpresaCommand command)
        {
            try
            {
                Empresa empresa = EmpresaHelper.GerarEntidade(command);

                AddNotificacao(empresa.Nome.Notificacoes);

                if (Invalido)
                    return new CommandResult("Inconsistência(s) no(s) dado(s)", (IReadOnlyCollection<Erro>) Notificacoes);

                _repository.Salvar(empresa);
                
                empresa.Id = _repository.LocalizarMaxId();

                object dadosRetorno = EmpresaHelper.GerarDadosRetornoCommandResult(empresa);

                return new CommandResult("Empresa gravada com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ICommandResult Handler(AtualizarEmpresaCommand command)
        {
            try
            {
                Empresa empresa = EmpresaHelper.GerarEntidade(command);

                AddNotificacao(empresa.Nome.Notificacoes);

                if (!_repository.CheckId(empresa.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult("Inconsistência(s) no(s) dado(s)", (IReadOnlyCollection<Erro>) Notificacoes);

                _repository.Atualizar(empresa);

                object dadosRetorno = EmpresaHelper.GerarDadosRetornoCommandResult(empresa);

                return new CommandResult("Empresa atualizada com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ICommandResult Handler(ApagarEmpresaCommand command)
        {
            try
            {
                if (!_repository.CheckId(command.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult("Inconsistência(s) no(s) dado(s)", (IReadOnlyCollection<Erro>) Notificacoes);

                _repository.Deletar(command.Id);

                return new CommandResult("Empresa excluída com sucesso!", new { Id = command.Id });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}