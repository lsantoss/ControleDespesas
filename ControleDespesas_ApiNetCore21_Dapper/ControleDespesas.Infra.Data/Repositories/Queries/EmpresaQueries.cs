namespace ControleDespesas.Infra.Data.Repositories.Queries
{
    public static class EmpresaQueries
    {
        public static string Salvar { get; } = @"INSERT INTO Empresa (Nome, Logo) VALUES (@Nome, @Logo); SELECT SCOPE_IDENTITY();";

        public static string Atualizar { get; } = @"UPDATE Empresa SET Nome = @Nome, Logo = @Logo WHERE Id = @Id";

        public static string Deletar { get; } = @"DELETE FROM Empresa WHERE Id = @Id";

        public static string Obter { get; } = @"SELECT 
                                                    Id AS Id, 
                                                    Nome AS Nome,
                                                    Logo AS Logo 
                                                FROM Empresa WITH(NOLOCK)
                                                WHERE Id = @Id";

        public static string Listar { get; } = @"SELECT 
                                                    Id AS Id, 
                                                    Nome AS Nome,
                                                    Logo AS Logo 
                                                FROM Empresa WITH(NOLOCK)
                                                ORDER BY Id ASC";

        public static string CheckId { get; } = @"SELECT Id FROM Empresa WITH(NOLOCK) WHERE Id = @Id";

        public static string LocalizarMaxId { get; } = @"SELECT MAX(Id) FROM Empresa WITH(NOLOCK)";
    }
}