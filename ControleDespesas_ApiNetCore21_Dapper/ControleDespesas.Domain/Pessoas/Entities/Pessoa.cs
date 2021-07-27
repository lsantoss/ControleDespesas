using ControleDespesas.Domain.Pagamentos.Entities;
using LSCode.Validador.ValidacoesNotificacoes;
using System.Collections.Generic;
using System.Linq;

namespace ControleDespesas.Domain.Pessoas.Entities
{
    public class Pessoa : Notificadora
    {
        public long Id { get; private set; }
        public long IdUsuario { get; private set; }
        public string Nome { get; private set; }
        public string ImagemPerfil { get; private set; }
        public IReadOnlyCollection<Pagamento> Pagamentos => _pagamentos.ToList();

        private readonly IList<Pagamento> _pagamentos = new List<Pagamento>();

        public Pessoa(long id)
        {
            DefinirId(id);
        }

        public Pessoa(long idUsuario, string nome, string imagemPerfil)
        {
            DefinirIdUsuario(idUsuario);
            DefinirNome(nome);
            DefinirImagemPerfil(imagemPerfil);
        }

        public Pessoa(long id, long idUsuario, string nome, string imagemPerfil)
        {
            DefinirId(id);
            DefinirIdUsuario(idUsuario);
            DefinirNome(nome);
            DefinirImagemPerfil(imagemPerfil);
        }

        public void DefinirId(long id)
        {
            Id = id;

            if (Id <= 0)
                AddNotificacao("Id", "Id não é valido");
        }

        public void DefinirIdUsuario(long idUsuario)
        {
            IdUsuario = idUsuario;

            if (IdUsuario <= 0)
                AddNotificacao("Id do Usuário", "Id do Usuário não é valido");
        }

        public void DefinirNome(string nome)
        {
            Nome = nome;

            if (string.IsNullOrEmpty(Nome))
                AddNotificacao("Nome", "Nome é um campo obrigatório");
            else if (Nome.Length > 100)
                AddNotificacao("Nome", "Nome maior que o esperado");
        }

        public void DefinirImagemPerfil(string imagemPerfil)
        {
            ImagemPerfil = imagemPerfil;

            if (string.IsNullOrEmpty(ImagemPerfil))
                AddNotificacao("ImagemPerfil", "ImagemPerfil é um campo obrigatório");
        }

        public void AdicionarPagamento(Pagamento pagamento)
        {
            _pagamentos.Add(pagamento);
        }

        public void AdicionarPagamentos(List<Pagamento> pagamentos)
        {
            foreach (var pagamento in pagamentos)
                _pagamentos.Add(pagamento);
        }

        public void RemoverPagamentos()
        {
            _pagamentos.Clear();
        }
    }
}