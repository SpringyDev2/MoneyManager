-- Create Currencies table
CREATE TABLE [dbo].[Currencies] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Currency] NVARCHAR(60) NOT NULL,
    CONSTRAINT [PK_Currencies]
        PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

-- Create Admin table
CREATE TABLE [dbo].[Admin] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Username] NVARCHAR(60) NOT NULL,
    [Password] NVARCHAR(60) NOT NULL,
    CONSTRAINT [PK_Admin]
        PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

-- Create Users table
CREATE TABLE [dbo].[Users] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Username] NVARCHAR(60) NOT NULL,
    [Password] NVARCHAR(60) NOT NULL,
    [TotalAmount] DECIMAL(18,2) NOT NULL
        CONSTRAINT [DF_Users_TotalAmount] DEFAULT (0),
    [CurrencyId] INT NOT NULL,

    CONSTRAINT [PK_Users]
        PRIMARY KEY CLUSTERED ([Id] ASC),

    CONSTRAINT [FK_Users_ToCurrencies]
        FOREIGN KEY ([CurrencyId])
        REFERENCES [dbo].[Currencies] ([Id])
);
GO

-- Create Transaction table
CREATE TABLE [dbo].[Transaction] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [UserId] INT NOT NULL,
    [Amount] DECIMAL(18,2) NOT NULL,
    [CurrencyId] INT NOT NULL,
    [Date] DATETIME NOT NULL,

    CONSTRAINT [PK_Transaction]
        PRIMARY KEY CLUSTERED ([Id] ASC),

    CONSTRAINT [FK_Transaction_ToCurrencies]
        FOREIGN KEY ([CurrencyId])
        REFERENCES [dbo].[Currencies] ([Id]),

    CONSTRAINT [FK_Transaction_ToUsers]
        FOREIGN KEY ([UserId])
        REFERENCES [dbo].[Users] ([Id])
);
GO

-- Insert currencies
INSERT INTO [dbo].[Currencies] ([Currency])
VALUES
    ('USD'),
    ('EUR'),
    ('ILS');

-- Insert admin
INSERT INTO [dbo].[Admin] ([Username], [Password])
VALUES
    ('admin', 'admin123');

-- Insert users
INSERT INTO [dbo].[Users] ([Username], [Password], [TotalAmount], [CurrencyId])
VALUES
    ('john', 'john123', 1500.00, 1),
    ('sarah', 'sarah123', 2500.50, 2),
    ('alon', '123', 2500.50, 3),
    ('david', 'david123', 800.00, 3);

-- Insert transactions
INSERT INTO [dbo].[Transaction] ([UserId], [Amount], [CurrencyId], [Date])
VALUES
    (1, 200.00, 1, '2026-10-10 10:30:00'),
    (1, 50.00, 1, '2026-10-12 14:15:00'),
    (2, 300.00, 2, '2026-10-13 09:00:00'),
    (3, 100.00, 3, '2026-10-15 16:45:00');