using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.Usuario;
using ControleDespesas.Infra.Data.Queries;
using ControleDespesas.Infra.Data.Settings;
using Dapper;
using LSCode.ConexoesBD.DataContexts;
using LSCode.ConexoesBD.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ControleDespesas.Infra.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DynamicParameters _parametros = new DynamicParameters();
        private readonly DataContext _ctx;

        public UsuarioRepository(SettingsInfraData settings)
        {
            _ctx = new DataContext(EBancoDadosRelacional.SQLServer, settings.ConnectionString);
        }

        public Usuario Salvar(Usuario usuario)
        {
            try
            {
                _parametros.Add("Login", usuario.Login, DbType.String);
                _parametros.Add("Senha", usuario.Senha, DbType.String);
                _parametros.Add("Privilegio", usuario.Privilegio, DbType.Int16);

                usuario.Id = _ctx.SQLServerConexao.ExecuteScalar<int>(UsuarioQueries.Salvar, _parametros);
                return usuario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Atualizar(Usuario usuario)
        {
            try
            {
                _parametros.Add("Id", usuario.Id, DbType.Int32);
                _parametros.Add("Login", usuario.Login, DbType.String);
                _parametros.Add("Senha", usuario.Senha, DbType.String);
                _parametros.Add("Privilegio", usuario.Privilegio, DbType.Int16);

                _ctx.SQLServerConexao.Execute(UsuarioQueries.Atualizar, _parametros);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Deletar(int id)
        {
            try
            {
                _parametros.Add("Id", id, DbType.Int32);

                _ctx.SQLServerConexao.Execute(UsuarioQueries.Deletar, _parametros);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public UsuarioQueryResult Obter(int id)
        {
            try
            {
                _parametros.Add("Id", id, DbType.Int32);

                return _ctx.SQLServerConexao.Query<UsuarioQueryResult>(UsuarioQueries.Obter, _parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<UsuarioQueryResult> Listar()
        {
            try
            {
                return _ctx.SQLServerConexao.Query<UsuarioQueryResult>(UsuarioQueries.Listar).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public UsuarioQueryResult Logar(string login, string senha)
        {
            try
            {
                _parametros.Add("Login", login, DbType.String);
                _parametros.Add("Senha", senha, DbType.String);

                return _ctx.SQLServerConexao.Query<UsuarioQueryResult>(UsuarioQueries.Logar, _parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool CheckLogin(string login)
        {
            try
            {
                _parametros.Add("Login", login, DbType.String);

                string retono = _ctx.SQLServerConexao.Query<string>(UsuarioQueries.CheckLogin, _parametros).FirstOrDefault();

                return retono != null ? true : false;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public bool CheckId(int id)
        {
            try
            {
                _parametros.Add("Id", id, DbType.Int32);

                return _ctx.SQLServerConexao.Query<bool>(UsuarioQueries.CheckId, _parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public int LocalizarMaxId()
        {
            try
            {
                return _ctx.SQLServerConexao.Query<int>(UsuarioQueries.LocalizarMaxId).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}