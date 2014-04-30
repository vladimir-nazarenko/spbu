use m12nvv;
GO

--Code from slides
CREATE TABLE Employees
(
 empid int NOT NULL
 ,mgrid int NULL
 ,empname varchar(25) NOT NULL
 ,salary money NOT NULL
 CONSTRAINT PK_Employees PRIMARY KEY(empid)
);

--Code from slides
INSERT INTO Employees VALUES(1 , NULL, 'Nancy' , $10000.00);
INSERT INTO Employees VALUES(2 , 1 , 'Andrew' , $5000.00);
INSERT INTO Employees VALUES(3 , 1 , 'Janet' , $5000.00);
INSERT INTO Employees VALUES(4 , 1 , 'Margaret', $5000.00);
INSERT INTO Employees VALUES(5 , 2 , 'Steven' , $2500.00);
INSERT INTO Employees VALUES(6 , 2 , 'Michael' , $2500.00);
INSERT INTO Employees VALUES(7 , 3 , 'Robert' , $2500.00);
INSERT INTO Employees VALUES(8 , 3 , 'Laura' , $2500.00);
INSERT INTO Employees VALUES(9 , 3 , 'Ann' , $2500.00);
INSERT INTO Employees VALUES(10, 4 , 'Ina' , $2500.00);
INSERT INTO Employees VALUES(11, 7 , 'David' , $2000.00);
INSERT INTO Employees VALUES(12, 7 , 'Ron' , $2000.00);
INSERT INTO Employees VALUES(13, 7 , 'Dan' , $2000.00);
INSERT INTO Employees VALUES(14, 11 , 'James' , $1500.00);

/*First assignment - add manager name to ouput*/
WITH empoyees_info(manager, employe, employe_name, emp_salary, emp_level)
AS 
	(SELECT mgrid, empid, empname, salary, 0 FROM dbo.Employees
	 WHERE mgrid IS NULL
UNION ALL
	SELECT mgrid, empid, empname, salary, emp_level+1 FROM Employees 
	JOIN empoyees_info ON mgrid = employe 
),
employees_info_with_mgr_name(manager, employe, employe_name, emp_salary, emp_level, mgr_name)
AS(
	SELECT a.manager, a.employe, a.employe_name, a.emp_salary, a.emp_level, NULL FROM empoyees_info as a WHERE a.manager IS NULL
	UNION ALL
	SELECT a.manager, a.employe, a.employe_name, a.emp_salary, a.emp_level, b.empname FROM empoyees_info as a JOIN Employees as b ON a.manager=b.empid
)
SELECT * FROM employees_info_with_mgr_name
ORDER BY manager;
GO
--The second assignment: create function to return all the subodinate of a person
CREATE FUNCTION get_all_subordinate(@id int)
RETURNS TABLE
AS
RETURN
WITH subordinate(employe, e_name, e_salary)
AS(
	SELECT empid, empname, salary FROM Employees WHERE mgrid = @id
	UNION ALL
	SELECT a.empid, a.empname, a.salary FROM Employees as a JOIN subordinate ON a.mgrid=subordinate.employe
)
SELECT * FROM subordinate
GO

SELECT * FROM get_all_subordinate(3)
GO
--The third assignment: create function to return summary salary of all the subordinate
CREATE FUNCTION get_all_salary(@id int)
RETURNS money
AS
BEGIN
	RETURN (SELECT SUM(e_salary) FROM get_all_subordinate(@id))
END;
GO
SELECT dbo.get_all_salary(3)