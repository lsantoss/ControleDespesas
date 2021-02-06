using ControleDespesas.Domain.Commands.Usuario.Input;
using ControleDespesas.Domain.Commands.Usuario.Output;
using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Enums;
using System;

namespace ControleDespesas.Domain.Helpers
{
    public static class UsuarioHelper
    {
        public static Usuario GerarEntidade(AdicionarUsuarioCommand command)
        {
            try
            {
                string login = command.Login;
                string senha = command.Senha;
                EPrivilegioUsuario privilegio = command.Privilegio;

                Usuario usuario = new Usuario(0, login, senha, privilegio);
                usuario.Validar();
                return usuario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Usuario GerarEntidade(AtualizarUsuarioCommand command)
        {
            try
            {
                int id = command.Id;
                string login = command.Login;
                string senha = command.Senha;
                EPrivilegioUsuario privilegio = command.Privilegio;

                Usuario usuario = new Usuario(id, login, senha, privilegio);
                usuario.Validar();
                return usuario;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static AdicionarUsuarioCommandOutput GerarDadosRetornoInsert(Usuario usuario)
        {
            try 
            {
                return new AdicionarUsuarioCommandOutput
                {
                    Id = usuario.Id,
                    Login = usuario.Login,
                    Senha = usuario.Senha,
                    Privilegio = usuario.Privilegio
                };
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static AtualizarUsuarioCommandOutput GerarDadosRetornoUpdate(Usuario usuario)
        {
            try
            {
                return new AtualizarUsuarioCommandOutput
                {
                    Id = usuario.Id,
                    Login = usuario.Login,
                    Senha = usuario.Senha,
                    Privilegio = usuario.Privilegio
                };
            }
            catch (Exception e)
            {
                throw e;
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
                throw e;
            }
        }
    }
}