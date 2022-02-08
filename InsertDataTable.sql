
INSERT INTO dbo.DependentTypes
([DependentTypeId], [DependentType])
VALUES
(CONVERT(uniqueidentifier, NEWID()), 'Spouse');

INSERT INTO dbo.DependentTypes
([DependentTypeId], [DependentType])
VALUES
(CONVERT(uniqueidentifier, NEWID()), 'Child');

SELECT * FROM dbo.DependentTypes;

INSERT INTO dbo.PaycheckTypes
(PaycheckTypeId, PaycheckType)
VALUES
(CONVERT(uniqueidentifier, NEWID()), '26');

INSERT INTO dbo.PaycheckTypes
(PaycheckTypeId, PaycheckType)
VALUES
(CONVERT(uniqueidentifier, NEWID()), '52');

INSERT INTO dbo.PaycheckTypes
(PaycheckTypeId, PaycheckType)
VALUES
(CONVERT(uniqueidentifier, NEWID()), '104');

use Paylocity
INSERT INTO dbo.PaycheckTypes
(PaycheckType)
VALUES
('104');

SELECT * FROM dbo.PaycheckTypes;


USE [Paylocity]
GO

INSERT INTO [dbo].[Salaries]
           ([SalaryId]
           ,[EmployeeId]
           ,[PaycheckTypeId]
           ,[Salary]
		   ,[CreatedDate]
		   ,[ModifiedDate]
		   )
     VALUES
           (CONVERT(uniqueidentifier, NEWID())
           ,CONVERT(uniqueidentifier, 'B85F46EE-3683-EC11-9832-2C6DC102E08F')
           ,CONVERT(uniqueidentifier, '92CE1540-2B83-EC11-9832-2C6DC102E08F')
           ,100000
		   ,'0001-01-01 00:00:00.0000000'
		   ,'0001-01-01 00:00:00.0000000'
		   )
GO


USE [Paylocity]
GO

INSERT INTO [dbo].[Dependents]
           ([DependentId]
           ,[EmployeeId]
           ,[FirstName]
           ,[LastName]
           ,[DependentTypeId]
           ,[CreatedDate]
           ,[ModifiedDate]
		   )
     VALUES
           (CONVERT(uniqueidentifier, NEWID())
           ,CONVERT(uniqueidentifier, 'FD3A104A-7382-EC11-9831-2C6DC102E08F')
           ,'aara'
           ,'mitra'
           ,CONVERT(uniqueidentifier, '8A86715B-4357-4780-882B-302E41BFE448')
		   ,'0001-01-01 00:00:00.0000000'
		   ,'0001-01-01 00:00:00.0000000'
		   )
GO

USE [Paylocity]
GO

INSERT INTO [dbo].[Dependents]
           ([DependentId]
           ,[EmployeeId]
           ,[FirstName]
           ,[LastName]
           ,[DependentTypeId]
           ,[CreatedDate]
           ,[ModifiedDate]
		   )
     VALUES
           (CONVERT(uniqueidentifier, NEWID())
           ,CONVERT(uniqueidentifier, 'b85f46ee-3683-ec11-9832-2c6dc102e08f')
           ,'aara'
           ,'mitra'
           ,CONVERT(uniqueidentifier, '8A86715B-4357-4780-882B-302E41BFE448')
		   ,'0001-01-01 00:00:00.0000000'
		   ,'0001-01-01 00:00:00.0000000'
		   )
GO

USE [Paylocity]
GO
DECLARE @MinDate AS DATETIME = 0;
INSERT INTO [dbo].[Employees]
           ([FirstName]
           ,[LastName]
           ,[CreatedDate]
		   ,[ModifiedDate])
     VALUES
           ('qwer'
           ,'ty'
           ,GETDATE()
		   ,@MinDate)
GO


USE [Paylocity]
GO

INSERT INTO [dbo].[Salaries]
           ([SalaryId]
           ,[EmployeeId]
           ,[Salary]
		   ,[CreatedDate]
		   ,[ModifiedDate]
		   )
     VALUES
           (CONVERT(uniqueidentifier, NEWID())
           ,CONVERT(uniqueidentifier, 'B85F46EE-3683-EC11-9832-2C6DC102E08F')
           ,123
		   ,'0001-01-01 00:00:00.0000000'
		   ,'0001-01-01 00:00:00.0000000'
		   )
GO
