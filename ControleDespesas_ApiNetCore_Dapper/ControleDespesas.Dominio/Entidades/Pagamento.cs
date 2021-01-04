using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Dominio.Entidades
{
    public class Pagamento : Notificadora
    {
        public int Id { get; set; }
        public TipoPagamento TipoPagamento { get; set; }
        public Empresa Empresa { get; set; }
        public Pessoa Pessoa { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public DateTime DataVencimento { get; set; }
        public DateTime? DataPagamento { get; set; }
        public string ArquivoPagamento { get; set; }
        public string ArquivoComprovante { get; set; }

        public Pagamento(int id, 
                         TipoPagamento tipoPagamento, 
                         Empresa empresa, 
                         Pessoa pessoa,
                         string descricao, 
                         double valor, 
                         DateTime dataVencimento, 
                         DateTime? dataPagamento,
                         string arquivoPagamento,
                         string arquivoComprovante)
        {
            Id = id;
            TipoPagamento = tipoPagamento;
            Empresa = empresa;
            Pessoa = pessoa;
            Descricao = descricao;
            Valor = valor;
            DataVencimento = dataVencimento;
            DataPagamento = dataPagamento;
            ArquivoPagamento = arquivoPagamento;
            ArquivoComprovante = arquivoComprovante;

            Validar();
        }

        public Pagamento(int id) => Id = id;

        public void Validar()
        {
            if (TipoPagamento.Id <= 0)
                AddNotificacao("Id Tipo Pagamento", "Id Tipo Pagamento não é valido");

            if (Empresa.Id <= 0)
                AddNotificacao("Id Empresa", "Id Empresa não é valido");

            if (Pessoa.Id <= 0)
                AddNotificacao("Id Pessoa", "Id Pessoa não é valido");

            if (string.IsNullOrEmpty(Descricao))
                AddNotificacao("Descrição", "Descrição é um campo obrigatório");
            else if (Descricao.Length > 250)
                AddNotificacao("Descrição", "Descrição maior que o esperado");

            if (Valor <= 0)
                AddNotificacao("Valor", "Valor não é valido");
        }
    }
}