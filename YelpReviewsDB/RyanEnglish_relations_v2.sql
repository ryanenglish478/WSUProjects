SQL DDL: 
CREATE TABLE State( 
	StateName VARCHAR(14) NOT NULL,
	PRIMARY KEY(StateName)
);
//done insert

CREATE TABLE City(
	CityName VARCHAR(30) NOT NULL,
	UNIQUE(CityName),
	State VARCHAR(14) NOT NULL,
	PRIMARY KEY(CityName, State),
	FOREIGN KEY (State) REFERENCES State(StateName)
);


//doneinsert
//done
CREATE TABLE ZipCode(
	ZipCode CHAR(7) NOT NULL,
	NumOfBusiness INTEGER,
	Population INTEGER,
	AvgIncome FLOAT,
	AvgCheckin FLOAT,
	City VARCHAR(30) NOT NULL, 
	State VARCHAR(14) NOT NULL,
	PRIMARY KEY(ZipCode),
	FOREIGN KEY (City) REFERENCES City(CityName),
	FOREIGN KEY (State) REFERENCES State(StateName)
);//doneinsert
//done
CREATE TABLE Business(
	business_id VARCHAR(40) NOT NULL,
	UNIQUE(business_id),
	name VARCHAR(50) NOT NULL,
	address VARCHAR(100) NOT NULL,
	state VARCHAR(2) NOT NULL,
	city VARCHAR(30) NOT NULL,
	zipcode INTEGER NOT NULL,
	stars FLOAT,
	reviewrating FLOAT,
	numCheckins INTEGER,
	PRIMARY KEY (business_id)
);
//doneinsert
CREATE TABLE Contains(
	CategoryName VARCHAR(30) NOT NULL,
	business_id VARCHAR(40) NOT NULL,
	PRIMARY KEY (CategoryName, business_id),
	FOREIGN KEY (CategoryName) REFERENCES Category(CategoryName),
	FOREIGN KEY (business_id) REFERENCES Business(business_id)
);
//done
CREATE TABLE Category(
	CategoryName VARCHAR(30) NOT NULL,
	NumOfBusiness INTEGER,
	PRIMARY KEY (CategoryName)
);
//done

CREATE TABLE Review(
	Stars INT,
	user_id VARCHAR(50) NOT NULL,
	business_id VARCHAR(50) NOT NULL,
	PRIMARY KEY (user_id, business_id),
	FOREIGN KEY (user_id) REFERENCES "User"(user_id),
	FOREIGN KEY (business_id) REFERENCES Business(business_id)
);

CREATE TABLE "User"(
	UserName VARCHAR(50) NOT NULL,
	user_id VARCHAR(50) NOT NULL,
	UNIQUE(UserName, user_id),
	PRIMARY KEY (user_id)
);
	