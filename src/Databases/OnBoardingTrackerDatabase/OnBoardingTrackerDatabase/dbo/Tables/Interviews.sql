CREATE TABLE [dbo].[Interviews] (
    [Id]           INT            IDENTITY (1, 1) NOT NULL,
    [Title]        NVARCHAR (150) NOT NULL,
    [CandidateId]  INT            NOT NULL,
    [VacancyId]    INT            NOT NULL,
    [StartingTime] DATETIME       NOT NULL,
    [EndingTime]   DATETIME       NOT NULL,
    [Created]      DATETIME2 (7)  DEFAULT ('0001-01-01T00:00:00.0000000') NOT NULL,
    [CreatedBy]    INT            DEFAULT ((0)) NOT NULL,
    [IsDeleted]    BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Updated]      DATETIME2 (7)  NULL,
    [UpdatedBy]    INT            NULL,
    CONSTRAINT [PK_Interviews] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Interviews_Candidates] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[Candidates] ([Id]),
    CONSTRAINT [FK_Interviews_Vacancies] FOREIGN KEY ([VacancyId]) REFERENCES [dbo].[Vacancies] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Interviews_CandidateId]
    ON [dbo].[Interviews]([CandidateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Interviews_VacancyId]
    ON [dbo].[Interviews]([VacancyId] ASC);

