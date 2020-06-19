using ControleDespesas.Dominio.Commands.Pessoa.Input;
using ControleDespesas.Dominio.Commands.Pessoa.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using LSCode.Facilitador.Api.Exceptions;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;
using System;

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
            try
            {
                if (!command.ValidarCommand())
                    return new AdicionarPessoaCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

                Descricao100Caracteres nome = new Descricao100Caracteres(command.Nome, "Nome");
                string imagemPerfil = command.ImagemPerfil;

                Pessoa pessoa = new Pessoa(0, nome, imagemPerfil);

                AddNotificacao(nome.Notificacoes);

                if (Invalido)
                    return new AdicionarPessoaCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

                string retorno = _repository.Salvar(pessoa);

                if (retorno == "Sucesso")
                {
                    int id = _repository.LocalizarMaxId();

                    return new AdicionarPessoaCommandResult(true, "Pessoa gravada com sucesso!", new
                    {
                        Id = id,
                        Nome = pessoa.Nome.ToString(),
                        ImagemPerfil = pessoa.ImagemPerfil
                    });
                }
                else
                {
                    return new AdicionarPessoaCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
                }
            }
            catch (RepositoryException e)
            {
                throw new RepositoryException(e.Message);
            }
            catch (Exception e)
            {
                throw new HandlerException("HandlerException: " + e.Message);
            }
        }

        public ICommandResult Handle(AtualizarPessoaCommand command)
        {
            try
            {
                if (!command.ValidarCommand())
                    return new AtualizarPessoaCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

                int id = command.Id;
                Descricao100Caracteres nome = new Descricao100Caracteres(command.Nome, "Nome");
                string imagemPerfil = command.ImagemPerfil;

                Pessoa pessoa = new Pessoa(id, nome, imagemPerfil);

                AddNotificacao(nome.Notificacoes);

                if (pessoa.Id == 0)
                    AddNotificacao("Id", "Id não está vinculado à operação solicitada");

                if (!_repository.CheckId(pessoa.Id))
                    AddNotificacao("Id", "Este id não está cadastrado! Impossível prosseguir com este id.");

                if (Invalido)
                    return new AtualizarPessoaCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

                string retorno = _repository.Atualizar(pessoa);

                if (retorno == "Sucesso")
                {
                    return new AtualizarPessoaCommandResult(true, "Pessoa atualizada com sucesso!", new
                    {
                        Id = pessoa.Id,
                        Nome = pessoa.Nome.ToString(),
                        ImagemPerfil = pessoa.ImagemPerfil
                    });
                }
                else
                {
                    return new AtualizarPessoaCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
                }
            }
            catch (RepositoryException e)
            {
                throw new RepositoryException(e.Message);
            }
            catch (Exception e)
            {
                throw new HandlerException("HandlerException: " + e.Message);
            }
        }

        public ICommandResult Handle(ApagarPessoaCommand command)
        {
            try
            {
                if (!command.ValidarCommand())
                    return new ApagarPessoaCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

                if (!_repository.CheckId(command.Id))
                    AddNotificacao("Id", "Este id não está cadastrado! Impossível prosseguir sem um id válido.");

                if (Invalido)
                    return new ApagarPessoaCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

                string retorno = _repository.Deletar(command.Id);

                return retorno == "Sucesso"
                    ? new ApagarPessoaCommandResult(true, "Pessoa excluída com sucesso!", new { Id = command.Id })
                    : new ApagarPessoaCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
            }
            catch (RepositoryException e)
            {
                throw new RepositoryException(e.Message);
            }
            catch (Exception e)
            {
                throw new HandlerException("HandlerException: " + e.Message);
            }
        }
    }
}