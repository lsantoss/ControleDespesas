using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.Usuario.Results;
using ControleDespesas.Infra.Data.Repositories.Queries;
using ControleDespesas.Infra.Settings;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace ControleDespesas.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SettingsInfraData _settingsInfraData;
        private readonly DynamicParameters _parametros = new DynamicParameters();

        public UsuarioRepository(SettingsInfraData settingsInfraData)
        {
            _settingsInfraData = settingsInfraData;
        }

        public int Salvar(Usuario usuario)
        {
            _parametros.Add("Login", usuario.Login, DbType.String);
            _parametros.Add("Senha", usuario.Senha, DbType.String);
            _parametros.Add("Privilegio", usuario.Privilegio, DbType.Int16);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.ExecuteScalar<int>(UsuarioQueries.Salvar, _parametros);
            }
        }

        public void Atualizar(Usuario usuario)
        {
            _parametros.Add("Id", usuario.Id, DbType.Int32);
            _parametros.Add("Login", usuario.Login, DbType.String);
            _parametros.Add("Senha", usuario.Senha, DbType.String);
            _parametros.Add("Privilegio", usuario.Privilegio, DbType.Int16);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Execute(UsuarioQueries.Atualizar, _parametros);
            }
        }

        public void Deletar(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Execute(UsuarioQueries.Deletar, _parametros);
            }
        }

        public UsuarioQueryResult Obter(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<UsuarioQueryResult>(UsuarioQueries.Obter, _parametros).FirstOrDefault();
            }
        }

        public List<UsuarioQueryResult> Listar()
        {
            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<UsuarioQueryResult>(UsuarioQueries.Listar).ToList();
            }
        }

        public UsuarioQueryResult Logar(string login, string senha)
        {
            _parametros.Add("Login", login, DbType.String);
            _parametros.Add("Senha", senha, DbType.String);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<UsuarioQueryResult>(UsuarioQueries.Logar, _parametros).FirstOrDefault();
            }
        }

        public bool CheckLogin(string login)
        {
            _parametros.Add("Login", login, DbType.String);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<string>(UsuarioQueries.CheckLogin, _parametros).Any();
            }
        }

        public bool CheckId(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<bool>(UsuarioQueries.CheckId, _parametros).FirstOrDefault();
            }
        }

        public int LocalizarMaxId()
        {
            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<int>(UsuarioQueries.LocalizarMaxId).FirstOrDefault();
            }
        }
    }
}