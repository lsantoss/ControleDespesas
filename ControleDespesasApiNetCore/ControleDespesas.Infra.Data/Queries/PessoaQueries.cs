namespace ControleDespesas.Infra.Data.Queries
{
    public static class PessoaQueries
    {
        public static string Salvar { get; } = @"INSERT INTO Pessoa (Nome, ImagemPerfil) VALUES (@Nome, @ImagemPerfil)";

        public static string Atualizar { get; } = @"UPDATE Pessoa SET Nome = @Nome, ImagemPerfil = @ImagemPerfil WHERE Id = @Id";

        public static string Deletar { get; } = @"DELETE FROM Pessoa WHERE Id = @Id";

        public static string Obter { get; } = @"SELECT
                                                    Id AS Id,
                                                    Nome AS Nome,
                                                    ImagemPerfil AS ImagemPerfil 
                                                FROM Pessoa 
                                                WHERE Id = @Id";

        public static string Listar { get; } = @"SELECT
                                                    Id AS Id,
                                                    Nome AS Nome,
                                                    ImagemPerfil AS ImagemPerfil 
                                                FROM Pessoa 
                                                ORDER BY Id ASC";

        public static string CheckId { get; } = @"SELECT Id FROM Pessoa WHERE Id = @Id";

        public static string LocalizarMaxId { get; } = @"SELECT MAX(Id) FROM Pessoa";
    }
}