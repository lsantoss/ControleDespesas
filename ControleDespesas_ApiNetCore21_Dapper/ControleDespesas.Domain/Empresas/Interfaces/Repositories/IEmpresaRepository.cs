using ControleDespesas.Domain.Empresas.Entities;
using ControleDespesas.Domain.Empresas.Query.Results;
using System.Collections.Generic;

namespace ControleDespesas.Domain.Empresas.Interfaces.Repositories
{
    public interface IEmpresaRepository
    {
        long Salvar(Empresa empresa);
        void Atualizar(Empresa empresa);
        void Deletar(long id);

        EmpresaQueryResult Obter(long id);
        List<EmpresaQueryResult> Listar();

        bool CheckId(long id);
        long LocalizarMaxId();
    }
}