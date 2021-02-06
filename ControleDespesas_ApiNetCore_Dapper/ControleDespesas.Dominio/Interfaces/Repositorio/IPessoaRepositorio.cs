using ControleDespesas.Dominio.Entities;
using ControleDespesas.Dominio.Query.Pessoa;
using System.Collections.Generic;

namespace ControleDespesas.Dominio.Interfaces.Repositorio
{
    public interface IPessoaRepositorio
    {
        Pessoa Salvar(Pessoa pessoa);
        void Atualizar(Pessoa pessoa);
        void Deletar(int id);

        PessoaQueryResult Obter(int id);
        List<PessoaQueryResult> Listar(int idUsuario);

        bool CheckId(int id);
        int LocalizarMaxId();
    }
}