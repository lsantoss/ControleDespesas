using ControleDespesas.Dominio.Commands.Pessoa.Input;
using ControleDespesas.Dominio.Commands.Pessoa.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Helpers;
using ControleDespesas.Dominio.Repositorio;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Facilitador.Api.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Dominio.Handlers
{
    public class PessoaHandler : Notificadora, ICommandHandler<AdicionarPessoaCommand, Notificacao>,
                                               ICommandHandler<AtualizarPessoaCommand, Notificacao>,
                                               ICommandHandler<ApagarPessoaCommand, Notificacao>
    {
        private readonly IPessoaRepositorio _repository;

        public PessoaHandler(IPessoaRepositorio repository)
        {
            _repository = repository;
        }

        public ICommandResult<Notificacao> Handler(AdicionarPessoaCommand command)
        {
            try
            {
                Pessoa pessoa = PessoaHelper.GerarEntidade(command);

                AddNotificacao(pessoa.Nome.Notificacoes);

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Salvar(pessoa);

                pessoa.Id = _repository.LocalizarMaxId();

                AdicionarPessoaCommandOutput dadosRetorno = PessoaHelper.GerarDadosRetornoInsert(pessoa);

                return new CommandResult<Notificacao>("Pessoa gravada com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ICommandResult<Notificacao> Handler(AtualizarPessoaCommand command)
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
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Atualizar(pessoa);

                AtualizarPessoaCommandOutput dadosRetorno = PessoaHelper.GerarDadosRetornoUpdate(pessoa);

                return new CommandResult<Notificacao>("Pessoa atualizada com sucesso!", dadosRetorno);

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
                throw new Exception(e.Message);
            }
        }
    }
}