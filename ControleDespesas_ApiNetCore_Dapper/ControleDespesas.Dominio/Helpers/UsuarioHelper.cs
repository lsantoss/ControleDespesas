using ControleDespesas.Dominio.Commands.Usuario.Input;
using ControleDespesas.Dominio.Commands.Usuario.Output;
using ControleDespesas.Dominio.Entidades;
using ControleDespesas.Dominio.Enums;
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
                SenhaMedia senha = new SenhaMedia(command.Senha);
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
                SenhaMedia senha = new SenhaMedia(command.Senha);
                EPrivilegioUsuario privilegio = command.Privilegio;

                Usuario usuario = new Usuario(id, login, senha, privilegio);
                return usuario;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static AdicionarUsuarioCommandOutput GerarDadosRetornoInsert(Usuario usuario)
        {
            try 
            {
                return new AdicionarUsuarioCommandOutput
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

        public static AtualizarUsuarioCommandOutput GerarDadosRetornoUpdate(Usuario usuario)
        {
            try
            {
                return new AtualizarUsuarioCommandOutput
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

        public static ApagarUsuarioCommandOutput GerarDadosRetornoDelete(int id)
        {
            try
            {
                return new ApagarUsuarioCommandOutput { Id = id };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}