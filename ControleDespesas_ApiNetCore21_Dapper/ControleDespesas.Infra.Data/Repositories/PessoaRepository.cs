using ControleDespesas.Domain.Pessoas.Entities;
using ControleDespesas.Domain.Pessoas.Interfaces.Repositories;
using ControleDespesas.Domain.Pessoas.Query.Results;
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

        public int Salvar(Pessoa pessoa)
        {
            _parametros.Add("IdUsuario", pessoa.Usuario.Id, DbType.Int32);
            _parametros.Add("Nome", pessoa.Nome, DbType.String);
            _parametros.Add("ImagemPerfil", pessoa.ImagemPerfil, DbType.String);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.ExecuteScalar<int>(PessoaQueries.Salvar, _parametros);
            }
        }

        public void Atualizar(Pessoa pessoa)
        {
                _parametros.Add("Id", pessoa.Id, DbType.Int32);
                _parametros.Add("IdUsuario", pessoa.Usuario.Id, DbType.Int32);
                _parametros.Add("Nome", pessoa.Nome, DbType.String);
                _parametros.Add("ImagemPerfil", pessoa.ImagemPerfil, DbType.String);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Execute(PessoaQueries.Atualizar, _parametros);
            }
        }

        public void Deletar(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Execute(PessoaQueries.Deletar, _parametros);
            }
        }

        public PessoaQueryResult Obter(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<PessoaQueryResult>(PessoaQueries.Obter, _parametros).FirstOrDefault();
            }
        }

        public List<PessoaQueryResult> Listar(int idUsuario)
        {
            _parametros.Add("IdUsuario", idUsuario, DbType.Int32);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<PessoaQueryResult>(PessoaQueries.Listar, _parametros).ToList();
            }
        }

        public bool CheckId(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<bool>(PessoaQueries.CheckId, _parametros).FirstOrDefault();
            }
        }

        public int LocalizarMaxId()
        {
            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<int>(PessoaQueries.LocalizarMaxId).FirstOrDefault();
            }
        }
    }
}