----------------------------
drop database DinBianDon2
go
----------------------------

create database DinBianDon2
go
--
use DinBianDon2
use DinBianDon
go


--學生資料表-------------------------------------------------------------
create table Student
(
Student_ID int,
Student_name nvarchar(50),
Student_class int,
primary key (Student_ID)
)

drop table Student
go
--廠商資料表-----------------------------------------------------------
create table Company
(
Company_ID int identity(1,1),
Company_name nvarchar(50),
Company_address nvarchar(50),
Company_phone nvarchar(50),
primary key (Company_ID)
)
--菜單-----------------------------------------------------------
create table Product
(
Product_ID int,
Product_name nvarchar(50),
Product_price int,
Company_ID int,
primary key (Product_ID),
foreign key(Company_ID) references Company(Company_ID)
)

--訂單主檔-----------------------------------------------------------
create table Order_master
(
Order_ID int identity(1,1),
Order_amount int,
Company_ID int,
Class int,
Order_date date NOT NULL DEFAULT GETDATE(),
primary key (Order_ID),
foreign key(Company_ID) references Company(Company_ID)
)

drop table Order_master
go
--訂單明細---------------------------------------------------------------------
create table Order_detail
(
Order_ID int,
Item_ID int identity(1,1),
Product_ID int,
Price int,
Quantity int,
Student_ID int,
primary key(Order_ID,Item_ID),
foreign key(Order_ID) references Order_master(Order_ID),
foreign key(Product_ID) references Product(Product_ID),
foreign key(Student_ID) references Student(Student_ID)
)

drop table Order_detail
go
------------------------------------------------------------------------------


------------------------------------------------------------------------------




