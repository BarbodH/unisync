--Initialize faculties

insert into Core.Faculty (Name, Abbreviation)
values ('Lassonde School of Engineering', 'LE');

-- Initialize departments

insert into Core.Department (Name, Abbreviation, FacultyId)
values ('Civil Engineering', 'CIVL', 1),
       ('Earth and Space Science and Engineering', 'ESSE', 1),
       ('Electrical Engineering & Computer Science', 'EECS', 1),
       ('Mechanical Engineering', 'MECH', 1);

-- Initialize programs

insert into Core.Program (Name, DepartmentId)
values -- Civil Engineering
       ('Civil Engineering', 1),
       -- Earth and Space Science and Engineering
       ('Earth & Atmospheric Science', 2),
       ('Geomatics Engineering', 2),
       ('Space Engineering', 2),
       -- Electrical Engineering & Computer Science
       ('Computer Engineering', 3),
       ('Computer Science', 3),
       ('Computer Security', 3),
       ('Digital Media', 3),
       ('Electrical Engineering', 3),
       ('Software Engineering', 3),
       -- Mechanical Engineering
       ('Mechanical Engineering', 4);

-- Initialize some courses

insert into Core.Course (DepartmentId, Title, Number, Credits)
values -- CIVL
       (1, 'Geological Processes', 2160, 3),
       (1, 'Fluid Mechanics', 2210, 4),
       (1, 'Mechanics of Materials', 2220, 4),
       (1, 'Sanity and Environmental Engineering', 3240, 3),
       (1, 'Transportation Planning and Evaluation', 3260, 3),
       -- ESSE
       (2, 'Introduction to Atmospheric Science', 1011, 3),
       (2, 'Natural, Technological and Human-induced Disasters', 1410, 6),
       (2, 'Space Systems Engineering', 2361, 3),
       (2, 'Introduction to Continuum Mechanics', 2470, 3),
       (2, 'Atmospheric Radiation and Thermodynamics', 3030, 3),
       -- EECS
       (3, 'Introduction to Object Oriented Programming', 1022, 3),
       (3, 'Computer Organization', 2021, 4),
       (3, 'Fundamentals of Data Structures', 2101, 3),
       (3, 'Design and Analysis of Algorithms', 3101, 3),
       (3, 'Digital Logic Design', 3201, 4),
       -- MECH
       (4, 'Heat and Flow Engineering Principles', 2202, 3),
       (4, 'Modern Instrumentation and Measurement Techniques', 2502, 3),
       (4, 'Solid Mechanics and Materials Laboratory', 3502, 3),
       (4, 'Macro-and-Micro Manufacturing Methods', 3503, 3),
       (4, 'Aerodynamics', 4202, 3);