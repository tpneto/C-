CREATE DATABASE StudentProg;

CREATE TABLE Programs (
    ProgId INT PRIMARY KEY NOT NULL,
    Name VARCHAR (50) NOT NULL
);

CREATE TABLE Students (
    StudId INT PRIMARY KEY NOT NULL,
    Name VARCHAR (50) NOT NULL,
    YearEnrollment INT NOT NULL,
    ProgId INT NOT NULL,
    FOREIGN KEY (ProgId) REFERENCES Programs(ProgId)
);


INSERT INTO Programs (ProgId, Name)
VALUES (1, 'Programming');
INSERT INTO Programs (ProgId, Name)
VALUES (2, 'Fashion');
INSERT INTO Programs (ProgId, Name)
VALUES (3, 'Management');
INSERT INTO Programs (ProgId, Name)
VALUES (4, 'Tourism');

INSERT INTO Students (StudId, Name, YearEnrollment, ProgId)
VALUES (1, 'Mary', 2022, 3);
INSERT INTO Students (StudId, Name, YearEnrollment, ProgId)
VALUES (3, 'John', 2021, 1);
INSERT INTO Students (StudId, Name, YearEnrollment, ProgId)
VALUES (7, 'Brian', 2021, 2);
INSERT INTO Students (StudId, Name, YearEnrollment, ProgId)
VALUES (14, 'Anne', 2020, 4);
INSERT INTO Students (StudId, Name, YearEnrollment, ProgId)
VALUES (32, 'James', 2020, 1);

