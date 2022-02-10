/****** Script for Select Employee table  ******/
SELECT TOP (1000) [EmployeeId]
      ,[FirstName]
      ,[LastName]
      ,[CreatedDate]
      ,[ModifiedDate]
  FROM [Paylocity].[dbo].[Employees]
  WITH(INDEX ([PK_Employees]));

  /****** Script for Select Dependent table  ******/
SELECT TOP (1000) [DependentId]
      ,[EmployeeId]
      ,[FirstName]
      ,[LastName]
      ,[DependentTypeId]
      ,[CreatedDate]
      ,[ModifiedDate]
	  FROM [Paylocity].[dbo].[Dependents]
	  WITH(INDEX ([PK_Dependents]));

  /****** Script for Select Salary table  ******/
SELECT TOP (1000) [SalaryId]
      ,[EmployeeId]
      ,[PaycheckTypeId]
      ,[Salary]
      ,[CreatedDate]
      ,[ModifiedDate]
  FROM [Paylocity].[dbo].[Salaries]
  WITH(INDEX ([PK_Salaries]))
  
  /****** Script for Select DependentTypes table  ******/
SELECT TOP (1000) [DependentTypeId]
      ,[DependentType]
  FROM [Paylocity].[dbo].[DependentTypes]
    WITH(INDEX (PK_DependentTypes, UK_DependentTypes))

  /****** Script for Select PaycheckTypes table  ******/
SELECT TOP (1000) [PaycheckTypeId]
      ,[PaycheckType]
  FROM [Paylocity].[dbo].[PaycheckTypes]
  WITH(INDEX (PK_PaycheckTypes))



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



   SELECT *
  FROM [Paylocity].[dbo].[Employees]
  WITH(INDEX ([PK_Employees]))