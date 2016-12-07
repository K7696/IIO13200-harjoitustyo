USE ScrumManager

-- ############################################################################

/*
* Aja tiedoston sisältö SQL Management Studion kautta.
* Kannattaa ajaa sa-käyttäjällä.
*/


GO

-- ############################################################################

/*
* Kuvaus: Poista taulut, jos ne ovat jo olemassa.
*/

-- Drop Companies
IF EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Companies')
BEGIN
    
	DROP TABLE Companies;
	
END

GO

-- Drop Customers
IF EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Customers')
BEGIN
    
	DROP TABLE Customers;
	
END

GO

-- Drop Features
IF EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Features')
BEGIN
    
	DROP TABLE Features;
	
END

GO

-- Drop Items
IF EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Items')
BEGIN
    
	DROP TABLE Items;
	
END

GO

-- Drop Persons
IF EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Persons')
BEGIN
    
	DROP TABLE Persons;
	
END

GO

-- Drop Projects
IF EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Projects')
BEGIN
    
	DROP TABLE Projects;
	
END

GO

-- Drop Roles
IF EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Roles')
BEGIN
    
	DROP TABLE Roles;
	
END

GO

-- Drop Sprints
IF EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Sprints')
BEGIN
    
	DROP TABLE Sprints;
	
END

GO

-- Drop Stories
IF EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Stories')
BEGIN
    
	DROP TABLE Stories;
	
END

GO

-- Drop Teams
IF EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Teams')
BEGIN
    
	DROP TABLE Teams;
	
END

GO

-- ############################################################################

/*
* Kuvaus: Luo taulut
*
*/

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- Create Companies
IF NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Companies')
BEGIN
    
	CREATE TABLE [dbo].[Companies](
		[CompanyId] [int] IDENTITY(1,1) NOT NULL,
		[ObjectId] [uniqueidentifier] NULL,
		[ShortCode] [nvarchar](500) NULL,
		[Name] [nvarchar](500) NULL,
		[Description] [nvarchar](1000) NULL,
		[Created] [datetime2](7) NOT NULL,
		[Modified] [datetime2](7) NOT NULL,
		[CreatorId] [int] NOT NULL,
		[ModifierId] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
	(
		[CompanyId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
END

GO

-- Create Customers
IF NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Customers')
BEGIN
    
	CREATE TABLE [dbo].[Customers](
		[CustomerId] [int] IDENTITY(1,1) NOT NULL,
		[CompanyId] [int] NOT NULL,
		[ObjectId] [uniqueidentifier] NULL,
		[ShortCode] [nvarchar](500) NULL,
		[Name] [nvarchar](500) NULL,
		[Description] [nvarchar](1000) NULL,
		[Created] [datetime2](7) NOT NULL,
		[Modified] [datetime2](7) NOT NULL,
		[CreatorId] [int] NOT NULL,
		[ModifierId] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
	(
		[CustomerId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
END

GO

-- Create Features
IF NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Features')
BEGIN
    
	CREATE TABLE [dbo].[Features](
		[FeatureId] [int] IDENTITY(1,1) NOT NULL,
		[CompanyId] [int] NOT NULL,
		[ProjectId] [int] NOT NULL,
		[ObjectId] [uniqueidentifier] NULL,
		[ShortCode] [nvarchar](500) NULL,
		[Name] [nvarchar](500) NULL,
		[Description] [nvarchar](1000) NULL,
		[Created] [datetime2](7) NOT NULL,
		[Modified] [datetime2](7) NOT NULL,
		[CreatorId] [int] NOT NULL,
		[ModifierId] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
	(
		[FeatureId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
END

GO

-- Create Items
IF NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Items')
BEGIN
    
	CREATE TABLE [dbo].[Items](
		[ItemId] [int] IDENTITY(1,1) NOT NULL,
		[CompanyId] [int] NOT NULL,
		[ProjectId] [int] NOT NULL,
		[FeatureId] [int] NULL,
		[StoryId] [int] NULL,
		[UserAssignedTo] [int] NULL,
		[WorkLeft] [float] NULL,
		[ObjectId] [uniqueidentifier] NULL,
		[ShortCode] [nvarchar](500) NULL,
		[Name] [nvarchar](500) NULL,
		[Description] [nvarchar](1000) NULL,
		[Created] [datetime2](7) NOT NULL,
		[Modified] [datetime2](7) NOT NULL,
		[CreatorId] [int] NOT NULL,
		[ModifierId] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
	(
		[ItemId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
END

GO

-- Create Persons
IF NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Persons')
BEGIN
    
	CREATE TABLE [dbo].[Persons](
		[PersonId] [int] IDENTITY(1,1) NOT NULL,
		[CompanyId] [int] NOT NULL,
		[TeamId] [int] NOT NULL,
		[RoleId] [int] NOT NULL,
		[Firstname] [nvarchar](500) NULL,
		[Lastname] [nvarchar](500) NULL,
		[Email] [nvarchar](500) NULL,
		[Password] [nvarchar](500) NULL,
		[Phonenumber] [nvarchar](500) NULL,
		[ObjectId] [uniqueidentifier] NULL,
		[ShortCode] [nvarchar](500) NULL,
		[Name] [nvarchar](500) NULL,
		[Description] [nvarchar](1000) NULL,
		[Created] [datetime2](7) NOT NULL,
		[Modified] [datetime2](7) NOT NULL,
		[CreatorId] [int] NOT NULL,
		[ModifierId] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
	(
		[PersonId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
END

GO

-- Create Projects
IF NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Projects')
BEGIN
    
	CREATE TABLE [dbo].[Projects](
		[ProjectId] [int] IDENTITY(1,1) NOT NULL,
		[CompanyId] [int] NOT NULL,
		[CustomerId] [int] NOT NULL,
		[StartDate] [datetime2] NULL,
		[Deadline] [datetime2] NULL,
		[ObjectId] [uniqueidentifier] NULL,
		[ShortCode] [nvarchar](500) NULL,
		[Name] [nvarchar](500) NULL,
		[Description] [nvarchar](1000) NULL,
		[Created] [datetime2](7) NOT NULL,
		[Modified] [datetime2](7) NOT NULL,
		[CreatorId] [int] NOT NULL,
		[ModifierId] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
	(
		[ProjectId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
END

GO

-- Create Roles
IF NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Roles')
BEGIN
    
	CREATE TABLE [dbo].[Roles](
		[RoleId] [int] IDENTITY(1,1) NOT NULL,
		[CompanyId] [int] NOT NULL,
		[ObjectId] [uniqueidentifier] NULL,
		[ShortCode] [nvarchar](500) NULL,
		[Name] [nvarchar](500) NULL,
		[Description] [nvarchar](1000) NULL,
		[Created] [datetime2](7) NOT NULL,
		[Modified] [datetime2](7) NOT NULL,
		[CreatorId] [int] NOT NULL,
		[ModifierId] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
	(
		[RoleId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
END

GO

-- Create Sprints
IF NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Sprints')
BEGIN
    
	CREATE TABLE [dbo].[Sprints](
		[SprintId] [int] IDENTITY(1,1) NOT NULL,
		[CompanyId] [int] NOT NULL,
		[ProjectId] [int] NOT NULL,
		[TeamId] [int] NOT NULL,
		[StartDate] [datetime2] NULL,
		[EndDate] [datetime2] NULL,
		[ObjectId] [uniqueidentifier] NULL,
		[ShortCode] [nvarchar](500) NULL,
		[Name] [nvarchar](500) NULL,
		[Description] [nvarchar](1000) NULL,
		[Created] [datetime2](7) NOT NULL,
		[Modified] [datetime2](7) NOT NULL,
		[CreatorId] [int] NOT NULL,
		[ModifierId] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
	(
		[SprintId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
END

GO

-- Create Stories
IF NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Stories')
BEGIN
    
	CREATE TABLE [dbo].[Stories](
		[StoryId] [int] IDENTITY(1,1) NOT NULL,
		[CompanyId] [int] NOT NULL,
		[ProjectId] [int] NOT NULL,
		[FeatureId] [int] NULL,
		[Priority] [int] NULL,
		[AcceptanceCriteria] [nvarchar](1000) NULL,
		[ObjectId] [uniqueidentifier] NULL,
		[ShortCode] [nvarchar](500) NULL,
		[Name] [nvarchar](500) NULL,
		[Description] [nvarchar](1000) NULL,
		[Created] [datetime2](7) NOT NULL,
		[Modified] [datetime2](7) NOT NULL,
		[CreatorId] [int] NOT NULL,
		[ModifierId] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
	(
		[StoryId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
END

GO

-- Create Teams
IF NOT EXISTS (SELECT * 
                 FROM INFORMATION_SCHEMA.TABLES 
                 WHERE TABLE_SCHEMA = 'dbo' 
                 AND  TABLE_NAME = 'Teams')
BEGIN
    
	CREATE TABLE [dbo].[Teams](
		[TeamId] [int] IDENTITY(1,1) NOT NULL,
		[CompanyId] [int] NOT NULL,
		[ObjectId] [uniqueidentifier] NULL,
		[ShortCode] [nvarchar](500) NULL,
		[Name] [nvarchar](500) NULL,
		[Description] [nvarchar](1000) NULL,
		[Created] [datetime2](7) NOT NULL,
		[Modified] [datetime2](7) NOT NULL,
		[CreatorId] [int] NOT NULL,
		[ModifierId] [int] NOT NULL,
	PRIMARY KEY CLUSTERED 
	(
		[TeamId] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY]
	
END

GO








