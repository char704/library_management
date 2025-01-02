DROP TABLE users;
DROP TABLE books;
DROP TABLE issues;
DROP TABLE students;
create table library_management.dbo.books
(
	id int primary key identity(1,1),
	book_title varchar(max) null,
	author varchar(max) null,
	published_date date null,
	status varchar(max) null,
	image varchar(max) null,
	date_insert date null,
	date_update date null,
	date_delete date null
)
create table library_management.dbo.users
(
	id int primary key identity(1,1),
	email varchar(max) null,
	username varchar(max) null,
	password varchar(max) null,
	date_register date null

)
create table library_management.dbo.issues
(
	id int primary key identity(1,1),
	issue_id varchar(max) null,
	full_name varchar(max) null,
	contact varchar(max) null,
	email varchar(max) null,
	book_title varchar(max) null,
	author varchar(max) null,
	image varchar(max) null,
	status varchar(max) null,
	issue_date date null,
	return_date date null,
	date_insert date null,
	date_update date null,
	date_delete date null
)
CREATE TABLE library_management.dbo.students
(id INT PRIMARY KEY IDENTITY(1,1),
    name VARCHAR(MAX) NULL,
	enroll VARCHAR(MAX) NULL,
    contact VARCHAR(MAX) NULL,
	email VARCHAR(MAX) NULL,
    nissue_book INT NULL,
    image VARCHAR(MAX) NULL,
	date_insert DATETIME NULL DEFAULT GETDATE(),
    date_update DATETIME NULL DEFAULT GETDATE(),
    date_delete DATETIME NULL
)
select *from users;
select *from books;
select *from issues;
select *from students;