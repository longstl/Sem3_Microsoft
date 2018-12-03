-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2018-11-25 04:35:55.938

-- tables
-- Table: Admin
CREATE TABLE Admin (
    id int  NOT NULL IDENTITY(1, 1),
    firstName nvarchar(50)  NOT NULL,
    lastName nvarchar(50)  NOT NULL,
    email nvarchar(50)  NOT NULL,
    phone nvarchar(50)  NOT NULL,
    address nvarchar(50)  NOT NULL,
    username nvarchar(50)  NOT NULL,
    password nvarchar(50)  NOT NULL,
    salt nvarchar(50)  NOT NULL,
    status int  NOT NULL DEFAULT 1,
    CONSTRAINT Admin_ak_1 UNIQUE (username, email, phone),
    CONSTRAINT Admin_pk PRIMARY KEY  (id)
);

-- Table: Customer
CREATE TABLE Customer (
    id int  NOT NULL IDENTITY(1, 1),
    username nvarchar(50)  NOT NULL,
    password nvarchar(50)  NOT NULL,
    salt nvarchar(50)  NOT NULL,
    firstName nvarchar(50)  NOT NULL,
    lastName nvarchar(50)  NOT NULL,
    address nvarchar(50)  NOT NULL,
    email nvarchar(50)  NOT NULL,
    phone nvarchar(50)  NOT NULL,
    IsEmailVerified bit  NOT NULL,
    ActivationCode uniqueidentifier  NOT NULL,
    Payment_Method_id int  NOT NULL,
    status int  NOT NULL DEFAULT 0,
    CONSTRAINT Customer_ak_1 UNIQUE (username, email),
    CONSTRAINT Customer_pk PRIMARY KEY  (id)
);

-- Table: Employee
CREATE TABLE Employee (
    id int  NOT NULL IDENTITY(1, 1),
    firstName nvarchar(50)  NOT NULL,
    lastName nvarchar(50)  NOT NULL,
    email nvarchar(50)  NOT NULL,
    phone nvarchar(50)  NOT NULL,
    address nvarchar(50)  NOT NULL,
    username nvarchar(50)  NOT NULL,
    password nvarchar(50)  NOT NULL,
    salt nvarchar(50)  NOT NULL,
    status int  NOT NULL DEFAULT 1,
    CONSTRAINT Employee_ak_1 UNIQUE (email, phone, username),
    CONSTRAINT Employee_pk PRIMARY KEY  (id)
);

-- Table: Image
CREATE TABLE Image (
    id int  NOT NULL IDENTITY(1, 1),
    Customer_id int  NOT NULL,
    url text  NOT NULL,
    title nvarchar(50)  NOT NULL,
    description text  NOT NULL,
    CONSTRAINT Image_pk PRIMARY KEY  (id)
);

-- Table: Material
CREATE TABLE Material (
    id int  NOT NULL IDENTITY(1, 1),
    material_name nvarchar(50)  NOT NULL,
    CONSTRAINT Material_pk PRIMARY KEY  (id)
);

-- Table: Order
CREATE TABLE "Order" (
    id int  NOT NULL IDENTITY(1, 1),
    shipName nvarchar(50)  NOT NULL,
    shipAddress nvarchar(50)  NOT NULL,
    shipPhone nvarchar(50)  NOT NULL,
    shipEmail nvarchar(50)  NOT NULL,
    Customer_id int  NOT NULL,
    total_Price decimal(10,2)  NOT NULL,
    status int  NOT NULL DEFAULT 0,
    Employee_id int  NOT NULL,
    email_title nvarchar(50)  NOT NULL,
    email_text text  NOT NULL,
    CONSTRAINT Order_pk PRIMARY KEY  (id)
);

-- Table: Order_Detail
CREATE TABLE Order_Detail (
    id int  NOT NULL IDENTITY(1, 1),
    Order_id int  NOT NULL,
    Image_id int  NOT NULL,
    Price_id int  NOT NULL,
    quantity int  NOT NULL,
    unitPrice decimal(10,2)  NOT NULL,
    CONSTRAINT Order_Detail_pk PRIMARY KEY  (id)
);

-- Table: Payment_Method
CREATE TABLE Payment_Method (
    id int  NOT NULL IDENTITY(1, 1),
    payment_name nvarchar(50)  NOT NULL,
    CONSTRAINT Payment_Method_pk PRIMARY KEY  (id)
);

-- Table: Price
CREATE TABLE Price (
    id int  NOT NULL IDENTITY(1, 1),
    price decimal(10,2)  NOT NULL,
    Size_id int  NOT NULL,
    Material_id int  NOT NULL,
    CONSTRAINT Price_pk PRIMARY KEY  (id)
);

-- Table: Size
CREATE TABLE Size (
    id int  NOT NULL IDENTITY(1, 1),
    size_name nvarchar(50)  NOT NULL,
    CONSTRAINT Size_pk PRIMARY KEY  (id)
);

-- foreign keys
-- Reference: Customer_Price_Method (table: Customer)
ALTER TABLE Customer ADD CONSTRAINT Customer_Price_Method
    FOREIGN KEY (Payment_Method_id)
    REFERENCES Payment_Method (id);

-- Reference: Image_Customer (table: Image)
ALTER TABLE Image ADD CONSTRAINT Image_Customer
    FOREIGN KEY (Customer_id)
    REFERENCES Customer (id);

-- Reference: Order_Customer (table: Order)
ALTER TABLE "Order" ADD CONSTRAINT Order_Customer
    FOREIGN KEY (Customer_id)
    REFERENCES Customer (id);

-- Reference: Order_Detail_Image (table: Order_Detail)
ALTER TABLE Order_Detail ADD CONSTRAINT Order_Detail_Image
    FOREIGN KEY (Image_id)
    REFERENCES Image (id);

-- Reference: Order_Detail_Order (table: Order_Detail)
ALTER TABLE Order_Detail ADD CONSTRAINT Order_Detail_Order
    FOREIGN KEY (Order_id)
    REFERENCES "Order" (id);

-- Reference: Order_Detail_Price (table: Order_Detail)
ALTER TABLE Order_Detail ADD CONSTRAINT Order_Detail_Price
    FOREIGN KEY (Price_id)
    REFERENCES Price (id);

-- Reference: Order_Employee (table: Order)
ALTER TABLE "Order" ADD CONSTRAINT Order_Employee
    FOREIGN KEY (Employee_id)
    REFERENCES Employee (id);

-- Reference: Price_Material (table: Price)
ALTER TABLE Price ADD CONSTRAINT Price_Material
    FOREIGN KEY (Material_id)
    REFERENCES Material (id);

-- Reference: Price_Size (table: Price)
ALTER TABLE Price ADD CONSTRAINT Price_Size
    FOREIGN KEY (Size_id)
    REFERENCES Size (id);

-- End of file.

