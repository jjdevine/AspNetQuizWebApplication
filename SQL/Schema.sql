-- CREATE SCHEMA quiz;

CREATE TABLE [quiz].[Users](
	[Name] [nvarchar](50) NOT NULL,
	CONSTRAINT [pk_users] PRIMARY KEY (Name)
) ON [PRIMARY]
GO

CREATE TABLE [quiz].[UserQuizzes](
	[QuizId] [uniqueidentifier] NOT NULL,
	[User] [nvarchar](50) NOT NULL,
	[QuizName] [nvarchar](50) NOT NULL,
	CONSTRAINT [pk_userquizzes] PRIMARY KEY (QuizId)
) ON [PRIMARY]
GO

CREATE TABLE [quiz].[QuizQuestions](
	[QuestionId] [uniqueidentifier] NOT NULL,
	[QuizId] [uniqueidentifier] NOT NULL,
	[Question] [nvarchar](4000) NOT NULL,
	[Answer] [nvarchar](4000) NOT NULL,
	[Order] [int] NOT NULL,
	CONSTRAINT [pk_quizquestions] PRIMARY KEY (QuestionId)
) ON [PRIMARY]
GO

-- sample inserts
INSERT INTO [quiz].[UserQuizzes] (QuizId, [User], QuizName) values (newid(), 'test', 'quiz1');

-- batch insert
INSERT INTO [quiz].[QuizQuestions] ([QuestionId], [QuizId], [Question], [Answer]) VALUES
	(newid(), newid(), 'test question', 'test answer'),
	(newid(), newid(), 'test question2', 'test answer2')
