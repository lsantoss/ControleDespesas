namespace ControleDespesas.Infra.Data.Repositories.Queries
{
    public static class PessoaQueries
    {
        public static string Salvar { get; } = @"INSERT INTO Pessoa (IdUsuario, Nome, ImagemPerfil) 
                                                 VALUES (@IdUsuario, @Nome, @ImagemPerfil); SELECT SCOPE_IDENTITY();";

        public static string Atualizar { get; } = @"UPDATE Pessoa SET IdUsuario = @IdUsuario, Nome = @Nome, ImagemPerfil = @ImagemPerfil 
                                                    WHERE Id = @Id AND IdUsuario = @IdUsuario";

        public static string Deletar { get; } = @"DELETE FROM Pessoa WHERE Id = @Id AND IdUsuario = @IdUsuario";

        public static string Obter { get; } = @"SELECT
                                                    Id AS Id,
                                                    IdUsuario AS IdUsuario,
                                                    Nome AS Nome,
                                                    ImagemPerfil AS ImagemPerfil 
                                                FROM Pessoa WITH(NOLOCK)
                                                WHERE Id = @Id AND IdUsuario = @IdUsuario";

        public static string ObterContendoRegistrosFilhos { get; } = @"SELECT
                                                                            Pessoa.Id AS Id,
                                                                            Pessoa.IdUsuario AS IdUsuario,
                                                                            Pessoa.Nome AS Nome,
                                                                            Pessoa.ImagemPerfil AS ImagemPerfil,
                                                                            Pagamento.Id AS Id,
                                                                            Pagamento.IdTipoPagamento AS IdTipoPagamento,
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
                                                                            Empresa.Logo AS Logo
                                                                        FROM Pessoa WITH(NOLOCK)
                                                                        LEFT JOIN Pagamento WITH(NOLOCK) ON Pessoa.Id = Pagamento.IdPessoa
                                                                        LEFT JOIN TipoPagamento WITH(NOLOCK) ON Pagamento.IdTipoPagamento = TipoPagamento.Id 
                                                                        LEFT JOIN Empresa WITH(NOLOCK) ON Pagamento.IdEmpresa = Empresa.Id 
                                                                        WHERE Pessoa.Id = @Id AND Pessoa.IdUsuario = @IdUsuario
                                                                        ORDER BY Pessoa.Id, Pagamento.Id, TipoPagamento.Id, Empresa.Id";

        public static string Listar { get; } = @"SELECT
                                                    Id AS Id,
                                                    IdUsuario AS IdUsuario,
                                                    Nome AS Nome,
                                                    ImagemPerfil AS ImagemPerfil 
                                                FROM Pessoa WITH(NOLOCK)
                                                WHERE IdUsuario = @IdUsuario 
                                                ORDER BY Id ASC";

        public static string ListarContendoRegistrosFilhos { get; } = @"SELECT
                                                                            Pessoa.Id AS Id,
                                                                            Pessoa.IdUsuario AS IdUsuario,
                                                                            Pessoa.Nome AS Nome,
                                                                            Pessoa.ImagemPerfil AS ImagemPerfil,
                                                                            Pagamento.Id AS Id,
                                                                            Pagamento.IdTipoPagamento AS IdTipoPagamento,
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
                                                                            Empresa.Logo AS Logo
                                                                        FROM Pessoa WITH(NOLOCK)
                                                                        LEFT JOIN Pagamento WITH(NOLOCK) ON Pessoa.Id = Pagamento.IdPessoa
                                                                        LEFT JOIN TipoPagamento WITH(NOLOCK) ON Pagamento.IdTipoPagamento = TipoPagamento.Id 
                                                                        LEFT JOIN Empresa WITH(NOLOCK) ON Pagamento.IdEmpresa = Empresa.Id
                                                                        WHERE Pessoa.IdUsuario = @IdUsuario 
                                                                        ORDER BY Pessoa.Id, Pagamento.Id, TipoPagamento.Id, Empresa.Id";

        public static string CheckId { get; } = @"SELECT Id FROM Pessoa WITH(NOLOCK) WHERE Id = @Id";

        public static string LocalizarMaxId { get; } = @"SELECT MAX(Id) FROM Pessoa WITH(NOLOCK)";
    }
}