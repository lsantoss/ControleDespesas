using ControleDespesas.Dominio.Commands.Empresa.Input;
using ControleDespesas.Dominio.Commands.Empresa.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Repositorio;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;

namespace ControleDespesas.Dominio.Handlers
{
    public class EmpresaHandler : Notificadora, ICommandHandler<AdicionarEmpresaCommand>,
                                                ICommandHandler<AtualizarEmpresaCommand>,
                                                ICommandHandler<ApagarEmpresaCommand>
    {
        private readonly EmpresaRepositorio _repository;

        public EmpresaHandler(EmpresaRepositorio repository)
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

            //Persiste os dados
            string retorno = _repository.Salvar(empresa);

            //Notifica
            if (retorno == "Sucesso")
            {
                Empresa empresaSalvo = new Empresa(_repository.LocalizarMaxId(), empresa.Nome, empresa.Logo);

                // Retornar o resultado para tela
                return new AdicionarEmpresaCommandResult(true, "Empresa gravada com sucesso!", new
                {
                    Id = empresaSalvo.Id,
                    Nome = empresaSalvo.Nome.ToString(),
                    Logo = empresaSalvo.Logo
                });
            }
            else
            {
                // Retornar o resultado para tela
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

            //Validando dependências
            if (empresa.Id == 0)
            {
                AddNotificacao("Id", "Id não está vinculado à operação solicitada");
            }

            if (!_repository.CheckId(empresa.Id))
                AddNotificacao("Id", "Este Id não está cadastrado! Impossível prosseguir com este Id.");

            if (Invalido)
                return new AtualizarEmpresaCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            //Persiste os dados
            string retorno = _repository.Atualizar(empresa);

            //Notifica
            if (retorno == "Sucesso")
            {
                // Retornar o resultado para tela
                return new AtualizarEmpresaCommandResult(true, "Empresa atualizada com sucesso!", new
                {
                    Id = empresa.Id,
                    Nome = empresa.Nome.ToString(),
                    Logo = empresa.Logo
                });
            }
            else
            {
                // Retornar o resultado para tela
                return new AtualizarEmpresaCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
            }
        }

        public ICommandResult Handle(ApagarEmpresaCommand command)
        {
            if (!command.ValidarCommand())
                return new ApagarEmpresaCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

            if (!_repository.CheckId(command.Id))
                AddNotificacao("Id", "Este Id não está cadastrado! Impossível prosseguir sem um Id válido.");

            if (Invalido)
                return new ApagarEmpresaCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            //Persiste os dados
            string retorno = _repository.Deletar(command.Id);

            //Notifica
            if (retorno == "Sucesso")
            {
                // Retornar o resultado para tela
                return new ApagarEmpresaCommandResult(true, "Empresa excluída com sucesso!", new { Id = command.Id });
            }
            else
            {
                // Retornar o resultado para tela
                return new ApagarEmpresaCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
            }
        }
    }
}