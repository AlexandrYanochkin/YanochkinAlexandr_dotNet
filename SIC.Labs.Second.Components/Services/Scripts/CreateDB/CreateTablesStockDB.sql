USE [StockDB];

CREATE TABLE Manufacturer(
	ID INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(20),
	Adress NVARCHAR(20),
	PhoneNumber NVARCHAR(20) 
);	

CREATE TABLE Employee(
	ID INT PRIMARY KEY IDENTITY,
	FullName NVARCHAR(20),
	Age INT,
	PhoneNumber NVARCHAR(20),
	CHECK (Age >= 18 AND Age <= 75)
);

CREATE TABLE Stock(
	ID INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(20),
	[Address] NVARCHAR(20),
	Allowance INT,
	CHECK (Allowance >= 0 AND Allowance <= 1)
);

CREATE TABLE Commodity(
	ID INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(20),
	Price MONEY,
	ManufacturerID INT,
	FOREIGN KEY (ManufacturerID) REFERENCES Manufacturer(ID)
);

CREATE TABLE StockItem(
	ID INT PRIMARY KEY IDENTITY,
	[Count] INT,
	StockID INT,
	CommodityID INT,
	FOREIGN KEY (StockID) REFERENCES Stock(ID),
	FOREIGN KEY (CommodityID) REFERENCES Commodity(ID)
);

CREATE TABLE [Order](
	ID INT PRIMARY KEY IDENTITY,
	CreateDate DATE,
	ModificationDate DATE,
	[Status] INT,
	[Count] INT,
	FullPrice INT,
	StockItemID INT,
	EmployeeID INT, 
	FOREIGN KEY (StockItemID) REFERENCES StockItem(ID),
	FOREIGN KEY (EmployeeID) REFERENCES Employee(ID),
	CHECK (ModificationDate >= CreateDate)
);