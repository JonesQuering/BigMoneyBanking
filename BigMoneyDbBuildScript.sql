USE tempdb

DROP DATABASE IF EXISTS BigMoneyDb

CREATE DATABASE BigMoneyDb;
GO

USE BigMoneyDb
GO

CREATE SCHEMA t;
GO

CREATE SCHEMA p;
GO

CREATE TABLE t.Customer
(
	CustomerId INT PRIMARY KEY,
	FirstName NVARCHAR(50),
	LastName NVARCHAR(50),
	Email NVARCHAR(50),
	Phone NVARCHAR(50),
	Address NVARCHAR(50),
	City NVARCHAR(50),
	State NVARCHAR(50),
	Zip NVARCHAR(50),
	LastActiveDate DATETIME
);
GO

CREATE TABLE t.BankAccount
(
	AccountId INT,
	CustomerId INT,
	AccountType NVARCHAR(50),
	AccountNumber NVARCHAR(50),
	RoutingNumber NVARCHAR(50),
	Balance DECIMAL(18, 2),
	Inactive BIT
);
GO
CREATE TABLE t.[AccountTransaction]
(
	TransactionId INT PRIMARY KEY,
	AccountId INT,
	TransactionType NVARCHAR(50),
	Amount DECIMAL(18, 2),
	TransactionDate DATETIME
);
GO

CREATE OR ALTER PROCEDURE p.CreateBankAccount
	@customerId INT = 5,
	@deposit DECIMAL(18, 2) = 525.00,
	@accountTypeId NVARCHAR(50) = 2
AS
BEGIN 
SET NOCOUNT ON;
SET XACT_ABORT ON;
BEGIN TRY
	
	--if not exists (select 1 from t.Customer where CustomerId = @customerId)
	--	RETURN 1; --customer does not exist
	if @accountTypeId != 2
		RETURN 2 --invalid account type

	if @deposit < 100
		RETURN 3 --deposit amount too low

	INSERT INTO t.BankAccount (CustomerId, AccountType, Balance)
	VALUES (@customerId, @accountTypeId, @deposit)

	RETURN 0 --success

END TRY
BEGIN CATCH
	RETURN 1 --some random error
END CATCH
END;

GO

CREATE OR ALTER PROCEDURE p.GetBankAccounts
	@customerId INT = 5
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	BEGIN TRY
		SELECT CustomerId as customerId 
		, AccountId as accountId
		, AccountType as accountTypeId
		, Balance as balance		
		FROM t.BankAccount WHERE CustomerId = @customerId

		RETURN 0
	END TRY
	BEGIN CATCH
		RETURN 1 --some random error
	END CATCH
END;
GO

CREATE OR ALTER PROCEDURE p.UpdateBalance
     @customerId INT = 5
	,@accountId INT = 17
	,@amount DECIMAL(18, 2) = 525.00
AS 
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	BEGIN TRY
		UPDATE t.BankAccount
		SET Balance = Balance + @amount
		WHERE CustomerId = @customerId 
		AND AccountId = @accountId;

		RETURN 0
	END TRY
	BEGIN CATCH
		RETURN 1 --some random error
	END CATCH
END;

GO
CREATE OR ALTER PROC p.CloseAccount
	@customerId INT = 5
	,@accountId INT = 17
AS
BEGIN
	SET NOCOUNT ON;
	SET XACT_ABORT ON;
	BEGIN TRY

		DECLARE @balance DECIMAL(18, 2) = (SELECT TOP (1)Balance
		 FROM t.BankAccount 
		 WHERE CustomerId = @customerId 
		 AND AccountId = @accountId)

		IF @balance > 0
			RETURN 2 --balance must be zero to close account

		UPDATE t.BankAccount
		SET Inactive = 1
		WHERE CustomerId = @customerId 
		AND AccountId = @accountId;

		RETURN 0
	END TRY
	BEGIN CATCH
		RETURN 1 --some random error
	END CATCH
END;
GO

--create server user
CREATE LOGIN [BigMoneyAppUser] WITH PASSWORD = 'Password123';
GO

USE BigMoneyDb;
GO
--create database user

CREATE USER FOR LOGIN [BigMoneyAppUser];
GO

--grant execute permissions on proc schema
GRANT EXECUTE ON SCHEMA::p TO [BigMoneyAppUser];
GO

