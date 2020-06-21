using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query.Usuario;
using ControleDespesas.Infra.Data.Queries;
using Dapper;
using LSCode.ConexoesBD.DbContext;
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
        DynamicParameters parametros = new DynamicParameters();
        private readonly DbContext _ctx;

        public UsuarioRepositorio(IOptions<SettingsInfraData> options)
        {
            _ctx = new DbContext(EBancoDadosRelacional.SQLServer, options.Value.ConnectionString);
        }

        public string Salvar(Usuario usuario)
        {
            try
            {
                parametros.Add("Login", usuario.Login.ToString(), DbType.String);
                parametros.Add("Senha", usuario.Senha.ToString(), DbType.String);
                parametros.Add("Privilegio", usuario.Privilegio, DbType.Int16);

                _ctx.SQLServerConexao.Execute(UsuarioQueries.Salvar, parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string Atualizar(Usuario usuario)
        {
            try
            {
                parametros.Add("Id", usuario.Id, DbType.Int32);
                parametros.Add("Login", usuario.Login.ToString(), DbType.String);
                parametros.Add("Senha", usuario.Senha.ToString(), DbType.String);
                parametros.Add("Privilegio", usuario.Privilegio, DbType.Int16);

                _ctx.SQLServerConexao.Execute(UsuarioQueries.Atualizar, parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public string Deletar(int id)
        {
            try
            {
                parametros.Add("Id", id, DbType.Int32);

                _ctx.SQLServerConexao.Execute(UsuarioQueries.Deletar, parametros);

                return "Sucesso";
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
                parametros.Add("Id", id, DbType.Int32);

                return _ctx.SQLServerConexao.Query<UsuarioQueryResult>(UsuarioQueries.Obter, parametros).FirstOrDefault();
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
                parametros.Add("Login", login, DbType.String);
                parametros.Add("Senha", senha, DbType.String);

                return _ctx.SQLServerConexao.Query<UsuarioQueryResult>(UsuarioQueries.Logar, parametros).FirstOrDefault();
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
                parametros.Add("Login", login, DbType.String);

                string retono = _ctx.SQLServerConexao.Query<string>(UsuarioQueries.CheckLogin, parametros).FirstOrDefault();

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
                parametros.Add("Id", id, DbType.Int32);

                return _ctx.SQLServerConexao.Query<bool>(UsuarioQueries.CheckId, parametros).FirstOrDefault();
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