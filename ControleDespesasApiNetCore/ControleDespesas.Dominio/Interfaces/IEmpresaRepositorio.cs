using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Empresa;
using System.Collections.Generic;

namespace ControleDespesas.Dominio.Interfaces
{
    public interface IEmpresaRepositorio
    {
        string Salvar(Empresa empresa);
        string Atualizar(Empresa empresa);
        string Deletar(int id);

        EmpresaQueryResult Obter(int id);
        List<EmpresaQueryResult> Listar();

        bool CheckId(int id);
        int LocalizarMaxId();
    }
}