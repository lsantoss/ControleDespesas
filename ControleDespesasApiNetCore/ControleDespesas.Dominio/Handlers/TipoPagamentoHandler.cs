using ControleDespesas.Dominio.Commands.TipoPagamento.Input;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Helpers;
using ControleDespesas.Dominio.Interfaces;
using LSCode.Facilitador.Api.Command;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
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
                TipoPagamento tipoPagamento = TipoPagamentoHelper.GerarEntidade(command);

                AddNotificacao(tipoPagamento.Descricao.Notificacoes);

                if (Invalido)
                    return new CommandResult(false, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Salvar(tipoPagamento);

                tipoPagamento.Id = _repository.LocalizarMaxId();

                object dadosRetorno = TipoPagamentoHelper.GerarDadosRetornoCommandResult(tipoPagamento);

                return new CommandResult(true, "Tipo Pagamento gravado com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ICommandResult Handle(AtualizarTipoPagamentoCommand command)
        {
            try
            {
                TipoPagamento tipoPagamento = TipoPagamentoHelper.GerarEntidade(command);

                AddNotificacao(tipoPagamento.Descricao.Notificacoes);

                if (!_repository.CheckId(tipoPagamento.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult(false, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Atualizar(tipoPagamento);

                object dadosRetorno = TipoPagamentoHelper.GerarDadosRetornoCommandResult(tipoPagamento);

                return new CommandResult(true, "Tipo Pagamento gravado com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ICommandResult Handle(ApagarTipoPagamentoCommand command)
        {
            try
            {
                if (!_repository.CheckId(command.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult(false, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Deletar(command.Id);

                return new CommandResult(true, "Tipo Pagamento excluído com sucesso!", new { Id = command.Id });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}