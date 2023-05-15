create database GYMDB
USE master;
GO
ALTER DATABASE GYMDB
SET SINGLE_USER 
WITH ROLLBACK IMMEDIATE;
GO

DROP database IF EXISTS GYMDB;
GO
Create database GYMDB
GO
use GYMDB
------------Tables Creation (user entity)-----------------
CREATE TABLE user_gym (
first_name VARCHAR(50) Not NULL,
last_name VARCHAR(50) NOT NULL,
birthday DATE NOT NULL,
age as DATEDIFF(HOUR, birthday,GETDATE())/8766, -- derived calculated by itself 
gender VARCHAR(10) NOT NULL,
email VARCHAR(100) UNIQUE NOT NULL, CONSTRAINT check_email CHECK (email LIKE '%_@__%.__%') ,
job VARCHAR(50) NOT NULL,
user_name VARCHAR(12) PRIMARY KEY NOT NULL,
password_ VARCHAR(16) NOT NULL
profil_pic varchar(50) 
);
---- cause Phonenumber is multivalued ---
CREATE TABLE phone_numbers (
  user_name VARCHAR (12) FOREIGN KEY REFERENCES user_gym,
  Phone_num VARCHAR(20),
 );
--------------------------------------------------------------------
------------Tables Creation (Body info entity)-----------------
CREATE TABLE Body_info(
user_name VARCHAR (12) FOREIGN KEY REFERENCES user_gym,
weight_ INT 
height INT NOT NULL,
Muscles_Percentage INT NOT NULL,
Fats_Percentage INT NOT NULL,
date_added_in date, -- represents the date data added in
);
---- cause Previous Injuries is multivalued ---
CREATE TABLE Previous_Injuries (
user_name VARCHAR (12) FOREIGN KEY REFERENCES user_gym,
Previous_Injuries VARCHAR(500),
 );
 --------------------------------------------------------------------
 ------------Tables Creation (Time Slots entity)-----------------
CREATE TABLE Time_Slots (
day_of_week VARCHAR(10) ,
shift_ int NOT NULL PRIMARY KEY,
shift_hours varchar(10),
who_is_allowed varchar(30)
);

--------------------------------------------------------------------
 ------------Tables Creation (Subscription and offer  entities)-----------------
CREATE TABLE Subscription (
user_name VARCHAR(12) FOREIGN KEY REFERENCES user_gym,
sub_num INT PRIMARY KEY NOT NULL,
type_sub VARCHAR(100),
price VARCHAR(100),
Period_sub VARCHAR(100),
Start_in DATE,
valid_until AS DATEADD(DAY, 
                    CASE 
                        WHEN Period_sub LIKE '%month%' THEN CAST(SUBSTRING(Period_sub, 1, CHARINDEX(' ', Period_sub) - 1) AS INT) * 30
                        WHEN Period_sub LIKE '%day%' THEN CAST(SUBSTRING(Period_sub, 1, CHARINDEX(' ', Period_sub) - 1) AS INT)
                        WHEN Period_sub LIKE '%year%' THEN CAST(SUBSTRING(Period_sub, 1, CHARINDEX(' ', Period_sub) - 1) AS INT) * 365
                    END, 
                    Start_in),
discount_price DECIMAL(10, 2) DEFAULT NULL
);

CREATE TABLE Offer (
offer_id INT PRIMARY KEY NOT NULL,
description VARCHAR(100),
time_of_offer DATE NOT NULL,
valid_from DATE NOT NULL,
valid_until DATE NOT NULL
);

--------------------------------------------------------------------
 ------------Tables Creation (Gym Entity )-----------------
--CREATE TABLE GYM(
--day_of_week VARCHAR(10) FOREIGN KEY REFERENCES Time_Slots ,
--Location_Gym VARCHAR(100) NOT NULL UNIQUE ,
--);

--------------------------------------------------------------------
------------Tables Creation (Employee Entity )-----------------
CREATE TABLE Employee (
first_name VARCHAR(50) Not NULL,
last_name VARCHAR(50) NOT NULL,
birthday DATE NOT NULL,
age as DATEDIFF(HOUR, birthday,GETDATE())/8766,
email VARCHAR(100) UNIQUE NOT NULL, CONSTRAINT check_email1 CHECK (email LIKE '%_@__%.__%') ,
Address_ VARCHAR(100) NOT NULL,
user_name VARCHAR(12) PRIMARY KEY NOT NULL,
password_ VARCHAR(16) NOT NULL,
Job_title  VARCHAR(50) NOT NULL,
Salary VARCHAR(16) NOT NULL,
);
--------------------------------------------------------------------
------------Tables Creation (Administration Office Entity )-----------------

CREATE TABLE Administration (
user_name VARCHAR(12) PRIMARY KEY NOT NULL,
password_ VARCHAR(16) NOT NULL,
Role_  VARCHAR(50) Unique  ,
Name_ VARCHAR(30) NOT NULL,
email VARCHAR(100) UNIQUE NOT NULL, CONSTRAINT check_email2 CHECK (email LIKE '%_@__%.__%') , 
);
--------------------------------------------------------------------
------------Tables Creation (Equipment Entity )-----------------
CREATE TABLE Equipment(
Equipment_num INT PRIMARY KEY,
Last_maintenance DATE,
Name_ VARCHAR(50) NoT Null,
Photo VARCHAR(1000) NoT Null,
Condition VARCHAR(100) NoT Null, 
Next_maintenance DATE,
);
---- cause Previous Injuries is multivalued ---
CREATE TABLE Working_Muscles (
 Equipment_num INT FOREIGN KEY REFERENCES Equipment,
Working_Muscles VARCHAR(500),
 );
 insert into Equipment(Equipment_num,Name_,Photo,Condition)
 values (10,'back pull','back pull.jpg',100)

 insert into Working_Muscles(Equipment_num,Working_Muscles)
 values (10,'Back')

 Delete from Working_Muscles where Equipment_num=10 Delete from Equipment where Equipment_num=10
 

select*from Equipment inner join Working_Muscles on Working_Muscles.Equipment_num=Equipment.Equipment_num

 --------------------------------------------------------------------
------------Tables Creation (Finance Entity )-----------------
CREATE TABLE Finance (
income DECIMAL(10,2),
taxes DECIMAL(10,2),
maintenance_cost DECIMAL(10,2)
);
 --------------------------------------------------------------------
------------Tables Creation (Feedback  Entity )-----------------
CREATE TABLE Feedback (
user_name VARCHAR (12) FOREIGN KEY REFERENCES user_gym,
income DECIMAL(10,2) DEFAULT NULL,
taxes DECIMAL(10,2) DEFAULT NULL,
maintenance_cost DECIMAL(10,2),
Muscles_after INT DEFAULT NULL,
Fats_after INT DEFAULT NULL,
Comment VARCHAR(500) DEFAULT Null,
star_count TINYINT CHECK (star_count >= 0 AND star_count <= 5) DEFAULT NULL, 
assigend_to VARCHAR(500) DEFAULT NULL
);
CREATE TABLE Feedback_from_user (
user_name VARCHAR (12) FOREIGN KEY REFERENCES user_gym,
facilitis_rate int ,
coach_rate int,
general_comment varchar(200)
);


insert into user_gym(first_name,last_name,birthday,gender,email,job,user_name,password_)
values('abdulrahman','ahmed','6-26-2003','male','s-abdel-rahman.ahmed@zewailcity.edu.eg','student','aboshareb','2003')
-- this user is used to test the home page for a default user 
insert into Body_info(user_name,height,Muscles_Percentage,Fats_Percentage,weight_)
values('aboshareb',183,41,11,82) 


insert into user_gym(first_name,last_name,birthday,gender,email,job,user_name,password_)
values('mohamed','sayed','12-6-1999','male','s-mohamed.sayed@zewailcity.edu.eg','Coach','Zee','1234')
-- this user is used to test the home page for the captain 
insert into Body_info(user_name,height,Muscles_Percentage,Fats_Percentage,weight_)
values('Zee',190,48,24,100)

insert into user_gym(first_name,last_name,birthday,gender,email,job,user_name,password_)
values('sara','Ezaby','9-21-1988','female','s-sarah.ezaby@zewailcity.edu.eg','SU','sarah','1234')
-- this user is to test the home page of the admistration officer (student affaires in this example )
insert into Body_info(user_name,height,Muscles_Percentage,Fats_Percentage,weight_,date_added_in)
values('sarah',173,35,30,88,'8-8-2019')

-- please execute the code to create the data base then execute the insert queries 
-- you can login with each specific user and it will be redirected to its page automatically 
-- you can also test it by signing up (makin	g a new user) but it will be a default user with default body info data that can be edited in his home page 


INSERT INTO Subscription (user_name, sub_num, type_sub, price, Period_sub, Start_in, discount_price)
VALUES ('aboshareb', 1, 'Premium', '50', '1 month', '07-05-2023', 0);

select *from user_gym where exists(select * from Subscription where user_name='dfs')

create procedure addsubscribtion @usname varchar(30) , @num int ,@price varchar(30), @period varchar(30) , @start varchar(10)
AS
delete Subscription where user_name=@usname
INSERT INTO Subscription (user_name, sub_num, type_sub, price, Period_sub, Start_in, discount_price)
VALUES (@usname, @num, 'default', @price, @period, @start, 0);
Go

Exec addsubscribtion @usname='Zee' , @num =10 ,@price ='450', @period ='3 month' , @start ='05-17-2023'

create procedure add_Feedback_from_user @usname varchar(30) , @Fac_rate int , @c_rate int,@comment varchar(200)
AS
delete Feedback_from_user where user_name=@usname
INSERT INTO Feedback_from_user(user_name, facilitis_rate,coach_rate,general_comment)
VALUES (@usname, @Fac_rate, @c_rate,@comment);
Go

Exec add_Feedback_from_user @usname='aboshareb',@Fac_rate=5,@c_rate=5,@comment=' perfect gym '
SELECT TOP 1 * FROM Body_info
WHERE user_name = 'aboshareb'
ORDER BY date_added_in DESC;

select * from Body_info where date_added_in like '%2023%-05-%' and user_name='aboshareb'

select*from user_gym

alter table user_gym
add profil_pic varchar(50)

select valid_until from Subscription where user_name='aboshareb'


