namespace ControleDespesas.Infra.Data.Repositories.Queries
{
    public static class PessoaQueries
    {
        public static string Salvar { get; } = @"INSERT INTO Pessoa (IdUsuario, Nome, ImagemPerfil) VALUES (@IdUsuario, @Nome, @ImagemPerfil); SELECT SCOPE_IDENTITY();";

        public static string Atualizar { get; } = @"UPDATE Pessoa SET IdUsuario = @IdUsuario, Nome = @Nome, ImagemPerfil = @ImagemPerfil WHERE Id = @Id";

        public static string Deletar { get; } = @"DELETE FROM Pessoa WHERE Id = @Id";

        public static string Obter { get; } = @"SELECT
                                                    Id AS Id,
                                                    IdUsuario AS IdUsuario,
                                                    Nome AS Nome,
                                                    ImagemPerfil AS ImagemPerfil 
                                                FROM Pessoa WITH(NOLOCK)
                                                WHERE Id = @Id";

        public static string Listar { get; } = @"SELECT
                                                    Id AS Id,
                                                    IdUsuario AS IdUsuario,
                                                    Nome AS Nome,
                                                    ImagemPerfil AS ImagemPerfil 
                                                FROM Pessoa WITH(NOLOCK)
                                                WHERE IdUsuario = @IdUsuario 
                                                ORDER BY Id ASC";

        public static string CheckId { get; } = @"SELECT Id FROM Pessoa WITH(NOLOCK) WHERE Id = @Id";

        public static string LocalizarMaxId { get; } = @"SELECT MAX(Id) FROM Pessoa WITH(NOLOCK)";
    }
}