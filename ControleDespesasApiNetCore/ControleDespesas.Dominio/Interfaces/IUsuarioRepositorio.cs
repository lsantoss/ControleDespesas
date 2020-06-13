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

        UsuarioQueryResult ObterUsuario(int id);
        List<UsuarioQueryResult> ListarUsuarios();

        bool CheckId(int id);
        int LocalizarMaxId();
    }
}