namespace ControleDespesas.Infra.Data.Queries
{
    public static class TipoPagamentoQueries
    {
        public static string Salvar { get; } = @"INSERT INTO TipoPagamento (Descricao) VALUES (@Descricao); SELECT SCOPE_IDENTITY();";

        public static string Atualizar { get; } = @"UPDATE TipoPagamento SET Descricao = @Descricao WHERE Id = @Id";

        public static string Deletar { get; } = @"DELETE FROM TipoPagamento WHERE Id = @Id";

        public static string Obter { get; } = @"SELECT 
                                                    Id AS Id,  
                                                    Descricao AS Descricao  
                                                FROM TipoPagamento  
                                                WHERE Id = @Id";

        public static string Listar { get; } = @"SELECT  
                                                    Id AS Id,  
                                                    Descricao AS Descricao  
                                                FROM TipoPagamento  
                                                ORDER BY Id ASC";

        public static string CheckId { get; } = @"SELECT Id FROM TipoPagamento WHERE Id = @Id";

        public static string LocalizarMaxId { get; } = @"SELECT MAX(Id) FROM TipoPagamento";
    }
}