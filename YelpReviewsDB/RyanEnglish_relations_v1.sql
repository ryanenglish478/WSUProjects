SQL DDL:
CREATE TABLE State(
	StateName VARCHAR(14) NOT NULL,
	PRIMARY KEY(StateName)
);

CREATE TABLE CityInState(
	StateName VARCHAR(14) NOT NULL,
	CityName VARCHAR(30) NOT NULL,
	PRIMARY KEY(CityName)
	FOREIGN KEY (StateName) REFERENCES State(StateName),
	FOREIGN KEY (CityName) REFERENCES City(CityName)
);

CREATE TABLE City(
	CityName VARCHAR(30) NOT NULL,
	State VARCHAR(14) NOT NULL,
	PRIMARY KEY(CityName)
	FOREIGN KEY (State) REFERENCES State(StateName)
);

CREATE TABLE ZipInCity(
	ZipCode CHAR(7) NOT NULL,
	CityName VARCHAR(30),
	PRIMARY KEY(ZipCode),
	FOREIGN KEY (CityName) REFERENCES City(CityName)
);

CREATE TABLE ZipCode(
	ZipCode CHAR(7) NOT NULL,
	NumOfBusiness INTEGER,
	Population INTEGER,
	AvgIncome FLOAT,
	City VARCHAR(30) NOT NULL, 
	State VARCHAR(14) NOT NULL,
	PRIMARY KEY(ZipCode),
	FOREIGN KEY (City) REFERENCES City(CityName),
	FOREIGN KEY (State) REFERENCES State(StateName)
);

CREATE TABLE BusinessInZip(
	Name VARCHAR(50) NOT NULL,
	Address VARCHAR (100) NOT NULL,
	ZipCode CHAR(7) NOT NULL,
	PRIMARY KEY(Name, Address),
	FOREIGN KEY (Name, Address) REFERENCES Business(Name, Address),
	FOREIGN KEY (ZipCode) REFERENCES ZipCode(ZipCode)
);

CREATE TABLE Business(
	Name VARCHAR(50) NOT NULL,
	Address VARCHAR(100) NOT NULL,
	City VARCHAR(30) NOT NULL,
	Stars INTEGER,
	NumReviews INTEGER,
	AvgRating FLOAT,
	NumCheckIn INTEGER,
	PRIMARY KEY (Name, Address)
	FOREIGN KEY (City) REFERENCES City(CityName)
);
	
CREATE TABLE Contains(
	CategoryName VARCHAR(30) NOT NULL,
	Name VARCHAR(50) NOT NULL,
	Address VARCHAR(100) NOT NULL,
	PRIMARY KEY (Name, Address),
	FOREIGN KEY (CategoryName) REFERENCES Category(CategoryName),
	FOREIGN KEY (Name, Address) REFERENCES Business(Name, Address)
);

CREATE TABLE Category(
	CategoryName VARCHAR(30) NOT NULL,
	NumOfBusiness INTEGER,
	PRIMARY KEY (CategoryName)
);

CREATE TABLE ReviewOf(
	UserName VARCHAR(50) NOT NULL,
	Name VARCHAR(50) NOT NULL,
	Address VARCHAR(100) NOT NULL,
	PRIMARY KEY (UserName),
	FOREIGN KEY (UserName) REFERENCES User(UserName),
	FOREIGN KEY (Name, Address) REFERENCES Business(Name, Address)
);

CREATE TABLE Review(
	ReviewRating FLOAT,
	Stars INT,
	UserName VARCHAR(50) NOT NULL,
	PRIMARY KEY (UserName)
	FOREIGN KEY (UserName) REFERENCES User(UserName)
);

CREATE TABLE UserReview(
	UserName VARCHAR(50) NOT NULL,
	PRIMARY KEY (UserName)
	FOREIGN KEY (UserName) REFERENCES User(UserName)
);

CREATE TABLE User(
	UserName VARCHAR(50) NOT NULL,
	PRIMARY KEY (UserName)
);
	
