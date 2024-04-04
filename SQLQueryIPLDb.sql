create database IPLDb
use IPLDb
create table Teams
(Id int identity primary key,
Name nvarchar(50) not null,
Slogan nvarchar(50) not null,
City nvarchar(50) not null)

create table Players
(Id int identity primary key,
Name nvarchar(50) not null,
Average int  not null,
Position nvarchar(50) not null,
TeamId int foreign key references Teams)
set identity_insert Teams on
insert into Teams (Id,Name,Slogan,City) values (1,'CSK','Whistle Podu','Chennai')
insert into Teams (Id,Name,Slogan,City) values (2,'MI','Duniya Hila Denge','Mumbai')
insert into Teams (Id,Name,Slogan,City) values (3,'KKR','Korbo Lorbo Jeetbo Re','Kolkata')
set identity_insert Teams off
set identity_insert Players on
insert into Players (Id,Name,Average,Position,TeamId) values (1,'MSD',56,'Captain',1)
insert into Players (Id,Name,Average,Position,TeamId) values (2,'Rohit Sharma',45,'Batsman',2)
insert into Players (Id,Name,Average,Position,TeamId) values (3,'Hardik Pandya',51,'All-Rounder',2)
insert into Players (Id,Name,Average,Position,TeamId) values (4,'Sameer Rizvi',44, 'All-Rounder', 1)
set identity_insert Players off
select * from Teams
select * from Players
-------------------------------------
select t.Id,t.Name,t.Slogan,t.City from Teams t  join Players p on t.Id=p.TeamId where p.Id=4
--------------------------------------------------
create proc usp_ShowTeamsofPlayer
@id int
as
select t.Id,t.Name,t.Slogan,t.City from Teams t  join Players p on t.Id=p.TeamId where p.Id=@id
-----------------------------------------------------------
exec usp_ShowTeamsofPlayer 3

