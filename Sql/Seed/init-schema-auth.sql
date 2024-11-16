create table Auth.Account
(
    Id        INT primary key identity (1,1),
    Role      INT                  not null, -- 0: Admin, 1: Instructor, 2: Student
    FirstName NVARCHAR(50)         not null,
    LastName  NVARCHAR(50)         not null,
    Email     NVARCHAR(100) unique not null,
    CreatedAt DATETIME default getdate()
);

create table Auth.Credentials
(
    Id           INT primary key identity (1, 1),
    Email        NVARCHAR(100) not null,
    PasswordHash VARBINARY(255) not null,
    PasswordSalt VARBINARY(255) not null,
);