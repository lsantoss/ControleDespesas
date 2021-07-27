using ControleDespesas.Domain.Pessoas.Entities;
using ControleDespesas.Domain.Pessoas.Query.Results;
using System.Collections.Generic;

namespace ControleDespesas.Domain.Pessoas.Interfaces.Repositories
{
    public interface IPessoaRepository
    {
        long Salvar(Pessoa pessoa);
        void Atualizar(Pessoa pessoa);
        void Deletar(long id, long idUsuario);

        PessoaQueryResult Obter(long id, long idUsuario);
        PessoaQueryResult ObterContendoRegistrosFilhos(long id, long idUsuario);

        List<PessoaQueryResult> Listar(long idUsuario);
        List<PessoaQueryResult> ListarContendoRegistrosFilhos(long idUsuario);

        bool CheckId(long id);
        long LocalizarMaxId();
    }
}