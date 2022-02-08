/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [EmployeeId]
      ,[FirstName]
      ,[LastName]
      ,[CreatedDate]
      ,[ModifiedDate]
  FROM [Paylocity].[dbo].[Employees]

  /****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [DependentId]
      ,[EmployeeId]
      ,[FirstName]
      ,[LastName]
      ,[DependentTypeId]
      ,[CreatedDate]
      ,[ModifiedDate]
  FROM [Paylocity].[dbo].[Dependents]

  /****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [SalaryId]
      ,[EmployeeId]
      ,[PaycheckTypeId]
      ,[Salary]
      ,[CreatedDate]
      ,[ModifiedDate]
  FROM [Paylocity].[dbo].[Salaries]
  
  /****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [DependentTypeId]
      ,[DependentType]
  FROM [Paylocity].[dbo].[DependentTypes]

  /****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [PaycheckTypeId]
      ,[PaycheckType]
  FROM [Paylocity].[dbo].[PaycheckTypes]



SELECT 
[Employees].[EmployeeId], [Employees].[FirstName] AS 'EmployeeFirstName', [Employees].[LastName] AS 'EmployeeLastName',  
[Dependents].DependentTypeId, [Dependents].[FirstName] AS 'DependentFirstName', [Dependents].[LastName] AS 'DependentLastName',
[DependentTypes].DependentType, 
[Salaries].SalaryId,[Salaries].Salary,
[PaycheckTypes].PaycheckType
FROM [Employees], [Dependents], [DependentTypes], [Salaries], [PaycheckTypes]
WHERE Employees.EmployeeId = Dependents.EmployeeId
AND [Dependents].DependentTypeId = [DependentTypes].DependentTypeId
AND Employees.EmployeeId = Salaries.EmployeeId
AND Salaries.PaycheckTypeId = PaycheckTypes.PaycheckTypeId;

SELECT  
[Employees].[EmployeeId], [Employees].[FirstName] AS 'EmployeeFirstName', [Employees].[LastName] AS 'EmployeeLastName',  
[Dependents].DependentTypeId, [Dependents].[FirstName] AS 'DependentFirstName', [Dependents].[LastName] AS 'DependentLastName',
[DependentTypes].DependentType,
[Salaries].SalaryId,[Salaries].Salary,
[PaycheckTypes].PaycheckType
   FROM [Employees]
   INNER JOIN [Dependents]
   ON Employees.EmployeeId = Dependents.EmployeeId
   INNER JOIN [DependentTypes]
   ON [Dependents].DependentTypeId = [DependentTypes].DependentTypeId
   INNER JOIN [Salaries]
   ON Employees.EmployeeId = Salaries.EmployeeId
   INNER JOIN [PaycheckTypes]
   ON Salaries.PaycheckTypeId = PaycheckTypes.PaycheckTypeId;

 SELECT  
[Employees].[EmployeeId], [Employees].[FirstName] AS 'EmployeeFirstName', [Employees].[LastName] AS 'EmployeeLastName',  
[Salaries].SalaryId,[Salaries].Salary,
[PaycheckTypes].PaycheckType
   FROM [Employees]
   LEFT OUTER JOIN [Salaries]
   ON Employees.EmployeeId = Salaries.EmployeeId
   LEFT OUTER JOIN [PaycheckTypes]
   ON Salaries.PaycheckTypeId = PaycheckTypes.PaycheckTypeId;