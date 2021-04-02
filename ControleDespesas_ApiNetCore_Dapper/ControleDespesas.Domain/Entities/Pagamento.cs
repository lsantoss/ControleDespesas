using LSCode.Validador.ValidacoesNotificacoes;
using System;

namespace ControleDespesas.Domain.Entities
{
    public class Pagamento : Notificadora
    {
        public int Id { get; private set; }
        public TipoPagamento TipoPagamento { get; private set; }
        public Empresa Empresa { get; private set; }
        public Pessoa Pessoa { get; private set; }
        public string Descricao { get; private set; }
        public double Valor { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public DateTime? DataPagamento { get; private set; }
        public string ArquivoPagamento { get; private set; }
        public string ArquivoComprovante { get; private set; }

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
        }

        public Pagamento(int id)
        {
            Id = id;
        }

        public void DefinirId(int id)
        {
            Id = id;
        }

        public void DefinirTipoPagamento(TipoPagamento tipoPagamento)
        {
            TipoPagamento = tipoPagamento;
        }

        public void DefinirEmpresa(Empresa empresa)
        {
            Empresa = empresa;
        }

        public void DefinirPessoa(Pessoa pessoa)
        {
            Pessoa = pessoa;
        }

        public void DefinirDescricao(string descricao)
        {
            Descricao = descricao;
        }

        public void DefinirValor(double valor)
        {
            Valor = valor;
        }

        public void DefinirDataVencimento(DateTime dataVencimento)
        {
            DataVencimento = dataVencimento;
        }

        public void DefinirDataPagamento(DateTime? dataPagamento)
        {
            DataPagamento = dataPagamento;
        }

        public void DefinirArquivoPagamento(string arquivoPagamento)
        {
            ArquivoPagamento = arquivoPagamento;
        }

        public void DefinirArquivoComprovante(string arquivoComprovante)
        {
            ArquivoComprovante = arquivoComprovante;
        }

        public void Validar()
        {
            if (TipoPagamento.Id <= 0)
                AddNotificacao("IdTipoPagamento", "IdTipoPagamento não é valido");

            if (Empresa.Id <= 0)
                AddNotificacao("IdEmpresa", "IdEmpresa não é valido");

            if (Pessoa.Id <= 0)
                AddNotificacao("IdPessoa", "IdPessoa não é valido");

            if (string.IsNullOrEmpty(Descricao))
                AddNotificacao("Descricao", "Descricao é um campo obrigatório");
            else if (Descricao.Length > 250)
                AddNotificacao("Descricao", "Descricao maior que o esperado");

            if (Valor <= 0)
                AddNotificacao("Valor", "Valor não é valido");
        }
    }
}