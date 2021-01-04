using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Usuario;
using ControleDespesas.Dominio.Repositorio;
using ControleDespesas.Infra.Data.Queries;
using ControleDespesas.Infra.Data.Settings;
using Dapper;
using LSCode.ConexoesBD.DataContexts;
using LSCode.ConexoesBD.Enums;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ControleDespesas.Infra.Data.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly DynamicParameters _parametros = new DynamicParameters();
        private readonly DataContext _ctx;

        public UsuarioRepositorio(IOptions<SettingsInfraData> options)
        {
            _ctx = new DataContext(EBancoDadosRelacional.SQLServer, options.Value.ConnectionString);
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
                throw new Exception(e.Message);
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
                throw new Exception(e.Message);
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
                throw new Exception(e.Message);
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
                throw new Exception(e.Message);
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
                throw new Exception(e.Message);
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
                throw new Exception(e.Message);
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
                throw new Exception(e.Message);
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
                throw new Exception(e.Message);
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
                throw new Exception(e.Message);
            }
        }
    }
}