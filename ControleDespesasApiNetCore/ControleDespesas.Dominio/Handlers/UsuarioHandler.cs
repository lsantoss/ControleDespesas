using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Helpers;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query.Usuario;
using LSCode.Facilitador.Api.Command;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Dominio.Handlers
{
    public class UsuarioHandler : Notificadora, ICommandHandler<AdicionarUsuarioCommand>,
                                                ICommandHandler<AtualizarUsuarioCommand>,
                                                ICommandHandler<ApagarUsuarioCommand>,
                                                ICommandHandler<LoginUsuarioCommand>
    {
        private readonly IUsuarioRepositorio _repository;

        public UsuarioHandler(IUsuarioRepositorio repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(AdicionarUsuarioCommand command)
        {
            try
            {
                Usuario usuario = UsuarioHelper.GerarEntidade(command);

                AddNotificacao(usuario.Login.Notificacoes);
                AddNotificacao(usuario.Senha.Notificacoes);

                if (_repository.CheckLogin(usuario.Login.ToString()))
                    AddNotificacao("Login", "Esse login não está disponível pois já está sendo usado por outro usuário");

                if (Invalido)
                    return new CommandResult(false, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Salvar(usuario);

                usuario.Id = _repository.LocalizarMaxId();

                object dadosRetorno = UsuarioHelper.GerarDadosRetornoCommandResult(usuario);

                return new CommandResult(true, "Usuário gravado com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ICommandResult Handle(AtualizarUsuarioCommand command)
        {
            try
            {
                Usuario usuario = UsuarioHelper.GerarEntidade(command);

                AddNotificacao(usuario.Login.Notificacoes);
                AddNotificacao(usuario.Senha.Notificacoes);

                if (usuario.Id == 0)
                    AddNotificacao("Id", "Id não está vinculado à operação solicitada");

                if (!_repository.CheckId(usuario.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (_repository.CheckLogin(usuario.Login.ToString()))
                    AddNotificacao("Login", "Esse login não está disponível pois já está sendo usado por outro usuário");

                if (Invalido)
                    return new CommandResult(false, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Atualizar(usuario);

                object dadosRetorno = UsuarioHelper.GerarDadosRetornoCommandResult(usuario);

                return new CommandResult(true, "Usuário atualizado com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ICommandResult Handle(ApagarUsuarioCommand command)
        {
            try
            {
                if (!_repository.CheckId(command.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult(false, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Deletar(command.Id);

                return new CommandResult(true, "Usuário excluído com sucesso!", new { Id = command.Id });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ICommandResult Handle(LoginUsuarioCommand command)
        {
            try
            {
                string login = command.Login;
                string senha = command.Senha;

                if (!_repository.CheckLogin(login))
                    AddNotificacao("Login", "Login incorreto! Esse login de usuário não existe");

                if (Invalido)
                    return new CommandResult(false, "nconsistência(s) no(s) dado(s)", Notificacoes);

                UsuarioQueryResult usuario = _repository.Logar(login, senha);

                if (usuario != null)
                {
                    object dadosRetorno = UsuarioHelper.GerarDadosRetornoCommandResult(usuario);
                    return new CommandResult(true, "Usuário logado com sucesso!", dadosRetorno);
                }
                else
                {
                    AddNotificacao("Senha", "Senha incorreta!");
                    return new CommandResult(false, "Inconsistência(s) no(s) dado(s)", Notificacoes);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}