create table Core.AcademicSession
(
    Id     INT primary key identity (1,1),
    Season INT check (Season between 0 and 2) not null, -- Season: 0 = Winter, 1 = Summer, 2 = Fall
    Year   INT check (Year >= 1959)           not null, -- Valid year, starting from York establishment
    unique (Season, Year)
);

create table Core.Faculty
(
    Id           INT primary key identity (1,1),
    Name         NVARCHAR(50) unique not null,
    Abbreviation NVARCHAR(2) unique  not null
);

create table Core.Department
(
    Id           INT primary key identity (1,1),
    Name         NVARCHAR(100) unique not null,
    Abbreviation NVARCHAR(4) unique   not null,
    FacultyId    INT                  not null,
    foreign key (FacultyId) references Core.Faculty (Id)
);

create table Core.Course
(
    Id           INT primary key identity (1,1),
    Title        NVARCHAR(100)                            not null,
    DepartmentId INT                                      not null,
    Number       INT check (Number between 1000 and 9999) not null,
    Credits      INT check (Credits >= 0)                 not null,
    unique (DepartmentId, Number)
);

create table Core.Instructor
(
    Id           INT primary key identity (1,1),
    FirstName    NVARCHAR(50)         not null,
    LastName     NVARCHAR(50)         not null,
    Email        NVARCHAR(100) unique not null,
    DepartmentId INT                  not null,
    foreign key (DepartmentId) references Core.Department (Id)
);

create table Core.Enrolment
(
    Id           INT primary key identity (1,1),
    CourseId     INT not null,
    SessionId    INT not null,
    InstructorId INT not null,
    foreign key (CourseId) references Core.Course (Id),
    foreign key (SessionId) references Core.AcademicSession (Id),
    foreign key (InstructorId) references Core.Instructor (Id)
);

create table Core.Program
(
    Id           INT primary key identity (1,1),
    Name         NVARCHAR(100) unique not null,
    DepartmentId INT                  not null,
    foreign key (DepartmentId) references Core.Department (Id)
);

create table Core.Student
(
    Id        INT primary key identity (1, 1),
    FirstName NVARCHAR(50),
    LastName  NVARCHAR(50),
    Email     NVARCHAR(100),
    ProgramId INT not null,
    foreign key (ProgramId) references Core.Program (Id)
);

create table Core.StudentEnrolment
(
    StudentId   INT not null,
    EnrolmentId INT not null,
    primary key (StudentId, EnrolmentId),
    foreign key (StudentId) references Core.Student (Id),
    foreign key (EnrolmentId) references Core.Enrolment (Id)
);