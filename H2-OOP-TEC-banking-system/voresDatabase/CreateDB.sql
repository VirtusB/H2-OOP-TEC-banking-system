
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
	LastName nvarchar(50) NOT NULL,
	Address nvarchar(50),
	City nvarchar(50),
	PostalCode int,
	Active bit NOT NULL default 1
	)

GO

INSERT INTO Customers (FirstName, LastName, Address, City, PostalCode)
	VALUES 	('Test', 'Testman', 'Testvej', 'TestCity', 2400),
			('Testie','Testman2', 'Testgade', 'TestVillage', 2800)
	
Create Table AccountTypes (
	AccountTypeID int IDENTITY(1,1) PRIMARY KEY,
	AccountTypeName nvarchar(50),
	InterestRate float NOT NULL default 0
)

GO

INSERT INTO ACcountTypes (AccountTypeName, InterestRate)
VALUES	('Opsparing', 0.027), 
		('Pensionskonto', 0.04), 
		('Børneopsparing', 0.05), 
		('BudgetKonto', 0.01)
		
INSERT INTO ACcountTypes (AccountTypeName)
VALUES	('Lån'),
		('Lønkonto')

Create Table Accounts (
	AccountID int IDENTITY(1,1) PRIMARY KEY,
	CustomerId int NOT NULL FOREIGN KEY REFERENCES Customers(CustomerID),
	Created datetime DEFAULT GETDATE(),
	AccountNo nvarchar(50) NOT NULL,
	AccountTypeId int FOREIGN KEY REFERENCES AccountTypes(AccountTypeID),
	Saldo float default 0,
	Active bit NOT NULL default 1,
	)

GO

INSERT INTO Accounts (customerId, AccountNo, AccountTypeId, saldo)
VALUES	(1, 1050, 3, 10000),
		(2, 1100, 2, -3.50)
		(2, 4250, 2, 1337.65)

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
	Amount float NOT NULL,
	TransactionTypeId int NOT NULL FOREIGN KEY REFERENCES TransactionTypes(TransactionTypeID)
	)

GO

INSERT INTO Transactions (AccountId, Amount, TransactionTypeID)
VALUES		(1, 200, 1),
			(1, -300, 2)

GO

/*SELECT CONCAT(FirstName, ' ', Lastname) as Name,
Active,
AccountTypes.AccountTypeName
FROM customers
INNER JOIN Accounts ON Customers.CustomerID = Accounts.CustomerId
INNER JOIN AccountTypes ON Accounts.AccountTypeId = AccountTypes.AccountTypeID
*/
