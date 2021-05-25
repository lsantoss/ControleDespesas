using ControleDespesas.Domain.Usuarios.Entities;
using ControleDespesas.Domain.Usuarios.Query.Results;
using System.Collections.Generic;

namespace ControleDespesas.Domain.Usuarios.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        int Salvar(Usuario usuario);
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