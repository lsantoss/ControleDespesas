using ControleDespesas.Dominio.Commands.TipoPagamento.Input;
using ControleDespesas.Dominio.Commands.TipoPagamento.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using LSCode.Facilitador.Api.Exceptions;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;
using System;

namespace ControleDespesas.Dominio.Handlers
{
    public class TipoPagamentoHandler : Notificadora, ICommandHandler<AdicionarTipoPagamentoCommand>,
                                                      ICommandHandler<AtualizarTipoPagamentoCommand>,
                                                      ICommandHandler<ApagarTipoPagamentoCommand>
    {
        private readonly ITipoPagamentoRepositorio _repository;

        public TipoPagamentoHandler(ITipoPagamentoRepositorio repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(AdicionarTipoPagamentoCommand command)
        {
            try
            {
                if (!command.ValidarCommand())
                    return new AdicionarTipoPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

                Descricao250Caracteres descricao = new Descricao250Caracteres(command.Descricao, "Descrição");

                TipoPagamento tipoPagamento = new TipoPagamento(0, descricao);

                AddNotificacao(descricao.Notificacoes);

                if (Invalido)
                    return new AdicionarTipoPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

                string retorno = _repository.Salvar(tipoPagamento);

                if (retorno == "Sucesso")
                {
                    int id = _repository.LocalizarMaxId();

                    return new AdicionarTipoPagamentoCommandResult(true, "Tipo Pagamento gravado com sucesso!", new
                    {
                        Id = id,
                        Descricao = tipoPagamento.Descricao.ToString()
                    });
                }
                else
                {
                    return new AdicionarTipoPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
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

        public ICommandResult Handle(AtualizarTipoPagamentoCommand command)
        {
            try
            {
                if (!command.ValidarCommand())
                    return new AtualizarTipoPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

                int id = command.Id;
                Descricao250Caracteres descricao = new Descricao250Caracteres(command.Descricao, "Descrição");

                TipoPagamento tipoPagamento = new TipoPagamento(id, descricao);

                AddNotificacao(descricao.Notificacoes);

                if (tipoPagamento.Id == 0)
                    AddNotificacao("Id", "Id não está vinculado à operação solicitada");

                if (!_repository.CheckId(tipoPagamento.Id))
                    AddNotificacao("Id", "Este Id não está cadastrado! Impossível prosseguir com este Id.");

                if (Invalido)
                    return new AtualizarTipoPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

                string retorno = _repository.Atualizar(tipoPagamento);

                if (retorno == "Sucesso")
                {
                    return new AtualizarTipoPagamentoCommandResult(true, "Tipo Pagamento atualizado com sucesso!", new
                    {
                        Id = tipoPagamento.Id,
                        Descricao = tipoPagamento.Descricao.ToString()
                    });
                }
                else
                {
                    return new AtualizarTipoPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
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

        public ICommandResult Handle(ApagarTipoPagamentoCommand command)
        {
            try
            {
                if (!command.ValidarCommand())
                    return new ApagarTipoPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

                if (!_repository.CheckId(command.Id))
                    AddNotificacao("Id", "Este id não está cadastrado! Impossível prosseguir sem um id válido.");

                if (Invalido)
                    return new ApagarTipoPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

                string retorno = _repository.Deletar(command.Id);

                return retorno == "Sucesso"
                    ? new ApagarTipoPagamentoCommandResult(true, "Tipo Pagamento excluído com sucesso!", new { Id = command.Id })
                    : new ApagarTipoPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
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