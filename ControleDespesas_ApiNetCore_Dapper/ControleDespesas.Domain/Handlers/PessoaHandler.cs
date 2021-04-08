using ControleDespesas.Domain.Commands.Pessoa.Input;
using ControleDespesas.Domain.Helpers;
using ControleDespesas.Domain.Interfaces.Handlers;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Infra.Commands;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Http;

namespace ControleDespesas.Domain.Handlers
{
    public class PessoaHandler : Notificadora, IPessoaHandler
    {
        private readonly IPessoaRepository _repository;

        public PessoaHandler(IPessoaRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult<Notificacao> Handler(AdicionarPessoaCommand command)
        {
            if (command == null)
                return new CommandResult(StatusCodes.Status400BadRequest, "Parâmentros inválidos", "Parâmetros de entrada", "Parâmetros de entrada estão nulos");

            if (!command.ValidarCommand())
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Parâmentros inválidos", command.Notificacoes);

            var pessoa = PessoaHelper.GerarEntidade(command);
            AddNotificacao(pessoa.Notificacoes);

            if (Invalido)
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", Notificacoes);

            var id = _repository.Salvar(pessoa);
            pessoa.DefinirId(id);
            var dadosRetorno = PessoaHelper.GerarDadosRetornoInsert(pessoa);
            return new CommandResult(StatusCodes.Status201Created, "Pessoa gravada com sucesso!", dadosRetorno);
        }

        public ICommandResult<Notificacao> Handler(int id, AtualizarPessoaCommand command)
        {
            if (command == null)
                return new CommandResult(StatusCodes.Status400BadRequest, "Parâmetros de entrada", "Parâmetros de entrada", "Parâmetros de entrada estão nulos");

            command.Id = id;

            if (!command.ValidarCommand())
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Parâmentros inválidos", command.Notificacoes);

            var pessoa = PessoaHelper.GerarEntidade(command);
            AddNotificacao(pessoa.Notificacoes);

            if (Invalido)
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", Notificacoes);

            if (!_repository.CheckId(pessoa.Id))
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", "Id", "Id inválido. Este id não está cadastrado!");

            _repository.Atualizar(pessoa);
            var dadosRetorno = PessoaHelper.GerarDadosRetornoUpdate(pessoa);
            return new CommandResult(StatusCodes.Status200OK, "Pessoa atualizada com sucesso!", dadosRetorno);
        }

        public ICommandResult<Notificacao> Handler(int id)
        {
            if (!_repository.CheckId(id))
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", "Id", "Id inválido. Este id não está cadastrado!");

            _repository.Deletar(id);
            var dadosRetorno = PessoaHelper.GerarDadosRetornoDelete(id);
            return new CommandResult(StatusCodes.Status200OK, "Pessoa excluída com sucesso!", dadosRetorno);
        }
    }
}