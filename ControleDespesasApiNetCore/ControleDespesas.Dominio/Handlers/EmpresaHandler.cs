using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Dominio.Commands.Empresa.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;

namespace ControleDespesas.Dominio.Handlers
{
    public class EmpresaHandler : Notificadora, ICommandHandler<AdicionarEmpresaCommand>,
                                                ICommandHandler<AtualizarEmpresaCommand>,
                                                ICommandHandler<ApagarEmpresaCommand>
    {
        private readonly IEmpresaRepositorio _repository;

        public EmpresaHandler(IEmpresaRepositorio repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(AdicionarEmpresaCommand command)
        {
            if (!command.ValidarCommand())
                return new AdicionarEmpresaCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

            Descricao100Caracteres nome = new Descricao100Caracteres(command.Nome, "Nome");
            string logo = command.Logo;

            Empresa empresa = new Empresa(0, nome, logo);

            AddNotificacao(nome.Notificacoes);

            if (Invalido)
                return new AdicionarEmpresaCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            string retorno = _repository.Salvar(empresa);

            if (retorno == "Sucesso")
            {
                int id = _repository.LocalizarMaxId();

                return new AdicionarEmpresaCommandResult(true, "Empresa gravada com sucesso!", new
                {
                    Id = id,
                    Nome = empresa.Nome.ToString(),
                    Logo = empresa.Logo
                });
            }
            else
            {
                return new AdicionarEmpresaCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
            }
        }

        public ICommandResult Handle(AtualizarEmpresaCommand command)
        {
            if (!command.ValidarCommand())
                return new AtualizarEmpresaCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

            int id = command.Id;
            Descricao100Caracteres nome = new Descricao100Caracteres(command.Nome, "Nome");
            string logo = command.Logo;

            Empresa empresa = new Empresa(id, nome, logo);

            AddNotificacao(nome.Notificacoes);

            if (empresa.Id == 0)
                AddNotificacao("Id", "Id não está vinculado à operação solicitada");

            if (!_repository.CheckId(empresa.Id))
                AddNotificacao("Id", "Este id não está cadastrado! Impossível prosseguir com este id.");

            if (Invalido)
                return new AtualizarEmpresaCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            string retorno = _repository.Atualizar(empresa);

            if (retorno == "Sucesso")
            {
                return new AtualizarEmpresaCommandResult(true, "Empresa atualizada com sucesso!", new
                {
                    Id = empresa.Id,
                    Nome = empresa.Nome.ToString(),
                    Logo = empresa.Logo
                });
            }
            else
            {
                return new AtualizarEmpresaCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
            }
        }

        public ICommandResult Handle(ApagarEmpresaCommand command)
        {
            if (!command.ValidarCommand())
                return new ApagarEmpresaCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

            if (!_repository.CheckId(command.Id))
                AddNotificacao("Id", "Este id não está cadastrado! Impossível prosseguir sem um id válido.");

            if (Invalido)
                return new ApagarEmpresaCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            string retorno = _repository.Deletar(command.Id);

            return retorno == "Sucesso"
                ? new ApagarEmpresaCommandResult(true, "Empresa excluída com sucesso!", new { Id = command.Id })
                : new ApagarEmpresaCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
        }
    }
}