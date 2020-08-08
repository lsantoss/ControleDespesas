using ControleDespesas.Dominio.Commands.Pagamento.Input;
using ControleDespesas.Dominio.Commands.Pagamento.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Helpers;
using ControleDespesas.Dominio.Interfaces;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Facilitador.Api.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Dominio.Handlers
{
    public class PagamentoHandler : Notificadora, ICommandHandler<AdicionarPagamentoCommand, Notificacao>,
                                                  ICommandHandler<AtualizarPagamentoCommand, Notificacao>,
                                                  ICommandHandler<ApagarPagamentoCommand, Notificacao>
    {
        private readonly IPagamentoRepositorio _repository;

        public PagamentoHandler(IPagamentoRepositorio repository)
        {
            _repository = repository;
        }

        public ICommandResult<Notificacao> Handler(AdicionarPagamentoCommand command)
        {
            try
            {
                Pagamento pagamento = PagamentoHelper.GerarEntidade(command);

                AddNotificacao(pagamento.Descricao.Notificacoes);

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Salvar(pagamento);

                pagamento.Id = _repository.LocalizarMaxId();

                AdicionarPagamentoCommandOutput dadosRetorno = PagamentoHelper.GerarDadosRetornoInsert(pagamento);

                return new CommandResult<Notificacao>("Pagamento gravado com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ICommandResult<Notificacao> Handler(AtualizarPagamentoCommand command)
        {
            try
            {
                Pagamento pagamento = PagamentoHelper.GerarEntidade(command);

                AddNotificacao(pagamento.Descricao.Notificacoes);

                if (!_repository.CheckId(pagamento.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Atualizar(pagamento);

                AtualizarPagamentoCommandOutput dadosRetorno = PagamentoHelper.GerarDadosRetornoUpdate(pagamento);

                return new CommandResult<Notificacao>("Pagamento atualizado com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ICommandResult<Notificacao> Handler(ApagarPagamentoCommand command)
        {
            try
            {
                if (!_repository.CheckId(command.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Deletar(command.Id);

                ApagarPagamentoCommandoutput dadosRetorno = PagamentoHelper.GerarDadosRetornoDelete(command.Id);

                return new CommandResult<Notificacao>("Pagamento excluído com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}