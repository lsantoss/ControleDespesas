﻿namespace ControleDespesas.Domain.Commands.Pessoa.Output
{
    public class PessoaCommandOutput
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public string Nome { get; set; }
        public string ImagemPerfil { get; set; }
    }
}