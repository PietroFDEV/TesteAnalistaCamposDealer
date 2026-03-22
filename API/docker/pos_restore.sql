USE TesteCamposDealer;
GO

-- Ajustes na tabela de produtos (preço unitário)
IF COL_LENGTH('Produto', 'vlrProduto') IS NULL
BEGIN
    ALTER TABLE Produto ADD vlrProduto DECIMAL(18,2) NOT NULL DEFAULT(0);
END
GO

-- Ajustes na tabela de vendas (total)
IF COL_LENGTH('Venda', 'vlrTotal') IS NULL
BEGIN
    ALTER TABLE Venda ADD vlrTotal DECIMAL(18,2) NOT NULL DEFAULT(0);
END
GO

-- Default para data de registro do cliente
IF OBJECT_ID('DF_Cliente_dthRegistro', 'D') IS NULL
BEGIN
    ALTER TABLE Cliente ADD CONSTRAINT DF_Cliente_dthRegistro DEFAULT (GETDATE()) FOR dthRegistro;
END
GO

-- Default para data de registro da venda
IF OBJECT_ID('DF_Venda_dthRegistro', 'D') IS NULL
BEGIN
    ALTER TABLE Venda ADD CONSTRAINT DF_Venda_dthRegistro DEFAULT (GETDATE()) FOR dthRegistro;
END
GO

-- Criar tabela de itens de venda
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'VendaItem')
BEGIN
    CREATE TABLE VendaItem (
        Id INT IDENTITY PRIMARY KEY,
        IdVenda INT,
        IdProduto INT,
        Quantidade INT,
        ValorUnitario DECIMAL(18,2),
        FOREIGN KEY (IdVenda) REFERENCES Venda(idVenda),
        FOREIGN KEY (IdProduto) REFERENCES Produto(idProduto)
    );
END
GO

-- Histórico de preço
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'ProdutoPrecoHistorico')
BEGIN
    CREATE TABLE ProdutoPrecoHistorico (
        Id INT IDENTITY PRIMARY KEY,
        ProdutoId INT,
        Preco DECIMAL(18,2),
        DataAlteracao DATETIME
    );
END
GO
