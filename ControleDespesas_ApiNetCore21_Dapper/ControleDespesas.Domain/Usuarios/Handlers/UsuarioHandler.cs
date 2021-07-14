using ControleDespesas.Domain.Usuarios.Commands.Input;
using ControleDespesas.Domain.Usuarios.Commands.Output;
using ControleDespesas.Domain.Usuarios.Helpers;
using ControleDespesas.Domain.Usuarios.Interfaces.Handlers;
using ControleDespesas.Domain.Usuarios.Interfaces.Helpers;
using ControleDespesas.Domain.Usuarios.Interfaces.Repositories;
using ControleDespesas.Infra.Commands;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Http;

namespace ControleDespesas.Domain.Usuarios.Handlers
{
    public class UsuarioHandler : Notificadora, IUsuarioHandler
    {
        private readonly IUsuarioRepository _repository;
        private readonly ITokenJwtHelper _tokenJwtHelper;

        public UsuarioHandler(IUsuarioRepository repository, ITokenJwtHelper tokenJwtHelper)
        {
            _repository = repository;
            _tokenJwtHelper = tokenJwtHelper;
        }

        public ICommandResult<Notificacao> Handler(AdicionarUsuarioCommand command)
        {
            if (command == null)
                return new CommandResult(StatusCodes.Status400BadRequest, "Parâmentros inválidos", "Parâmetros de entrada", "Parâmetros de entrada estão nulos");

            if (!command.ValidarCommand())
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Parâmentros inválidos", command.Notificacoes);

            var usuario = UsuarioHelper.GerarEntidade(command);

            if (usuario.Invalido)
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", usuario.Notificacoes);

            if (_repository.CheckLogin(usuario.Login))
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", "Login", "Esse login não está disponível pois já está sendo usado por outro usuário");

            var id = _repository.Salvar(usuario);
            usuario.DefinirId(id);

            var dadosRetorno = UsuarioHelper.GerarDadosRetorno(usuario);

            return new CommandResult(StatusCodes.Status201Created, "Usuário gravado com sucesso!", dadosRetorno);
        }

        public ICommandResult<Notificacao> Handler(long id, AtualizarUsuarioCommand command)
        {
            if (command == null)
                return new CommandResult(StatusCodes.Status400BadRequest, "Parâmentros inválidos", "Parâmetros de entrada", "Parâmetros de entrada estão nulos");

            command.Id = id;

            if (!command.ValidarCommand())
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Parâmentros inválidos", command.Notificacoes);

            var usuario = UsuarioHelper.GerarEntidade(command);

            if (usuario.Invalido)
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", usuario.Notificacoes);

            if (!_repository.CheckId(usuario.Id))
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", "Id", "Id inválido. Este id não está cadastrado!");

            if (_repository.CheckLogin(usuario.Login))
            {
                var userDoIdEnviadoBaseDados = _repository.Obter(usuario.Id);

                if (userDoIdEnviadoBaseDados.Login != usuario.Login)
                    return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", "Login", "Esse login não está disponível pois já está sendo usado por outro usuário");
            }

            _repository.Atualizar(usuario);

            var dadosRetorno = UsuarioHelper.GerarDadosRetorno(usuario);

            return new CommandResult(StatusCodes.Status200OK, "Usuário atualizado com sucesso!", dadosRetorno);
        }

        public ICommandResult<Notificacao> Handler(long id)
        {
            if (!_repository.CheckId(id))
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", "Id", "Id inválido. Este id não está cadastrado!");

            _repository.Deletar(id);

            var dadosRetorno = UsuarioHelper.GerarDadosRetornoDelete(id);

            return new CommandResult(StatusCodes.Status200OK, "Usuário excluído com sucesso!", dadosRetorno);
        }

        public ICommandResult<Notificacao> Handler(LoginUsuarioCommand command)
        {
            if (command == null)
                return new CommandResult(StatusCodes.Status400BadRequest, "Parâmentros inválidos", "Parâmetros de entrada", "Parâmetros de entrada estão nulos");

            if (!command.ValidarCommand())
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Parâmentros inválidos", command.Notificacoes);

            if (!_repository.CheckLogin(command.Login))
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", "Login", "Login incorreto! Esse login de usuário não existe");

            var usuario = _repository.Logar(command.Login, command.Senha);

            if (usuario != null)
            {
                var token = _tokenJwtHelper.GenerarTokenJwt(usuario);
                var usuarioComToken = new TokenCommandOutput(token);
                return new CommandResult(StatusCodes.Status200OK, "Usuário logado com sucesso!", usuarioComToken);
            }
            else
            {
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", "Senha", "Senha incorreta!");
            }
        }
    }
}