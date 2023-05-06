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
Fats_mass INT NOT NULL,
);
---- cause Previous Injuries is multivalued ---
CREATE TABLE Previous_Injuries (
user_name VARCHAR (12) FOREIGN KEY REFERENCES user_gym,
Previous_Injuries VARCHAR(500),
 );
 --------------------------------------------------------------------
 ------------Tables Creation (Time Slots entity)-----------------
CREATE TABLE Time_Slots (
day_of_week VARCHAR(10) NOT NULL PRIMARY KEY,
mix_slots TIME ,
Holidays VARCHAR(10) NOT NULL,
Working_hours_start TIME ,  
Working_hours_end TIME ,  
);

CREATE TABLE men_slots (
day_of_week VARCHAR(10) NOT NULL FOREIGN KEY REFERENCES Time_Slots,
From_hours TIME ,
To_hours TIME ,
);
CREATE TABLE women_slots (
day_of_week VARCHAR(10) NOT NULL FOREIGN KEY REFERENCES Time_Slots,
From_hours TIME ,
To_hours TIME ,
);
CREATE TABLE Mix_slots (
day_of_week VARCHAR(10) NOT NULL FOREIGN KEY REFERENCES Time_Slots,
From_hours TIME ,
To_hours TIME ,
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
CREATE TABLE GYM(
day_of_week VARCHAR(10) FOREIGN KEY REFERENCES Time_Slots ,
Location_Gym VARCHAR(100) NOT NULL UNIQUE ,
);
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


insert into user_gym(first_name,last_name,birthday,gender,email,job,user_name,password_)
values('sara','ezaby','6-26-2003','female','sara@zc.city','SU','ezaby','123456789')

insert into Body_info(user_name,height,Muscles_Percentage,Fats_Percentage,weight_)
values('ezaby',0,0,0,0)

insert into user_gym(first_name,last_name,birthday,gender,email,job,user_name,password_)
values('mohamed','sayed','9-2-1999','male','mzakria@zewailcity.edu.eg','Coach','zee','9090')

insert into Body_info(user_name,height,Muscles_Percentage,Fats_Percentage,weight_)
values('zee',0,0,0,0)

insert into user_gym(first_name,last_name,birthday,gender,email,job,user_name,password_)
values('abdulrahman','ahmed','6-26-2003','male','s-abdel-rahman.ahmedzewailcity.edu.eg','student','aboshareb','01212624129')

insert into Body_info(user_name,height,Muscles_Percentage,Fats_Percentage,weight_)
values('aboshareb',183,44,14,90)



