using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Helpers;
using ControleDespesas.Dominio.Interfaces;
using LSCode.Facilitador.Api.Command;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

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

        public ICommandResult Handle(AdicionarEmpresaCommand command)
        {
            try
            {
                Empresa empresa = EmpresaHelper.GerarEntidade(command);

                AddNotificacao(empresa.Nome.Notificacoes);

                if (Invalido)
                    return new CommandResult(false, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Salvar(empresa);
                
                empresa.Id = _repository.LocalizarMaxId();

                object dadosRetorno = EmpresaHelper.GerarDadosRetornoCommandResult(empresa);

                return new CommandResult(true, "Empresa gravada com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ICommandResult Handle(AtualizarEmpresaCommand command)
        {
            try
            {
                Empresa empresa = EmpresaHelper.GerarEntidade(command);

                AddNotificacao(empresa.Nome.Notificacoes);

                if (empresa.Id == 0)
                    AddNotificacao("Id", "Id não está vinculado à operação solicitada");

                if (!_repository.CheckId(empresa.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult(false, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Atualizar(empresa);

                object dadosRetorno = EmpresaHelper.GerarDadosRetornoCommandResult(empresa);

                return new CommandResult(true, "Empresa gravada com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ICommandResult Handle(ApagarEmpresaCommand command)
        {
            try
            {
                if (command.Id == 0)
                    AddNotificacao("Id", "Id não está vinculado à operação solicitada");

                if (!_repository.CheckId(command.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult(false, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Deletar(command.Id);

                return new CommandResult(true, "Empresa excluída com sucesso!", new { Id = command.Id });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}