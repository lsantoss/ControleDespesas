﻿using LSCode.Facilitador.Api.Interfaces.Commands;
using LSCode.Validador.ValidacoesNotificacoes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDespesas.Dominio.Commands.Pessoa.Input
{
    public class AtualizarPessoaCommand : Notificadora, CommandPadrao
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public int IdUsuario { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string ImagemPerfil { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                if (Id <= 0)
                    AddNotificacao("Id", "Id não é valido");

                if (IdUsuario <= 0)
                    AddNotificacao("IdUsuario", "IdUsuario não é valido");

                if (string.IsNullOrEmpty(Nome))
                    AddNotificacao("Nome", "Nome é um campo obrigatório");
                else if (Nome.Length > 100)
                    AddNotificacao("Nome", "Nome maior que o esperado");

                if (string.IsNullOrEmpty(ImagemPerfil))
                    AddNotificacao("ImagemPerfil", "ImagemPerfil é um campo obrigatório");

                return Valido;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}