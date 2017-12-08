DROP TABLE Course_GF CASCADE CONSTRAINTS;
CREATE TABLE Course_GF  
(  
Id NUMBER(10) NOT NULL PRIMARY KEY,  
Name VARCHAR2(50) NOT NULL,   
Price NUMBER(8,2) NOT NULL  
);

DROP INDEX CourseNameUniqueIndex;
CREATE UNIQUE INDEX CourseNameUniqueIndex ON Course_GF (Name);

DROP TABLE User_GF CASCADE CONSTRAINTS;
CREATE TABLE User_GF  
(  
Id NUMBER(10) NOT NULL PRIMARY KEY,  
Login VARCHAR2(50) NOT NULL,   
Password VARCHAR(256) NOT NULL,
CONSTRAINT Unique_User_Login UNIQUE(Login)  
);

DROP TABLE Session_GF CASCADE CONSTRAINTS;
CREATE TABLE Session_GF  
(  
Id NUMBER(10) NOT NULL PRIMARY KEY,  
StartDate Date NOT NULL,   
EndDate Date NOT NULL,  
CourseId NUMBER(10) NOT NULL,  
CONSTRAINT FK_Seassion_Course FOREIGN KEY (CourseId) REFERENCES Course_GF (Id) --ON DELETE NO ACTION
);

DROP TABLE SessionUsers_GF CASCADE CONSTRAINTS;
CREATE TABLE SessionUsers_GF  
(  
SessionId NUMBER(10) NOT NULL,  
UserId NUMBER(10) NOT NULL,  
CONSTRAINT FK_SessionUsers_Session FOREIGN KEY (SessionId) REFERENCES Session_GF (Id) ON DELETE CASCADE,  
CONSTRAINT FK_SessionUsers_User FOREIGN KEY (UserId) REFERENCES User_GF (Id) ON DELETE CASCADE,  
CONSTRAINT PK_SessionUsers PRIMARY KEY(SessionId, UserId)  
);

DROP TABLE Lecture_GF CASCADE CONSTRAINTS;
CREATE TABLE Lecture_GF  
(  
Id NUMBER(10) NOT NULL PRIMARY KEY,  
Name VARCHAR2(50) NOT NULL,  
CourseId NUMBER(10) NOT NULL,  
Content VARCHAR2(4000),  
CONSTRAINT Unique_Lecture_Name UNIQUE(Name),  
CONSTRAINT FK_Lecture_Course FOREIGN KEY (CourseId) REFERENCES Course_GF (Id) --ON DELETE NO ACTION
);

DROP TABLE CourseMark_GF CASCADE CONSTRAINTS;
CREATE TABLE CourseMark_GF  
(  
UserId NUMBER(10) NOT NULL,  
CourseId NUMBER(10) NOT NULL,  
Message VARCHAR2(500),  
Mark NUMBER(2,1),  
CreationDate DATE NOT NULL,  
CONSTRAINT FK_CourseMark_User FOREIGN KEY (UserId) REFERENCES User_GF (Id) ON DELETE CASCADE,  
CONSTRAINT FK_CourseMark_Course FOREIGN KEY (CourseId) REFERENCES Course_GF (Id), --ON DELETE NO ACTION  
CONSTRAINT PK_CourseMark PRIMARY KEY(UserId, CourseId),  
CONSTRAINT Check_Mark CHECK(Mark BETWEEN 1.0 AND 5.0)  
);

DROP TABLE AuditTrail_GF CASCADE CONSTRAINTS;
CREATE TABLE AuditTrail_GF
(
	Id INTEGER NOT NULL,
	ObjectType VARCHAR2(50),
	Action VARCHAR2(50),
	Status VARCHAR2(20),
	Timestamp TIMESTAMP,
	Message VARCHAR2(500),
	CONSTRAINT PK_AuditTrail PRIMARY KEY(Id)
);

DROP SEQUENCE AuditTrailId_Autoincrement_GF;
CREATE SEQUENCE AuditTrailId_Autoincrement_GF START WITH 1;

/

CREATE OR REPLACE TRIGGER OnBInsertAuditTrailTrigger_GF
BEFORE INSERT 
	ON AuditTrail_GF 
	FOR EACH ROW
BEGIN
  SELECT AuditTrailId_Autoincrement_GF.NEXTVAL
  INTO   :new.Id
  FROM   DUAL;
END;

/
CREATE OR REPLACE TRIGGER OnDeleteCourseTrigger_GF
BEFORE DELETE
   ON Course_GF
   FOR EACH ROW
DECLARE
   deletedCourseId NUMBER;
   deletedCourseName VARCHAR2(50);
   ts TIMESTAMP;
BEGIN
	deletedCourseId := :old.Id;
	deletedCourseName := :old.Name;
	
	DELETE 
	FROM Lecture_GF
	WHERE CourseId = deletedCourseId;

	DELETE 
	FROM Session_GF
	WHERE CourseId = deletedCourseId;

	DELETE 
	FROM CourseMark_GF
	WHERE CourseId = deletedCourseId;

    SELECT CURRENT_TIMESTAMP INTO ts FROM DUAL;
            
    INSERT INTO AuditTrail_GF(Id, ObjectType, Action, Status, Timestamp, Message)
    VALUES(0, 'Course', 'Delete', 'Success', ts, 'An attempt to delete course "' || deletedCourseName || '"');
END;

/

CREATE OR REPLACE TRIGGER OnCUDelUserTrigger_GF
    BEFORE
        INSERT OR
        UPDATE OF Login OR
        DELETE
    ON User_GF
    FOR EACH ROW
    DECLARE
        action VARCHAR2(50);
        ts TIMESTAMP;
        message VARCHAR2(500);
    BEGIN
        SELECT CURRENT_TIMESTAMP INTO ts FROM DUAL;
        
        CASE
            WHEN INSERTING THEN
                action := 'Create';
                message := 'An attempt to create user with login: "' || :new.Login || '".';
            WHEN UPDATING('Login') THEN
                action := 'Update';
                message := 'An attempt to update user login: old - "' || :old.Login || '", new - "' || :new.Login || '"';
            WHEN DELETING THEN
                action := 'Delete';
                message := 'An attempt to delete user. Login - "' || :old.Login || '", Id - "' || TO_CHAR(:old.Id) || '"';
        END CASE;
        
        BEGIN
            INSERT INTO AuditTrail_GF(Id, ObjectType, Action, Status, Timestamp, Message)
            VALUES(0, 'User', action, 'Success', ts, message);
        END;
    END;

/

---------------------------------------------------------------------------------------------------------
INSERT INTO Course_GF (Id, Name, Price) 
VALUES(1, 'Course 1', 9.99);
INSERT INTO Course_GF (Id, Name, Price) 
VALUES(2, 'Course 2', 19.99);
INSERT INTO Course_GF (Id, Name, Price) 
VALUES(3, 'Course 3', 0.0);


INSERT INTO User_GF (Id, Login, Password) 
VALUES(1, 'Superman', 'Password12');
INSERT INTO User_GF (Id, Login, Password) 
VALUES(2, 'AquaMan', 'Password12');
INSERT INTO User_GF (Id, Login, Password) 
VALUES(3, 'Flash', 'Password12');
INSERT INTO User_GF (Id, Login, Password) 
VALUES(4, 'WonderWoman', 'Password12');


INSERT INTO Session_GF (Id, StartDate, EndDate, CourseId) 
VALUES(1, '10-dec-16', '10-jan-18', 1);
INSERT INTO Session_GF (Id, StartDate, EndDate, CourseId) 
VALUES(2, '10-oct-17', '10-dec-17', 2);


INSERT INTO SessionUsers_GF (SessionId, UserId) 
VALUES(1, 1);
INSERT INTO SessionUsers_GF (SessionId, UserId) 
VALUES(1, 2);
INSERT INTO SessionUsers_GF (SessionId, UserId) 
VALUES(2, 1);
INSERT INTO SessionUsers_GF (SessionId, UserId) 
VALUES(2, 4);


INSERT INTO Lecture_GF (Id, CourseId, Name, Content) 
VALUES(1, 1, 'Lecture 1', 'This is content of the Course 1 lecture 1');
INSERT INTO Lecture_GF (Id, CourseId, Name, Content) 
VALUES(2, 1, 'Lecture 2', 'This is content of the Course 1 lecture 2');
INSERT INTO Lecture_GF (Id, CourseId, Name, Content) 
VALUES(3, 1, 'Lecture 3', 'This is content of the Course 1 lecture 3');
INSERT INTO Lecture_GF (Id, CourseId, Name, Content) 
VALUES(4, 2, 'Lecture 1 C1', 'This is content of the Course 2 lecture 1');
INSERT INTO Lecture_GF (Id, CourseId, Name, Content) 
VALUES(5, 3, 'Lecture 1 C2', 'This is content of the Course 3 lecture 1');


INSERT INTO CourseMark_GF (CourseId, UserId, Message, CreationDate, Mark) 
VALUES(1, 1, 'This course is awesome!', '23-oct-17', 5.0);
INSERT INTO CourseMark_GF (CourseId, UserId, Message, CreationDate, Mark) 
VALUES(1, 2, 'This course is quite good enough!', '23-sep-17', 4.5);
INSERT INTO CourseMark_GF (CourseId, UserId, Message, CreationDate, Mark) 
VALUES(2, 1, 'Does not like it much', '10-oct-17', 3.5);
INSERT INTO CourseMark_GF (CourseId, UserId, Message, CreationDate, Mark) 
VALUES(2, 4, NULL, '12-oct-17', 5.0);

/

---------------------------------------------------------------------------------------------------------    
CREATE OR REPLACE PACKAGE CourseAppPackage_GF AS
    violation_of_constraint EXCEPTION;
    
    TYPE CourseType IS RECORD
	(	
		Id NUMBER(10),  
		Name VARCHAR2(50),  
		Mark NUMBER(2,1),  
		Price NUMBER(8,2)
	);

	TYPE CourseTypeSet IS TABLE OF CourseType;
    
    TYPE SessionType IS RECORD  
	(  
		Id NUMBER(10),  
		StartDate DATE,
		EndDate DATE
	);
    
	FUNCTION GetTopPopularCourses(pageNumber IN NUMBER, pageSize IN NUMBER) RETURN CourseTypeSet PIPELINED;
	
	FUNCTION GetLecturesCount(courseId IN NUMBER) RETURN NUMBER;

	FUNCTION GetCurrentUserSession(courseId IN NUMBER, userId IN NUMBER) RETURN SessionType;

	PROCEDURE CreateCourseProc (courseName IN VARCHAR2, coursePrice IN NUMBER, newCourseId OUT NUMBER);
	
	PROCEDURE DeleteCourseProc (courseId IN NUMBER);
END CourseAppPackage_GF;

/

CREATE OR REPLACE PACKAGE BODY CourseAppPackage_GF AS
    FUNCTION GetTopPopularCourses(pageNumber IN NUMBER, pageSize IN NUMBER)  
    	RETURN CourseTypeSet PIPELINED IS
    		out_rec CourseType;
    	BEGIN  
    		DECLARE  
    			lastToSkip NUMBER := (pageNumber - 1) * pageSize;  
    			lastToTake NUMBER := lastToSkip + pageSize;  
            
    			BEGIN
    				FOR course IN (
            					SELECT	Id,  
            						Name,  
            						Mark,  
            						Price  
                				FROM  
                				(  
                					SELECT  Id,  
                							Name,  
                							Price,  
                							Mark,  
                							ROW_NUMBER() OVER (ORDER BY Id asc) RowNumber  
                					FROM   
                					(  
                						SELECT  C.Id,  
                								C.Name,  
                								C.Price,  
                								AVG(CM.Mark) AS Mark  
                						FROM Course_GF C  
                							INNER JOIN CourseMark_GF CM ON C.Id = CM.CourseId  
                						GROUP BY C.Id, C.Name, C.Price  
                					) RatedCourses  
                				) Numbered  
                				WHERE 0 < RowNumber AND RowNumber <= 12)
    					   LOOP
    							out_rec.Id := course.Id;
    							out_rec.Name := course.Name;
    							out_rec.Mark := course.Mark;
    							out_rec.Price := course.Price;
    							PIPE ROW (out_rec);
    						
    					   END LOOP;
    			END;
    			
    		RETURN;
    	END;

	---------------------------------------------------------------------------------------------------------
	FUNCTION GetLecturesCount(courseId IN NUMBER) 
	   RETURN NUMBER 
	   IS lecturesCount NUMBER(10);
	   BEGIN 
		  SELECT COUNT(*) 
		  INTO lecturesCount 
		  FROM Lecture_GF 
		  WHERE CourseId = courseId; 
		  RETURN(lecturesCount); 
		END;

	---------------------------------------------------------------------------------------------------------
	FUNCTION GetCurrentUserSession(courseId IN NUMBER, userId IN NUMBER) 
	   RETURN SessionType 
	   IS sessionEntry SessionType;
	   BEGIN
		  SELECT S.Id, 
				 S.StartDate, 
				 S.EndDate
		  INTO sessionEntry.Id, sessionEntry.StartDate, sessionEntry.EndDate
		  FROM Session_GF S
				INNER JOIN SessionUsers_GF SU ON S.Id = SU.SessionId
		  WHERE S.CourseId = 1 
				AND SU.UserId = 1
				AND S.StartDate < to_date(SYSDATE,'DD-MON-YY')
				AND S.EndDate > to_date(SYSDATE,'DD-MON-YY');
		  RETURN(sessionEntry); 
		 END;

	---------------------------------------------------------------------------------------------------------
	PROCEDURE CreateCourseProc (courseName IN VARCHAR2, coursePrice IN NUMBER, newCourseId OUT NUMBER) AS
	   BEGIN
			DECLARE
				lastCourseId NUMBER;
			BEGIN
				SELECT MAX(Id) + 1
				INTO newCourseId
				FROM Course_GF;

				BEGIN
						SAVEPOINT X;
							INSERT INTO Course_GF (Id, Name, Price) 
							VALUES(newCourseId, courseName, coursePrice);
						EXCEPTION 
							WHEN DUP_VAL_ON_INDEX THEN
								ROLLBACK TO X;
								COMMIT;
								RAISE violation_of_constraint;
					END;

			END;
	   END;
	   ---------------------------------------------------------------------------------------------------------
	   PROCEDURE DeleteCourseProc (courseId IN NUMBER) AS
	        BEGIN
	            DELETE
	            FROM Course_GF
	            WHERE Id = courseId;

				COMMIT;
	        END;
END CourseAppPackage_GF;

/

