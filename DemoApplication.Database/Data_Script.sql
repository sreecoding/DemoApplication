USE [DemoDB]
GO

INSERT INTO [dbo].[Country]
           ([CountryId]
           ,[CountryName]
           ,[CountryCode])
     VALUES
           (1
           ,'United Kingdom'
           ,'UK')

INSERT INTO [dbo].[GiftAidTaxRate]
           ([TaxRateId]
           ,[CountryId]
           ,[TaxRate])
     VALUES
           (1
           ,1
           ,20)
GO


