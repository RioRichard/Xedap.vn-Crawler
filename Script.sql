/*
Created		3/6/2022
Modified		3/8/2022
Project		
Model			
Company		
Author		
Version		
Database		MS SQL 2005 
*/


Create table [Role]
(
	[IDRole] uniqueidentifier NOT NULL,
	[RoleName] Nvarchar(50) NULL,
	[IsDelete] Bit NULL,
Primary Key ([IDRole])
) 
go

Create table [AccountStaff]
(
	[IDStaff] varchar(64) NOT NULL,
	[UserName] Varbinary(32) NULL,
	[Password] Varbinary(64) NULL,
	[Email] Nvarchar(50) NULL,
	[Token] Varchar(64) NULL,
	[ExpiredTokenTime] Datetime NULL,
	[IsConfirmed] Bit NULL,
	[IsDelete] Bit NULL,
	[FullName] Nvarchar(50) NULL,
	[Gender] Bit NULL,
Primary Key ([IDStaff])
) 
go

Create table [StaffRole]
(
	[IDRole] uniqueidentifier foreign key references Role(IDRole) NOT NULL,
	[IDStaff] varchar(64) foreign key references [AccountStaff]([IDStaff]) NOT NULL,
	[IsDelete] Bit NULL,
Primary Key ([IDRole],[IDStaff])
) 
go

Create table [Account]
(
	[IDAccount] Varchar(64) NOT NULL,
	[UserName] Varbinary(32) NULL,
	[Password] Varchar(32) NULL,
	[Email] Varchar(50) NULL,
	[Token] Varchar(64) NULL,
	[ExpiredTokenTime] Datetime NULL,
	[IsConfirmed] Bit NULL,
	[IsDelete] Bit NULL,
	[FullName] Nvarchar(50) NULL,
	[Gender] Bit NULL,
Primary Key ([IDAccount])
) 
go

Create table [Address]
(
	[IDAddress] Integer identity(1,1) NOT NULL ,
	[Phone] Varchar(12) NULL,
	[Reciever] Nvarchar(64) NULL,
Primary Key ([IDAddress])
) 
go

Create table [AccountAddress]
(
	[IDAddress] Integer foreign key references Address(IDAddress) NOT NULL,
	[IDAccount] Varchar(64) foreign key references Account(IdAccount) NOT NULL,
	[IsDefault] Bit NULL,
Primary Key ([IDAddress],[IDAccount])
) 
go

Create table [Product]
(
	[IDProduct] Integer identity(1,1) NOT NULL,
	[IDCategory] Integer NOT NULL,
	[Name] Nvarchar(128) NULL,
	[Price] Integer NULL,
	[Stock] Integer NULL,
	[ImageURL] Varchar(256) NULL,
	[IsDelete] Bit NULL,
	[Description] Nvarchar(1024) NULL,
Primary Key ([IDProduct])
) 
go

Create table [Category]
(
	[IDCategory] Integer identity(1,1) NOT NULL,
	[CategoryName] Nvarchar(64) NULL,
	[Isdelete] Bit NULL,
Primary Key ([IDCategory])
) 
go

Create table [Attribute]
(
	[IDAttribute] Integer identity(1,1) NOT NULL,
	[AttributeName] Nvarchar(64) NULL,
	[IsDelete] Bit NULL,
Primary Key ([IDAttribute])
) 
go

Create table [ProductAttribute]
(
	[IDProduct] Integer NOT NULL,
	[IDAttribute] Integer NOT NULL,
Primary Key ([IDProduct],[IDAttribute])
) 
go

Create table [Cart]
(
	[IDCart] uniqueidentifier NOT NULL,
	[IsExpired] Bit NULL,
	[IDAccount] Varchar(20) NOT NULL,
Primary Key ([IDCart])
) 
go

Create table [Invoice]
(
	[IDInvoice] Uniqueidentifier NOT NULL,
	[DateCreated] Datetime NULL,
	[DateExpired] Datetime NULL,
	[IDCart] Integer NOT NULL,
	[IDAddress] Integer NOT NULL,
	[IDStatus] Integer NOT NULL,
Primary Key ([IDInvoice])
) 
go

Create table [Status]
(
	[IDStatus] Integer identity(1,1) NOT NULL,
	[StatusName] Nvarchar(64) NULL,
	[IsDelete] Bit NULL,
Primary Key ([IDStatus])
) 
go

Create table [ProductCart]
(
	[IDCart] uniqueidentifier NOT NULL,
	[IDProduct] Integer NOT NULL,
	[Quantity] Integer NULL,
	[PaymentPrice] Integer NULL,
Primary Key ([IDCart],[IDProduct])
) 
go


Set quoted_identifier on
go


Set quoted_identifier off
go


