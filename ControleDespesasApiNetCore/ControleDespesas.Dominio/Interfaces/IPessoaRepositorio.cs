using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Pessoa;
using System.Collections.Generic;

namespace ControleDespesas.Dominio.Interfaces
{
    public interface IPessoaRepositorio
    {
        void Salvar(Pessoa pessoa);
        void Atualizar(Pessoa pessoa);
        void Deletar(int id);

        PessoaQueryResult Obter(int id);
        List<PessoaQueryResult> Listar();

        bool CheckId(int id);
        int LocalizarMaxId();
    }
}