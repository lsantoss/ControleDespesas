using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query.Usuario;
using Dapper;
using LSCode.ConexoesBD.DbContext;
using LSCode.ConexoesBD.Enums;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ControleDespesas.Infra.Data.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        StringBuilder Sql = new StringBuilder();
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

                Sql.Clear();
                Sql.Append("INSERT INTO Usuario (");
                Sql.Append("Login, ");
                Sql.Append("Senha, ");
                Sql.Append("Privilegio) ");
                Sql.Append("VALUES(");
                Sql.Append("@Login, ");
                Sql.Append("@Senha, ");
                Sql.Append("@Privilegio)");

                _ctx.SQLServerConexao.Execute(Sql.ToString(), parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                return e.Message;
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

                Sql.Clear();
                Sql.Append("UPDATE Usuario SET ");
                Sql.Append("Login = @Login, ");
                Sql.Append("Senha = @Senha, ");
                Sql.Append("Privilegio = @Privilegio ");
                Sql.Append("WHERE Id = @Id");

                _ctx.SQLServerConexao.Execute(Sql.ToString(), parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Deletar(int id)
        {
            try
            {
                parametros.Add("Id", id, DbType.Int32);

                Sql.Clear();
                Sql.Append("DELETE FROM Usuario ");
                Sql.Append("WHERE Id = @Id");

                _ctx.SQLServerConexao.Execute(Sql.ToString(), parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public UsuarioQueryResult ObterUsuario(int id)
        {
            parametros.Add("Id", id, DbType.Int32);

            Sql.Clear();
            Sql.Append("SELECT ");
            Sql.Append("Id AS Id,");
            Sql.Append("Login AS Login,");
            Sql.Append("Senha AS Senha,");
            Sql.Append("Privilegio AS Privilegio ");
            Sql.Append("FROM Usuario ");
            Sql.Append("WHERE Id = @Id ");

            return _ctx.SQLServerConexao.Query<UsuarioQueryResult>(Sql.ToString(), parametros).FirstOrDefault();
        }

        public List<UsuarioQueryResult> ListarUsuarios()
        {
            Sql.Clear();
            Sql.Append("SELECT ");
            Sql.Append("Id AS Id,");
            Sql.Append("Login AS Login,");
            Sql.Append("Senha AS Senha,");
            Sql.Append("Privilegio AS Privilegio ");
            Sql.Append("FROM Usuario ");

            return _ctx.SQLServerConexao.Query<UsuarioQueryResult>(Sql.ToString()).ToList();
        }

        public UsuarioQueryResult LogarUsuario(string login, string senha)
        {
            parametros.Add("Login", login, DbType.String);
            parametros.Add("Senha", senha, DbType.String);

            Sql.Clear();
            Sql.Append("SELECT ");
            Sql.Append("Id AS Id,");
            Sql.Append("Login AS Login,");
            Sql.Append("Senha AS Senha,");
            Sql.Append("Privilegio AS Privilegio ");
            Sql.Append("FROM Usuario ");
            Sql.Append("WHERE Login = @Login and Senha = @Senha ");

            return _ctx.SQLServerConexao.Query<UsuarioQueryResult>(Sql.ToString(), parametros).FirstOrDefault();
        }

        public bool CheckLogin(string login)
        {
            parametros.Add("Login", login, DbType.String);

            Sql.Clear();
            Sql.Append("SELECT Login FROM Usuario WHERE Login = @Login ");

            return _ctx.SQLServerConexao.Query<bool>(Sql.ToString(), parametros).FirstOrDefault();
        }

        public bool CheckId(int id)
        {
            parametros.Add("Id", id, DbType.Int32);

            Sql.Clear();
            Sql.Append("SELECT Id FROM Usuario WHERE Id = @Id ");

            return _ctx.SQLServerConexao.Query<bool>(Sql.ToString(), parametros).FirstOrDefault();
        }

        public int LocalizarMaxId()
        {
            Sql.Clear();
            Sql.Append("SELECT MAX(Id) FROM Usuario");

            return _ctx.SQLServerConexao.Query<int>(Sql.ToString()).FirstOrDefault();
        }
    }
}