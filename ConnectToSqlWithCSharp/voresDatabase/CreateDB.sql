
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
		('BudgetKonto', 0.01),
		('Børnekonto', 0.008)
		
INSERT INTO ACcountTypes (AccountTypeName)
VALUES	('Lånekonto'),
		('Lønkonto')

Create Table Accounts (
	AccountID int IDENTITY(1,1) PRIMARY KEY,
	CustomerId int NOT NULL FOREIGN KEY REFERENCES Customers(CustomerID),
	Created datetime DEFAULT GETDATE(),
	AccountNo int NOT NULL UNIQUE,
	AccountTypeId int FOREIGN KEY REFERENCES AccountTypes(AccountTypeID),
	Saldo float default 0,
	Active bit NOT NULL default 1,
	)

GO

INSERT INTO Accounts (customerId, AccountNo, AccountTypeId, saldo)
VALUES	(1, 1001, 3, 10000),
		(2, 1002, 2, -3.50),
		(2, 1003, 6, 1337.65)

CREATE TABLE TransactionTypes (
	TransactionTypeID int PRIMARY KEY NOT NULL ,
	TransactionName nvarchar(50) NOT NULL
	)

INSERT INTO TransactionTypes(TransactionTypeID,TransactionName)
VALUES 
(1, 'Udbetaling'),
(2, 'Indbetaling')

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
VALUES		(1, 200, 2),
			(1, -300, 1),
			(3, 600, 2),
			(2, -10, 1)

GO

CREATE TABLE Users (
	UserID int IDENTITY(1,1) PRIMARY KEY,
	Created DATETIME default GETDATE(),
	UserName nvarchar(50) NOT NULL,
	UserPassword nvarchar(32) NOT NULL
	)
GO

INSERT INTO Users (UserName, UserPassword)
VALUES ('bruger', '5f4dcc3b5aa765d61d8327deb882cf99')
	   


CREATE TABLE UserLogging (
	LoggingID int IDENTITY(1,1) PRIMARY KEY,
	UserId int NOT NULL FOREIGN KEY REFERENCES Users(UserID),
	Timestamp datetime default GETDATE()
	)
GO

INSERT INTO USerLogging (UserId)
VALUES (1)
		
	
INSERT INTO USerLogging (UserId,timestamp)
VALUES (1, '2018-01-20'),
	   (1, '2018-01-20')
	   
		
	

/*SELECT CONCAT(FirstName, ' ', Lastname) as Name,
Active,
AccountTypes.AccountTypeName
FROM customers
INNER JOIN Accounts ON Customers.CustomerID = Accounts.CustomerId
INNER JOIN AccountTypes ON Accounts.AccountTypeId = AccountTypes.AccountTypeID



SELECT AccountNo, AccountTypeName, Transactions.Created, Transactions.Amount, TransactionName
FROM customers
JOIN Accounts ON Customers.CustomerID = Accounts.CustomerId
JOIN AccountTypes ON Accounts.AccountTypeId = AccountTypes.AccountTypeID
JOIN Transactions ON Accounts.AccountID = Transactions.AccountId
JOIN TransactionTypes ON Transactions.TransactiontypeId = TransactionTypes.TransactionTypeID
WHERE Customers.customerid = 1


SELECT Transactions.TransactionID, Accounts.AccountNo, Transactions.Created, Transactions.Amount, TransactionTypes.TransactionName
FROM Transactions
JOIN Accounts ON Accounts.AccountID = Transactions.AccountId
JOIN TransactionTypes ON Transactions.TransactionTypeId = TransactionTypes.TransactionTypeID
WHERE AccountNO = 4250

SELECT AccountID, Accounts.CustomerId, Accounts.Created, AccountNo, Accounts.AccountTypeId, Saldo, Accounts.Active FROM Accounts 
INNER JOIN AccountTypes ON accounts.AccountTypeId = accounttypes.AccountTypeID
INNER JOIN customers ON accounts.customerid = Customers.CustomerID
WHERE CONCAT(firstname, ' ',lastname) like '%%'

DELETE FROM Transactions
WHERE AccountID IN(SELECT DISTINCT AccountID FROM Accounts WHERE accounts.CustomerId=2); 
DELETE FROM Accounts WHERE Accounts.customerId = 2;
DELETE FROM Customers where CustomerID = 2;

SELECT COUNT(*) FROM Customers where CustomerID = @custid

cmd cmd.Scalar != 1
*/
