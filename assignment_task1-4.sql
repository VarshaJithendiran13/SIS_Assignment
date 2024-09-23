--TASK 1
-- 1. Create the database named "SISDB"
CREATE DATABASE SISDB;
USE SISDB;

/*2. Define the schema for the Students, Courses, Enrollments, Teacher, and Payments tables based  on the provided schema. Write SQL scripts to create the mentioned tables with appropriate data 
types, constraints, and relationships. 
a. Students 
b. Courses
c. Enrollments 
d. Teacher 
e. Payments*/
CREATE TABLE Students (
    student_id INT PRIMARY KEY NOT NULL,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    date_of_birth DATE NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    phone_number VARCHAR(15)
);

CREATE TABLE Teacher (
    teacher_id INT PRIMARY KEY NOT NULL,
    first_name VARCHAR(50) NOT NULL,
    last_name VARCHAR(50) NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL
);
CREATE TABLE Courses (
    course_id INT PRIMARY KEY NOT NULL,
    course_name VARCHAR(100) NOT NULL,
    credits INT NOT NULL,
    teacher_id INT,
    FOREIGN KEY (teacher_id) REFERENCES Teacher(teacher_id)
);

CREATE TABLE Enrollments (
    enrollment_id INT PRIMARY KEY,
    student_id INT,
    course_id INT,
    enrollment_date DATE,
    FOREIGN KEY (student_id) REFERENCES Students(student_id) ON DELETE CASCADE,
    FOREIGN KEY (course_id) REFERENCES Courses(course_id)
);


CREATE TABLE Payments (
    payment_id INT PRIMARY KEY,
    student_id INT,
    amount DECIMAL(10, 2) NOT NULL,
    payment_date DATE NOT NULL,
    FOREIGN KEY (student_id) REFERENCES Students(student_id)
);

-- 3. Create an ERD (Entity Relationship Diagram) for the database.
--    SAVED in file SISDB_ERD

-- 4. Create appropriate Primary Key and Foreign Key constraints for referential integrity.

-- 5. Insert at least 10 sample records into each of the following tables.
INSERT INTO Students (student_id, first_name, last_name, date_of_birth, email, phone_number)
VALUES
(1001, 'Aarav', 'Sharma', '2000-05-15', 'aarav.sharma@example.in', '9876543210'),
(1002, 'Ishita', 'Mehta', '2001-07-21', 'ishita.mehta@example.in', '9876543211'),
(1003, 'Rohan', 'Patel', '1999-09-10', 'rohan.patel@example.in', '9876543212'),
(1004, 'Nisha', 'Kumar', '2000-03-05', 'nisha.kumar@example.in', '9876543213'),
(1005, 'Dev', 'Reddy', '1998-11-25', 'dev.reddy@example.in', '9876543214'),
(1006, 'Priya', 'Singh', '2000-01-13', 'priya.singh@example.in', '9876543215'),
(1007, 'Karan', 'Kapoor', '2001-04-19', 'karan.kapoor@example.in', '9876543216'),
(1008, 'Simran', 'Agarwal', '2002-06-09', 'simran.agarwal@example.in', '9876543217'),
(1009, 'Rahul', 'Verma', '1999-12-31', 'rahul.verma@example.in', '9876543218'),
(1010, 'Sneha', 'Desai', '2000-08-17', 'sneha.desai@example.in', '9876543219');

INSERT INTO Courses (course_id, course_name, credits, teacher_id)
VALUES
(101,'Mathematics', 3, 101),
(102,'Physics', 4, 102),
(103,'Chemistry', 4, 103),
(104,'Computer Science', 5, 104),
(105,'Biology', 3, 105),
(106,'English Literature', 2, 106),
(107,'Economics', 4, 107),
(108,'History', 3, 108),
(109,'Political Science', 3, 109),
(110,'Psychology', 4, 110);


INSERT INTO Enrollments (enrollment_id, student_id, course_id, enrollment_date)
VALUES
(1, 1001, 101, '2024-09-01'),  -- Student 1 enrolled in Course 101
(2, 1002, 102, '2024-09-01'),  -- Student 2 enrolled in Course 102
(3, 1003, 103, '2024-09-01'),  -- Student 3 enrolled in Course 103
(4, 1001, 102, '2024-09-02'),  -- Student 1 also enrolled in Course 102
(5, 1004, 104, '2024-09-03'),  -- Student 4 enrolled in Course 104
(6, 1005, 101, '2024-09-04'),  -- Student 5 enrolled in Course 101
(7, 1006, 103, '2024-09-05'),  -- Student 6 enrolled in Course 103
(8, 1007, 102, '2024-09-06'),  -- Student 7 enrolled in Course 102
(9, 1008, 105, '2024-09-07'),  -- Student 8 enrolled in Course 105
(10, 1009, 104, '2024-09-08'); -- Student 9 enrolled in Course 104


INSERT INTO Payments (payment_id, student_id, amount, payment_date)
VALUES
(1, 1001, 5500.00, '2024-09-15'),
(2, 1002, 5000.00, '2024-09-16'),
(3, 1003, 4800.00, '2024-09-17'),
(4, 1004, 7100.00, '2024-09-18'),
(5, 1005, 6750.00, '2024-09-19'),
(6, 1006, 5600.00, '2024-09-20'),
(7, 1007, 7550.00, '2024-09-21'),
(8, 1008, 4200.00, '2024-09-22'),
(9, 1009, 5900.00, '2024-09-23'),
(10, 1010, 6850.00, '2024-09-24');


INSERT INTO Teacher (teacher_id, first_name, last_name, email)
VALUES
(101, 'Anil', 'Rathore', 'anil.rathore@example.in'),
(102, 'Sanjay', 'Iyer', 'sanjay.iyer@example.in'),
(103, 'Megha', 'Nair', 'megha.nair@example.in'),
(104, 'Vikram', 'Menon', 'vikram.menon@example.in'),
(105, 'Neha', 'Chaudhary', 'neha.chaudhary@example.in'),
(106, 'Suresh', 'Pillai', 'suresh.pillai@example.in'),
(107, 'Mona', 'Jain', 'mona.jain@example.in'),
(108, 'Rakesh', 'Ghosh', 'rakesh.ghosh@example.in'),
(109, 'Sonal', 'Thakur', 'sonal.thakur@example.in'),
(110, 'Alok', 'Bhatia', 'alok.bhatia@example.in');

SELECT * FROM Students;
SELECT * FROM Teacher;
SELECT * FROM Payments;

SELECT * FROM Courses;
SELECT * FROM Enrollments;
/* Tasks 2: Select, Where, Between, AND, LIKE: 
1. Write an SQL query to insert a new student into the "Students" table with the following details:
a. First Name: John
b. Last Name: Doe
c. Date of Birth: 1995-08-15
d. Email: john.doe@example.com
e. Phone Number: 1234567890 */
-- 1: Insert a new student into the "Students" table
INSERT INTO Students (student_id, first_name, last_name, date_of_birth, email, phone_number)
VALUES (1011, 'John', 'Doe', '1995-08-15', 'john.doe@example.com', '1234567890');

-- 2: Enroll a student in a course
INSERT INTO Enrollments (enrollment_id, student_id, course_id, enrollment_date)
VALUES (11, 1011, 101, '2024-09-25'); 

-- 3: Update the email address of a specific teacher
UPDATE Teacher
SET email = 'sanjay.newemail@example.in'
WHERE teacher_id = 102;

-- 4: Delete a specific enrollment record from the "Enrollments" table (example: student_id = 1011, course_id = 101)
DELETE FROM Enrollments
WHERE student_id = 1011 AND course_id = 101;

-- 5: Update the "Courses" table to assign a specific teacher to a course 
UPDATE Courses
SET teacher_id = 104
WHERE course_id = 103;

-- 6: Delete a specific student from the "Students" table and remove their enrollment records
DELETE FROM Students
WHERE student_id = 1011;

-- 7: Update the payment amount for a specific payment record
UPDATE Payments
SET amount = 2500.00
WHERE student_id = 1004;

-- Task 3 aggregate fxns, Having, Order By, GroupBy and Joins:
-- adding entries to give output
INSERT INTO Students (student_id, first_name, last_name, date_of_birth, email, phone_number)
VALUES (1012, 'Alice', 'Smith', '1998-02-20', 'alice.smith@example.com', '9876543211');
-- 1. Write an SQL query to calculate the total payments made by a specific student.
SELECT s.first_name, s.last_name, SUM(p.amount) AS total_payments
FROM Students s
JOIN Payments p ON s.student_id = p.student_id
WHERE s.student_id = 1001  
GROUP BY s.first_name, s.last_name;

-- 2. Write an SQL query to retrieve a list of courses along with the count of students enrolled in each course.
SELECT c.course_name, COUNT(e.student_id) AS student_count
FROM Courses c
JOIN Enrollments e ON c.course_id = e.course_id
GROUP BY c.course_name
ORDER BY student_count DESC;

-- 3. Write an SQL query to find the names of students who have not enrolled in any course.
SELECT s.first_name, s.last_name
FROM Students s
LEFT JOIN Enrollments e ON s.student_id = e.student_id
WHERE e.course_id IS NULL;

-- 4. Write an SQL query to retrieve the first name, last name of students, and the names of the courses they are enrolled in.
SELECT s.first_name, s.last_name, c.course_name
FROM Students s
JOIN Enrollments e ON s.student_id = e.student_id
JOIN Courses c ON e.course_id = c.course_id
ORDER BY s.first_name, s.last_name;

-- 5. Create a query to list the names of teachers and the courses they are assigned to.
SELECT t.first_name, t.last_name, c.course_name
FROM Teacher t
JOIN Courses c ON t.teacher_id = c.teacher_id
ORDER BY t.first_name, t.last_name;

-- 6. Retrieve a list of students and their enrollment dates for a specific course.
SELECT s.first_name, s.last_name, e.enrollment_date
FROM Students s
JOIN Enrollments e ON s.student_id = e.student_id
JOIN Courses c ON e.course_id = c.course_id
WHERE c.course_id = 104 
ORDER BY e.enrollment_date;

-- 7. Find the names of students who have not made any payments.
SELECT s.first_name, s.last_name
FROM Students s
LEFT JOIN Payments p ON s.student_id = p.student_id
WHERE p.payment_id IS NULL;

-- 8. Write a query to identify courses that have no enrollments.
SELECT c.course_name
FROM Courses c
LEFT JOIN Enrollments e ON c.course_id = e.course_id
WHERE e.course_id IS NULL;

-- 9. Identify students who are enrolled in more than one course.
SELECT s.first_name, s.last_name, COUNT(e.course_id) AS courses_enrolled
FROM Students s
JOIN Enrollments e ON s.student_id = e.student_id
GROUP BY s.first_name, s.last_name
HAVING COUNT(e.course_id) > 1;

-- 10. Find teachers who are not assigned to any courses.
SELECT t.first_name, t.last_name
FROM Teacher t
LEFT JOIN Courses c ON t.teacher_id = c.teacher_id
WHERE c.course_id IS NULL;

-- task 4 - subquery and its type:
-- adding entries
INSERT INTO Enrollments (enrollment_id, student_id, course_id, enrollment_date)
VALUES
(11, 1001, 105, '2024-09-09'),  -- Student 1 enrolled in Course 105
(12, 1002, 106, '2024-09-10'),  -- Student 2 enrolled in Course 106
(13, 1003, 107, '2024-09-11'),  -- Student 3 enrolled in Course 107
(14, 1004, 108, '2024-09-12'),  -- Student 4 enrolled in Course 108
(15, 1005, 109, '2024-09-13'),  -- Student 5 enrolled in Course 109
(16, 1006, 110, '2024-09-14'),  -- Student 6 enrolled in Course 110
(17, 1007, 101, '2024-09-15'),  -- Student 7 re-enrolled in Course 101
(18, 1008, 102, '2024-09-16'),  -- Student 8 re-enrolled in Course 102
(19, 1009, 103, '2024-09-17'),  -- Student 9 re-enrolled in Course 103
(20, 1010, 104, '2024-09-18');  -- Student 10 enrolled in Course 104
INSERT INTO Enrollments (enrollment_id, student_id, course_id, enrollment_date)
VALUES
(21, 1001, 103, '2024-09-15'),
(22, 1001, 104, '2024-09-15'),
(23, 1001, 106, '2024-09-15'),
(24, 1001, 107, '2024-09-15'),
(25, 1001, 108, '2024-09-15'),
(26, 1001, 109, '2024-09-15'),
(27, 1001, 110, '2024-09-15');


-- 1. Calculate the average number of students enrolled in each course.
SELECT course_name, 
       CAST((SELECT COUNT(student_id) FROM Enrollments e WHERE e.course_id = c.course_id) AS FLOAT) / 
       (SELECT COUNT(DISTINCT course_id) FROM Enrollments) AS avg_students
FROM Courses c;


-- 2. Identify the student(s) who made the highest payment.
SELECT first_name, last_name
FROM Students s
WHERE student_id = (SELECT student_id 
                    FROM Payments 
                    WHERE amount = (SELECT MAX(amount) FROM Payments));

-- 3. Retrieve a list of courses with the highest number of enrollments.
SELECT TOP 1 course_name 
FROM Courses
WHERE course_id IN (
    SELECT course_id 
    FROM Enrollments 
    GROUP BY course_id 
    HAVING COUNT(student_id) = (
        SELECT MAX(enrollment_count) FROM (
            SELECT course_id, COUNT(student_id) AS enrollment_count
            FROM Enrollments
            GROUP BY course_id
        ) AS counts
    )
);


-- 4. Calculate the total payments made to courses taught by each teacher.
SELECT t.first_name, t.last_name, 
       SUM(p.amount) AS total_payments
FROM Teacher t
LEFT JOIN Courses c ON t.teacher_id = c.teacher_id
LEFT JOIN Enrollments e ON c.course_id = e.course_id
LEFT JOIN Payments p ON e.student_id = p.student_id
GROUP BY t.first_name, t.last_name;

-- 5. Identify students who are enrolled in all available courses.
SELECT first_name, last_name 
FROM Students s
WHERE NOT EXISTS (
    SELECT c.course_id 
    FROM Courses c 
    WHERE NOT EXISTS (
        SELECT 1 
        FROM Enrollments e 
        WHERE e.student_id = s.student_id AND e.course_id = c.course_id
    )
);

-- 6. Retrieve the names of teachers who have not been assigned to any courses.
SELECT first_name, last_name 
FROM Teacher t
WHERE NOT EXISTS (
    SELECT 1 
    FROM Courses c 
    WHERE c.teacher_id = t.teacher_id
);

-- 7. Calculate the average age of all students.
SELECT AVG(DATEDIFF(YEAR, date_of_birth, GETDATE())) AS avg_age
FROM Students;

-- 8. Identify courses with no enrollments.
SELECT course_name 
FROM Courses c
WHERE NOT EXISTS (
    SELECT 1 
    FROM Enrollments e 
    WHERE e.course_id = c.course_id
);

-- 9. Calculate the total payments made by each student for each course they are enrolled in.
SELECT s.first_name, s.last_name, c.course_name, 
       SUM(p.amount) AS total_payments
FROM Students s
JOIN Enrollments e ON s.student_id = e.student_id
JOIN Courses c ON e.course_id = c.course_id
LEFT JOIN Payments p ON p.student_id = s.student_id
GROUP BY s.first_name, s.last_name, c.course_name
ORDER BY total_payments DESC;

-- 10. Identify students who have made more than one payment.
INSERT INTO Payments (payment_id, student_id, amount, payment_date)
VALUES (11, 1003, 2000.00, '2024-09-25');
SELECT s.first_name, s.last_name 
FROM Students s
WHERE (SELECT COUNT(p.payment_id) 
       FROM Payments p 
       WHERE p.student_id = s.student_id) > 1;

-- 11. Calculate the total payments made by each student.
SELECT s.first_name, s.last_name, 
       (SELECT SUM(p.amount) FROM Payments p WHERE p.student_id = s.student_id) AS total_payments
FROM Students s
GROUP BY s.student_id, s.first_name, s.last_name;


-- 12. Retrieve a list of course names along with the count of students enrolled in each course.
SELECT c.course_name, 
       (SELECT COUNT(e.student_id) FROM Enrollments e WHERE e.course_id = c.course_id) AS student_count
FROM Courses c
GROUP BY c.course_id, c.course_name;

-- 13. Calculate the average payment amount made by students.
SELECT AVG(amount) AS avg_payment
FROM Payments;

/*
SELECT * FROM Students;
SELECT * FROM Teacher;
SELECT * FROM Payments;

SELECT * FROM Courses;
SELECT * FROM Enrollments;
*/

