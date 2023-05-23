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
password_ VARCHAR(16) NOT NULL,
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
weight_ INT ,
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
  day_of_week VARCHAR(10) NOT NULL PRIMARY KEY,
  mix_slots TIME,
  Holidays VARCHAR(10) NOT NULL,
  Working_hours_start TIME DEFAULT '10:00 AM',
  Working_hours_end TIME DEFAULT '8:00 PM'
);
-- a table representing the gym availabilty (busy / not)
CREATE TABLE gym_availability (
	n int primary key,
	gym_availabilty varchar(30)
);


CREATE TABLE women_slots (
  day_of_week VARCHAR(10) NOT NULL PRIMARY KEY,
  From_hours TIME,
  To_hours TIME,
  FOREIGN KEY (day_of_week) REFERENCES Time_Slots(day_of_week)
);


 
CREATE PROCEDURE GetSlotStrings
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @output VARCHAR(MAX) = '';

    SELECT @output = @output + '___________________________' + CHAR(13) + CHAR(10)
        + 'Day: ' + ts.day_of_week + ', Holidays: ' + ts.Holidays + ', Working Hours: ' 
        + CONVERT(VARCHAR(8), ts.Working_hours_start, 108) + ' - ' + CONVERT(VARCHAR(8), ts.Working_hours_end, 108) 
        + ', Women Slots: ' + CONVERT(VARCHAR(8), ws.From_hours, 108) + ' - ' + CONVERT(VARCHAR(8), ws.To_hours, 108) + CHAR(13) + CHAR(10)
        + '____________________________' + CHAR(13) + CHAR(10)
    FROM Time_Slots ts
    JOIN women_slots ws ON ts.day_of_week = ws.day_of_week;

    PRINT @output;
END;
GO	
EXEC GetSlotStrings;

select* from Time_Slots  inner join women_slots on women_slots.day_of_week=Time_Slots.day_of_week
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
create table Plans(
number int primary key,
type varchar(20),
value_for_students int,
value_for_workers int,
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
insert into Employee(first_name,last_name,birthday,email,Address_,user_name,password_,Job_title,Salary)
Values('Mohamed','sayed','1-1-2000','s-mohamed.sayed@zewailcity.edu.eg','6 of october','Zee','1234','coach',1000)
select*from Employee
--------------------------------------------------------------------
------------Tables Creation (Administration Office Entity )-----------------

CREATE TABLE Administration (
user_name VARCHAR(12) PRIMARY KEY NOT NULL,
password_ VARCHAR(16) NOT NULL,
Role_  VARCHAR(50) Unique  ,
Name_ VARCHAR(30) NOT NULL,
email VARCHAR(100) UNIQUE NOT NULL, CONSTRAINT check_email2 CHECK (email LIKE '%_@__%.__%') , 
);
insert into Administration(user_name,password_,Role_,Name_,email)
values ('ezaby','1234','SU','Sara Ezaby','s-sarah.ezaby@zewailcity.edu.eg')

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
-- insertions important to run the code

insert into user_gym(first_name,last_name,birthday,gender,email,job,user_name,password_)
values
('mohamed ','mahmoud','1-6-2004','male','s-mohamed1@zewailcity.edu.eg','student','mohamed','2003'),
('marwa','ahmed','8-10-2001','female','s-marwa@zewailcity.edu.eg','student','marwa','2003'),
('aly','omar','9-1-2000','male','s-aly@zewailcity.edu.eg','student','aly','2003'),
('marawan','hossam','12-6-2003','male','s-marwan@zewailcity.edu.eg','student','marwan','2003'),
('menna','moataz','8-7-2005','female','s-menna@zewailcity.edu.eg','student','menna','2003'),
('shrouq','ahmed','2-4-1999','female','s-shrouq@zewailcity.edu.eg','student','shrouq','2003'),
('mohamed','morshedy','12-12-2003','male','s-mohamed2@zewailcity.edu.eg','student','morshedy','2003'),
('mariam','alaa','10-31-2001','female','s-mariam@zewailcity.edu.eg','student','mariam','2003'),
('kareem','araby','3-1-2001','male','s-araby@zewailcity.edu.eg','student','araby','2003'),
('belal','gamal','11-11-2003','male','s-belal@zewailcity.edu.eg','student','belal','2003'),
('hassan','shahien','11-12-1998','male','s-hassan@zewailcity.edu.eg','student','hhassan','2003'),
('ahmed','roushdy','11-12-1998','male','s-roushdy@zewailcity.edu.eg','student','roushdy','2003'),
('moustafa','korym','11-12-1998','male','s-korym@zewailcity.edu.eg','student','korym','2003'),
('eslam','mohamed','11-12-1998','male','s-loay@zewailcity.edu.eg','student','eslam','2003'),
('ziad','bahgat','12-13-1999','male','s-bahgat@zewailcity.edu.eg','student','bahgat','2003'),
('hamada','moheb','12-30-2000','male','s-hamada@zewailcity.edu.eg','student','hamada','2003'),
('hareedy','mohsen','11-12-1998','male','s-hareedy@zewailcity.edu.eg','student','hareedy','2003'),
('hazem','arafa','11-12-1998','male','s-loay2@zewailcity.edu.eg','student','hazem','2003'),
('yussef','mohseb','11-12-1998','male','s-loay8@zewailcity.edu.eg','student','yussef','2003')

-- this user is used to test the home page for a default user 
insert into Body_info(user_name,height,Muscles_Percentage,Fats_Percentage,weight_)
values
('Zee',190,48,20,100),
('sarah',160,31,25,77),
('aboshareb',184,45,13,89),
('mohamed',180,42,16,90) ,
('marwa',170,35,19,62) ,
('aly',173,39,19,61) ,
('marwan',190,49,25,99) ,
('menna',155,30,25,60) ,
('shrouq',178,30,25,60) ,
('morshedy',182,40,20,78) ,
('mariam',172,30,20,68) ,
('araby',180,41,18,98) ,
('belal',177,34,15,68) ,
('hhassan',177,34,15,68) ,
('roushdy',184,37,15,78) ,
('korym',180,40,12,78) ,
('eslam',180,40,17,81) ,
('bahgat',170,32,27,81) ,
('hamada',180,30,32,90) ,
('hareedy',180,30,32,90) ,
('hazem',180,30,32,90) ,
('yussef',180,30,32,90) 


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
-- these insertions is for the equipment imgs
insert into Equipment(Equipment_num,Name_,Photo,Condition)
 values (9,'Treadmil','treadmil.jpg',100),
 (1,'back pull','back pull.jpg',100),
 (2,'bike','bike.jpg',100),
 (3,'cable cross','cable cross.jpg',100),
 (4,'chest fly','chest fly.jpg',100),
 (5,'eliptical','eliptical.jpg',100),
 (6,'leg press','leg press.jpg',100),
 (7,'back extension','lowback.jpg',100),
 (8,'Presses machine ','shoulder press.jpg',100),
 (10,'smith machine ','smith.jpg',100)


 insert into Working_Muscles(Equipment_num,Working_Muscles)
 values (1,'Back'),
 (2,'Cardio'),(3,'Multi muscles '),
 (4,'chest and rear delts'),
 (5,'Cardio'),
 (6,'Legs'),
 (7,'lower back'),
 (8,'Chest and shoulder '),
 (9,'Cardio'),
 (10,'Multi muscles')
 -- this is insertions for timeslots 
 INSERT INTO Time_Slots (day_of_week, Holidays,Working_hours_start,Working_hours_end) VALUES
  ('Sunday', 'No','10:00 AM','8:00 PM'),
  ('Monday', 'No','10:00 AM','8:00 PM'),
  ('Tuesday', 'No','10:00 AM','8:00 PM'),
  ('Wednesday', 'No','10:00 AM','8:00 PM'),
  ('Thursday', 'No','10:00 AM','8:00 PM'),
  ('Friday', 'Yes','10:00 AM','8:00 PM'),
  ('Saturday', 'Yes','10:00 AM','9:00 PM');

INSERT INTO women_slots (day_of_week, From_hours, To_hours) VALUES
  ('Sunday', '1:00 PM', '2:00 PM'),
  ('Tuesday', '1:00 PM', '2:00 PM'),
  ('Thursday', '1:00 PM', '2:00 PM'),
  ('Monday', '5:30 PM', '6:30 PM'),
  ('Wednesday', '5:30 PM', '6:30 PM');
IF EXISTS (SELECT * FROM sys.objects WHERE type = 'P' AND name = 'GetSlotStrings')
    DROP PROCEDURE GetSlotStrings;
GO
-- insertions for plans 
insert into Plans(number,type,value_for_students,value_for_workers)
values (1,'monthly',150,200)
insert into Plans(number,type,value_for_students,value_for_workers)
values (2,'3 months',300,450),(3,'6 months',500,750),(4,'Yearly',800,1200)

insert into gym_availability(n,gym_availabilty)
values (1,'true')

-- please execute the code to create the data base then execute the insert queries 
-- you can login with each specific user and it will be redirected to its page automatically 
-- you can also test it by signing up (makin	g a new user) but it will be a default user with default body info data that can be edited in his home page 




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

