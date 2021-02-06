﻿using ControleDespesas.Domain.Entities;
using ControleDespesas.Domain.Query.Pessoa;
using System.Collections.Generic;

namespace ControleDespesas.Domain.Interfaces.Repositorio
{
    public interface IPessoaRepositorio
    {
        Pessoa Salvar(Pessoa pessoa);
        void Atualizar(Pessoa pessoa);
        void Deletar(int id);

        PessoaQueryResult Obter(int id);
        List<PessoaQueryResult> Listar(int idUsuario);

        bool CheckId(int id);
        int LocalizarMaxId();
    }
}