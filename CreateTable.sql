
CREATE TABLE [dbo].[PaycheckTypes]
(
	[PaycheckTypeId]	UNIQUEIDENTIFIER CONSTRAINT [DF_PaycheckTypes_PaycheckTypeId] DEFAULT (NEWSEQUENTIALID()) ROWGUIDCOL NOT NULL,
	[PaycheckType]	NVARCHAR (256) NOT NULL,

	CONSTRAINT [PK_PaycheckTypes] PRIMARY KEY ([PaycheckTypeId]),
	CONSTRAINT [UK_PaycheckTypes] UNIQUE ([PaycheckType])
);

CREATE TABLE [dbo].[DependentTypes]
(
	[DependentTypeId]	UNIQUEIDENTIFIER CONSTRAINT [DF_DependentTypes_DependentTypeId] DEFAULT (NEWSEQUENTIALID()) ROWGUIDCOL NOT NULL,
	[DependentType]	NVARCHAR (256) NOT NULL,

	CONSTRAINT [PK_DependentTypes] PRIMARY KEY ([DependentTypeId]),
	CONSTRAINT [UK_DependentTypes] UNIQUE ([DependentType])
);

CREATE TABLE [dbo].[Employees]
(
	[EmployeeId]	UNIQUEIDENTIFIER CONSTRAINT [DF_Employees_EmployeeId] DEFAULT (NEWSEQUENTIALID()) ROWGUIDCOL NOT NULL,
	[FirstName]		NVARCHAR (256) NOT NULL,
	[LastName]		NVARCHAR (256) NOT NULL,
	[CreatedDate]	DATETIME2 (7) CONSTRAINT [DF_Employees_CreatedDate] DEFAULT (GETUTCDATE()) NOT NULL, 
	[ModifiedDate]	DATETIME2 (7) NOT NULL, 

	CONSTRAINT [PK_Employees] PRIMARY KEY ([EmployeeId])
);

CREATE TABLE [dbo].[Salaries]
(
	[SalaryId]	UNIQUEIDENTIFIER CONSTRAINT [DF_Salaries_SalaryId] DEFAULT (NEWSEQUENTIALID()) ROWGUIDCOL NOT NULL,
	[EmployeeId]	UNIQUEIDENTIFIER NOT NULL,
	[PaycheckTypeId]		UNIQUEIDENTIFIER NOT NULL,
	[Salary]		NVARCHAR (256) NOT NULL,
	[CreatedDate]	DATETIME2 (7) CONSTRAINT [DF_Salaries_CreatedDate] DEFAULT (GETUTCDATE()) NOT NULL, 
	[ModifiedDate]	DATETIME2 (7) NOT NULL, 

	CONSTRAINT [PK_Salaries] PRIMARY KEY ([SalaryId]),
	CONSTRAINT [FK_Salaries_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([EmployeeId]),
	CONSTRAINT [FK_Salaries_PaycheckTypes] FOREIGN KEY ([PaycheckTypeId]) REFERENCES [PaycheckTypes]([PaycheckTypeId])
);

CREATE TABLE [dbo].[Dependents]
(
	[DependentId]	UNIQUEIDENTIFIER CONSTRAINT [DF_Dependents_DependentId] DEFAULT (NEWSEQUENTIALID()) ROWGUIDCOL NOT NULL,
	[EmployeeId]	UNIQUEIDENTIFIER NOT NULL,
	[FirstName]		NVARCHAR (256) NOT NULL,
	[LastName]		NVARCHAR (256) NOT NULL,
	[DependentTypeId] UNIQUEIDENTIFIER NOT NULL,
	[CreatedDate]	DATETIME2 (7) CONSTRAINT [DF_Dependents_CreatedDate] DEFAULT (GETUTCDATE()) NOT NULL, 
	[ModifiedDate]	DATETIME2 (7) NOT NULL, 

	CONSTRAINT [PK_Dependents] PRIMARY KEY ([DependentId]),
	CONSTRAINT [FK_Dependents_Employees] FOREIGN KEY ([EmployeeId]) REFERENCES [Employees]([EmployeeId]),
	CONSTRAINT [FK_Dependents_DependentTypes] FOREIGN KEY ([DependentTypeId]) REFERENCES [DependentTypes]([DependentTypeId])
);


CREATE TABLE [dbo].[TransactionLogs](  
    [Id] [int] IDENTITY(1,1) NOT NULL,  
    [Message] [nvarchar](max) NULL,  
    [MessageTemplate] [nvarchar](max) NULL,  
    [Level] [nvarchar](128) NULL,  
    [TimeStamp] [datetimeoffset](7) NOT NULL,  
    [Exception] [nvarchar](max) NULL,  
    [Properties] [xml] NULL,  
    [LogEvent] [nvarchar](max) NULL,
	[Application] [nvarchar](max) NULL
 CONSTRAINT [PK_Logs] PRIMARY KEY CLUSTERED   
(  
    [Id] ASC  
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]  
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]  