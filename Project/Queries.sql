USE SocialService;
GO

-- ����� ���� ������������� �������
SELECT * FROM Tasks WHERE Tasks.FinishedDate IS NULL;

-- ����� �����������
SELECT * FROM Employees WHERE Employees.Qualification = 'plumbery'

-- ���������� ������� ������ ���� � ������� ��������� �����������
-- 2 - service room of building 0
INSERT INTO Tasks VALUES (2, (SELECT Employees.ID FROM Employees WHERE Employees.Surname = 'Electronov'), DEFAULT, DATEADD(DAY, 3, GETDATE()), NULL, 'electricity', 'first phase') 

-- ����� ����������, ��������������� �� �������
SELECT * FROM Employees WHERE Employees.WorkingBuildingID = 3

-- ����� ����� ������� ������ � ������� ���� ���������� ������ ����
SELECT Rooms.Number, Buildings.Adress FROM Rooms JOIN Tasks ON Rooms.ID = Tasks.RoomID JOIN Buildings ON Rooms.BuildingID = Buildings.ID 
WHERE Tasks.Description = 'first phase' AND BuildingID = 0

-- ����� ���������� �������� � �/� ����� �������, ������������� ���������� ������������
SELECT COUNT(*) FROM Rooms WHERE Rooms.BuildingID = (SELECT Employees.WorkingBuildingID FROM Employees WHERE Surname = 'Electronov')

-- ����� ������������� ����� ��� ��������, ������������ �� ����������� ������, ��������� ��� � ���������
SELECT * FROM Tasks WHERE Tasks.RoomID IN (SELECT ID FROM Rooms WHERE Rooms.PhoneNumber = '12345678912')

-- ����� �����������, ����������� ������ �� ��������� ����� � ���� � ���������� �����
SELECT Employees.Surname, COUNT(*) as Count FROM Employees JOIN Tasks ON Tasks.EmployeeID = Employees.ID 
WHERE DATEDIFF(DAY, Tasks.FinishedDate, GETDATE()) < 30 AND DATEDIFF(DAY, Tasks.FinishedDate, Tasks.DeadlineDate) >= 0 
GROUP BY Employees.Surname

-- ����� ������� �� ������� ������� ���� ��������� ����������� � ����.
SELECT Tasks.AssignDate, Tasks.Class, Tasks.DeadlineDate, Tasks.Description, Rooms.Number, Buildings.Adress 
FROM Tasks JOIN Employees ON Tasks.EmployeeID = Employees.ID JOIN Rooms ON Tasks.RoomID = Rooms.ID JOIN Buildings ON Buildings.ID = Rooms.ID
WHERE Employees.Surname='Electronov' AND Tasks.Class='electricity' AND Tasks.FinishedDate IS NULL


-- ����� �����, ����������� �����������, �� ��������������� �� ��������, ��� ������� ���������� ������
SELECT Tasks.*, Employees.* FROM Tasks JOIN Rooms ON Tasks.RoomID = Rooms.ID JOIN Employees ON Employees.ID = Tasks.EmployeeID WHERE Rooms.BuildingID <> Employees.WorkingBuildingID