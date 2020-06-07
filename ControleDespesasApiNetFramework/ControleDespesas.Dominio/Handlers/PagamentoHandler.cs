using ControleDespesas.Dominio.Commands.Pagamento.Input;
using ControleDespesas.Dominio.Commands.Pagamento.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Repositorio;
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
        private readonly PagamentoRepositorio _repository;

        public PagamentoHandler(PagamentoRepositorio repository)
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

            //Persiste os dados
            string retorno = _repository.Salvar(pagamento);

            //Notifica
            if (retorno == "Sucesso")
            {
                Pagamento pagamentoSalvo = new Pagamento(_repository.LocalizarMaxId(), pagamento.TipoPagamento, pagamento.Empresa, pagamento.Pessoa,
                                           pagamento.Descricao, pagamento.Valor, pagamento.DataPagamento, pagamento.DataVencimento);

                // Retornar o resultado para tela
                return new AdicionarPagamentoCommandResult(true, "Pagamento gravado com sucesso!", new
                {
                    Id = pagamentoSalvo.Id,
                    IdTipoPagamento = pagamentoSalvo.TipoPagamento.Id,
                    IdEmpresa = pagamentoSalvo.Empresa.Id,
                    IdPessoa = pagamentoSalvo.Pessoa.Id,
                    Descricao = pagamentoSalvo.Descricao.ToString(),
                    Valor = pagamentoSalvo.Valor,
                    DataPagamento = pagamentoSalvo.DataPagamento,
                    DataVencimento = pagamentoSalvo.DataVencimento
                });
            }
            else
            {
                // Retornar o resultado para tela
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

            //Validando dependências
            if (pagamento.Id == 0)
            {
                AddNotificacao("Id", "Id não está vinculado à operação solicitada");
            }

            if (!_repository.CheckId(pagamento.Id))
                AddNotificacao("Id", "Este Id não está cadastrado! Impossível prosseguir com este Id.");

            if (Invalido)
                return new AtualizarPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            //Persiste os dados
            string retorno = _repository.Atualizar(pagamento);

            //Notifica
            if (retorno == "Sucesso")
            {
                // Retornar o resultado para tela
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
                // Retornar o resultado para tela
                return new AtualizarPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
            }
        }

        public ICommandResult Handle(ApagarPagamentoCommand command)
        {
            if (!command.ValidarCommand())
                return new ApagarPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

            if (!_repository.CheckId(command.Id))
                AddNotificacao("Id", "Este Id não está cadastrado! Impossível prosseguir sem um Id válido.");

            if (Invalido)
                return new ApagarPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            //Persiste os dados
            string retorno = _repository.Deletar(command.Id);

            //Notifica
            if (retorno == "Sucesso")
            {
                // Retornar o resultado para tela
                return new ApagarPagamentoCommandResult(true, "Pagamento excluído com sucesso!", new { Id = command.Id });
            }
            else
            {
                // Retornar o resultado para tela
                return new ApagarPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
            }
        }
    }
}