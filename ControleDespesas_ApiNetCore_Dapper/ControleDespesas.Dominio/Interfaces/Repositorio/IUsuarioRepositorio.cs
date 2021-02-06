using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Usuario;
using System.Collections.Generic;

namespace ControleDespesas.Dominio.Interfaces.Repositorio
{
    public interface IUsuarioRepositorio
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