using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Dominio.Commands.Usuario.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Helpers;
using ControleDespesas.Dominio.Query.Usuario;
using ControleDespesas.Dominio.Interfaces.Repositorio;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Dominio.Handlers
{
    public class UsuarioHandler : Notificadora, ICommandHandler<AdicionarUsuarioCommand, Notificacao>,
                                                ICommandHandler<AtualizarUsuarioCommand, Notificacao>,
                                                ICommandHandler<ApagarUsuarioCommand, Notificacao>,
                                                ICommandHandler<LoginUsuarioCommand, Notificacao>
    {
        private readonly IUsuarioRepositorio _repository;

        public UsuarioHandler(IUsuarioRepositorio repository)
        {
            _repository = repository;
        }

        public ICommandResult<Notificacao> Handler(AdicionarUsuarioCommand command)
        {
            try
            {
                Usuario usuario = UsuarioHelper.GerarEntidade(command);

                AddNotificacao(usuario.Notificacoes);

                if (_repository.CheckLogin(usuario.Login.ToString()))
                    AddNotificacao("Login", "Esse login não está disponível pois já está sendo usado por outro usuário");

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                usuario = _repository.Salvar(usuario);

                AdicionarUsuarioCommandOutput dadosRetorno = UsuarioHelper.GerarDadosRetornoInsert(usuario);

                return new CommandResult<Notificacao>("Usuário gravado com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ICommandResult<Notificacao> Handler(AtualizarUsuarioCommand command)
        {
            try
            {
                Usuario usuario = UsuarioHelper.GerarEntidade(command);

                AddNotificacao(usuario.Notificacoes);

                if (!_repository.CheckId(usuario.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (_repository.CheckLogin(usuario.Login.ToString()))
                {
                    UsuarioQueryResult userDoIdEnviadoBaseDados = _repository.Obter(usuario.Id);

                    if (userDoIdEnviadoBaseDados.Login != usuario.Login.ToString())
                        AddNotificacao("Login", "Esse login não está disponível pois já está sendo usado por outro usuário");
                }

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Atualizar(usuario);

                AtualizarUsuarioCommandOutput dadosRetorno = UsuarioHelper.GerarDadosRetornoUpdate(usuario);

                return new CommandResult<Notificacao>("Usuário atualizado com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ICommandResult<Notificacao> Handler(ApagarUsuarioCommand command)
        {
            try
            {
                if (!_repository.CheckId(command.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Deletar(command.Id);

                ApagarUsuarioCommandOutput dadosRetorno = UsuarioHelper.GerarDadosRetornoDelete(command.Id);

                return new CommandResult<Notificacao>("Usuário excluído com sucesso!", dadosRetorno);
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
                string login = command.Login;
                string senha = command.Senha;

                if (!_repository.CheckLogin(login))
                    AddNotificacao("Login", "Login incorreto! Esse login de usuário não existe");

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                UsuarioQueryResult usuario = _repository.Logar(login, senha);

                if (usuario != null)
                {
                    return new CommandResult<Notificacao>("Usuário logado com sucesso!", usuario);
                }
                else
                {
                    AddNotificacao("Senha", "Senha incorreta!");
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}