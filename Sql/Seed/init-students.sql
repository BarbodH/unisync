insert into Core.AcademicSession (Season, Year)
values (0, 2024),
       (1, 2025);

insert into Core.Instructor (FirstName, LastName, Email, DepartmentId)
values ('Jeffery', 'Edmonds', 'jeff@cse.yorku.ca', 3),
       ('Parke', 'Godfrey', 'godfrey@yorku.ca', 3),
       ('Ilir', 'Dema', 'demailir@yorku.ca', 3);

insert into Core.Enrolment (CourseId, SessionId, InstructorId)
values (14, 1, 3),
       (15, 1, 1),
       (13, 2, 2);

insert into Core.Student (FirstName, LastName, Email, ProgramId)
values ('James', 'Gosling', 'james.gosling@my.yorku.ca', 6);

insert into Core.StudentEnrolment (StudentId, EnrolmentId)
values (1, 1),
       (1, 2),
       (1, 3);
