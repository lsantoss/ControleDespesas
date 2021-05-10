using ControleDespesas.Domain.Empresas.Query.Results;
using ControleDespesas.Domain.Pagamentos.Entities;
using ControleDespesas.Domain.Pagamentos.Enums;
using ControleDespesas.Domain.Pagamentos.Interfaces.Repositories;
using ControleDespesas.Domain.Pagamentos.Query.Results;
using ControleDespesas.Domain.Pessoas.Query.Results;
using ControleDespesas.Domain.TiposPagamentos.Query.Results;
using ControleDespesas.Infra.Data.Repositories.Queries;
using ControleDespesas.Infra.Settings;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ControleDespesas.Infra.Data.Repositories
{
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly SettingsInfraData _settingsInfraData;
        private readonly DynamicParameters _parametros = new DynamicParameters();

        public PagamentoRepository(SettingsInfraData settingsInfraData)
        {
            _settingsInfraData = settingsInfraData;
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

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.ExecuteScalar<int>(PagamentoQueries.Salvar, _parametros);
            }
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

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Execute(PagamentoQueries.Atualizar, _parametros);
            }
        }

        public void Deletar(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Execute(PagamentoQueries.Deletar, _parametros);
            }
        }

        public PagamentoQueryResult Obter(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<PagamentoQueryResult,
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
        }

        public List<PagamentoQueryResult> Listar(int idPessoa, EPagamentoStatus? status)
        {
            _parametros.Add("IdPessoa", idPessoa, DbType.Int32);

            string sql = "";
            switch (status)
            {
                case EPagamentoStatus.Pago:
                    sql = PagamentoQueries.ListarPagamentoConcluido;
                    break;
                case EPagamentoStatus.Pendente:
                    sql = PagamentoQueries.ListarPagamentoPendente;
                    break;
                default:
                    sql = PagamentoQueries.Listar;
                    break;
            }

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<PagamentoQueryResult,
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
        }

        public PagamentoArquivoQueryResult ObterArquivoPagamento(int idPagamento)
        {
            _parametros.Add("IdPagamento", idPagamento, DbType.Int32);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<PagamentoArquivoQueryResult>(PagamentoQueries.ObterArquivoPagamento, _parametros).FirstOrDefault();
            }
        }

        public PagamentoArquivoQueryResult ObterArquivoComprovante(int idPagamento)
        {
            _parametros.Add("IdPagamento", idPagamento, DbType.Int32);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<PagamentoArquivoQueryResult>(PagamentoQueries.ObterArquivoComprovante, _parametros).FirstOrDefault();
            }
        }

        public PagamentoGastosQueryResult ObterGastos(int idPessoa, int? ano, int? mes)
        {
            _parametros.Add("IdPessoa", idPessoa, DbType.Int32);
            _parametros.Add("Ano", ano, DbType.Int32);
            _parametros.Add("Mes", mes, DbType.Int32);

            string sql = "";
            if (ano == null && mes == null)
                sql = PagamentoQueries.ObterGastos;
            else if (mes == null)
                sql = PagamentoQueries.ObterGastosAno;
            else
                sql = PagamentoQueries.ObterGastosAnoMes;

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<PagamentoGastosQueryResult>(sql, _parametros).FirstOrDefault();
            }
        }

        public bool CheckId(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<bool>(PagamentoQueries.CheckId, _parametros).FirstOrDefault();
            }
        }

        public int LocalizarMaxId()
        {
            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<int>(PagamentoQueries.LocalizarMaxId).FirstOrDefault();
            }
        }
    }
}