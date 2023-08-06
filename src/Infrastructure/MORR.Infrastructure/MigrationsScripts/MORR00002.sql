BEGIN TRANSACTION;
GO

CREATE TABLE [Categroy] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Categroy] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Product] (
    [Id] int NOT NULL IDENTITY,
    [ItemCode] nvarchar(450) NOT NULL,
    [ItemName] nvarchar(max) NULL,
    [SKUCode] nvarchar(max) NULL,
    [CategoryId] int NOT NULL,
    [DisplayInFrontPage] bit NULL DEFAULT CAST(0 AS bit),
    [Price] decimal(6,2) NOT NULL,
    [IsShowWeb] bit NOT NULL DEFAULT CAST(0 AS bit),
    [ShortDescription] nvarchar(max) NULL,
    [LongDescription] nvarchar(max) NULL,
    [ItemImageUrl] nvarchar(max) NULL,
    [IsDeleted] bit NOT NULL DEFAULT CAST(0 AS bit),
    [LoyalityPoints] decimal(18,2) NULL,
    [Longitude] decimal(18,2) NULL,
    [Latitude] decimal(18,2) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Product_Categroy_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categroy] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Product_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Product_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [ProductInventory] (
    [ProductId] int NOT NULL,
    [Quantity] int NOT NULL,
    [SpecialNotes] nvarchar(max) NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NULL,
    CONSTRAINT [PK_ProductInventory] PRIMARY KEY ([ProductId]),
    CONSTRAINT [FK_ProductInventory_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProductInventory_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ProductInventory_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Product_CategoryId] ON [Product] ([CategoryId]);
GO

CREATE INDEX [IX_Product_CreatedByUserId] ON [Product] ([CreatedByUserId]);
GO

CREATE UNIQUE INDEX [IX_Product_ItemCode] ON [Product] ([ItemCode]);
GO

CREATE INDEX [IX_Product_UpdatedByUserId] ON [Product] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_ProductInventory_CreatedByUserId] ON [ProductInventory] ([CreatedByUserId]);
GO

CREATE INDEX [IX_ProductInventory_UpdatedByUserId] ON [ProductInventory] ([UpdatedByUserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230806154221_MORR00002', N'7.0.9');
GO

COMMIT;
GO

