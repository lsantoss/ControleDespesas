CREATE TABLE Pagamento (
    Id              INTEGER         PRIMARY KEY AUTOINCREMENT
                                    NOT NULL,
    IdTipoPagamento INTEGER         REFERENCES TipoPagamento (Id) 
                                    NOT NULL,
    IdEmpresa       INTEGER         REFERENCES Empresa (Id) 
                                    NOT NULL,
    IdPessoa        INTEGER         REFERENCES Pessoa (Id) 
                                    NOT NULL,
    Descricao       NVARCHAR (250)  NOT NULL,
    Valor           DECIMAL (10, 2) NOT NULL,
    DataPagamento   DATETIME        NOT NULL,
    DataVencimento  DATETIME        NOT NULL
);