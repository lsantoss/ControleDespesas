namespace ControleDespesas.Infra.Data.Queries
{
    public static class EmpresaQueries
    {
        public static string Salvar { get; } = @"INSERT INTO Empresa (Nome, Logo) VALUES (@Nome, @Logo)";

        public static string Atualizar { get; } = @"UPDATE Empresa SET Nome = @Nome, Logo = @Logo WHERE Id = @Id";

        public static string Deletar { get; } = @"DELETE FROM Empresa WHERE Id = @Id";

        public static string Obter { get; } = @"SELECT 
                                                    Id AS Id, 
                                                    Nome AS Nome,
                                                    Logo AS Logo 
                                                FROM Empresa 
                                                WHERE Id = @Id";


        public static string Listar { get; } = @"SELECT 
                                                    Id AS Id, 
                                                    Nome AS Nome,
                                                    Logo AS Logo 
                                                FROM Empresa 
                                                ORDER BY Id ASC";


        public static string CheckId { get; } = @"SELECT Id FROM Empresa WHERE Id = @Id";

        public static string LocalizarMaxId { get; } = @"SELECT MAX(Id) FROM Empresa";
    }
}