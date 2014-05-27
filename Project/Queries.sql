USE SocialService;
GO

-- Вывод всех невыполненных заданий
SELECT * FROM Tasks WHERE Tasks.FinishedDate IS NULL;

-- Вывод сантехников
SELECT * FROM Employees WHERE Employees.Qualification = 'plumbery'

-- Присвоение починки первой фазы в дежурке электрику Электронову
-- 2 - service room of building 0
INSERT INTO Tasks VALUES (2, (SELECT Employees.ID FROM Employees WHERE Employees.Surname = 'Electronov'), DEFAULT, DATEADD(DAY, 3, GETDATE()), NULL, 'electricity', 'first phase') 

-- Вывод работников, ассоциированных со зданием
SELECT * FROM Employees WHERE Employees.WorkingBuildingID = 3

-- Вывод жилых квартир здания в которых была неисправна первая фаза
SELECT Rooms.Number, Buildings.Adress FROM Rooms JOIN Tasks ON Rooms.ID = Tasks.RoomID JOIN Buildings ON Rooms.BuildingID = Buildings.ID 
WHERE Tasks.Description = 'first phase' AND BuildingID = 0

-- Вывод количества учтенных в б/д жилых квартир, обслуживаемых электриком Электроновым
SELECT COUNT(*) FROM Rooms WHERE Rooms.BuildingID = (SELECT Employees.WorkingBuildingID FROM Employees WHERE Surname = 'Electronov')

-- Вывод невыполненных задач для квартиры, определенной по телефонному новеру, выданному АОН у консьержа
SELECT * FROM Tasks WHERE Tasks.RoomID IN (SELECT ID FROM Rooms WHERE Rooms.PhoneNumber = '12345678912')

-- Вывод сотрудников, выполнивших задачи за последний месяц в срок и количества задач
SELECT Employees.Surname, COUNT(*) as Count FROM Employees JOIN Tasks ON Tasks.EmployeeID = Employees.ID 
WHERE DATEDIFF(DAY, Tasks.FinishedDate, GETDATE()) < 30 AND DATEDIFF(DAY, Tasks.FinishedDate, Tasks.DeadlineDate) >= 0 
GROUP BY Employees.Surname

-- Вывод заданий на текущий рабочий день электрику Электронову в доме.
SELECT Tasks.AssignDate, Tasks.Class, Tasks.DeadlineDate, Tasks.Description, Rooms.Number, Buildings.Adress 
FROM Tasks JOIN Employees ON Tasks.EmployeeID = Employees.ID JOIN Rooms ON Tasks.RoomID = Rooms.ID JOIN Buildings ON Buildings.ID = Rooms.ID
WHERE Employees.Surname='Electronov' AND Tasks.Class='electricity' AND Tasks.FinishedDate IS NULL


-- Вывод задач, назначенных сотрудникам, не ассоциированным со зданиями, для которых поставлены задачи
SELECT Tasks.*, Employees.* FROM Tasks JOIN Rooms ON Tasks.RoomID = Rooms.ID JOIN Employees ON Employees.ID = Tasks.EmployeeID WHERE Rooms.BuildingID <> Employees.WorkingBuildingID