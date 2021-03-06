﻿namespace ControleDespesas.Infra.Data.Repositories.Queries
{
    public static class UsuarioQueries
    {
        public static string Salvar { get; } = @"INSERT INTO Usuario (
                                                    Login,
                                                    Senha,
                                                    Privilegio) 
                                                VALUES(
                                                    @Login,
                                                    @Senha,
                                                    @Privilegio); 
                                                SELECT SCOPE_IDENTITY();";

        public static string Atualizar { get; } = @"UPDATE Usuario SET 
                                                        Login = @Login, 
                                                        Senha = @Senha, 
                                                        Privilegio = @Privilegio  
                                                    WHERE Id = @Id";

        public static string Deletar { get; } = @"DELETE FROM Usuario WHERE Id = @Id";

        public static string Obter { get; } = @"SELECT 
                                                    Id AS Id,  
                                                    Login AS Login,  
                                                    Senha AS Senha,  
                                                    Privilegio AS Privilegio  
                                                FROM Usuario WITH(NOLOCK) 
                                                WHERE Id = @Id";

        public static string Listar { get; } = @"SELECT 
                                                    Id AS Id,  
                                                    Login AS Login,  
                                                    Senha AS Senha,  
                                                    Privilegio AS Privilegio  
                                                FROM Usuario WITH(NOLOCK)
                                                ORDER BY Id ASC";

        public static string Logar { get; } = @"SELECT 
                                                    Id AS Id,  
                                                    Login AS Login,  
                                                    Senha AS Senha,  
                                                    Privilegio AS Privilegio  
                                                FROM Usuario WITH(NOLOCK)
                                                WHERE Login = @Login and Senha = @Senha";

        public static string CheckLogin { get; } = @"SELECT Login FROM Usuario WITH(NOLOCK) WHERE Login = @Login";

        public static string CheckId { get; } = @"SELECT Id FROM Usuario WITH(NOLOCK) WHERE Id = @Id";

        public static string LocalizarMaxId { get; } = @"SELECT MAX(Id) FROM Usuario WITH(NOLOCK)";
    }
}