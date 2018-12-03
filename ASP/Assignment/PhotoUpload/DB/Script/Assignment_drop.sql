-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2018-11-25 04:35:55.938

-- foreign keys
ALTER TABLE Customer DROP CONSTRAINT Customer_Price_Method;

ALTER TABLE Image DROP CONSTRAINT Image_Customer;

ALTER TABLE "Order" DROP CONSTRAINT Order_Customer;

ALTER TABLE Order_Detail DROP CONSTRAINT Order_Detail_Image;

ALTER TABLE Order_Detail DROP CONSTRAINT Order_Detail_Order;

ALTER TABLE Order_Detail DROP CONSTRAINT Order_Detail_Price;

ALTER TABLE "Order" DROP CONSTRAINT Order_Employee;

ALTER TABLE Price DROP CONSTRAINT Price_Material;

ALTER TABLE Price DROP CONSTRAINT Price_Size;

-- tables
DROP TABLE Admin;

DROP TABLE Customer;

DROP TABLE Employee;

DROP TABLE Image;

DROP TABLE Material;

DROP TABLE "Order";

DROP TABLE Order_Detail;

DROP TABLE Payment_Method;

DROP TABLE Price;

DROP TABLE Size;

-- End of file.

