using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Interfaces.Repositories;
using ControleDespesas.Domain.Query.Empresa.Results;
using ControleDespesas.Infra.Data.Queries;
using ControleDespesas.Infra.Settings;
using Dapper;
using LSCode.ConexoesBD.DataContexts;
using LSCode.ConexoesBD.Enums;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace ControleDespesas.Infra.Data.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly DynamicParameters _parametros = new DynamicParameters();
        private readonly DataContext _dataContext;

        public EmpresaRepository(SettingsInfraData settings)
        {
            _dataContext = new DataContext(EBancoDadosRelacional.SQLServer, settings.ConnectionString);
        }

        public Empresa Salvar(Empresa empresa)
        {
            _parametros.Add("Nome", empresa.Nome, DbType.String);
            _parametros.Add("Logo", empresa.Logo, DbType.String);

            empresa.Id = _dataContext.SQLServerConexao.ExecuteScalar<int>(EmpresaQueries.Salvar, _parametros);
            return empresa;
        }

        public void Atualizar(Empresa empresa)
        {
            _parametros.Add("Id", empresa.Id, DbType.Int32);
            _parametros.Add("Nome", empresa.Nome, DbType.String);
            _parametros.Add("Logo", empresa.Logo, DbType.String);

            _dataContext.SQLServerConexao.Execute(EmpresaQueries.Atualizar, _parametros);
        }

        public void Deletar(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);                

            _dataContext.SQLServerConexao.Execute(EmpresaQueries.Deletar, _parametros);
        }

        public EmpresaQueryResult Obter(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            return _dataContext.SQLServerConexao.Query<EmpresaQueryResult>(EmpresaQueries.Obter, _parametros).FirstOrDefault();
        }

        public List<EmpresaQueryResult> Listar()
        {
            return _dataContext.SQLServerConexao.Query<EmpresaQueryResult>(EmpresaQueries.Listar).ToList();
        }

        public bool CheckId(int id)
        {
            _parametros.Add("Id", id, DbType.Int32);

            return _dataContext.SQLServerConexao.Query<bool>(EmpresaQueries.CheckId, _parametros).FirstOrDefault();
        }

        public int LocalizarMaxId()
        {
            return _dataContext.SQLServerConexao.Query<int>(EmpresaQueries.LocalizarMaxId).FirstOrDefault();
        }
    }
}