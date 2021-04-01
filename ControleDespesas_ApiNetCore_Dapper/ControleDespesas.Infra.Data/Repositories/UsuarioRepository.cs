using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.Usuario.Results;
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
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DynamicParameters _parametros = new DynamicParameters();
        private readonly DataContext _dataContext;

        public UsuarioRepository(SettingsInfraData settings)
        {
            _dataContext = new DataContext(EBancoDadosRelacional.SQLServer, settings.ConnectionString);
        }

        public int Salvar(Usuario usuario)
        {
            _parametros.Add("Login", usuario.Login, DbType.String);
            _parametros.Add("Senha", usuario.Senha, DbType.String);
            _parametros.Add("Privilegio", usuario.Privilegio, DbType.Int16);

            return _dataContext.SQLServerConexao.ExecuteScalar<int>(UsuarioQueries.Salvar, _parametros);
        }

        public void Atualizar(Usuario usuario)
        {
            _parametros.Add("Id", usuario.Id, DbType.Int32);
            _parametros.Add("Login", usuario.Login, DbType.String);
            _parametros.Add("Senha", usuario.Senha, DbType.String);
            _parametros.Add("Privilegio", usuario.Privilegio, DbType.Int16);

            _dataContext.SQLServerConexao.Execute(UsuarioQueries.Atualizar, _parametros);
        }

        public void Deletar(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            _dataContext.SQLServerConexao.Execute(UsuarioQueries.Deletar, _parametros);
        }

        public UsuarioQueryResult Obter(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            return _dataContext.SQLServerConexao.Query<UsuarioQueryResult>(UsuarioQueries.Obter, _parametros).FirstOrDefault();
        }

        public List<UsuarioQueryResult> Listar()
        {
            return _dataContext.SQLServerConexao.Query<UsuarioQueryResult>(UsuarioQueries.Listar).ToList();
        }

        public UsuarioQueryResult Logar(string login, string senha)
        {
            _parametros.Add("Login", login, DbType.String);
            _parametros.Add("Senha", senha, DbType.String);

            return _dataContext.SQLServerConexao.Query<UsuarioQueryResult>(UsuarioQueries.Logar, _parametros).FirstOrDefault();
        }

        public bool CheckLogin(string login)
        {
            _parametros.Add("Login", login, DbType.String);

            string retono = _dataContext.SQLServerConexao.Query<string>(UsuarioQueries.CheckLogin, _parametros).FirstOrDefault();

            return retono != null ? true : false;
        }

        public bool CheckId(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            return _dataContext.SQLServerConexao.Query<bool>(UsuarioQueries.CheckId, _parametros).FirstOrDefault();
        }

        public int LocalizarMaxId()
        {
            return _dataContext.SQLServerConexao.Query<int>(UsuarioQueries.LocalizarMaxId).FirstOrDefault();
        }
    }
}