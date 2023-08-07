BEGIN TRANSACTION;
GO

ALTER TABLE [Product] DROP CONSTRAINT [FK_Product_Categroy_CategoryId];
GO

ALTER TABLE [Product] ADD CONSTRAINT [FK_Product_Categroy_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categroy] ([Id]) ON DELETE NO ACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20230807153446_MORR00004', N'7.0.9');
GO

COMMIT;
GO

