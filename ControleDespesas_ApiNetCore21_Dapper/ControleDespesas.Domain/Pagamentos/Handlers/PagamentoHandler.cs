using ControleDespesas.Domain.Empresas.Interfaces.Repositories;
using ControleDespesas.Domain.Pagamentos.Commands.Input;
using ControleDespesas.Domain.Pagamentos.Helpers;
using ControleDespesas.Domain.Pagamentos.Interfaces.Handlers;
using ControleDespesas.Domain.Pagamentos.Interfaces.Repositories;
using ControleDespesas.Domain.Pessoas.Interfaces.Repositories;
using ControleDespesas.Domain.TiposPagamentos.Interfaces.Repositories;
using ControleDespesas.Infra.Commands;
using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using Microsoft.AspNetCore.Http;

namespace ControleDespesas.Domain.Pagamentos.Handlers
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
            if (command == null)
                return new CommandResult(StatusCodes.Status400BadRequest, "Parâmentros inválidos", "Parâmetros de entrada", "Parâmetros de entrada estão nulos");

            if (!command.ValidarCommand())
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Parâmentros inválidos", command.Notificacoes);

            var pagamento = PagamentoHelper.GerarEntidade(command);

            if (pagamento.Invalido)
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", pagamento.Notificacoes);

            if (!_repositoryEmpresa.CheckId(pagamento.Empresa.Id))
                AddNotificacao("IdEmpresa", "Id inválido. Este id não está cadastrado!");

            if (!_repositoryPessoa.CheckId(pagamento.Pessoa.Id))
                AddNotificacao("IdPessoa", "Id inválido. Este id não está cadastrado!");

            if (!_repositoryTipoPagamento.CheckId(pagamento.TipoPagamento.Id))
                AddNotificacao("IdTipoPagamento", "Id inválido. Este id não está cadastrado!");

            if (Invalido)
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", Notificacoes);

            var id = _repository.Salvar(pagamento);
            pagamento.DefinirId(id);
            var dadosRetorno = PagamentoHelper.GerarDadosRetornoInsert(pagamento);
            return new CommandResult(StatusCodes.Status201Created, "Pagamento gravado com sucesso!", dadosRetorno);
        }

        public ICommandResult<Notificacao> Handler(int id, AtualizarPagamentoCommand command)
        {
            if (command == null)
                return new CommandResult(StatusCodes.Status400BadRequest, "Parâmentros inválidos", "Parâmetros de entrada", "Parâmetros de entrada estão nulos");

            command.Id = id;

            if (!command.ValidarCommand())
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Parâmentros inválidos", command.Notificacoes);

            var pagamento = PagamentoHelper.GerarEntidade(command);

            if (pagamento.Invalido)
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", pagamento.Notificacoes);

            if (!_repository.CheckId(pagamento.Id))
                AddNotificacao("Id", "Id inválido. Este id não está cadastrado!");

            if (!_repositoryEmpresa.CheckId(pagamento.Empresa.Id))
                AddNotificacao("IdEmpresa", "Id inválido. Este id não está cadastrado!");

            if (!_repositoryPessoa.CheckId(pagamento.Pessoa.Id))
                AddNotificacao("IdPessoa", "Id inválido. Este id não está cadastrado!");

            if (!_repositoryTipoPagamento.CheckId(pagamento.TipoPagamento.Id))
                AddNotificacao("IdTipoPagamento", "Id inválido. Este id não está cadastrado!");

            if (Invalido)
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", Notificacoes);

            _repository.Atualizar(pagamento);
            var dadosRetorno = PagamentoHelper.GerarDadosRetornoUpdate(pagamento);
            return new CommandResult(StatusCodes.Status200OK, "Pagamento atualizado com sucesso!", dadosRetorno);
        }

        public ICommandResult<Notificacao> Handler(int id)
        {
            if (!_repository.CheckId(id))
                return new CommandResult(StatusCodes.Status422UnprocessableEntity, "Inconsistência(s) no(s) dado(s)", "Id", "Id inválido. Este id não está cadastrado!");

            _repository.Deletar(id);
            var dadosRetorno = PagamentoHelper.GerarDadosRetornoDelete(id);
            return new CommandResult(StatusCodes.Status200OK, "Pagamento excluído com sucesso!", dadosRetorno);
        }
    }
}