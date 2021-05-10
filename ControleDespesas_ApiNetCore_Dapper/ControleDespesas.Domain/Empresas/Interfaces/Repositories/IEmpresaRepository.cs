using ControleDespesas.Domain.Empresas.Entities;
using ControleDespesas.Domain.Empresas.Query.Results;
using System.Collections.Generic;

namespace ControleDespesas.Domain.Empresas.Interfaces.Repositories
{
    public interface IEmpresaRepository
    {
        int Salvar(Empresa empresa);
        void Atualizar(Empresa empresa);
        void Deletar(int id);

        EmpresaQueryResult Obter(int id);
        List<EmpresaQueryResult> Listar();

        bool CheckId(int id);
        int LocalizarMaxId();
    }
}