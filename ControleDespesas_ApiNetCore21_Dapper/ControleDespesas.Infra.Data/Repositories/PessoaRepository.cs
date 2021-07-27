using ControleDespesas.Domain.Empresas.Query.Results;
using ControleDespesas.Domain.Pagamentos.Query.Results;
using ControleDespesas.Domain.Pessoas.Entities;
using ControleDespesas.Domain.Pessoas.Interfaces.Repositories;
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
    public class PessoaRepository : IPessoaRepository
    {
        private readonly SettingsInfraData _settingsInfraData;
        private readonly DynamicParameters _parametros = new DynamicParameters();

        public PessoaRepository(SettingsInfraData settingsInfraData)
        {
            _settingsInfraData = settingsInfraData;
        }

        public long Salvar(Pessoa pessoa)
        {
            _parametros.Add("IdUsuario", pessoa.IdUsuario, DbType.Int64);
            _parametros.Add("Nome", pessoa.Nome, DbType.String);
            _parametros.Add("ImagemPerfil", pessoa.ImagemPerfil, DbType.String);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.ExecuteScalar<long>(PessoaQueries.Salvar, _parametros);
            }
        }

        public void Atualizar(Pessoa pessoa)
        {
                _parametros.Add("Id", pessoa.Id, DbType.Int64);
                _parametros.Add("IdUsuario", pessoa.IdUsuario, DbType.Int64);
                _parametros.Add("Nome", pessoa.Nome, DbType.String);
                _parametros.Add("ImagemPerfil", pessoa.ImagemPerfil, DbType.String);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Execute(PessoaQueries.Atualizar, _parametros);
            }
        }

        public void Deletar(long id, long idUsuario)
        {
            _parametros.Add("Id", id, DbType.Int64);
            _parametros.Add("IdUsuario", idUsuario, DbType.Int64);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Execute(PessoaQueries.Deletar, _parametros);
            }
        }

        public PessoaQueryResult Obter(long id, long idUsuario)
        {
            _parametros.Add("Id", id, DbType.Int64);
            _parametros.Add("IdUsuario", idUsuario, DbType.Int64);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<PessoaQueryResult>(PessoaQueries.Obter, _parametros).FirstOrDefault();
            }
        }

        public PessoaQueryResult ObterContendoRegistrosFilhos(long id, long idUsuario)
        {
            _parametros.Add("IdUsuario", idUsuario, DbType.Int64);
            _parametros.Add("Id", id, DbType.Int64);

            var pessoaDic = new Dictionary<long, PessoaQueryResult>();
            PessoaQueryResult pessoaQR = new PessoaQueryResult();

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<PessoaQueryResult, PagamentoQueryResult, TipoPagamentoQueryResult, EmpresaQueryResult, PessoaQueryResult>(
                    PessoaQueries.ObterContendoRegistrosFilhos,
                    map: (pessoa, pagamento, tipoPagamento, empresa) =>
                    {
                        if (pessoa != null)
                            if (!pessoaDic.TryGetValue(pessoa.Id, out pessoaQR))
                                pessoaDic.Add(pessoa.Id, pessoaQR = pessoa);

                        if (pessoaQR.Pagamentos == null)
                            pessoaQR.Pagamentos = new List<PagamentoQueryResult>();

                        if (pagamento != null)
                        {
                            pagamento.TipoPagamento = tipoPagamento;
                            pagamento.Empresa = empresa;
                            pessoaQR.Pagamentos.Add(pagamento);
                        }

                        return pessoaQR;
                    },
                    _parametros,
                    splitOn: "Id, Id, Id, Id").FirstOrDefault();
            }
        }

        public List<PessoaQueryResult> Listar(long idUsuario)
        {
            _parametros.Add("IdUsuario", idUsuario, DbType.Int64);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<PessoaQueryResult>(PessoaQueries.Listar, _parametros).ToList();
            }
        }

        public List<PessoaQueryResult> ListarContendoRegistrosFilhos(long idUsuario)
        {
            _parametros.Add("IdUsuario", idUsuario, DbType.Int64);

            var pessoaDic = new Dictionary<long, PessoaQueryResult>();
            PessoaQueryResult pessoaQR = new PessoaQueryResult();

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<PessoaQueryResult, PagamentoQueryResult, TipoPagamentoQueryResult, EmpresaQueryResult, PessoaQueryResult>(
                    PessoaQueries.ListarContendoRegistrosFilhos,
                    map: (pessoa, pagamento, tipoPagamento, empresa) =>
                    {
                        if (pessoa != null)
                            if (!pessoaDic.TryGetValue(pessoa.Id, out pessoaQR))
                                pessoaDic.Add(pessoa.Id, pessoaQR = pessoa);

                        if (pessoaQR.Pagamentos == null)
                            pessoaQR.Pagamentos = new List<PagamentoQueryResult>();

                        if (pagamento != null)
                        {
                            pagamento.TipoPagamento = tipoPagamento;
                            pagamento.Empresa = empresa;
                            pessoaQR.Pagamentos.Add(pagamento);
                        }

                        return pessoaQR;
                    },
                    _parametros,
                    splitOn: "Id, Id, Id, Id").Distinct().ToList();
            }
        }

        public bool CheckId(long id)
        {
            _parametros.Add("Id", id, DbType.Int64);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<bool>(PessoaQueries.CheckId, _parametros).FirstOrDefault();
            }
        }

        public long LocalizarMaxId()
        {
            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<long>(PessoaQueries.LocalizarMaxId).FirstOrDefault();
            }
        }
    }
}