using ControleDespesas.Domain.Pessoas.Entities;
using ControleDespesas.Domain.Pessoas.Query.Results;
using System.Collections.Generic;

namespace ControleDespesas.Domain.Pessoas.Interfaces.Repositories
{
    public interface IPessoaRepository
    {
        int Salvar(Pessoa pessoa);
        void Atualizar(Pessoa pessoa);
        void Deletar(int id);

        PessoaQueryResult Obter(int id);
        List<PessoaQueryResult> Listar(int idUsuario);

        bool CheckId(int id);
        int LocalizarMaxId();
    }
}