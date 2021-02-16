using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Query.Usuario.Results;
using System.Collections.Generic;

namespace ControleDespesas.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario Salvar(Usuario usuario);
        void Atualizar(Usuario usuario);
        void Deletar(int id);

        UsuarioQueryResult Obter(int id);
        List<UsuarioQueryResult> Listar();

        UsuarioQueryResult Logar(string login, string senha);

        bool CheckLogin(string login);
        bool CheckId(int id);
        int LocalizarMaxId();
    }
}