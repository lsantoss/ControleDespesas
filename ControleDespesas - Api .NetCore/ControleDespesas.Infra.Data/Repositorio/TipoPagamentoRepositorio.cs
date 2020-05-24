using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query;
using Dapper;
using LSCode.ConexoesBD.DbContext;
using LSCode.ConexoesBD.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ControleDespesas.Infra.Data.Repositorio
{
    public class TipoPagamentoRepositorio : ITipoPagamentoRepositorio
    {
        StringBuilder Sql = new StringBuilder();
        DynamicParameters parametros = new DynamicParameters();
        private readonly DbContext _ctx;

        public TipoPagamentoRepositorio()
        {
            _ctx = new DbContext(EBancoDadosRelacional.SQLServer, Settings.Settings.ConnectionString);
        }

        public string Salvar(TipoPagamento tipoPagamento)
        {
            try
            {
                parametros.Add("Descricao", tipoPagamento.Descricao.ToString(), DbType.String);

                Sql.Clear();
                Sql.Append("INSERT INTO TipoPagamento (");
                Sql.Append("Descricao) ");
                Sql.Append("VALUES(");
                Sql.Append("@Descricao)");

                _ctx.SQLServerConexao.Execute(Sql.ToString(), parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Atualizar(TipoPagamento tipoPagamento)
        {
            try
            {
                parametros.Add("Id", tipoPagamento.Id, DbType.Int32);
                parametros.Add("Descricao", tipoPagamento.Descricao.ToString(), DbType.String);

                Sql.Clear();
                Sql.Append("UPDATE TipoPagamento SET ");
                Sql.Append("Descricao = @Descricao ");
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
                Sql.Append("DELETE FROM TipoPagamento ");
                Sql.Append("WHERE Id = @Id");

                _ctx.SQLServerConexao.Execute(Sql.ToString(), parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public TipoPagamentoQueryResult ObterTipoPagamento(int id)
        {
            Sql.Clear();
            Sql.Append("SELECT ");
            Sql.Append("Id AS Id,");
            Sql.Append("Descricao AS Descricao ");
            Sql.Append("FROM TipoPagamento ");
            Sql.Append("WHERE Id = @Id ");

            return _ctx.SQLServerConexao.Query<TipoPagamentoQueryResult>(Sql.ToString(), new { Id = id }).FirstOrDefault();
        }

        public List<TipoPagamentoQueryResult> ListarTipoPagamentos()
        {
            Sql.Clear();
            Sql.Append("SELECT ");
            Sql.Append("Id AS Id,");
            Sql.Append("Descricao AS Descricao ");
            Sql.Append("FROM TipoPagamento ");
            Sql.Append("ORDER BY Id ASC ");

            return _ctx.SQLServerConexao.Query<TipoPagamentoQueryResult>(Sql.ToString()).ToList();
        }

        public bool CheckId(int id)
        {
            Sql.Clear();
            Sql.Append("SELECT Id ");
            Sql.Append("FROM TipoPagamento ");
            Sql.Append("where Id = @Id ");

            return _ctx.SQLServerConexao.Query<bool>(Sql.ToString(), new { Id = id }).FirstOrDefault();
        }

        public int LocalizarMaxId()
        {
            Sql.Clear();
            Sql.Append("SELECT MAX(Id) ");
            Sql.Append("FROM TipoPagamento");

            return _ctx.SQLServerConexao.Query<int>(Sql.ToString()).FirstOrDefault();
        }
    }
}