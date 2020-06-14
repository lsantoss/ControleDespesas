using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Dominio.Commands.Usuario.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Enums;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query.Usuario;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;

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
            if (!command.ValidarCommand())
                return new AdicionarUsuarioCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

            Descricao50Caracteres login = new Descricao50Caracteres(command.Login, "Login");
            Descricao50Caracteres senha = new Descricao50Caracteres(command.Senha, "Senha");
            EPrivilegioUsuario privilegio = command.Privilegio;

            Usuario usuario = new Usuario(0, login, senha, privilegio);

            AddNotificacao(usuario.Login.Notificacoes);
            AddNotificacao(usuario.Senha.Notificacoes);

            if (_repository.CheckLogin(usuario.Login.ToString()))
                AddNotificacao("Login", "Esse login não está disponível pois já está sendo usado por outro usuário");

            if (Invalido)
                return new AdicionarUsuarioCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            string retorno = _repository.Salvar(usuario);

            if (retorno == "Sucesso")
            {
                int id = _repository.LocalizarMaxId();

                return new AdicionarUsuarioCommandResult(true, "Usuário gravado com sucesso!", new
                {
                    Id = id,
                    Login = usuario.Login.ToString(),
                    Senha = usuario.Senha.ToString(),
                    Privilegio = usuario.Privilegio
                });
            }
            else
            {
                return new AdicionarUsuarioCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
            }
        }

        public ICommandResult Handle(AtualizarUsuarioCommand command)
        {
            if (!command.ValidarCommand())
                return new AtualizarUsuarioCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

            int id = command.Id;
            Descricao50Caracteres login = new Descricao50Caracteres(command.Login, "Login");
            Descricao50Caracteres senha = new Descricao50Caracteres(command.Senha, "Senha");
            EPrivilegioUsuario privilegio = command.Privilegio;

            Usuario usuario = new Usuario(id, login, senha, privilegio);

            AddNotificacao(usuario.Login.Notificacoes);
            AddNotificacao(usuario.Senha.Notificacoes);

            if (usuario.Id == 0)
                AddNotificacao("Id", "Id não está vinculado à operação solicitada");            

            if (!_repository.CheckId(usuario.Id))
                AddNotificacao("Id", "Este id não está cadastrado! Impossível prosseguir com este id.");

            if (_repository.CheckLogin(usuario.Login.ToString()))
                AddNotificacao("Login", "Esse login não está disponível pois já está sendo usado por outro usuário");

            if (Invalido)
                return new AtualizarUsuarioCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            string retorno = _repository.Atualizar(usuario);

            if (retorno == "Sucesso")
            {
                return new AtualizarUsuarioCommandResult(true, "Usuário atualizado com sucesso!", new
                {
                    Id = usuario.Id,
                    Nome = usuario.Login.ToString(),
                    Senha = usuario.Senha.ToString(),
                    Privilegio = usuario.Privilegio
                });
            }
            else
            {
                return new AtualizarUsuarioCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
            }
        }

        public ICommandResult Handle(ApagarUsuarioCommand command)
        {
            if (!command.ValidarCommand())
                return new ApagarUsuarioCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

            if (!_repository.CheckId(command.Id))
                AddNotificacao("Id", "Este Id não está cadastrado! Impossível prosseguir sem um Id válido.");

            if (Invalido)
                return new ApagarUsuarioCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            string retorno = _repository.Deletar(command.Id);

            return retorno == "Sucesso"
                ? new ApagarUsuarioCommandResult(true, "Pessoa excluída com sucesso!", new { Id = command.Id })
                : new ApagarUsuarioCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
        }

        public ICommandResult Handle(LoginUsuarioCommand command)
        {
            if (!command.ValidarCommand())
                return new LoginUsuarioCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

            string login = command.Login;
            string senha = command.Senha;

            if (_repository.CheckLogin(login))
                AddNotificacao("Login", "Login incorreto! Esse login de usuário não existe");

            if (Invalido)
                return new LoginUsuarioCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            UsuarioQueryResult retorno = _repository.LogarUsuario(login, senha);

            if (retorno == null)
            {
                AddNotificacao("Senha", "Senha incorreta!");
                return new LoginUsuarioCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
            }
            else
            {
                return new LoginUsuarioCommandResult(true, "Usuário logado com sucesso!", new
                {
                    Id = retorno.Id,
                    Login = retorno.Login,
                    Senha = retorno.Senha,
                    Privilegio = retorno.Privilegio
                });
            }
        }
    }
}