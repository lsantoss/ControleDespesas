using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.Empresa.Results;
using ControleDespesas.Domain.Query.Pagamento.Results;
using ControleDespesas.Domain.Query.Pessoa.Results;
using ControleDespesas.Domain.Query.TipoPagamento.Results;
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
    public class PagamentoRepository : IPagamentoRepository
    {
        private readonly DynamicParameters _parametros = new DynamicParameters();
        private readonly DataContext _dataContext;

        public PagamentoRepository(SettingsInfraData settings)
        {
            _dataContext = new DataContext(EBancoDadosRelacional.SQLServer, settings.ConnectionString);
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

                pagamento.Id = _dataContext.SQLServerConexao.ExecuteScalar<int>(PagamentoQueries.Salvar, _parametros);
                return pagamento;
            }
            catch (Exception e)
            {
                throw e;
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

                _dataContext.SQLServerConexao.Execute(PagamentoQueries.Atualizar, _parametros);
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

                _dataContext.SQLServerConexao.Execute(PagamentoQueries.Deletar, _parametros);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PagamentoQueryResult Obter(int id)
        {
            try
            {
                _parametros.Add("Id", id, DbType.Int32);

                return _dataContext.SQLServerConexao.Query<PagamentoQueryResult,
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
                throw e;
            }
        }

        public List<PagamentoQueryResult> Listar(int idPessoa)
        {
            try
            {
                _parametros.Add("IdPessoa", idPessoa, DbType.Int32);

                return _dataContext.SQLServerConexao.Query<PagamentoQueryResult,
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
                throw e;
            }
        }

        public List<PagamentoQueryResult> ListarPagamentoConcluido(int idPessoa)
        {
            try
            {
                _parametros.Add("IdPessoa", idPessoa, DbType.Int32);

                return _dataContext.SQLServerConexao.Query<PagamentoQueryResult,
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
                throw e;
            }
        }

        public List<PagamentoQueryResult> ListarPagamentoPendente(int idPessoa)
        {
            try
            {
                _parametros.Add("IdPessoa", idPessoa, DbType.Int32);

                return _dataContext.SQLServerConexao.Query<PagamentoQueryResult,
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
                throw e;
            }
        }

        public PagamentoArquivoQueryResult ObterArquivoPagamento(int idPagamento)
        {
            try
            {
                _parametros.Add("IdPagamento", idPagamento, DbType.Int32);

                return _dataContext.SQLServerConexao.Query<PagamentoArquivoQueryResult>(PagamentoQueries.ObterArquivoPagamento, _parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PagamentoArquivoQueryResult ObterArquivoComprovante(int idPagamento)
        {
            try
            {
                _parametros.Add("IdPagamento", idPagamento, DbType.Int32);

                return _dataContext.SQLServerConexao.Query<PagamentoArquivoQueryResult>(PagamentoQueries.ObterArquivoComprovante, _parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PagamentoGastosQueryResult CalcularValorGastoTotal(int idPessoa)
        {
            try
            {
                _parametros.Add("IdPessoa", idPessoa, DbType.Int32);

                return _dataContext.SQLServerConexao.Query<PagamentoGastosQueryResult>(PagamentoQueries.CalcularValorGastoTotal, _parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PagamentoGastosQueryResult CalcularValorGastoAno(int idPessoa, int ano)
        {
            try
            {
                _parametros.Add("IdPessoa", idPessoa, DbType.Int32);
                _parametros.Add("Ano", ano, DbType.Int32);

                return _dataContext.SQLServerConexao.Query<PagamentoGastosQueryResult>(PagamentoQueries.CalcularValorGastoAno, _parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PagamentoGastosQueryResult CalcularValorGastoAnoMes(int idPessoa, int ano, int mes)
        {
            try
            {
                _parametros.Add("IdPessoa", idPessoa, DbType.Int32);
                _parametros.Add("Ano", ano, DbType.Int32);
                _parametros.Add("Mes", mes, DbType.Int32);

                return _dataContext.SQLServerConexao.Query<PagamentoGastosQueryResult>(PagamentoQueries.CalcularValorGastoAnoMes, _parametros).FirstOrDefault();
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

                return _dataContext.SQLServerConexao.Query<bool>(PagamentoQueries.CheckId, _parametros).FirstOrDefault();
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
                return _dataContext.SQLServerConexao.Query<int>(PagamentoQueries.LocalizarMaxId).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}