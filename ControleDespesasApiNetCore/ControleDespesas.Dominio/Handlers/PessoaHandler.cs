using ControleDespesas.Dominio.Commands.Pessoa.Input;
using ControleDespesas.Dominio.Commands.Pessoa.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;

namespace ControleDespesas.Dominio.Handlers
{
    public class PessoaHandler : Notificadora, ICommandHandler<AdicionarPessoaCommand>,
                                               ICommandHandler<AtualizarPessoaCommand>,
                                               ICommandHandler<ApagarPessoaCommand>
    {
        private readonly IPessoaRepositorio _repository;

        public PessoaHandler(IPessoaRepositorio repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(AdicionarPessoaCommand command)
        {
            if (!command.ValidarCommand())
                return new AdicionarPessoaCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

            Descricao100Caracteres nome = new Descricao100Caracteres(command.Nome, "Nome");
            string imagemPerfil = command.ImagemPerfil;

            Pessoa pessoa = new Pessoa(0, nome, imagemPerfil);

            AddNotificacao(nome.Notificacoes);

            if (Invalido)
                return new AdicionarPessoaCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            //Persiste os dados
            string retorno = _repository.Salvar(pessoa);

            //Notifica
            if (retorno == "Sucesso")
            {
                Pessoa pessoaSalvo = new Pessoa(_repository.LocalizarMaxId(), pessoa.Nome, pessoa.ImagemPerfil);

                // Retornar o resultado para tela
                return new AdicionarPessoaCommandResult(true, "Pessoa gravada com sucesso!", new
                {
                    Id = pessoaSalvo.Id,
                    Nome = pessoaSalvo.Nome.ToString(),
                    ImagemPerfil = pessoaSalvo.ImagemPerfil
                });
            }
            else
            {
                // Retornar o resultado para tela
                return new AdicionarPessoaCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
            }
        }

        public ICommandResult Handle(AtualizarPessoaCommand command)
        {
            if (!command.ValidarCommand())
                return new AtualizarPessoaCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

            int id = command.Id;
            Descricao100Caracteres nome = new Descricao100Caracteres(command.Nome, "Nome");
            string imagemPerfil = command.ImagemPerfil;

            Pessoa pessoa = new Pessoa(id, nome, imagemPerfil);

            AddNotificacao(nome.Notificacoes);

            //Validando dependências
            if (pessoa.Id == 0)
            {
                AddNotificacao("Id", "Id não está vinculado à operação solicitada");
            }

            if (!_repository.CheckId(pessoa.Id))
                AddNotificacao("Id", "Este Id não está cadastrado! Impossível prosseguir com este Id.");

            if (Invalido)
                return new AtualizarPessoaCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            //Persiste os dados
            string retorno = _repository.Atualizar(pessoa);

            //Notifica
            if (retorno == "Sucesso")
            {
                // Retornar o resultado para tela
                return new AtualizarPessoaCommandResult(true, "Pessoa atualizada com sucesso!", new
                {
                    Id = pessoa.Id,
                    Nome = pessoa.Nome.ToString(),
                    ImagemPerfil = pessoa.ImagemPerfil
                });
            }
            else
            {
                // Retornar o resultado para tela
                return new AtualizarPessoaCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
            }
        }

        public ICommandResult Handle(ApagarPessoaCommand command)
        {
            if (!command.ValidarCommand())
                return new ApagarPessoaCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

            if (!_repository.CheckId(command.Id))
                AddNotificacao("Id", "Este Id não está cadastrado! Impossível prosseguir sem um Id válido.");

            if (Invalido)
                return new ApagarPessoaCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            //Persiste os dados
            string retorno = _repository.Deletar(command.Id);

            //Notifica
            if (retorno == "Sucesso")
            {
                // Retornar o resultado para tela
                return new ApagarPessoaCommandResult(true, "Pessoa excluída com sucesso!", new { Id = command.Id });
            }
            else
            {
                // Retornar o resultado para tela
                return new ApagarPessoaCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
            }
        }
    }
}