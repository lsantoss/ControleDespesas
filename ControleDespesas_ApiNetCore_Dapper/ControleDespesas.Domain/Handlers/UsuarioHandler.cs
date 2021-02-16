using ControleDespesas.Domain.Commands.Usuario.Input;
using ControleDespesas.Domain.Commands.Usuario.Output;
using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Helpers;
using ControleDespesas.Domain.Interfaces.Handlers;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.Usuario.Results;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Domain.Handlers
{
    public class UsuarioHandler : Notificadora, IUsuarioHandler
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioHandler(IUsuarioRepository repository)
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
                    return new CommandResult<Notificacao>(422, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                usuario = _repository.Salvar(usuario);

                AdicionarUsuarioCommandOutput dadosRetorno = UsuarioHelper.GerarDadosRetornoInsert(usuario);

                return new CommandResult<Notificacao>(201, "Usuário gravado com sucesso!", dadosRetorno);
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
                    return new CommandResult<Notificacao>(422, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Atualizar(usuario);

                AtualizarUsuarioCommandOutput dadosRetorno = UsuarioHelper.GerarDadosRetornoUpdate(usuario);

                return new CommandResult<Notificacao>(200, "Usuário atualizado com sucesso!", dadosRetorno);
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
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult<Notificacao>(422, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Deletar(id);

                ApagarUsuarioCommandOutput dadosRetorno = UsuarioHelper.GerarDadosRetornoDelete(id);

                return new CommandResult<Notificacao>(200, "Usuário excluído com sucesso!", dadosRetorno);
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
                    return new CommandResult<Notificacao>(422, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                UsuarioQueryResult usuario = _repository.Logar(login, senha);

                if (usuario != null)
                {
                    return new CommandResult<Notificacao>(200, "Usuário logado com sucesso!", usuario);
                }
                else
                {
                    AddNotificacao("Senha", "Senha incorreta!");
                    return new CommandResult<Notificacao>(422, "Inconsistência(s) no(s) dado(s)", Notificacoes);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}