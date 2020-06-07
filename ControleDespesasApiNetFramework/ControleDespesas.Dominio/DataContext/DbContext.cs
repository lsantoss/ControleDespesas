using System;
using System.Data;
using System.Data.SqlClient;

namespace ControleDespesas.Dominio.DataContext
{
    public class DbContext : IDisposable
    {
        public SqlConnection SQLServerConexao { get; set; }

        public DbContext(string stringConexao)
        {
            SQLServerConexao = new SqlConnection(stringConexao);
            SQLServerConexao.Open();
        }

        public void Dispose()
        {
            if (SQLServerConexao.State != ConnectionState.Closed)
                SQLServerConexao.Close();
        }
    }
}