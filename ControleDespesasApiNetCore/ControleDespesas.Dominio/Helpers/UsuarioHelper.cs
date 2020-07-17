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
                Texto login = new Texto(command.Login, "Login", 50);
                Texto senha = new Texto(command.Senha, "Senha", 50);
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
                Texto login = new Texto(command.Login, "Login", 50);
                Texto senha = new Texto(command.Senha, "Senha", 50);
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