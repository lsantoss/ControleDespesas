using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Interfaces;
using ControleDespesas.Dominio.Query.Empresa;
using ControleDespesas.Dominio.Query.Pagamento;
using ControleDespesas.Dominio.Query.Pessoa;
using ControleDespesas.Dominio.Query.TipoPagamento;
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
    public class PagamentoRepositorio : IPagamentoRepositorio
    {
        DynamicParameters parametros = new DynamicParameters();
        private readonly DbContext _ctx;

        public PagamentoRepositorio(IOptions<SettingsInfraData> options)
        {
            _ctx = new DbContext(EBancoDadosRelacional.SQLServer, options.Value.ConnectionString);
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

                _ctx.SQLServerConexao.Execute(PagamentoQueries.Salvar, parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
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

                _ctx.SQLServerConexao.Execute(PagamentoQueries.Atualizar, parametros);

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

                _ctx.SQLServerConexao.Execute(PagamentoQueries.Deletar, parametros);

                return "Sucesso";
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PagamentoQueryResult Obter(int id)
        {
            try
            {
                parametros.Add("Id", id, DbType.Int32);

                return _ctx.SQLServerConexao.Query<PagamentoQueryResult,
                                                   TipoPagamentoQueryResult,
                                                   EmpresaQueryResult,
                                                   PessoaQueryResult,
                                                   PagamentoQueryResult>(
                        PagamentoQueries.Obter,
                        map: (pagamento, tipoPagamento, empresa, pessoa) =>
                        {
                            pagamento.TipoPagamento = tipoPagamento;
                            pagamento.Empresa = empresa;
                            pagamento.Pessoa = pessoa;
                            return pagamento;
                        },
                        parametros,
                        splitOn: "Id, Id, Id, Id").FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PagamentoQueryResult> Listar()
        {
            try
            {
                return _ctx.SQLServerConexao.Query<PagamentoQueryResult,
                                               TipoPagamentoQueryResult,
                                               EmpresaQueryResult,
                                               PessoaQueryResult,
                                               PagamentoQueryResult>(
                    PagamentoQueries.Listar,
                    map: (pagamento, tipoPagamento, empresa, pessoa) =>
                    {
                        pagamento.TipoPagamento = tipoPagamento;
                        pagamento.Empresa = empresa;
                        pagamento.Pessoa = pessoa;
                        return pagamento;
                    },
                    splitOn: "Id, Id, Id, Id").Distinct().ToList();
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

                return _ctx.SQLServerConexao.Query<bool>(PagamentoQueries.CheckId, parametros).FirstOrDefault();
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
                return _ctx.SQLServerConexao.Query<int>(PagamentoQueries.LocalizarMaxId).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
