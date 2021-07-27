using ControleDespesas.Domain.Pessoas.Commands.Input;
using ControleDespesas.Domain.Pessoas.Helpers;
using ControleDespesas.Domain.Pessoas.Interfaces.Handlers;
using ControleDespesas.Domain.Pessoas.Interfaces.Repositories;
using ControleDespesas.Domain.Usuarios.Interfaces.Repositories;
using ControleDespesas.Infra.Commands;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Http;

namespace ControleDespesas.Domain.Pessoas.Handlers
{
    public class PessoaHandler : Notificadora, IPessoaHandler
    {
        private readonly IPessoaRepository _repository;
        private readonly IUsuarioRepository _repositoryUsuario;

        public PessoaHandler(IPessoaRepository repository,
                             IUsuarioRepository repositoryUsuario)
        {
            _repository = repository;
            _repositoryUsuario = repositoryUsuario;
        }

        public ICommandResult<Notificacao> Handler(AdicionarPessoaCommand command)
        {
            if (command == null)
                return new CommandResult(StatusCodes.Status400BadRequest, "Parâmentros inválidos", "Parâmetros de entrada", "Parâmetros de entrada estão nulos");

            if (!command.ValidarCommand())
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Parâmentros inválidos", command.Notificacoes);

            var pessoa = PessoaHelper.GerarEntidade(command);

            if (pessoa.Invalido)
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", pessoa.Notificacoes);

            if (!_repositoryUsuario.CheckId(pessoa.IdUsuario))
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", "IdUsuario", "Id inválido. Este id não está cadastrado!");

            var id = _repository.Salvar(pessoa);
            pessoa.DefinirId(id);

            var dadosRetorno = PessoaHelper.GerarDadosRetorno(pessoa);

            return new CommandResult(StatusCodes.Status201Created, "Pessoa gravada com sucesso!", dadosRetorno);
        }

        public ICommandResult<Notificacao> Handler(long id, AtualizarPessoaCommand command)
        {
            if (command == null)
                return new CommandResult(StatusCodes.Status400BadRequest, "Parâmetros de entrada", "Parâmetros de entrada", "Parâmetros de entrada estão nulos");

            command.Id = id;

            if (!command.ValidarCommand())
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Parâmentros inválidos", command.Notificacoes);

            var pessoa = PessoaHelper.GerarEntidade(command);

            if (pessoa.Invalido)
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", pessoa.Notificacoes);

            if (!_repository.CheckId(pessoa.Id))
                AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

            if (!_repositoryUsuario.CheckId(pessoa.IdUsuario))
                AddNotificacao("IdUsuario", "Id inválido. Este id não está cadastrado!");

            if (Invalido)
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", Notificacoes);

            _repository.Atualizar(pessoa);

            var dadosRetorno = PessoaHelper.GerarDadosRetorno(pessoa);

            return new CommandResult(StatusCodes.Status200OK, "Pessoa atualizada com sucesso!", dadosRetorno);
        }

        public ICommandResult<Notificacao> Handler(long id, long idUsuario)
        {
            if (!_repository.CheckId(id))
                AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

            if (!_repositoryUsuario.CheckId(idUsuario))
                AddNotificacao("IdUsuario", "Id inválido. Este id não está cadastrado!");

            if (Invalido)
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", Notificacoes);

            _repository.Deletar(id, idUsuario);

            var dadosRetorno = PessoaHelper.GerarDadosRetornoDelete(id);

            return new CommandResult(StatusCodes.Status200OK, "Pessoa excluída com sucesso!", dadosRetorno);
        }
    }
}