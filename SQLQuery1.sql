CREATE TABLE [dbo].[ContactUs]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,

    -- optional: link message to logged-in user
    [UserId] INT NULL,

    [Name] NVARCHAR(150) NOT NULL,
    [Email] NVARCHAR(255) NOT NULL,
    [Subject] NVARCHAR(200) NOT NULL,
    [Message] NVARCHAR(MAX) NOT NULL,

    [CreatedAt] DATETIME NOT NULL DEFAULT GETDATE(),

    CONSTRAINT FK_ContactUs_Users
        FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users]([Id])
);
