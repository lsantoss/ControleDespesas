using ControleDespesas.Dominio.Commands.Pessoa.Input;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Helpers;
using ControleDespesas.Dominio.Interfaces;
using LSCode.Facilitador.Api.Command;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Dominio.Handlers
{
    public class PessoaHandler : Notificadora, ICommandHandler<AdicionarPessoaCommand>,
                                               ICommandHandler<AtualizarPessoaCommand>,
                                               ICommandHandler<ApagarPessoaCommand>
    {
        private readonly IPessoaRepositorio _repository;

        public PessoaHandler(IPessoaRepositorio repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(AdicionarPessoaCommand command)
        {
            try
            {
                Pessoa pessoa = PessoaHelper.GerarEntidade(command);

                AddNotificacao(pessoa.Nome.Notificacoes);

                if (Invalido)
                    return new CommandResult(false, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Salvar(pessoa);

                pessoa.Id = _repository.LocalizarMaxId();

                object dadosRetorno = PessoaHelper.GerarDadosRetornoCommandResult(pessoa);

                return new CommandResult(true, "Pessoa gravada com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ICommandResult Handle(AtualizarPessoaCommand command)
        {
            try
            {
                Pessoa pessoa = PessoaHelper.GerarEntidade(command);

                AddNotificacao(pessoa.Nome.Notificacoes);

                if (pessoa.Id == 0)
                    AddNotificacao("Id", "Id não está vinculado à operação solicitada");

                if (!_repository.CheckId(pessoa.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult(false, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Atualizar(pessoa);

                object dadosRetorno = PessoaHelper.GerarDadosRetornoCommandResult(pessoa);

                return new CommandResult(true, "Pessoa atualizada com sucesso!", dadosRetorno);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ICommandResult Handle(ApagarPessoaCommand command)
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

                return new CommandResult(true, "Pessoa excluída com sucesso!", new { Id = command.Id });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}