CREATE TABLE Usuario (
    Id   INTEGER        PRIMARY KEY AUTOINCREMENT
                        NOT NULL,
    Login NVARCHAR (50) NOT NULL,
    Senha NVARCHAR (15) NOT NULL,
    Privilegio INTEGER  NOT NULL
);