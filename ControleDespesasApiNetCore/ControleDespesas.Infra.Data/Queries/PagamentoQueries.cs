﻿namespace ControleDespesas.Infra.Data.Queries
{
    public static class PagamentoQueries
    {
        public static string Salvar { get; } = @"INSERT INTO Pagamento (
                                                    IdTipoPagamento, 
                                                    IdEmpresa,
                                                    IdPessoa, 
                                                    Descricao,
                                                    Valor, 
                                                    DataPagamento, 
                                                    DataVencimento)
                                                VALUES(
                                                    @IdTipoPagamento, 
                                                    @IdEmpresa,
                                                    @IdPessoa, 
                                                    @Descricao,
                                                    @Valor, 
                                                    @DataPagamento, 
                                                    @DataVencimento)";

        public static string Atualizar { get; } = @"UPDATE Pagamento SET
                                                        IdTipoPagamento = @IdTipoPagamento,
                                                        IdEmpresa = @IdEmpresa,
                                                        IdPessoa = @IdPessoa,
                                                        Descricao = @Descricao,
                                                        Valor = @Valor,
                                                        DataPagamento = @DataPagamento,
                                                        DataVencimento = @DataVencimento 
                                                    WHERE Id = @Id";

        public static string Deletar { get; } = @"DELETE FROM Pagamento WHERE Id = @Id";

        public static string Obter { get; } = @"SELECT 
                                                           Pagamento.IdTipoPagamento AS IdTipoPagamento,,
                                                           Pagamento.IdEmpresa AS IdEmpresa,
                                                           Pagamento.IdPessoa AS IdPessoa,
                                                           Pagamento.Descricao AS Descricao,
                                                           Pagamento.Valor AS Valor,
                                                           Pagamento.DataPagamento AS DataPagamento,
                                                           Pagamento.DataVencimento AS DataVencimento,

                                                           TipoPagamento.Id AS Id,
                                                           TipoPagamento.Descricao AS Descricao, 

                                                           Empresa.Id AS Id,
                                                           Empresa.Nome AS Nome, 
                                                           Empresa.Logo AS Logo,

                                                           Pessoa.Id AS Id, 
                                                           Pessoa.Nome AS Nome,
                                                           Pessoa.ImagemPerfil AS ImagemPerfil

                                                       FROM Pagamento 
                                                       INNER JOIN TipoPagamento ON Pagamento.IdTipoPagamento = TipoPagamento.Id 
                                                       INNER JOIN Empresa ON Pagamento.IdEmpresa = Empresa.Id 
                                                       INNER JOIN Pessoa ON Pagamento.IdPessoa = Pessoa.Id 

                                                       WHERE Pagamento.Id = @Id";

        public static string Listar { get; } = @"SELECT 
                                                            Pagamento.IdTipoPagamento AS IdTipoPagamento,,
                                                            Pagamento.IdEmpresa AS IdEmpresa,
                                                            Pagamento.IdPessoa AS IdPessoa,
                                                            Pagamento.Descricao AS Descricao,
                                                            Pagamento.Valor AS Valor,
                                                            Pagamento.DataPagamento AS DataPagamento,
                                                            Pagamento.DataVencimento AS DataVencimento,

                                                            TipoPagamento.Id AS Id,
                                                            TipoPagamento.Descricao AS Descricao, 

                                                            Empresa.Id AS Id,
                                                            Empresa.Nome AS Nome, 
                                                            Empresa.Logo AS Logo,

                                                            Pessoa.Id AS Id, 
                                                            Pessoa.Nome AS Nome,
                                                            Pessoa.ImagemPerfil AS ImagemPerfil

                                                        FROM Pagamento 
                                                        INNER JOIN TipoPagamento ON Pagamento.IdTipoPagamento = TipoPagamento.Id 
                                                        INNER JOIN Empresa ON Pagamento.IdEmpresa = Empresa.Id 
                                                        INNER JOIN Pessoa ON Pagamento.IdPessoa = Pessoa.Id 

                                                        ORDER BY Pagamento.Id ASC";

        public static string CheckId { get; } = @"SELECT Id FROM Pagamento WHERE Id = @Id";

        public static string LocalizarMaxId { get; } = @"SELECT MAX(Id) FROM Pagamento";
    }
}