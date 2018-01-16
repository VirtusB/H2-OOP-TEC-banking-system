
USE master

GO
ALTER DATABASE H2_OOP_TEC_Banking_System SET SINGLE_USER WITH ROLLBACK IMMEDIATE
GO

IF EXISTS(select * from sys.databases where name='H2_OOP_TEC_Banking_System')
DROP DATABASE H2_OOP_TEC_Banking_System
CREATE DATABASE H2_OOP_TEC_Banking_System

GO

USE H2_OOP_TEC_Banking_System


GO

CREATE TABLE Customers (
	CustomerID int IDENTITY(1,1) PRIMARY KEY,
	Created datetime NOT NULL DEFAULT GETDATE(),
	FirstName nvarchar(50) NOT NULL,
	LastName nvarchar(50) NOT NULL
	)

GO

Create Table AccountTypes (
	AccountTypeID int PRIMARY KEY,
	AccountTypeName nvarchar(50),
	InterestRate decimal NOT NULL default 0
)

GO


Create Table Accounts (
	AccountID int IDENTITY(1,1) PRIMARY KEY,
	CustomerId int NOT NULL FOREIGN KEY REFERENCES Customers(CustomerID),
	Created datetime DEFAULT GETDATE(),
	AccountNo int NOT NULL,
	AccountTypeId int FOREIGN KEY REFERENCES AccountTypes(AccountTypeID),
	Saldo decimal,
	Active bit NOT NULL default 1,
	)

GO

CREATE TABLE TransactionTypes (
	TransactionTypeID int PRIMARY KEY NOT NULL ,
	TransactionName nvarchar(50) NOT NULL
	)

INSERT INTO TransactionTypes(TransactionTypeID,TransactionName)
VALUES 
(1, 'Withdrawal'),
(2, 'Deposit')

GO

CREATE TABLE Transactions (
	TransactionID int IDENTITY(1,1) PRIMARY KEY,
	AccountId int NOT NULL FOREIGN KEY REFERENCES Accounts(AccountID),
	Created datetime NOT NULL default GETDATE(),
	Amount decimal NOT NULL,
	TransactionTypeId int NOT NULL FOREIGN KEY REFERENCES TransactionTypes(TransactionTypeID)
	)

GO

