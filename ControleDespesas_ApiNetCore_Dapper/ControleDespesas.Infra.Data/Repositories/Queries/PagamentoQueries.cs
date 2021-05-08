namespace ControleDespesas.Infra.Data.Repositories.Queries
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
                                                    DataVencimento,
                                                    ArquivoPagamento,
                                                    ArquivoComprovante)
                                                VALUES(
                                                    @IdTipoPagamento, 
                                                    @IdEmpresa,
                                                    @IdPessoa, 
                                                    @Descricao,
                                                    @Valor, 
                                                    @DataPagamento, 
                                                    @DataVencimento,
                                                    @ArquivoPagamento,
                                                    @ArquivoComprovante); 

                                                SELECT SCOPE_IDENTITY();";

        public static string Atualizar { get; } = @"UPDATE Pagamento SET
                                                        IdTipoPagamento = @IdTipoPagamento,
                                                        IdEmpresa = @IdEmpresa,
                                                        IdPessoa = @IdPessoa,
                                                        Descricao = @Descricao,
                                                        Valor = @Valor,
                                                        DataPagamento = @DataPagamento,
                                                        DataVencimento = @DataVencimento,
                                                        ArquivoPagamento = @ArquivoPagamento,
                                                        ArquivoComprovante = @ArquivoComprovante  
                                                    WHERE Id = @Id";

        public static string Deletar { get; } = @"DELETE FROM Pagamento WHERE Id = @Id";

        public static string Obter { get; } = @"SELECT 
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
                                                    Empresa.Logo AS Logo,

                                                    Pessoa.Id AS Id, 
                                                    Pessoa.IdUsuario AS IdUsuario,
                                                    Pessoa.Nome AS Nome,
                                                    Pessoa.ImagemPerfil AS ImagemPerfil

                                                FROM Pagamento WITH(NOLOCK)
                                                INNER JOIN TipoPagamento WITH(NOLOCK) ON Pagamento.IdTipoPagamento = TipoPagamento.Id 
                                                INNER JOIN Empresa WITH(NOLOCK) ON Pagamento.IdEmpresa = Empresa.Id 
                                                INNER JOIN Pessoa WITH(NOLOCK) ON Pagamento.IdPessoa = Pessoa.Id 

                                                WHERE Pagamento.Id = @Id";

        public static string Listar { get; } = @"SELECT 
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
                                                    Empresa.Logo AS Logo,

                                                    Pessoa.Id AS Id, 
                                                    Pessoa.Nome AS Nome,
                                                    Pessoa.IdUsuario AS IdUsuario,
                                                    Pessoa.ImagemPerfil AS ImagemPerfil

                                                FROM Pagamento WITH(NOLOCK)
                                                INNER JOIN TipoPagamento WITH(NOLOCK) ON Pagamento.IdTipoPagamento = TipoPagamento.Id 
                                                INNER JOIN Empresa WITH(NOLOCK) ON Pagamento.IdEmpresa = Empresa.Id 
                                                INNER JOIN Pessoa WITH(NOLOCK) ON Pagamento.IdPessoa = Pessoa.Id 

                                                WHERE Pagamento.IdPessoa = @IdPessoa 

                                                ORDER BY Pagamento.Id ASC";

        public static string ListarPagamentoConcluido { get; } = @"SELECT 
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
                                                                       Empresa.Logo AS Logo,

                                                                       Pessoa.Id AS Id, 
                                                                       Pessoa.Nome AS Nome,
                                                                       Pessoa.IdUsuario AS IdUsuario,
                                                                       Pessoa.ImagemPerfil AS ImagemPerfil

                                                                   FROM Pagamento WITH(NOLOCK) 
                                                                   INNER JOIN TipoPagamento WITH(NOLOCK) ON Pagamento.IdTipoPagamento = TipoPagamento.Id 
                                                                   INNER JOIN Empresa WITH(NOLOCK) ON Pagamento.IdEmpresa = Empresa.Id 
                                                                   INNER JOIN Pessoa WITH(NOLOCK) ON Pagamento.IdPessoa = Pessoa.Id 

                                                                   WHERE Pagamento.IdPessoa = @IdPessoa
                                                                   AND Pagamento.DataPagamento is not NULL

                                                                   ORDER BY Pagamento.Id ASC";

        public static string ListarPagamentoPendente { get; } = @"SELECT 
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
                                                                       Empresa.Logo AS Logo,

                                                                       Pessoa.Id AS Id, 
                                                                       Pessoa.Nome AS Nome,
                                                                       Pessoa.IdUsuario AS IdUsuario,
                                                                       Pessoa.ImagemPerfil AS ImagemPerfil

                                                                   FROM Pagamento WITH(NOLOCK)
                                                                   INNER JOIN TipoPagamento WITH(NOLOCK) ON Pagamento.IdTipoPagamento = TipoPagamento.Id 
                                                                   INNER JOIN Empresa WITH(NOLOCK) ON Pagamento.IdEmpresa = Empresa.Id 
                                                                   INNER JOIN Pessoa WITH(NOLOCK) ON Pagamento.IdPessoa = Pessoa.Id 

                                                                   WHERE Pagamento.IdPessoa = @IdPessoa
                                                                   AND Pagamento.DataPagamento is NULL 

                                                                   ORDER BY Pagamento.Id ASC";

        public static string ObterArquivoPagamento { get; } = @"SELECT ArquivoPagamento AS Arquivo 
                                                                FROM Pagamento WITH(NOLOCK) 
                                                                WHERE Id = @IdPagamento";

        public static string ObterArquivoComprovante { get; } = @"SELECT ArquivoComprovante AS Arquivo 
                                                                  FROM Pagamento WITH(NOLOCK) 
                                                                  WHERE Id = @IdPagamento";

        public static string ObterGastos { get; } = @"SELECT SUM(Pagamento.Valor) As Valor 
                                                      FROM Pagamento WITH(NOLOCK)
                                                      WHERE Pagamento.IdPessoa = @IdPessoa";

        public static string ObterGastosAno { get; } = @"SELECT SUM(Pagamento.Valor) As Valor 
                                                         FROM Pagamento WITH(NOLOCK)
                                                         WHERE Pagamento.IdPessoa = @IdPessoa 
                                                         AND YEAR(Pagamento.DataVencimento) = @Ano";

        public static string ObterGastosAnoMes { get; } = @"SELECT SUM(Pagamento.Valor) As Valor 
                                                            FROM Pagamento WITH(NOLOCK)
                                                            WHERE Pagamento.IdPessoa = @IdPessoa 
                                                            AND YEAR(Pagamento.DataVencimento) = @Ano
                                                            AND MONTH(Pagamento.DataVencimento) = @Mes";

        public static string CheckId { get; } = @"SELECT Id FROM Pagamento WITH(NOLOCK) WHERE Id = @Id";

        public static string LocalizarMaxId { get; } = @"SELECT MAX(Id) FROM Pagamento WITH(NOLOCK)";
    }
}