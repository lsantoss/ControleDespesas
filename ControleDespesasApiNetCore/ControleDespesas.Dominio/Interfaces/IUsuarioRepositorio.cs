using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Query.Usuario;
using System.Collections.Generic;

namespace ControleDespesas.Dominio.Interfaces
{
    public interface IUsuarioRepositorio
    {
        string Salvar(Usuario usuario);
        string Atualizar(Usuario usuario);
        string Deletar(int id);

        UsuarioQueryResult Obter(int id);
        List<UsuarioQueryResult> Listar();
        UsuarioQueryResult Logar(string login, string senha);

        bool CheckLogin(string login);
        bool CheckId(int id);
        int LocalizarMaxId();
    }
}