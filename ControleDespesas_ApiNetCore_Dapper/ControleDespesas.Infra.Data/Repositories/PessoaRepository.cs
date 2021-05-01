using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.Pessoa.Results;
using ControleDespesas.Infra.Data.Repositories.Queries;
using ControleDespesas.Infra.Settings;
using Dapper;
using LSCode.ConexoesBD.DataContexts;
using LSCode.ConexoesBD.Enums;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ControleDespesas.Infra.Data.Repositories
{
    public class PessoaRepository : IPessoaRepository
    {
        private readonly DynamicParameters _parametros = new DynamicParameters();
        private readonly DataContext _dataContext;

        public PessoaRepository(SettingsInfraData settings)
        {
            _dataContext = new DataContext(EBancoDadosRelacional.SQLServer, settings.ConnectionString);
        }

        public int Salvar(Pessoa pessoa)
        {
            _parametros.Add("IdUsuario", pessoa.Usuario.Id, DbType.Int32);
            _parametros.Add("Nome", pessoa.Nome, DbType.String);
            _parametros.Add("ImagemPerfil", pessoa.ImagemPerfil, DbType.String);

            return _dataContext.SQLServerConexao.ExecuteScalar<int>(PessoaQueries.Salvar, _parametros);
        }

        public void Atualizar(Pessoa pessoa)
        {
                _parametros.Add("Id", pessoa.Id, DbType.Int32);
                _parametros.Add("IdUsuario", pessoa.Usuario.Id, DbType.Int32);
                _parametros.Add("Nome", pessoa.Nome, DbType.String);
                _parametros.Add("ImagemPerfil", pessoa.ImagemPerfil, DbType.String);

                _dataContext.SQLServerConexao.Execute(PessoaQueries.Atualizar, _parametros);
        }

        public void Deletar(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            _dataContext.SQLServerConexao.Execute(PessoaQueries.Deletar, _parametros);
        }

        public PessoaQueryResult Obter(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            return _dataContext.SQLServerConexao.Query<PessoaQueryResult>(PessoaQueries.Obter, _parametros).FirstOrDefault();
        }

        public List<PessoaQueryResult> Listar(int idUsuario)
        {
            _parametros.Add("IdUsuario", idUsuario, DbType.Int32);

            return _dataContext.SQLServerConexao.Query<PessoaQueryResult>(PessoaQueries.Listar, _parametros).ToList();
        }

        public bool CheckId(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            return _dataContext.SQLServerConexao.Query<bool>(PessoaQueries.CheckId, _parametros).FirstOrDefault();
        }

        public int LocalizarMaxId()
        {
            return _dataContext.SQLServerConexao.Query<int>(PessoaQueries.LocalizarMaxId).FirstOrDefault();
        }
    }
}