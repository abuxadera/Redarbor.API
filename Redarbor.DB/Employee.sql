CREATE TABLE [dbo].[Employee]
(
	[Id] INT IDENTITY(1,1) PRIMARY KEY,
	[CompanyId] INT NOT NULL,
    [CreatedOn] DATETIME,
    [DeletedOn] DATETIME,
    [Email] VARCHAR(100),
    [Fax] VARCHAR(11),
    [Name] VARCHAR(100),
    [Lastlogin] DATETIME,
    [Password] VARCHAR(20),
    [PortalId] INT NOT NULL,
    [RoleId] INT NOT NULL,
    [StatusId] INT NOT NULL,
    [Telephone] VARCHAR(11),
    [UpdatedOn] DATETIME,
    [Username] VARCHAR(100),
)
