using ControleDespesas.Dominio.Command.TipoPagamento.Input;
using ControleDespesas.Dominio.Command.TipoPagamento.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
using LSCode.Validador.ValueObjects;

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
            if (!command.ValidarCommand())
                return new AdicionarTipoPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

            Descricao250Caracteres descricao = new Descricao250Caracteres(command.Descricao, "Descrição");

            TipoPagamento tipoPagamento = new TipoPagamento(0, descricao);

            AddNotificacao(descricao.Notificacoes);

            if (Invalido)
                return new AdicionarTipoPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            //Persiste os dados
            string retorno = _repository.Salvar(tipoPagamento);

            //Notifica
            if (retorno == "Sucesso")
            {
                TipoPagamento tipoPagamentoSalvo = new TipoPagamento(_repository.LocalizarMaxId(), tipoPagamento.Descricao);

                // Retornar o resultado para tela
                return new AdicionarTipoPagamentoCommandResult(true, "Tipo Pagamento gravado com sucesso!", new
                {
                    Id = tipoPagamentoSalvo.Id,
                    Descricao = tipoPagamentoSalvo.Descricao.ToString()
                });
            }
            else
            {
                // Retornar o resultado para tela
                return new AdicionarTipoPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
            }
        }

        public ICommandResult Handle(AtualizarTipoPagamentoCommand command)
        {
            if (!command.ValidarCommand())
                return new AtualizarTipoPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

            int id = command.Id;
            Descricao250Caracteres descricao = new Descricao250Caracteres(command.Descricao, "Descrição");

            TipoPagamento tipoPagamento = new TipoPagamento(id, descricao);

            AddNotificacao(descricao.Notificacoes);

            //Validando dependências
            if (tipoPagamento.Id == 0)
            {
                AddNotificacao("Id", "Id não está vinculado à operação solicitada");
            }

            if (!_repository.CheckId(tipoPagamento.Id))
                AddNotificacao("Id", "Este Id não está cadastrado! Impossível prosseguir com este Id.");

            if (Invalido)
                return new AtualizarTipoPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            //Persiste os dados
            string retorno = _repository.Atualizar(tipoPagamento);

            //Notifica
            if (retorno == "Sucesso")
            {
                // Retornar o resultado para tela
                return new AtualizarTipoPagamentoCommandResult(true, "Tipo Pagamento atualizado com sucesso!", new
                {
                    Id = tipoPagamento.Id,
                    Descricao = tipoPagamento.Descricao.ToString()
                });
            }
            else
            {
                // Retornar o resultado para tela
                return new AtualizarTipoPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
            }
        }

        public ICommandResult Handle(ApagarTipoPagamentoCommand command)
        {
            if (!command.ValidarCommand())
                return new ApagarTipoPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", command.Notificacoes);

            if (!_repository.CheckId(command.Id))
                AddNotificacao("Id", "Este Id não está cadastrado! Impossível prosseguir sem um Id válido.");

            if (Invalido)
                return new ApagarTipoPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", Notificacoes);

            //Persiste os dados
            string retorno = _repository.Deletar(command.Id);

            //Notifica
            if (retorno == "Sucesso")
            {
                // Retornar o resultado para tela
                return new ApagarTipoPagamentoCommandResult(true, "Tipo Pagamento excluído com sucesso!", new { Id = command.Id });
            }
            else
            {
                // Retornar o resultado para tela
                return new ApagarTipoPagamentoCommandResult(false, "Por favor, corrija as inconsistências abaixo", retorno);
            }
        }
    }
}