using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Empresa;
using ControleDespesas.Dominio.Query.Pagamento;
using ControleDespesas.Dominio.Query.Pessoa;
using ControleDespesas.Dominio.Query.TipoPagamento;
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
    public class PagamentoRepositorio : IPagamentoRepositorio
    {
        private readonly DynamicParameters _parametros = new DynamicParameters();
        private readonly DataContext _ctx;

        public PagamentoRepositorio(IOptions<SettingsInfraData> options)
        {
            _ctx = new DataContext(EBancoDadosRelacional.SQLServer, options.Value.ConnectionString);
        }

        public Pagamento Salvar(Pagamento pagamento)
        {
            try
            {
                _parametros.Add("IdTipoPagamento", pagamento.TipoPagamento.Id, DbType.Int32);
                _parametros.Add("IdEmpresa", pagamento.Empresa.Id, DbType.Int32);
                _parametros.Add("IdPessoa", pagamento.Pessoa.Id, DbType.Int32);
                _parametros.Add("Descricao", pagamento.Descricao, DbType.String);
                _parametros.Add("Valor", pagamento.Valor, DbType.Double);
                _parametros.Add("DataVencimento", pagamento.DataVencimento, DbType.Date);
                _parametros.Add("DataPagamento", pagamento.DataPagamento, DbType.Date);
                _parametros.Add("ArquivoPagamento", pagamento.ArquivoPagamento, DbType.String);
                _parametros.Add("ArquivoComprovante", pagamento.ArquivoComprovante, DbType.String);

                pagamento.Id = _ctx.SQLServerConexao.ExecuteScalar<int>(PagamentoQueries.Salvar, _parametros);
                return pagamento;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Atualizar(Pagamento pagamento)
        {
            try
            {
                _parametros.Add("Id", pagamento.Id, DbType.Int32);
                _parametros.Add("IdTipoPagamento", pagamento.TipoPagamento.Id, DbType.Int32);
                _parametros.Add("IdEmpresa", pagamento.Empresa.Id, DbType.Int32);
                _parametros.Add("IdPessoa", pagamento.Pessoa.Id, DbType.Int32);
                _parametros.Add("Descricao", pagamento.Descricao, DbType.String);
                _parametros.Add("Valor", pagamento.Valor, DbType.Double);
                _parametros.Add("DataVencimento", pagamento.DataVencimento, DbType.Date);
                _parametros.Add("DataPagamento", pagamento.DataPagamento, DbType.Date);
                _parametros.Add("ArquivoPagamento", pagamento.ArquivoPagamento, DbType.String);
                _parametros.Add("ArquivoComprovante", pagamento.ArquivoComprovante, DbType.String);

                _ctx.SQLServerConexao.Execute(PagamentoQueries.Atualizar, _parametros);
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

                _ctx.SQLServerConexao.Execute(PagamentoQueries.Deletar, _parametros);
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
                _parametros.Add("Id", id, DbType.Int32);

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
                        _parametros,
                        splitOn: "Id, Id, Id, Id").FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PagamentoQueryResult> Listar(int idPessoa)
        {
            try
            {
                _parametros.Add("IdPessoa", idPessoa, DbType.Int32);

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
                    _parametros,
                    splitOn: "Id, Id, Id, Id").Distinct().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PagamentoQueryResult> ListarPagamentoConcluido(int idPessoa)
        {
            try
            {
                _parametros.Add("IdPessoa", idPessoa, DbType.Int32);

                return _ctx.SQLServerConexao.Query<PagamentoQueryResult,
                                                   TipoPagamentoQueryResult,
                                                   EmpresaQueryResult,
                                                   PessoaQueryResult,
                                                   PagamentoQueryResult>(
                    PagamentoQueries.ListarPagamentoConcluido,
                    map: (pagamento, tipoPagamento, empresa, pessoa) =>
                    {
                        pagamento.TipoPagamento = tipoPagamento;
                        pagamento.Empresa = empresa;
                        pagamento.Pessoa = pessoa;
                        return pagamento;
                    },
                    _parametros,
                    splitOn: "Id, Id, Id, Id").Distinct().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<PagamentoQueryResult> ListarPagamentoPendente(int idPessoa)
        {
            try
            {
                _parametros.Add("IdPessoa", idPessoa, DbType.Int32);

                return _ctx.SQLServerConexao.Query<PagamentoQueryResult,
                                                   TipoPagamentoQueryResult,
                                                   EmpresaQueryResult,
                                                   PessoaQueryResult,
                                                   PagamentoQueryResult>(
                    PagamentoQueries.ListarPagamentoPendente,
                    map: (pagamento, tipoPagamento, empresa, pessoa) =>
                    {
                        pagamento.TipoPagamento = tipoPagamento;
                        pagamento.Empresa = empresa;
                        pagamento.Pessoa = pessoa;
                        return pagamento;
                    },
                    _parametros,
                    splitOn: "Id, Id, Id, Id").Distinct().ToList();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PagamentoArquivoPagamentoQueryResult ObterArquivoPagamento(int idPagamento)
        {
            try
            {
                _parametros.Add("IdPagamento", idPagamento, DbType.Int32);

                return _ctx.SQLServerConexao.Query<PagamentoArquivoPagamentoQueryResult>(PagamentoQueries.ObterArquivoPagamento, _parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PagamentoArquivoComprovanteQueryResult ObterArquivoComprovante(int idPagamento)
        {
            try
            {
                _parametros.Add("IdPagamento", idPagamento, DbType.Int32);

                return _ctx.SQLServerConexao.Query<PagamentoArquivoComprovanteQueryResult>(PagamentoQueries.ObterArquivoComprovante, _parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PagamentoGastosQueryResult CalcularValorGastoTotal(int idPessoa)
        {
            try
            {
                _parametros.Add("IdPessoa", idPessoa, DbType.Int32);

                return _ctx.SQLServerConexao.Query<PagamentoGastosQueryResult>(PagamentoQueries.CalcularValorGastoTotal, _parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PagamentoGastosQueryResult CalcularValorGastoAno(int idPessoa, int ano)
        {
            try
            {
                _parametros.Add("IdPessoa", idPessoa, DbType.Int32);
                _parametros.Add("Ano", ano, DbType.Int32);

                return _ctx.SQLServerConexao.Query<PagamentoGastosQueryResult>(PagamentoQueries.CalcularValorGastoAno, _parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public PagamentoGastosQueryResult CalcularValorGastoAnoMes(int idPessoa, int ano, int mes)
        {
            try
            {
                _parametros.Add("IdPessoa", idPessoa, DbType.Int32);
                _parametros.Add("Ano", ano, DbType.Int32);
                _parametros.Add("Mes", mes, DbType.Int32);

                return _ctx.SQLServerConexao.Query<PagamentoGastosQueryResult>(PagamentoQueries.CalcularValorGastoAnoMes, _parametros).FirstOrDefault();
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

                return _ctx.SQLServerConexao.Query<bool>(PagamentoQueries.CheckId, _parametros).FirstOrDefault();
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