
------------------------------------------------------------
-- 1) CREATE TABLE: Users
------------------------------------------------------------
CREATE TABLE [dbo].[Users]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Email] NVARCHAR(255) NOT NULL UNIQUE,
    [Password] NVARCHAR(255) NOT NULL,
    [UserType] NVARCHAR(50) NOT NULL
        CHECK ([UserType] IN ('homeowner', 'company')),
    [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE()
);


------------------------------------------------------------
-- 2) CREATE TABLE: Companies (FK to Users)
------------------------------------------------------------
CREATE TABLE [dbo].[Companies]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(200) NOT NULL,

    -- default is broker
    [Type] NVARCHAR(50) NOT NULL 
        CONSTRAINT DF_Companies_Type DEFAULT ('broker'),

    -- standard | good | excellent or NULL
    [Tier] NVARCHAR(20) NULL
        CONSTRAINT CK_Companies_Tier 
        CHECK ([Tier] IN ('standard', 'good', 'excellent') OR [Tier] IS NULL),

    -- owner user id
    [UserId] INT NOT NULL,

    [CreatedAt] DATETIME NOT NULL 
        CONSTRAINT DF_Companies_CreatedAt DEFAULT (GETDATE()),

    CONSTRAINT FK_Companies_Users
        FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id])
);


------------------------------------------------------------
-- 3) CREATE TABLE: UserProjects (FK to Users)
------------------------------------------------------------
CREATE TABLE [dbo].[UserProjects]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,

    [Location] NVARCHAR(100) NOT NULL,

    [Status] NVARCHAR(50) NOT NULL
        CHECK ([Status] IN ('new', 'in progress', 'completed')),

    [Floors] INT NOT NULL
        CHECK ([Floors] >= 1),

    [KrokiNumber] NVARCHAR(50) NOT NULL,

    [UserId] INT NOT NULL,

    [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_UserProjects_Users
        FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id])
);


------------------------------------------------------------
-- 4) CREATE TABLE: Milestones (FK to UserProjects)
------------------------------------------------------------
CREATE TABLE [dbo].[Milestones]
(
    [Id] BIGINT IDENTITY(1,1) NOT NULL PRIMARY KEY,

    [Project_Id] INT NOT NULL,  -- FK to UserProjects.Id

    [Title] NVARCHAR(200) NOT NULL,

    [Due_Date] DATETIME NULL,

    [Amount_Omr] NUMERIC(14,2) NULL,

    [Status] NVARCHAR(20) NOT NULL
        CONSTRAINT DF_Milestones_Status DEFAULT ('planned'),

    [Order_Index] INT NULL,

    [Created_At] DATETIME NOT NULL
        CONSTRAINT DF_Milestones_CreatedAt DEFAULT (GETDATE()),

    CONSTRAINT FK_Milestones_UserProjects
        FOREIGN KEY ([Project_Id])
        REFERENCES [dbo].[UserProjects]([Id])
        ON DELETE CASCADE
);


------------------------------------------------------------
-- 5) CREATE TABLE: ContactUs (User can only add)
------------------------------------------------------------
CREATE TABLE [dbo].[ContactUs]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,

    -- optional: link to user if logged in
    [UserId] INT NULL,

    [Name] NVARCHAR(150) NOT NULL,
    [Email] NVARCHAR(255) NOT NULL,
    [Subject] NVARCHAR(200) NOT NULL,
    [Message] NVARCHAR(MAX) NOT NULL,

    [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_ContactUs_Users
        FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id])
);


------------------------------------------------------------
-- 6) CREATE VIEW: ProjectOwners (Project + Owner User)
------------------------------------------------------------
CREATE VIEW [dbo].[ProjectOwners]
AS
SELECT 
    P.Id        AS ProjectId,
    P.Location,
    P.Status,
    P.Floors,
    P.KrokiNumber,
    P.CreatedAt AS ProjectCreatedAt,
    U.Id        AS OwnerId,
    U.Email     AS OwnerEmail,
    U.UserType,
    U.CreatedAt AS UserCreatedAt
FROM [dbo].[UserProjects] AS P
JOIN [dbo].[Users] AS U
    ON P.UserId = U.Id;


------------------------------------------------------------
-- 7) INSERT SAMPLE DATA: Users
------------------------------------------------------------
INSERT INTO [dbo].[Users] (Email, Password, UserType)
VALUES 
('ahmed@gmail.com',        '123456',     'homeowner'),  -- Id = 1
('fatma@hotmail.com',      'mypassword', 'homeowner'),  -- Id = 2
('saif@builder.com',       'pass123',    'company'),    -- Id = 3
('noor@consulting.com',    'abc12345',   'company'),    -- Id = 4
('decor@beauty.com',       'decor123',   'company');    -- Id = 5


------------------------------------------------------------
-- 8) INSERT SAMPLE DATA: Companies
------------------------------------------------------------
INSERT INTO [dbo].[Companies] (Name, Type, Tier, UserId)
VALUES
('Al Noor Construction',      'broker',     'excellent', 3),
('Golden Stone Consultants',  'consultant', 'good',      4),
('Dream Home Decoration',     'decoration', 'standard',  5),
('Badr Builders',             'broker',     NULL,        3),
('Elite Design Studio',       'decoration', 'excellent', 5);


------------------------------------------------------------
-- 9) INSERT SAMPLE DATA: UserProjects  (FIXED NAME)
------------------------------------------------------------
INSERT INTO [dbo].[UserProjects] (Location, Status, Floors, KrokiNumber, UserId)
VALUES
('Al Khoud, Muscat',   'new',          2, 'KR-001', 1),
('Al Mawaleh, Muscat', 'in progress',  3, 'KR-002', 1),
('Al Amerat, Muscat',  'completed',    1, 'KR-003', 2),
('Ruwi, Muscat',       'new',          5, 'KR-004', 2),
('Bausher, Muscat',    'in progress',  4, 'KR-005', 1);


------------------------------------------------------------
-- 10) INSERT SAMPLE DATA: Milestones
------------------------------------------------------------
INSERT INTO [dbo].[Milestones]
(Project_Id, Title, Due_Date, Amount_Omr, Status, Order_Index)
VALUES
-- Project 1
(1, 'Initial Meeting & Planning', '2025-01-10', 100.00, 'planned',     1),
(1, 'Site Survey',                '2025-01-15',  75.00, 'planned',     2),
(1, 'Drawing & Design',           '2025-01-25', 250.00, 'planned',     3),
(1, 'Client Approval',            '2025-02-01',   0.00, 'planned',     4),

-- Project 2
(2, 'Excavation Work',            '2025-01-20', 300.00, 'in progress', 1),
(2, 'Foundation Casting',         '2025-01-30',1200.00, 'planned',     2),
(2, 'Ground Floor Columns',       '2025-02-10', 950.00, 'planned',     3),

-- Project 3
(3, 'Project Kickoff',            '2025-01-12',  50.00, 'completed',   1),
(3, 'Base Structure Build',       '2025-01-28', 500.00, 'in progress', 2),
(3, 'Electrical Layout',          '2025-02-05', 300.00, 'planned',     3);


------------------------------------------------------------
-- 11) INSERT SAMPLE DATA: ContactUs
------------------------------------------------------------
INSERT INTO [dbo].[ContactUs]
(Name, Email, Subject, Message, UserId)
VALUES
('Ahmed Al Harthy', 'ahmed@gmail.com', 'Need help', 'I cannot see my milestones in the website.', 1);
