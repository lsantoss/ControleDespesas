﻿using ControleDespesas.Dominio.Commands.Pagamento.Input;
using ControleDespesas.Dominio.Commands.Pagamento.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Helpers;
using ControleDespesas.Dominio.Repositorio;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Facilitador.Api.Models.Results;
using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Dominio.Handlers
{
    public class PagamentoHandler : Notificadora, ICommandHandler<AdicionarPagamentoCommand, Notificacao>,
                                                  ICommandHandler<AtualizarPagamentoCommand, Notificacao>,
                                                  ICommandHandler<ApagarPagamentoCommand, Notificacao>
    {
        private readonly IPagamentoRepositorio _repository;
        private readonly IEmpresaRepositorio _repositoryEmpresa;
        private readonly IPessoaRepositorio _repositoryPessoa;
        private readonly ITipoPagamentoRepositorio _repositoryTipoPagamento;

        public PagamentoHandler(IPagamentoRepositorio repository, 
                                IEmpresaRepositorio repositoryEmpresa,
                                IPessoaRepositorio repositoryPessoa,
                                ITipoPagamentoRepositorio repositoryTipoPagamento)
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

                AddNotificacao(pagamento.Descricao.Notificacoes);

                if(!_repositoryEmpresa.CheckId(pagamento.Empresa.Id))
                    AddNotificacao("Id Empresa", "Id inválido. Este id não está cadastrado!");

                if (!_repositoryPessoa.CheckId(pagamento.Pessoa.Id))
                    AddNotificacao("Id Pessoa", "Id inválido. Este id não está cadastrado!");

                if (!_repositoryTipoPagamento.CheckId(pagamento.TipoPagamento.Id))
                    AddNotificacao("Id Tipo Pagamento", "Id inválido. Este id não está cadastrado!");

                if (Invalido)
                    return new CommandResult<Notificacao>("Inconsistência(s) no(s) dado(s)", Notificacoes);

                pagamento = _repository.Salvar(pagamento);

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

                if (!_repositoryEmpresa.CheckId(pagamento.Empresa.Id))
                    AddNotificacao("Id Empresa", "Id inválido. Este id não está cadastrado!");

                if (!_repositoryPessoa.CheckId(pagamento.Pessoa.Id))
                    AddNotificacao("Id Pessoa", "Id inválido. Este id não está cadastrado!");

                if (!_repositoryTipoPagamento.CheckId(pagamento.TipoPagamento.Id))
                    AddNotificacao("Id Tipo Pagamento", "Id inválido. Este id não está cadastrado!");

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

                ApagarPagamentoCommandOutput dadosRetorno = PagamentoHelper.GerarDadosRetornoDelete(command.Id);

                return new CommandResult<Notificacao>("Pagamento excluído com sucesso!", dadosRetorno);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}