﻿using ControleDespesas.Dominio.Commands.TipoPagamento.Input;
using ControleDespesas.Dominio.Commands.TipoPagamento.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Helpers;
using ControleDespesas.Dominio.Repositorio;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Facilitador.Api.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Dominio.Handlers
{
    public class TipoPagamentoHandler : Notificadora, ICommandHandler<AdicionarTipoPagamentoCommand, Notificacao>,
                                                      ICommandHandler<AtualizarTipoPagamentoCommand, Notificacao>,
                                                      ICommandHandler<ApagarTipoPagamentoCommand, Notificacao>
    {
        private readonly ITipoPagamentoRepositorio _repository;

        public TipoPagamentoHandler(ITipoPagamentoRepositorio repository)
        {
            _repository = repository;
        }

        public ICommandResult<Notificacao> Handler(AdicionarTipoPagamentoCommand command)
        {
            try
            {
                TipoPagamento tipoPagamento = TipoPagamentoHelper.GerarEntidade(command);

                AddNotificacao(tipoPagamento.Descricao.Notificacoes);

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Salvar(tipoPagamento);

                tipoPagamento.Id = _repository.LocalizarMaxId();

                AdicionarTipoPagamentoCommandOutput dadosRetorno = TipoPagamentoHelper.GerarDadosRetornoInserir(tipoPagamento);

                return new CommandResult<Notificacao>("Tipo Pagamento gravado com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ICommandResult<Notificacao> Handler(AtualizarTipoPagamentoCommand command)
        {
            try
            {
                TipoPagamento tipoPagamento = TipoPagamentoHelper.GerarEntidade(command);

                AddNotificacao(tipoPagamento.Descricao.Notificacoes);

                if (!_repository.CheckId(tipoPagamento.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Atualizar(tipoPagamento);

                AtualizarTipoPagamentoCommandOutput dadosRetorno = TipoPagamentoHelper.GerarDadosRetornoUpdate(tipoPagamento);

                return new CommandResult<Notificacao>("Tipo Pagamento gravado com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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
                throw new Exception(e.Message);
            }
        }
    }
}