using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Empresa;
using System.Collections.Generic;

namespace ControleDespesas.Dominio.Repositorio
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