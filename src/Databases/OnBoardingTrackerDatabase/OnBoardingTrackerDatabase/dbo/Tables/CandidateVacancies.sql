CREATE TABLE [dbo].[CandidateVacancies] (
    [Id]          INT IDENTITY (1, 1) NOT NULL,
    [CandidateId] INT NOT NULL,
    [VacancyId]   INT NOT NULL,
    CONSTRAINT [PK_CandidateVacancies] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CandidateVacancies_Candidates] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[Candidates] ([Id]),
    CONSTRAINT [FK_CandidateVacancies_Vacancies] FOREIGN KEY ([VacancyId]) REFERENCES [dbo].[Vacancies] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CandidateVacancies_CandidateId]
    ON [dbo].[CandidateVacancies]([CandidateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CandidateVacancies_VacancyId]
    ON [dbo].[CandidateVacancies]([VacancyId] ASC);

