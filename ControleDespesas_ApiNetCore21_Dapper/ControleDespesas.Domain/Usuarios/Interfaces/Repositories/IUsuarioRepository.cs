using ControleDespesas.Domain.Usuarios.Entities;
using ControleDespesas.Domain.Usuarios.Query.Results;
using System.Collections.Generic;

namespace ControleDespesas.Domain.Usuarios.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        long Salvar(Usuario usuario);
        void Atualizar(Usuario usuario);
        void Deletar(long id);

        UsuarioQueryResult Obter(long id);
        UsuarioQueryResult ObterContendoRegistrosFilhos(long id);

        List<UsuarioQueryResult> Listar();
        List<UsuarioQueryResult> ListarContendoRegistrosFilhos();

        UsuarioQueryResult Logar(string login, string senha);

        bool CheckLogin(string login);
        bool CheckId(long id);
        long LocalizarMaxId();
    }
}