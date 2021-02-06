using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Pessoa;
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
    public class PessoaRepositorio : IPessoaRepositorio
    {
        private readonly DynamicParameters _parametros = new DynamicParameters();
        private readonly DataContext _ctx;

        public PessoaRepositorio(IOptions<SettingsInfraData> options)
        {
            _ctx = new DataContext(EBancoDadosRelacional.SQLServer, options.Value.ConnectionString);
        }

        public Pessoa Salvar(Pessoa pessoa)
        {
            try
            {
                _parametros.Add("IdUsuario", pessoa.Usuario.Id, DbType.Int32);
                _parametros.Add("Nome", pessoa.Nome, DbType.String);
                _parametros.Add("ImagemPerfil", pessoa.ImagemPerfil, DbType.String);

                pessoa.Id = _ctx.SQLServerConexao.ExecuteScalar<int>(PessoaQueries.Salvar, _parametros);
                return pessoa;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Atualizar(Pessoa pessoa)
        {
            try
            {
                _parametros.Add("Id", pessoa.Id, DbType.Int32);
                _parametros.Add("IdUsuario", pessoa.Usuario.Id, DbType.Int32);
                _parametros.Add("Nome", pessoa.Nome, DbType.String);
                _parametros.Add("ImagemPerfil", pessoa.ImagemPerfil, DbType.String);

                _ctx.SQLServerConexao.Execute(PessoaQueries.Atualizar, _parametros);
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

                _ctx.SQLServerConexao.Execute(PessoaQueries.Deletar, _parametros);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PessoaQueryResult Obter(int id)
        {
            try
            {
                _parametros.Add("Id", id, DbType.Int32);

                return _ctx.SQLServerConexao.Query<PessoaQueryResult>(PessoaQueries.Obter, _parametros).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<PessoaQueryResult> Listar(int idUsuario)
        {
            try
            {
                _parametros.Add("IdUsuario", idUsuario, DbType.Int32);

                return _ctx.SQLServerConexao.Query<PessoaQueryResult>(PessoaQueries.Listar, _parametros).ToList();
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

                return _ctx.SQLServerConexao.Query<bool>(PessoaQueries.CheckId, _parametros).FirstOrDefault();
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
                return _ctx.SQLServerConexao.Query<int>(PessoaQueries.LocalizarMaxId).FirstOrDefault();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}