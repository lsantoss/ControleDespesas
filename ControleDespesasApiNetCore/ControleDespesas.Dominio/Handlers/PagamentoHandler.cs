using ControleDespesas.Dominio.Commands.Pagamento.Input;
using ControleDespesas.Dominio.Commands.Pagamento.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;
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
            if (!command.ValidarCommand())
                return new AdicionarPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

            TipoPagamento tipoPagamento = new TipoPagamento(command.IdTipoPagamento);
            Empresa empresa = new Empresa(command.IdEmpresa);
            Pessoa pessoa = new Pessoa(command.IdPessoa);
            Descricao250Caracteres descricao = new Descricao250Caracteres(command.Descricao, "Descrição");
            double valor = command.Valor;
            DateTime dataPagamento = command.DataPagamento;
            DateTime dataVencimento = command.DataVencimento;

            Pagamento pagamento = new Pagamento(0, tipoPagamento, empresa, pessoa, descricao, valor, dataPagamento, dataVencimento);

            AddNotificacao(descricao.Notificacoes);

            if (Invalido)
                return new AdicionarPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            string retorno = _repository.Salvar(pagamento);

            if (retorno == "Sucesso")
            {
                int id = _repository.LocalizarMaxId();

                return new AdicionarPagamentoCommandResult(true, "Pagamento gravado com sucesso!", new
                {
                    Id = id,
                    IdTipoPagamento = pagamento.TipoPagamento.Id,
                    IdEmpresa = pagamento.Empresa.Id,
                    IdPessoa = pagamento.Pessoa.Id,
                    Descricao = pagamento.Descricao.ToString(),
                    Valor = pagamento.Valor,
                    DataPagamento = pagamento.DataPagamento,
                    DataVencimento = pagamento.DataVencimento
                });
            }
            else
            {
                return new AdicionarPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
            }
        }

        public ICommandResult Handle(AtualizarPagamentoCommand command)
        {
            if (!command.ValidarCommand())
                return new AtualizarPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

            int id = command.Id;
            TipoPagamento tipoPagamento = new TipoPagamento(command.IdTipoPagamento);
            Empresa empresa = new Empresa(command.IdEmpresa);
            Pessoa pessoa = new Pessoa(command.IdPessoa);
            Descricao250Caracteres descricao = new Descricao250Caracteres(command.Descricao, "Descrição");
            double valor = command.Valor;
            DateTime dataPagamento = command.DataPagamento;
            DateTime dataVencimento = command.DataVencimento;

            Pagamento pagamento = new Pagamento(id, tipoPagamento, empresa, pessoa, descricao, valor, dataPagamento, dataVencimento);

            AddNotificacao(descricao.Notificacoes);

            if (pagamento.Id == 0)
                AddNotificacao("Id", "Id não está vinculado à operação solicitada");

            if (!_repository.CheckId(pagamento.Id))
                AddNotificacao("Id", "Este id não está cadastrado! Impossível prosseguir com este id.");

            if (Invalido)
                return new AtualizarPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            string retorno = _repository.Atualizar(pagamento);

            if (retorno == "Sucesso")
            {
                return new AtualizarPagamentoCommandResult(true, "Pagamento atualizado com sucesso!", new
                {
                    Id = pagamento.Id,
                    IdTipoPagamento = pagamento.TipoPagamento.Id,
                    IdEmpresa = pagamento.Empresa.Id,
                    IdPessoa = pagamento.Pessoa.Id,
                    Descricao = pagamento.Descricao.ToString(),
                    Valor = pagamento.Valor,
                    DataPagamento = pagamento.DataPagamento,
                    DataVencimento = pagamento.DataVencimento
                });
            }
            else
            {
                return new AtualizarPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
            }
        }

        public ICommandResult Handle(ApagarPagamentoCommand command)
        {
            if (!command.ValidarCommand())
                return new ApagarPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

            if (!_repository.CheckId(command.Id))
                AddNotificacao("Id", "Este id não está cadastrado! Impossível prosseguir sem um id válido.");

            if (Invalido)
                return new ApagarPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            string retorno = _repository.Deletar(command.Id);

            return retorno == "Sucesso"
                ? new ApagarPagamentoCommandResult(true, "Pagamento excluído com sucesso!", new { Id = command.Id })
                : new ApagarPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
        }
    }
}