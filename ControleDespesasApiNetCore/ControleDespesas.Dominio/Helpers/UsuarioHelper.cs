using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Enums;
using ControleDespesas.Dominio.Query.Usuario;
using LSCode.Validador.ValueObjects;
using System;

namespace ControleDespesas.Dominio.Helpers
{
    public static class UsuarioHelper
    {
        public static Usuario GerarEntidade(AdicionarUsuarioCommand command)
        {
            try
            {
                Descricao50Caracteres login = new Descricao50Caracteres(command.Login, "Login");
                Descricao50Caracteres senha = new Descricao50Caracteres(command.Senha, "Senha");
                EPrivilegioUsuario privilegio = command.Privilegio;

                Usuario usuario = new Usuario(0, login, senha, privilegio);
                return usuario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static Usuario GerarEntidade(AtualizarUsuarioCommand command)
        {
            try
            {
                int id = command.Id;
                Descricao50Caracteres login = new Descricao50Caracteres(command.Login, "Login");
                Descricao50Caracteres senha = new Descricao50Caracteres(command.Senha, "Senha");
                EPrivilegioUsuario privilegio = command.Privilegio;

                Usuario usuario = new Usuario(id, login, senha, privilegio);
                return usuario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static object GerarDadosRetornoCommandResult(Usuario usuario)
        {
            try
            {
                return new
                {
                    Id = usuario.Id,
                    Login = usuario.Login.ToString(),
                    Senha = usuario.Senha.ToString(),
                    Privilegio = usuario.Privilegio
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static object GerarDadosRetornoCommandResult(UsuarioQueryResult usuario)
        {
            try
            {
                return new
                {
                    Id = usuario.Id,
                    Login = usuario.Login.ToString(),
                    Senha = usuario.Senha.ToString(),
                    Privilegio = usuario.Privilegio
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}