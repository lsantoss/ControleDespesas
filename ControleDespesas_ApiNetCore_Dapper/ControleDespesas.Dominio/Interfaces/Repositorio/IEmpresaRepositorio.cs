﻿using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Query.Empresa;
using System.Collections.Generic;

namespace ControleDespesas.Domain.Interfaces.Repositorio
{
    public interface IEmpresaRepositorio
    {
        Empresa Salvar(Empresa empresa);
        void Atualizar(Empresa empresa);
        void Deletar(int id);

        EmpresaQueryResult Obter(int id);
        List<EmpresaQueryResult> Listar();

        bool CheckId(int id);
        int LocalizarMaxId();
    }
}