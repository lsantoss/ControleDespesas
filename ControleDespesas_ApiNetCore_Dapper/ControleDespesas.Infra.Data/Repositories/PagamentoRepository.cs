using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Enums;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.Empresa.Results;
using ControleDespesas.Domain.Query.Pagamento.Results;
using ControleDespesas.Domain.Query.Pessoa.Results;
using ControleDespesas.Domain.Query.TipoPagamento.Results;
using ControleDespesas.Infra.Data.Queries;
using ControleDespesas.Infra.Settings;
using Dapper;
using LSCode.ConexoesBD.DataContexts;
using LSCode.ConexoesBD.Enums;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ControleDespesas.Infra.Data.Repositories
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly DynamicParameters _parametros = new DynamicParameters();
        private readonly DataContext _dataContext;

        public PagamentoRepository(SettingsInfraData settings)
        {
            _dataContext = new DataContext(EBancoDadosRelacional.SQLServer, settings.ConnectionString);
        }

        public int Salvar(Pagamento pagamento)
        {
            _parametros.Add("IdTipoPagamento", pagamento.TipoPagamento.Id, DbType.Int32);
            _parametros.Add("IdEmpresa", pagamento.Empresa.Id, DbType.Int32);
            _parametros.Add("IdPessoa", pagamento.Pessoa.Id, DbType.Int32);
            _parametros.Add("Descricao", pagamento.Descricao, DbType.String);
            _parametros.Add("Valor", pagamento.Valor, DbType.Double);
            _parametros.Add("DataVencimento", pagamento.DataVencimento, DbType.Date);
            _parametros.Add("DataPagamento", pagamento.DataPagamento, DbType.Date);
            _parametros.Add("ArquivoPagamento", pagamento.ArquivoPagamento, DbType.String);
            _parametros.Add("ArquivoComprovante", pagamento.ArquivoComprovante, DbType.String);

            return _dataContext.SQLServerConexao.ExecuteScalar<int>(PagamentoQueries.Salvar, _parametros);
        }

        public void Atualizar(Pagamento pagamento)
        {
            _parametros.Add("Id", pagamento.Id, DbType.Int32);
            _parametros.Add("IdTipoPagamento", pagamento.TipoPagamento.Id, DbType.Int32);
            _parametros.Add("IdEmpresa", pagamento.Empresa.Id, DbType.Int32);
            _parametros.Add("IdPessoa", pagamento.Pessoa.Id, DbType.Int32);
            _parametros.Add("Descricao", pagamento.Descricao, DbType.String);
            _parametros.Add("Valor", pagamento.Valor, DbType.Double);
            _parametros.Add("DataVencimento", pagamento.DataVencimento, DbType.Date);
            _parametros.Add("DataPagamento", pagamento.DataPagamento, DbType.Date);
            _parametros.Add("ArquivoPagamento", pagamento.ArquivoPagamento, DbType.String);
            _parametros.Add("ArquivoComprovante", pagamento.ArquivoComprovante, DbType.String);

            _dataContext.SQLServerConexao.Execute(PagamentoQueries.Atualizar, _parametros);
        }

        public void Deletar(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            _dataContext.SQLServerConexao.Execute(PagamentoQueries.Deletar, _parametros);
        }

        public PagamentoQueryResult Obter(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            return _dataContext.SQLServerConexao.Query<PagamentoQueryResult,
                                                TipoPagamentoQueryResult,
                                                EmpresaQueryResult,
                                                PessoaQueryResult,
                                                PagamentoQueryResult>(
                    PagamentoQueries.Obter,
                    map: (pagamento, tipoPagamento, empresa, pessoa) =>
                    {
                        pagamento.TipoPagamento = tipoPagamento;
                        pagamento.Empresa = empresa;
                        pagamento.Pessoa = pessoa;
                        return pagamento;
                    },
                    _parametros,
                    splitOn: "Id, Id, Id, Id").FirstOrDefault();
        }

        public List<PagamentoQueryResult> Listar(int idPessoa, EPagamentoStatus? status)
        {
            _parametros.Add("IdPessoa", idPessoa, DbType.Int32);

            string sql = status == EPagamentoStatus.Pago ? PagamentoQueries.ListarPagamentoConcluido
                        : status == EPagamentoStatus.Pendente ? PagamentoQueries.ListarPagamentoPendente : PagamentoQueries.Listar;

            return _dataContext.SQLServerConexao.Query<PagamentoQueryResult,
                                                TipoPagamentoQueryResult,
                                                EmpresaQueryResult,
                                                PessoaQueryResult,
                                                PagamentoQueryResult>(
                sql,
                map: (pagamento, tipoPagamento, empresa, pessoa) =>
                {
                    pagamento.TipoPagamento = tipoPagamento;
                    pagamento.Empresa = empresa;
                    pagamento.Pessoa = pessoa;
                    return pagamento;
                },
                _parametros,
                splitOn: "Id, Id, Id, Id").Distinct().ToList();
        }

        public PagamentoArquivoQueryResult ObterArquivoPagamento(int idPagamento)
        {
            _parametros.Add("IdPagamento", idPagamento, DbType.Int32);

            return _dataContext.SQLServerConexao.Query<PagamentoArquivoQueryResult>(PagamentoQueries.ObterArquivoPagamento, _parametros).FirstOrDefault();
        }

        public PagamentoArquivoQueryResult ObterArquivoComprovante(int idPagamento)
        {
            _parametros.Add("IdPagamento", idPagamento, DbType.Int32);

            return _dataContext.SQLServerConexao.Query<PagamentoArquivoQueryResult>(PagamentoQueries.ObterArquivoComprovante, _parametros).FirstOrDefault();
        }

        public PagamentoGastosQueryResult ObterGastos(int idPessoa, int? ano, int? mes)
        {
            _parametros.Add("IdPessoa", idPessoa, DbType.Int32);
            _parametros.Add("Ano", ano, DbType.Int32);
            _parametros.Add("Mes", mes, DbType.Int32);

            string sql = ano == null && mes == null ? PagamentoQueries.ObterGastos
                        : mes == null ? PagamentoQueries.ObterGastosAno : PagamentoQueries.ObterGastosAnoMes;

            return _dataContext.SQLServerConexao.Query<PagamentoGastosQueryResult>(sql, _parametros).FirstOrDefault();
        }

        public bool CheckId(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            return _dataContext.SQLServerConexao.Query<bool>(PagamentoQueries.CheckId, _parametros).FirstOrDefault();
        }

        public int LocalizarMaxId()
        {
            return _dataContext.SQLServerConexao.Query<int>(PagamentoQueries.LocalizarMaxId).FirstOrDefault();
        }
    }
}