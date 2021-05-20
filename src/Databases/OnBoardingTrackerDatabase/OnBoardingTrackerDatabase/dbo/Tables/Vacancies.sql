CREATE TABLE [dbo].[Vacancies] (
    [Id]                  INT            IDENTITY (1, 1) NOT NULL,
    [Title]               NVARCHAR (100) NOT NULL,
    [MaxSalary]           MONEY          NOT NULL,
    [AssignedRecruiterId] INT            NOT NULL,
    [WorkExperience]      FLOAT (53)     NOT NULL,
    [SeniorityLevelId]    INT            NOT NULL,
    [JobTypeId]           INT            NOT NULL,
    [VacancyStatusId]     INT            NOT NULL,
    [Created]             DATETIME2 (7)  DEFAULT ('0001-01-01T00:00:00.0000000') NOT NULL,
    [CreatedBy]           INT            DEFAULT ((0)) NOT NULL,
    [IsDeleted]           BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Updated]             DATETIME2 (7)  NULL,
    [UpdatedBy]           INT            NULL,
    CONSTRAINT [PK_Vacancies] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Vacancies_JobTypes] FOREIGN KEY ([JobTypeId]) REFERENCES [dbo].[JobTypes] ([Id]),
    CONSTRAINT [FK_Vacancies_Recruiters] FOREIGN KEY ([AssignedRecruiterId]) REFERENCES [dbo].[Recruiters] ([Id]),
    CONSTRAINT [FK_Vacancies_SeniorityLevels] FOREIGN KEY ([SeniorityLevelId]) REFERENCES [dbo].[SeniorityLevels] ([Id]),
    CONSTRAINT [FK_Vacancies_VacancyStatuses] FOREIGN KEY ([VacancyStatusId]) REFERENCES [dbo].[VacancyStatuses] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Vacancies_AssignedRecruiterId]
    ON [dbo].[Vacancies]([AssignedRecruiterId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Vacancies_JobTypeId]
    ON [dbo].[Vacancies]([JobTypeId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Vacancies_SeniorityLevelId]
    ON [dbo].[Vacancies]([SeniorityLevelId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Vacancies_VacancyStatusId]
    ON [dbo].[Vacancies]([VacancyStatusId] ASC);

