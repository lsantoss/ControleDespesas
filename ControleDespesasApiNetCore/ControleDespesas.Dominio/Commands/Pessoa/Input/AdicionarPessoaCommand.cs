﻿using LSCode.Facilitador.Api.InterfacesCommand;
using LSCode.Validador.ValidacoesNotificacoes;
using System;
using System.ComponentModel.DataAnnotations;

namespace ControleDespesas.Dominio.Commands.Pessoa.Input
{
    public class AdicionarPessoaCommand : Notificadora, CommandPadrao
    {
        [Required]
        public string Nome { get; set; }

        [Required]
        public string ImagemPerfil { get; set; }

        public bool ValidarCommand()
        {
            try
            {
                AddNotificacao(new ContratoValidacao().NaoEhNuloOuVazio(Nome, "Nome", "Nome é um campo obrigatório"));
                AddNotificacao(new ContratoValidacao().TamanhoMaximo(Nome, 100, "Nome", "Nome maior que o esperado"));

                AddNotificacao(new ContratoValidacao().NaoEhNuloOuVazio(ImagemPerfil, "Imagem de Perfil", "Imagem de Perfil é um campo obrigatório"));

                return Valido;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}