CREATE DATABASE BigMoneyDb;
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
	Balance DECIMAL(18, 2)
);
GO
CREATE TABLE .[AccountTransaction]
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
		, AccountTypeId as accountTypeId
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
