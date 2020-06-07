using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query;
using ControleDespesas.Infra.Data.DataContext;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace ControleDespesas.Infra.Data.Repositorio
{
    public class PagamentoRepositorio : IPagamentoRepositorio
    {
        StringBuilder Sql = new StringBuilder();
        DynamicParameters parametros = new DynamicParameters();
        private readonly DbContext _ctx;

        public PagamentoRepositorio(DbContext ctx)
        {
            _ctx = ctx;
        }

        public string Salvar(Pagamento pagamento)
        {
            try
            {
                parametros.Add("IdTipoPagamento", pagamento.TipoPagamento.Id, DbType.Int32);
                parametros.Add("IdEmpresa", pagamento.Empresa.Id, DbType.Int32);
                parametros.Add("IdPessoa", pagamento.Pessoa.Id, DbType.Int32);
                parametros.Add("Descricao", pagamento.Descricao.ToString(), DbType.String);
                parametros.Add("Valor", pagamento.Valor, DbType.Double);
                parametros.Add("DataPagamento", pagamento.DataPagamento, DbType.Date);
                parametros.Add("DataVencimento", pagamento.DataVencimento, DbType.Date);

                Sql.Clear();
                Sql.Append("INSERT INTO Pagamento (");
                Sql.Append("IdTipoPagamento, ");
                Sql.Append("IdEmpresa, ");
                Sql.Append("IdPessoa, ");
                Sql.Append("Descricao, ");
                Sql.Append("Valor, ");
                Sql.Append("DataPagamento, ");
                Sql.Append("DataVencimento) ");
                Sql.Append("VALUES(");
                Sql.Append("@IdTipoPagamento, ");
                Sql.Append("@IdEmpresa, ");
                Sql.Append("@IdPessoa, ");
                Sql.Append("@Descricao, ");
                Sql.Append("@Valor, ");
                Sql.Append("@DataPagamento, ");
                Sql.Append("@DataVencimento)");

                _ctx.SQLServerConexao.Execute(Sql.ToString(), parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string Atualizar(Pagamento pagamento)
        {
            try
            {
                parametros.Add("Id", pagamento.Id, DbType.Int32);
                parametros.Add("IdTipoPagamento", pagamento.TipoPagamento.Id, DbType.Int32);
                parametros.Add("IdEmpresa", pagamento.Empresa.Id, DbType.Int32);
                parametros.Add("IdPessoa", pagamento.Pessoa.Id, DbType.Int32);
                parametros.Add("Descricao", pagamento.Descricao.ToString(), DbType.String);
                parametros.Add("Valor", pagamento.Valor, DbType.Double);
                parametros.Add("DataPagamento", pagamento.DataPagamento, DbType.Date);
                parametros.Add("DataVencimento", pagamento.DataVencimento, DbType.Date);

                Sql.Clear();
                Sql.Append("UPDATE Pagamento SET ");
                Sql.Append("IdTipoPagamento = @IdTipoPagamento, ");
                Sql.Append("IdEmpresa = @IdEmpresa, ");
                Sql.Append("IdPessoa = @IdPessoa, ");
                Sql.Append("Descricao = @Descricao, ");
                Sql.Append("Valor = @Valor, ");
                Sql.Append("DataPagamento = @DataPagamento, ");
                Sql.Append("DataVencimento = @DataVencimento ");
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
                Sql.Append("DELETE FROM Pagamento ");
                Sql.Append("WHERE Id = @Id");

                _ctx.SQLServerConexao.Execute(Sql.ToString(), parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public PagamentoQueryResult ObterPagamento(int id)
        {
            Sql.Clear();
            Sql.Append("SELECT ");
            Sql.Append("Pagamento.Id AS Id,");
            Sql.Append("Pagamento.IdTipoPagamento AS IdTipoPagamento,");
            Sql.Append("Pagamento.IdEmpresa AS IdEmpresa,");
            Sql.Append("Pagamento.IdPessoa AS IdPessoa,");
            Sql.Append("Pagamento.Descricao AS Descricao,");
            Sql.Append("Pagamento.Valor AS Valor,");
            Sql.Append("Pagamento.DataPagamento AS DataPagamento,");
            Sql.Append("Pagamento.DataVencimento AS DataVencimento,");

            Sql.Append("TipoPagamento.Id AS Id,");
            Sql.Append("TipoPagamento.Descricao AS Descricao,");

            Sql.Append("Empresa.Id AS Id,");
            Sql.Append("Empresa.Nome AS Nome,");
            Sql.Append("Empresa.Logo AS Logo,");

            Sql.Append("Pessoa.Id AS Id,");
            Sql.Append("Pessoa.Nome AS Nome,");
            Sql.Append("Pessoa.ImagemPerfil AS ImagemPerfil ");

            Sql.Append("FROM Pagamento ");
            Sql.Append("INNER JOIN TipoPagamento ON Pagamento.IdTipoPagamento = TipoPagamento.Id ");
            Sql.Append("INNER JOIN Empresa ON Pagamento.IdEmpresa = Empresa.Id ");
            Sql.Append("INNER JOIN Pessoa ON Pagamento.IdPessoa = Pessoa.Id ");

            Sql.Append("WHERE Pagamento.Id = @Id ");

            return _ctx.SQLServerConexao.Query<PagamentoQueryResult, 
                                               TipoPagamentoQueryResult, 
                                               EmpresaQueryResult, 
                                               PessoaQueryResult,
                                               PagamentoQueryResult>(
                    Sql.ToString(),
                    map: (pagamento, tipoPagamento, empresa, pessoa) =>
                    {
                        pagamento.TipoPagamento = tipoPagamento;
                        pagamento.Empresa = empresa;
                        pagamento.Pessoa = pessoa;
                        return pagamento;
                    },
                    new { Id = id },
                    splitOn: "Id, Id, Id, Id").FirstOrDefault();
        }

        public List<PagamentoQueryResult> ListarPagamentos()
        {
            Sql.Clear();
            Sql.Append("SELECT ");
            Sql.Append("Pagamento.Id AS Id,");
            Sql.Append("Pagamento.IdTipoPagamento AS IdTipoPagamento,");
            Sql.Append("Pagamento.IdEmpresa AS IdEmpresa,");
            Sql.Append("Pagamento.IdPessoa AS IdPessoa,");
            Sql.Append("Pagamento.Descricao AS Descricao,");
            Sql.Append("Pagamento.Valor AS Valor,");
            Sql.Append("Pagamento.DataPagamento AS DataPagamento,");
            Sql.Append("Pagamento.DataVencimento AS DataVencimento,");

            Sql.Append("TipoPagamento.Id AS Id,");
            Sql.Append("TipoPagamento.Descricao AS Descricao,");

            Sql.Append("Empresa.Id AS Id,");
            Sql.Append("Empresa.Nome AS Nome,");
            Sql.Append("Empresa.Logo AS Logo,");

            Sql.Append("Pessoa.Id AS Id,");
            Sql.Append("Pessoa.Nome AS Nome,");
            Sql.Append("Pessoa.ImagemPerfil AS ImagemPerfil ");

            Sql.Append("FROM Pagamento ");
            Sql.Append("INNER JOIN TipoPagamento ON Pagamento.IdTipoPagamento = TipoPagamento.Id ");
            Sql.Append("INNER JOIN Empresa ON Pagamento.IdEmpresa = Empresa.Id ");
            Sql.Append("INNER JOIN Pessoa ON Pagamento.IdPessoa = Pessoa.Id ");

            Sql.Append("ORDER BY Pagamento.Id ASC ");

            return _ctx.SQLServerConexao.Query<PagamentoQueryResult,
                                               TipoPagamentoQueryResult,
                                               EmpresaQueryResult,
                                               PessoaQueryResult,
                                               PagamentoQueryResult>(
                    Sql.ToString(),
                    map: (pagamento, tipoPagamento, empresa, pessoa) =>
                    {
                        pagamento.TipoPagamento = tipoPagamento;
                        pagamento.Empresa = empresa;
                        pagamento.Pessoa = pessoa;
                        return pagamento;
                    },
                    splitOn: "Id, Id, Id, Id").Distinct().ToList();
        }

        public bool CheckId(int id)
        {
            Sql.Clear();
            Sql.Append("SELECT Id ");
            Sql.Append("FROM Pagamento ");
            Sql.Append("where Id = @Id ");

            return _ctx.SQLServerConexao.Query<bool>(Sql.ToString(), new { Id = id }).FirstOrDefault();
        }

        public int LocalizarMaxId()
        {
            Sql.Clear();
            Sql.Append("SELECT MAX(Id) ");
            Sql.Append("FROM Pagamento");

            return _ctx.SQLServerConexao.Query<int>(Sql.ToString()).FirstOrDefault();
        }
    }
}
