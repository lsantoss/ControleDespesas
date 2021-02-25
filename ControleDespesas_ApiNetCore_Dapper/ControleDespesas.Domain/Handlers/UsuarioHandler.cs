using ControleDespesas.Domain.Commands.Usuario.Input;
using ControleDespesas.Domain.Helpers;
using ControleDespesas.Domain.Interfaces.Handlers;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Interfaces.Services;
using ControleDespesas.Domain.Query.Usuario.Results;
using ControleDespesas.Infra.Commands;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Http;
using System;

namespace ControleDespesas.Domain.Handlers
{
    public class UsuarioHandler : Notificadora, IUsuarioHandler
    {
        private readonly IUsuarioRepository _repository;
        private readonly ITokenJWTService _tokenJWTService;

        public UsuarioHandler(IUsuarioRepository repository, ITokenJWTService tokenJWTService)
        {
            _repository = repository;
            _tokenJWTService = tokenJWTService;
        }

        public ICommandResult<Notificacao> Handler(AdicionarUsuarioCommand command)
        {
            try
            {
                if (command == null)
                    return new CommandResult(StatusCodes.Status400BadRequest,
                                             "Parâmentros inválidos",
                                             "Parâmetros de entrada",
                                             "Parâmetros de entrada estão nulos");

                if (!command.ValidarCommand())
                    return new CommandResult(StatusCodes.Status422UnprocessableEntity,
                                             "Parâmentros inválidos",
                                             command.Notificacoes);

                var usuario = UsuarioHelper.GerarEntidade(command);

                AddNotificacao(usuario.Notificacoes);

                if (Invalido)
                    return new CommandResult(StatusCodes.Status422UnprocessableEntity,
                                             "Inconsistência(s) no(s) dado(s)",
                                             Notificacoes);

                if (_repository.CheckLogin(usuario.Login))
                    return new CommandResult(StatusCodes.Status422UnprocessableEntity,
                                             "Inconsistência(s) no(s) dado(s)",
                                             "Login",
                                             "Esse login não está disponível pois já está sendo usado por outro usuário");

                usuario = _repository.Salvar(usuario);

                var dadosRetorno = UsuarioHelper.GerarDadosRetornoInsert(usuario);

                return new CommandResult(StatusCodes.Status201Created, 
                                         "Usuário gravado com sucesso!", 
                                         dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ICommandResult<Notificacao> Handler(int id, AtualizarUsuarioCommand command)
        {
            try
            {
                if (command == null)
                    return new CommandResult(StatusCodes.Status400BadRequest,
                                             "Parâmentros inválidos",
                                             "Parâmetros de entrada",
                                             "Parâmetros de entrada estão nulos");

                command.Id = id;

                if (!command.ValidarCommand())
                    return new CommandResult(StatusCodes.Status422UnprocessableEntity,
                                             "Parâmentros inválidos",
                                             command.Notificacoes);

                var usuario = UsuarioHelper.GerarEntidade(command);

                AddNotificacao(usuario.Notificacoes);

                if (Invalido)
                    return new CommandResult(StatusCodes.Status422UnprocessableEntity,
                                             "Inconsistência(s) no(s) dado(s)",
                                             Notificacoes);

                if (!_repository.CheckId(usuario.Id))
                    return new CommandResult(StatusCodes.Status422UnprocessableEntity,
                                             "Inconsistência(s) no(s) dado(s)",
                                             "Id",
                                             "Id inválido. Este id não está cadastrado!");

                if (_repository.CheckLogin(usuario.Login.ToString()))
                {
                    var userDoIdEnviadoBaseDados = _repository.Obter(usuario.Id);

                    if (userDoIdEnviadoBaseDados.Login != usuario.Login.ToString())
                        return new CommandResult(StatusCodes.Status422UnprocessableEntity,
                                                 "Inconsistência(s) no(s) dado(s)",
                                                 "Login",
                                                 "Esse login não está disponível pois já está sendo usado por outro usuário");
                }

                _repository.Atualizar(usuario);

                var dadosRetorno = UsuarioHelper.GerarDadosRetornoUpdate(usuario);

                return new CommandResult(StatusCodes.Status200OK, 
                                         "Usuário atualizado com sucesso!", 
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
                    return new CommandResult(StatusCodes.Status422UnprocessableEntity,
                                             "Inconsistência(s) no(s) dado(s)",
                                             "Id",
                                             "Id inválido. Este id não está cadastrado!");

                _repository.Deletar(id);

                var dadosRetorno = UsuarioHelper.GerarDadosRetornoDelete(id);

                return new CommandResult(StatusCodes.Status200OK, 
                                         "Usuário excluído com sucesso!", 
                                         dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ICommandResult<Notificacao> Handler(LoginUsuarioCommand command)
        {
            try
            {
                if (command == null)
                    return new CommandResult(StatusCodes.Status400BadRequest,
                                             "Parâmentros inválidos",
                                             "Parâmetros de entrada",
                                             "Parâmetros de entrada estão nulos");

                if (!command.ValidarCommand())
                    return new CommandResult(StatusCodes.Status422UnprocessableEntity,
                                             "Parâmentros inválidos",
                                             command.Notificacoes);

                string login = command.Login;
                string senha = command.Senha;

                if (!_repository.CheckLogin(login))
                    return new CommandResult(StatusCodes.Status422UnprocessableEntity,
                                             "Inconsistência(s) no(s) dado(s)",
                                             "Login",
                                             "Login incorreto! Esse login de usuário não existe");

                var usuario = _repository.Logar(login, senha);

                if (usuario != null)
                {
                    var token = _tokenJWTService.GenerarTokenJwt(usuario);
                    var usuarioComToken = new UsuarioTokenQueryResult() 
                    { 
                        Id = usuario.Id,
                        Login = usuario.Login,
                        Senha = usuario.Senha,
                        Privilegio = usuario.Privilegio,
                        Token = token
                    };

                    return new CommandResult(StatusCodes.Status200OK, "Usuário logado com sucesso!", usuarioComToken);
                }
                else
                {
                    return new CommandResult(StatusCodes.Status422UnprocessableEntity,
                                             "Inconsistência(s) no(s) dado(s)",
                                             "Senha",
                                             "Senha incorreta!");
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}