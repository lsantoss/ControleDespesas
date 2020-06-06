using ControleDespesas.Infra.Data.Repositorio;
using LSCode.ConexoesBD.DbContext;
using LSCode.ConexoesBD.Enums;
using System;
using System.Data;

namespace ControleDespesas.Infra.Data.Factory
{
    public class DbFactory
    {
        private static DbFactory _instance = null;
        private DbContext _ctx;

        public PessoaRepositorio PessoaRepositorio { get; set; }

        private DbFactory()
        {
            try
            {
                Conectar();

                PessoaRepositorio = new PessoaRepositorio(_ctx);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível instanciar a DbFactory. " + ex.Message);
            }
        }

        public static DbFactory Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DbFactory();

                return _instance;
            }
        }

        private void Conectar()
        {
            try
            {
                //string connectionString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
                string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ControleDespesas;Data Source=SANTOS-PC\\SQLEXPRESS;";
                _ctx = new DbContext(EBancoDadosRelacional.SQLServer, connectionString);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível conectar ao banco de dados. " + ex.Message);
            }
            finally
            {
                if (_ctx.SQLServerConexao.State != ConnectionState.Closed)
                    _ctx.SQLServerConexao.Close();
            }
        }
    }
}