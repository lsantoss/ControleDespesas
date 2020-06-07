﻿using ControleDespesas.Infra.Data.DataContext;
using ControleDespesas.Infra.Data.Repositorio;
using System;

namespace ControleDespesas.Infra.Data.Factory
{
    public class DbFactory
    {
        private static DbFactory _instance = null;
        private DbContext _ctx;

        public PessoaRepositorio PessoaRepositorio { get; set; }
        public EmpresaRepositorio EmpresaRepositorio { get; set; }
        public TipoPagamentoRepositorio TipoPagamentoRepositorio { get; set; }
        public PagamentoRepositorio PagamentoRepositorio { get; set; }

        private DbFactory()
        {
            try
            {
                InstanciarDbContext();

                PessoaRepositorio = new PessoaRepositorio(_ctx);
                EmpresaRepositorio = new EmpresaRepositorio(_ctx);
                TipoPagamentoRepositorio = new TipoPagamentoRepositorio(_ctx);
                PagamentoRepositorio = new PagamentoRepositorio(_ctx);
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

        private void InstanciarDbContext()
        {
            try
            {
                //string connectionString = System.Configuration.ConfigurationSettings.AppSettings["ConnectionString"];
                string connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=ControleDespesas;Data Source=SANTOS-NOTE\\SQLEXPRESS;";
                _ctx = new DbContext(connectionString);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível conectar ao banco de dados. " + ex.Message);
            }
        }
    }
}