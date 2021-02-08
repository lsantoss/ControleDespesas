using ControleDespesas.Domain.Commands.Pessoa.Input;
using ControleDespesas.Domain.Commands.Pessoa.Output;
using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Helpers;
using ControleDespesas.Domain.Interfaces.Repositories;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Domain.Handlers
{
    public class PessoaHandler : Notificadora, ICommandHandler<AdicionarPessoaCommand, Notificacao>,
                                               ICommandHandler<AtualizarPessoaCommand, Notificacao>,
                                               ICommandHandler<ApagarPessoaCommand, Notificacao>
    {
        private readonly IPessoaRepository _repository;

        public PessoaHandler(IPessoaRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult<Notificacao> Handler(AdicionarPessoaCommand command)
        {
            try
            {
                Pessoa pessoa = PessoaHelper.GerarEntidade(command);

                AddNotificacao(pessoa.Notificacoes);

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                pessoa = _repository.Salvar(pessoa);

                AdicionarPessoaCommandOutput dadosRetorno = PessoaHelper.GerarDadosRetornoInsert(pessoa);

                return new CommandResult<Notificacao>("Pessoa gravada com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ICommandResult<Notificacao> Handler(AtualizarPessoaCommand command)
        {
            try
            {
                Pessoa pessoa = PessoaHelper.GerarEntidade(command);

                AddNotificacao(pessoa.Notificacoes);

                if (!_repository.CheckId(pessoa.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Atualizar(pessoa);

                AtualizarPessoaCommandOutput dadosRetorno = PessoaHelper.GerarDadosRetornoUpdate(pessoa);

                return new CommandResult<Notificacao>("Pessoa atualizada com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ICommandResult<Notificacao> Handler(ApagarPessoaCommand command)
        {
            try
            {
                if (!_repository.CheckId(command.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Deletar(command.Id);

                ApagarPessoaCommandOutput dadosRetorno = PessoaHelper.GerarDadosRetornoDelete(command.Id);

                return new CommandResult<Notificacao>("Pessoa excluída com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}