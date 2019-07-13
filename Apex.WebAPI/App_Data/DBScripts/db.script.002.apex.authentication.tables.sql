
SET QUOTED_IDENTIFIER OFF;
GO

IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_UserRoles_Roles]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRoles] DROP CONSTRAINT [FK_UserRoles_Roles];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRoles_Users]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRoles] DROP CONSTRAINT [FK_UserRoles_Users];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[UserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRoles];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [RoleId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(250)  NULL,
    [ActiveFlag] bit  NOT NULL,
    [ModifiedBy] int  NOT NULL,
    [ModifiedDate] datetime  NOT NULL
);
GO

-- Creating table 'UserRoles'
CREATE TABLE [dbo].[UserRoles] (
    [UserRoleId] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [RoleId] int  NOT NULL,
    [ActiveFlag] bit  NOT NULL,
    [ModifiedBy] int  NOT NULL,
    [ModifiedDate] datetime  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [UserId] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NOT NULL,
    [MiddleName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NOT NULL,
    [Address] nvarchar(100)  NULL,
    [City] nvarchar(50)  NULL,
    [Province] nvarchar(50)  NULL,
    [Zip] nvarchar(50)  NULL,
    [HomePhone] nvarchar(50)  NULL,
    [OfficePhone] nvarchar(50)  NULL,
    [Mobile] nvarchar(50)  NULL,
    [Email] nvarchar(100)  NOT NULL,
    [LoginId] nvarchar(50)  NOT NULL,
    [LoginPassword] nvarchar(25)  NOT NULL,
    [ActiveFlag] bit  NOT NULL,
    [ModifiedBy] int  NOT NULL,
    [ModifiedDate] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [RoleId] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([RoleId] ASC);
GO

-- Creating primary key on [UserRoleId] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [PK_UserRoles]
    PRIMARY KEY CLUSTERED ([UserRoleId] ASC);
GO

-- Creating primary key on [UserId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([UserId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [RoleId] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [FK_UserRoles_Roles]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([RoleId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRoles_Roles'
CREATE INDEX [IX_FK_UserRoles_Roles]
ON [dbo].[UserRoles]
    ([RoleId]);
GO

-- Creating foreign key on [UserId] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [FK_UserRoles_Users]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([UserId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRoles_Users'
CREATE INDEX [IX_FK_UserRoles_Users]
ON [dbo].[UserRoles]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------
