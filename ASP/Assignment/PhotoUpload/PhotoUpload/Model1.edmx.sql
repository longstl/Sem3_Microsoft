
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/01/2018 22:50:42
-- Generated from EDMX file: E:\project\Sem3_Microsoft\ASP\Assignment\PhotoUpload\PhotoUpload\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [AssignmentDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Customer_Price_Method]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Customer] DROP CONSTRAINT [FK_Customer_Price_Method];
GO
IF OBJECT_ID(N'[dbo].[FK_Image_Customer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Image] DROP CONSTRAINT [FK_Image_Customer];
GO
IF OBJECT_ID(N'[dbo].[FK_Order_Customer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_Customer];
GO
IF OBJECT_ID(N'[dbo].[FK_Order_Detail_Image]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order_Detail] DROP CONSTRAINT [FK_Order_Detail_Image];
GO
IF OBJECT_ID(N'[dbo].[FK_Order_Detail_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order_Detail] DROP CONSTRAINT [FK_Order_Detail_Order];
GO
IF OBJECT_ID(N'[dbo].[FK_Order_Detail_Price]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order_Detail] DROP CONSTRAINT [FK_Order_Detail_Price];
GO
IF OBJECT_ID(N'[dbo].[FK_Order_Employee]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order_Employee];
GO
IF OBJECT_ID(N'[dbo].[FK_Price_Material]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Price] DROP CONSTRAINT [FK_Price_Material];
GO
IF OBJECT_ID(N'[dbo].[FK_Price_Size]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Price] DROP CONSTRAINT [FK_Price_Size];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Admin]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Admin];
GO
IF OBJECT_ID(N'[dbo].[Customer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customer];
GO
IF OBJECT_ID(N'[dbo].[Employee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee];
GO
IF OBJECT_ID(N'[dbo].[Image]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Image];
GO
IF OBJECT_ID(N'[dbo].[Material]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Material];
GO
IF OBJECT_ID(N'[dbo].[Order]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Order];
GO
IF OBJECT_ID(N'[dbo].[Order_Detail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Order_Detail];
GO
IF OBJECT_ID(N'[dbo].[Payment_Method]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Payment_Method];
GO
IF OBJECT_ID(N'[dbo].[Price]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Price];
GO
IF OBJECT_ID(N'[dbo].[Size]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Size];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Admins'
CREATE TABLE [dbo].[Admins] (
    [id] int IDENTITY(1,1) NOT NULL,
    [firstName] nvarchar(50)  NOT NULL,
    [lastName] nvarchar(50)  NOT NULL,
    [email] nvarchar(50)  NOT NULL,
    [phone] nvarchar(50)  NOT NULL,
    [address] nvarchar(50)  NOT NULL,
    [username] nvarchar(50)  NOT NULL,
    [password] nvarchar(50)  NOT NULL,
    [salt] nvarchar(50)  NOT NULL,
    [status] int  NOT NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [id] int IDENTITY(1,1) NOT NULL,
    [username] nvarchar(50)  NOT NULL,
    [password] nvarchar(50)  NOT NULL,
    [salt] nvarchar(50)  NOT NULL,
    [firstName] nvarchar(50)  NOT NULL,
    [lastName] nvarchar(50)  NOT NULL,
    [address] nvarchar(50)  NOT NULL,
    [email] nvarchar(50)  NOT NULL,
    [phone] nvarchar(50)  NOT NULL,
    [Payment_Method_id] int  NOT NULL,
    [status] int  NOT NULL,
    [IsEmailVerified] bit  NOT NULL,
    [ActivationCode] uniqueidentifier  NOT NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [id] int IDENTITY(1,1) NOT NULL,
    [firstName] nvarchar(50)  NOT NULL,
    [lastName] nvarchar(50)  NOT NULL,
    [email] nvarchar(50)  NOT NULL,
    [phone] nvarchar(50)  NOT NULL,
    [address] nvarchar(50)  NOT NULL,
    [username] nvarchar(50)  NOT NULL,
    [password] nvarchar(50)  NOT NULL,
    [salt] nvarchar(50)  NOT NULL,
    [status] int  NOT NULL
);
GO

-- Creating table 'Images'
CREATE TABLE [dbo].[Images] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Customer_id] int  NOT NULL,
    [url] varchar(max)  NOT NULL,
    [title] nvarchar(50)  NOT NULL,
    [description] varchar(max)  NOT NULL
);
GO

-- Creating table 'Materials'
CREATE TABLE [dbo].[Materials] (
    [id] int IDENTITY(1,1) NOT NULL,
    [material_name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [id] int IDENTITY(1,1) NOT NULL,
    [shipName] nvarchar(50)  NOT NULL,
    [shipAddress] nvarchar(50)  NOT NULL,
    [shipPhone] nvarchar(50)  NOT NULL,
    [shipEmail] nvarchar(50)  NOT NULL,
    [Customer_id] int  NOT NULL,
    [total_Price] decimal(10,2)  NOT NULL,
    [status] int  NOT NULL,
    [Employee_id] int  NOT NULL,
    [email_title] nvarchar(50)  NOT NULL,
    [email_text] varchar(max)  NOT NULL
);
GO

-- Creating table 'Order_Detail'
CREATE TABLE [dbo].[Order_Detail] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Order_id] int  NOT NULL,
    [Image_id] int  NOT NULL,
    [Price_id] int  NOT NULL,
    [quantity] decimal(10,2)  NOT NULL,
    [unitPrice] decimal(10,2)  NOT NULL
);
GO

-- Creating table 'Payment_Method'
CREATE TABLE [dbo].[Payment_Method] (
    [id] int IDENTITY(1,1) NOT NULL,
    [payment_name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Prices'
CREATE TABLE [dbo].[Prices] (
    [id] int IDENTITY(1,1) NOT NULL,
    [price1] decimal(10,2)  NOT NULL,
    [Size_id] int  NOT NULL,
    [Material_id] int  NOT NULL
);
GO

-- Creating table 'Sizes'
CREATE TABLE [dbo].[Sizes] (
    [id] int IDENTITY(1,1) NOT NULL,
    [size_name] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [id] in table 'Admins'
ALTER TABLE [dbo].[Admins]
ADD CONSTRAINT [PK_Admins]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [PK_Images]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Materials'
ALTER TABLE [dbo].[Materials]
ADD CONSTRAINT [PK_Materials]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Order_Detail'
ALTER TABLE [dbo].[Order_Detail]
ADD CONSTRAINT [PK_Order_Detail]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Payment_Method'
ALTER TABLE [dbo].[Payment_Method]
ADD CONSTRAINT [PK_Payment_Method]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Prices'
ALTER TABLE [dbo].[Prices]
ADD CONSTRAINT [PK_Prices]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Sizes'
ALTER TABLE [dbo].[Sizes]
ADD CONSTRAINT [PK_Sizes]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Payment_Method_id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [FK_Customer_Price_Method]
    FOREIGN KEY ([Payment_Method_id])
    REFERENCES [dbo].[Payment_Method]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Customer_Price_Method'
CREATE INDEX [IX_FK_Customer_Price_Method]
ON [dbo].[Customers]
    ([Payment_Method_id]);
GO

-- Creating foreign key on [Customer_id] in table 'Images'
ALTER TABLE [dbo].[Images]
ADD CONSTRAINT [FK_Image_Customer]
    FOREIGN KEY ([Customer_id])
    REFERENCES [dbo].[Customers]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Image_Customer'
CREATE INDEX [IX_FK_Image_Customer]
ON [dbo].[Images]
    ([Customer_id]);
GO

-- Creating foreign key on [Customer_id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Order_Customer]
    FOREIGN KEY ([Customer_id])
    REFERENCES [dbo].[Customers]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_Customer'
CREATE INDEX [IX_FK_Order_Customer]
ON [dbo].[Orders]
    ([Customer_id]);
GO

-- Creating foreign key on [Employee_id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Order_Employee]
    FOREIGN KEY ([Employee_id])
    REFERENCES [dbo].[Employees]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_Employee'
CREATE INDEX [IX_FK_Order_Employee]
ON [dbo].[Orders]
    ([Employee_id]);
GO

-- Creating foreign key on [Image_id] in table 'Order_Detail'
ALTER TABLE [dbo].[Order_Detail]
ADD CONSTRAINT [FK_Order_Detail_Image]
    FOREIGN KEY ([Image_id])
    REFERENCES [dbo].[Images]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_Detail_Image'
CREATE INDEX [IX_FK_Order_Detail_Image]
ON [dbo].[Order_Detail]
    ([Image_id]);
GO

-- Creating foreign key on [Material_id] in table 'Prices'
ALTER TABLE [dbo].[Prices]
ADD CONSTRAINT [FK_Price_Material]
    FOREIGN KEY ([Material_id])
    REFERENCES [dbo].[Materials]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Price_Material'
CREATE INDEX [IX_FK_Price_Material]
ON [dbo].[Prices]
    ([Material_id]);
GO

-- Creating foreign key on [Order_id] in table 'Order_Detail'
ALTER TABLE [dbo].[Order_Detail]
ADD CONSTRAINT [FK_Order_Detail_Order]
    FOREIGN KEY ([Order_id])
    REFERENCES [dbo].[Orders]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_Detail_Order'
CREATE INDEX [IX_FK_Order_Detail_Order]
ON [dbo].[Order_Detail]
    ([Order_id]);
GO

-- Creating foreign key on [Price_id] in table 'Order_Detail'
ALTER TABLE [dbo].[Order_Detail]
ADD CONSTRAINT [FK_Order_Detail_Price]
    FOREIGN KEY ([Price_id])
    REFERENCES [dbo].[Prices]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_Detail_Price'
CREATE INDEX [IX_FK_Order_Detail_Price]
ON [dbo].[Order_Detail]
    ([Price_id]);
GO

-- Creating foreign key on [Size_id] in table 'Prices'
ALTER TABLE [dbo].[Prices]
ADD CONSTRAINT [FK_Price_Size]
    FOREIGN KEY ([Size_id])
    REFERENCES [dbo].[Sizes]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Price_Size'
CREATE INDEX [IX_FK_Price_Size]
ON [dbo].[Prices]
    ([Size_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------