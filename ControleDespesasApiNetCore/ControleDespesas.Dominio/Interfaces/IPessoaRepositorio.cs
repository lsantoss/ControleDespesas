using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Pessoa;
using System.Collections.Generic;

namespace ControleDespesas.Dominio.Interfaces
{
    public interface IPessoaRepositorio
    {
        string Salvar(Pessoa pessoa);
        string Atualizar(Pessoa pessoa);
        string Deletar(int id);

        PessoaQueryResult ObterPessoa(int id);
        List<PessoaQueryResult> ListarPessoas();

        bool CheckId(int id);
        int LocalizarMaxId();
    }
}