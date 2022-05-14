
use BitCoin
go

--create table Users
--(
--	Id INT IDENTITY PRIMARY KEY,
--	Email NVARCHAR(500) NOT NULL,
--	Password NVARCHAR(500) NOT NULL,
--)

--create table OrderOperation
--(
--	Id INT IDENTITY PRIMARY KEY,
--	Name NVARCHAR(100) NOT NULL,
--)

--create table Orders
--(
--	Id INT IDENTITY PRIMARY KEY,
--	Amount DECIMAL NOT NULL,
--	Price DECIMAL NOT NULL,
--	OperationId INT FOREIGN KEY REFERENCES OrderOperation(Id),
--	CreatorId INT FOREIGN KEY REFERENCES Users(Id)
--)

--insert into OrderOperation select 'Buy'
--insert into OrderOperation select 'Sell'

SELECT			*
FROM			Users

SELECT			*
FROM			Orders o
join			OrderOperation op on o.OperationId = op.Id

--create or alter procedure GetUser
--	@Email NVARCHAR(500),
--	@Password NVARCHAR(500)
--AS

--SELECT			u.Id					Id
--			   ,u.Email					Email
--			   ,u.Password				Password
--			   ,o.Id					Id
--			   ,o.Amount				Amount
--			   ,o.Price					Price
--			   ,o.OperationId			Operation
--FROM			Users u
--left join		Orders o on o.CreatorId = u.Id
--where			u.Email = @Email
--and				u.Password = @Password


--create or alter procedure InsertUser
--	@Email NVARCHAR(500),
--	@Password NVARCHAR(500)
--AS

--INSERT INTO Users SELECT @Email, @Password

--SELECT	SCOPE_IDENTITY()

--create or alter procedure InsertOrder
--	@Amount DECIMAL,
--	@Price DECIMAL,
--	@OperationId INT,
--	@CreatorId INT
--AS

--INSERT INTO Orders (Amount, Price, OperationId, CreatorId) SELECT @Amount, @Price, @OperationId, @CreatorId

--SELECT	SCOPE_IDENTITY()