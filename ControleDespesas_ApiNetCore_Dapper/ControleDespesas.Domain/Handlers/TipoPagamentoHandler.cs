using ControleDespesas.Domain.Commands.TipoPagamento.Input;
using ControleDespesas.Domain.Commands.TipoPagamento.Output;
using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Helpers;
using ControleDespesas.Domain.Interfaces.Handlers;
using ControleDespesas.Domain.Interfaces.Repositories;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Domain.Handlers
{
    public class TipoPagamentoHandler : Notificadora, ITipoPagamentoHandler
    {
        private readonly ITipoPagamentoRepository _repository;

        public TipoPagamentoHandler(ITipoPagamentoRepository repository)
        {
            _repository = repository;
        }

        public ICommandResult<Notificacao> Handler(AdicionarTipoPagamentoCommand command)
        {
            try
            {
                TipoPagamento tipoPagamento = TipoPagamentoHelper.GerarEntidade(command);

                AddNotificacao(tipoPagamento.Notificacoes);

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                tipoPagamento = _repository.Salvar(tipoPagamento);

                AdicionarTipoPagamentoCommandOutput dadosRetorno = TipoPagamentoHelper.GerarDadosRetornoInsert(tipoPagamento);

                return new CommandResult<Notificacao>("Tipo Pagamento gravado com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ICommandResult<Notificacao> Handler(AtualizarTipoPagamentoCommand command)
        {
            try
            {
                TipoPagamento tipoPagamento = TipoPagamentoHelper.GerarEntidade(command);

                AddNotificacao(tipoPagamento.Notificacoes);

                if (!_repository.CheckId(tipoPagamento.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Atualizar(tipoPagamento);

                AtualizarTipoPagamentoCommandOutput dadosRetorno = TipoPagamentoHelper.GerarDadosRetornoUpdate(tipoPagamento);

                return new CommandResult<Notificacao>("Tipo Pagamento atualizado com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ICommandResult<Notificacao> Handler(ApagarTipoPagamentoCommand command)
        {
            try
            {
                if (!_repository.CheckId(command.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Deletar(command.Id);

                ApagarTipoPagamentoCommandOutput dadosRetorno = TipoPagamentoHelper.GerarDadosRetornoDelete(command.Id);

                return new CommandResult<Notificacao>("Tipo Pagamento excluído com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}