using ControleDespesas.Dominio.Commands.Pagamento.Input;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Helpers;
using ControleDespesas.Dominio.Interfaces;
using LSCode.Facilitador.Api.Command;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Dominio.Handlers
{
    public class PagamentoHandler : Notificadora, ICommandHandler<AdicionarPagamentoCommand>,
                                                  ICommandHandler<AtualizarPagamentoCommand>,
                                                  ICommandHandler<ApagarPagamentoCommand>
    {
        private readonly IPagamentoRepositorio _repository;

        public PagamentoHandler(IPagamentoRepositorio repository)
        {
            _repository = repository;
        }

        public ICommandResult Handle(AdicionarPagamentoCommand command)
        {
            try
            {
                Pagamento pagamento = PagamentoHelper.GerarEntidade(command);

                AddNotificacao(pagamento.Descricao.Notificacoes);

                if (Invalido)
                    return new CommandResult(false, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Salvar(pagamento);

                pagamento.Id = _repository.LocalizarMaxId();

                object dadosRetorno = PagamentoHelper.GerarDadosRetornoCommandResult(pagamento);

                return new CommandResult(true, "Pagamento gravado com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ICommandResult Handle(AtualizarPagamentoCommand command)
        {
            try
            {
                Pagamento pagamento = PagamentoHelper.GerarEntidade(command);

                AddNotificacao(pagamento.Descricao.Notificacoes);

                if (!_repository.CheckId(pagamento.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult(false, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Atualizar(pagamento);

                object dadosRetorno = PagamentoHelper.GerarDadosRetornoCommandResult(pagamento);

                return new CommandResult(true, "Pagamento atualizado com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public ICommandResult Handle(ApagarPagamentoCommand command)
        {
            try
            {
                if (!_repository.CheckId(command.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult(false, "Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Deletar(command.Id);

                return new CommandResult(true, "Pagamento excluído com sucesso!", new { Id = command.Id });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}