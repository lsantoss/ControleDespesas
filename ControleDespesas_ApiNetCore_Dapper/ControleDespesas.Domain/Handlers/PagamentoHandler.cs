using ControleDespesas.Domain.Commands.Pagamento.Input;
using ControleDespesas.Domain.Commands.Pagamento.Output;
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
    public class PagamentoHandler : Notificadora, IPagamentoHandler
    {
        private readonly IPagamentoRepository _repository;
        private readonly IEmpresaRepository _repositoryEmpresa;
        private readonly IPessoaRepository _repositoryPessoa;
        private readonly ITipoPagamentoRepository _repositoryTipoPagamento;

        public PagamentoHandler(IPagamentoRepository repository, 
                                IEmpresaRepository repositoryEmpresa,
                                IPessoaRepository repositoryPessoa,
                                ITipoPagamentoRepository repositoryTipoPagamento)
        {
            _repository = repository;
            _repositoryEmpresa = repositoryEmpresa;
            _repositoryPessoa = repositoryPessoa;
            _repositoryTipoPagamento = repositoryTipoPagamento;
        }

        public ICommandResult<Notificacao> Handler(AdicionarPagamentoCommand command)
        {
            try
            {
                Pagamento pagamento = PagamentoHelper.GerarEntidade(command);

                AddNotificacao(pagamento.Notificacoes);

                if(!_repositoryEmpresa.CheckId(pagamento.Empresa.Id))
                    AddNotificacao("IdEmpresa", "Id inválido. Este id não está cadastrado!");

                if (!_repositoryPessoa.CheckId(pagamento.Pessoa.Id))
                    AddNotificacao("IdPessoa", "Id inválido. Este id não está cadastrado!");

                if (!_repositoryTipoPagamento.CheckId(pagamento.TipoPagamento.Id))
                    AddNotificacao("IdTipoPagamento", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                pagamento = _repository.Salvar(pagamento);

                AdicionarPagamentoCommandOutput dadosRetorno = PagamentoHelper.GerarDadosRetornoInsert(pagamento);

                return new CommandResult<Notificacao>("Pagamento gravado com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ICommandResult<Notificacao> Handler(AtualizarPagamentoCommand command)
        {
            try
            {
                Pagamento pagamento = PagamentoHelper.GerarEntidade(command);

                AddNotificacao(pagamento.Notificacoes);

                if (!_repository.CheckId(pagamento.Id))
                    AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

                if (!_repositoryEmpresa.CheckId(pagamento.Empresa.Id))
                    AddNotificacao("IdEmpresa", "Id inválido. Este id não está cadastrado!");

                if (!_repositoryPessoa.CheckId(pagamento.Pessoa.Id))
                    AddNotificacao("IdPessoa", "Id inválido. Este id não está cadastrado!");

                if (!_repositoryTipoPagamento.CheckId(pagamento.TipoPagamento.Id))
                    AddNotificacao("IdTipoPagamento", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                _repository.Atualizar(pagamento);

                AtualizarPagamentoCommandOutput dadosRetorno = PagamentoHelper.GerarDadosRetornoUpdate(pagamento);

                return new CommandResult<Notificacao>("Pagamento atualizado com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
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

                ApagarPagamentoCommandOutput dadosRetorno = PagamentoHelper.GerarDadosRetornoDelete(command.Id);

                return new CommandResult<Notificacao>("Pagamento excluído com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}