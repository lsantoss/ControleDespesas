using ControleDespesas.Domain.Pessoas.Query.Results;
using ControleDespesas.Domain.Usuarios.Entities;
using ControleDespesas.Domain.Usuarios.Interfaces.Repositories;
using ControleDespesas.Domain.Usuarios.Query.Results;
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

        public long Salvar(Usuario usuario)
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
            _parametros.Add("Id", usuario.Id, DbType.Int64);
            _parametros.Add("Login", usuario.Login, DbType.String);
            _parametros.Add("Senha", usuario.Senha, DbType.String);
            _parametros.Add("Privilegio", usuario.Privilegio, DbType.Int16);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Execute(UsuarioQueries.Atualizar, _parametros);
            }
        }

        public void Deletar(long id)
        {
            _parametros.Add("Id", id, DbType.Int64);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                connection.Execute(UsuarioQueries.Deletar, _parametros);
            }
        }

        public UsuarioQueryResult Obter(long id)
        {
            _parametros.Add("Id", id, DbType.Int64);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<UsuarioQueryResult>(UsuarioQueries.Obter, _parametros).FirstOrDefault();
            }
        }

        public UsuarioQueryResult ObterContendoRegistrosFilhos(long id)
        {
            _parametros.Add("Id", id, DbType.Int64);

            var usuarioDic = new Dictionary<long, UsuarioQueryResult>();
            UsuarioQueryResult usuarioQR = new UsuarioQueryResult();

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<UsuarioQueryResult, PessoaQueryResult, UsuarioQueryResult>(
                    UsuarioQueries.ObterContendoRegistrosFilhos,
                    map: (usuario, pessoa) =>
                    {
                        if (usuario != null)
                            if (!usuarioDic.TryGetValue(usuario.Id, out usuarioQR))
                                usuarioDic.Add(usuario.Id, usuarioQR = usuario);

                        if (usuarioQR.Pessoas == null)
                            usuarioQR.Pessoas = new List<PessoaQueryResult>();

                        if (pessoa != null)
                            usuarioQR.Pessoas.Add(pessoa);

                        return usuarioQR;
                    },
                    _parametros,
                    splitOn: "Id, Id").FirstOrDefault();
            }
        }

        public List<UsuarioQueryResult> Listar()
        {
            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<UsuarioQueryResult>(UsuarioQueries.Listar).ToList();
            }
        }

        public List<UsuarioQueryResult> ListarContendoRegistrosFilhos()
        {
            var usuarioDic = new Dictionary<long, UsuarioQueryResult>();
            UsuarioQueryResult usuarioQR = new UsuarioQueryResult();

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<UsuarioQueryResult, PessoaQueryResult, UsuarioQueryResult>(
                    UsuarioQueries.ListarContendoRegistrosFilhos,
                    map: (usuario, pessoa) =>
                    {
                        if (usuario != null)
                            if (!usuarioDic.TryGetValue(usuario.Id, out usuarioQR))
                                usuarioDic.Add(usuario.Id, usuarioQR = usuario);

                        if (usuarioQR.Pessoas == null)
                            usuarioQR.Pessoas = new List<PessoaQueryResult>();

                        if (pessoa != null)
                            usuarioQR.Pessoas.Add(pessoa);

                        return usuarioQR;
                    },
                    splitOn: "Id, Id").Distinct().ToList();
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

        public bool CheckId(long id)
        {
            _parametros.Add("Id", id, DbType.Int64);

            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<bool>(UsuarioQueries.CheckId, _parametros).FirstOrDefault();
            }
        }

        public long LocalizarMaxId()
        {
            using (var connection = new SqlConnection(_settingsInfraData.ConnectionString))
            {
                return connection.Query<long>(UsuarioQueries.LocalizarMaxId).FirstOrDefault();
            }
        }
    }
}