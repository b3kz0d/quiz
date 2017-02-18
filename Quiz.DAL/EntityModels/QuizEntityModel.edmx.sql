
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/13/2017 21:22:28
-- Generated from EDMX file: d:\Projects\GitProject\Quiz\Quiz.DAL\EntityModels\QuizEntityModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [QuizDatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_AnswerResult_QuestionAnswer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnswerResult] DROP CONSTRAINT [FK_AnswerResult_QuestionAnswer];
GO
IF OBJECT_ID(N'[dbo].[FK_AnswerResult_Result]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[AnswerResult] DROP CONSTRAINT [FK_AnswerResult_Result];
GO
IF OBJECT_ID(N'[dbo].[FK_Question_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Question] DROP CONSTRAINT [FK_Question_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_Question_QuestionLevel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Question] DROP CONSTRAINT [FK_Question_QuestionLevel];
GO
IF OBJECT_ID(N'[dbo].[FK_QuestionAnswer_Question]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuestionAnswer] DROP CONSTRAINT [FK_QuestionAnswer_Question];
GO
IF OBJECT_ID(N'[dbo].[FK_QuizSession_Category]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuizSession] DROP CONSTRAINT [FK_QuizSession_Category];
GO
IF OBJECT_ID(N'[dbo].[FK_QuizSession_QuizOption]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuizSession] DROP CONSTRAINT [FK_QuizSession_QuizOption];
GO
IF OBJECT_ID(N'[dbo].[FK_QuizSession_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[QuizSession] DROP CONSTRAINT [FK_QuizSession_User];
GO
IF OBJECT_ID(N'[dbo].[FK_Result_Question]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Result] DROP CONSTRAINT [FK_Result_Question];
GO
IF OBJECT_ID(N'[dbo].[FK_Result_QuizSession]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Result] DROP CONSTRAINT [FK_Result_QuizSession];
GO
IF OBJECT_ID(N'[dbo].[FK_Token_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Token] DROP CONSTRAINT [FK_Token_User];
GO
IF OBJECT_ID(N'[dbo].[FK_User_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[User] DROP CONSTRAINT [FK_User_Role];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[AnswerResult]', 'U') IS NOT NULL
    DROP TABLE [dbo].[AnswerResult];
GO
IF OBJECT_ID(N'[dbo].[Category]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Category];
GO
IF OBJECT_ID(N'[dbo].[Question]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Question];
GO
IF OBJECT_ID(N'[dbo].[QuestionAnswer]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionAnswer];
GO
IF OBJECT_ID(N'[dbo].[QuestionLevel]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuestionLevel];
GO
IF OBJECT_ID(N'[dbo].[QuizOption]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuizOption];
GO
IF OBJECT_ID(N'[dbo].[QuizSession]', 'U') IS NOT NULL
    DROP TABLE [dbo].[QuizSession];
GO
IF OBJECT_ID(N'[dbo].[Result]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Result];
GO
IF OBJECT_ID(N'[dbo].[Role]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Role];
GO
IF OBJECT_ID(N'[dbo].[Token]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Token];
GO
IF OBJECT_ID(N'[dbo].[User]', 'U') IS NOT NULL
    DROP TABLE [dbo].[User];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'AnswerResults'
CREATE TABLE [dbo].[AnswerResults] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [ResultId] int  NOT NULL,
    [QuestionAnswerId] int  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(250)  NULL
);
GO

-- Creating table 'Questions'
CREATE TABLE [dbo].[Questions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CategoryId] int  NOT NULL,
    [QuestionLevelId] int  NOT NULL,
    [QuestionContent] nvarchar(250)  NOT NULL
);
GO

-- Creating table 'QuestionAnswers'
CREATE TABLE [dbo].[QuestionAnswers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [QuestionId] int  NOT NULL,
    [AnswerContent] nvarchar(250)  NOT NULL,
    [IsCorrect] bit  NOT NULL
);
GO

-- Creating table 'QuestionLevels'
CREATE TABLE [dbo].[QuestionLevels] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Score] int  NOT NULL,
    [Description] nvarchar(250)  NULL
);
GO

-- Creating table 'QuizOptions'
CREATE TABLE [dbo].[QuizOptions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Amount] int  NOT NULL,
    [RequiredPercentage] decimal(18,0)  NOT NULL
);
GO

-- Creating table 'QuizSessions'
CREATE TABLE [dbo].[QuizSessions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserId] int  NOT NULL,
    [CategoryId] int  NOT NULL,
    [QuizOptionId] int  NOT NULL,
    [SessionStart] datetime  NOT NULL,
    [LastUpdate] datetime  NOT NULL,
    [Status] bit  NOT NULL
);
GO

-- Creating table 'Results'
CREATE TABLE [dbo].[Results] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [QuizSessionId] int  NOT NULL,
    [QuestionId] int  NOT NULL,
    [QuestionOrder] int  NOT NULL,
    [CreatedTime] datetime  NOT NULL,
    [UpdatedTime] datetime  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(35)  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int  NOT NULL,
    [RoleId] int  NOT NULL,
    [UserName] nvarchar(20)  NOT NULL,
    [Password] nvarchar(20)  NOT NULL,
    [Name] nvarchar(50)  NULL
);
GO

-- Creating table 'Tokens'
CREATE TABLE [dbo].[Tokens] (
    [AuthToken] nvarchar(50)  NOT NULL,
    [UserId] int  NOT NULL,
    [IssuedOn] datetime  NOT NULL,
    [ExpiresOn] datetime  NOT NULL,
    [LastLogin] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'AnswerResults'
ALTER TABLE [dbo].[AnswerResults]
ADD CONSTRAINT [PK_AnswerResults]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [PK_Questions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionAnswers'
ALTER TABLE [dbo].[QuestionAnswers]
ADD CONSTRAINT [PK_QuestionAnswers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuestionLevels'
ALTER TABLE [dbo].[QuestionLevels]
ADD CONSTRAINT [PK_QuestionLevels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuizOptions'
ALTER TABLE [dbo].[QuizOptions]
ADD CONSTRAINT [PK_QuizOptions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'QuizSessions'
ALTER TABLE [dbo].[QuizSessions]
ADD CONSTRAINT [PK_QuizSessions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Results'
ALTER TABLE [dbo].[Results]
ADD CONSTRAINT [PK_Results]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [AuthToken] in table 'Tokens'
ALTER TABLE [dbo].[Tokens]
ADD CONSTRAINT [PK_Tokens]
    PRIMARY KEY CLUSTERED ([AuthToken] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [QuestionAnswerId] in table 'AnswerResults'
ALTER TABLE [dbo].[AnswerResults]
ADD CONSTRAINT [FK_AnswerResult_QuestionAnswer]
    FOREIGN KEY ([QuestionAnswerId])
    REFERENCES [dbo].[QuestionAnswers]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AnswerResult_QuestionAnswer'
CREATE INDEX [IX_FK_AnswerResult_QuestionAnswer]
ON [dbo].[AnswerResults]
    ([QuestionAnswerId]);
GO

-- Creating foreign key on [ResultId] in table 'AnswerResults'
ALTER TABLE [dbo].[AnswerResults]
ADD CONSTRAINT [FK_AnswerResult_Result]
    FOREIGN KEY ([ResultId])
    REFERENCES [dbo].[Results]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_AnswerResult_Result'
CREATE INDEX [IX_FK_AnswerResult_Result]
ON [dbo].[AnswerResults]
    ([ResultId]);
GO

-- Creating foreign key on [CategoryId] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [FK_Question_Category]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Question_Category'
CREATE INDEX [IX_FK_Question_Category]
ON [dbo].[Questions]
    ([CategoryId]);
GO

-- Creating foreign key on [CategoryId] in table 'QuizSessions'
ALTER TABLE [dbo].[QuizSessions]
ADD CONSTRAINT [FK_QuizSession_Category]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuizSession_Category'
CREATE INDEX [IX_FK_QuizSession_Category]
ON [dbo].[QuizSessions]
    ([CategoryId]);
GO

-- Creating foreign key on [QuestionLevelId] in table 'Questions'
ALTER TABLE [dbo].[Questions]
ADD CONSTRAINT [FK_Question_QuestionLevel]
    FOREIGN KEY ([QuestionLevelId])
    REFERENCES [dbo].[QuestionLevels]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Question_QuestionLevel'
CREATE INDEX [IX_FK_Question_QuestionLevel]
ON [dbo].[Questions]
    ([QuestionLevelId]);
GO

-- Creating foreign key on [QuestionId] in table 'QuestionAnswers'
ALTER TABLE [dbo].[QuestionAnswers]
ADD CONSTRAINT [FK_QuestionAnswer_Question]
    FOREIGN KEY ([QuestionId])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuestionAnswer_Question'
CREATE INDEX [IX_FK_QuestionAnswer_Question]
ON [dbo].[QuestionAnswers]
    ([QuestionId]);
GO

-- Creating foreign key on [QuestionId] in table 'Results'
ALTER TABLE [dbo].[Results]
ADD CONSTRAINT [FK_Result_Question]
    FOREIGN KEY ([QuestionId])
    REFERENCES [dbo].[Questions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Result_Question'
CREATE INDEX [IX_FK_Result_Question]
ON [dbo].[Results]
    ([QuestionId]);
GO

-- Creating foreign key on [QuizOptionId] in table 'QuizSessions'
ALTER TABLE [dbo].[QuizSessions]
ADD CONSTRAINT [FK_QuizSession_QuizOption]
    FOREIGN KEY ([QuizOptionId])
    REFERENCES [dbo].[QuizOptions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuizSession_QuizOption'
CREATE INDEX [IX_FK_QuizSession_QuizOption]
ON [dbo].[QuizSessions]
    ([QuizOptionId]);
GO

-- Creating foreign key on [UserId] in table 'QuizSessions'
ALTER TABLE [dbo].[QuizSessions]
ADD CONSTRAINT [FK_QuizSession_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_QuizSession_User'
CREATE INDEX [IX_FK_QuizSession_User]
ON [dbo].[QuizSessions]
    ([UserId]);
GO

-- Creating foreign key on [QuizSessionId] in table 'Results'
ALTER TABLE [dbo].[Results]
ADD CONSTRAINT [FK_Result_QuizSession]
    FOREIGN KEY ([QuizSessionId])
    REFERENCES [dbo].[QuizSessions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Result_QuizSession'
CREATE INDEX [IX_FK_Result_QuizSession]
ON [dbo].[Results]
    ([QuizSessionId]);
GO

-- Creating foreign key on [RoleId] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_User_Role]
    FOREIGN KEY ([RoleId])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_User_Role'
CREATE INDEX [IX_FK_User_Role]
ON [dbo].[Users]
    ([RoleId]);
GO

-- Creating foreign key on [UserId] in table 'Tokens'
ALTER TABLE [dbo].[Tokens]
ADD CONSTRAINT [FK_Token_User]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Token_User'
CREATE INDEX [IX_FK_Token_User]
ON [dbo].[Tokens]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------