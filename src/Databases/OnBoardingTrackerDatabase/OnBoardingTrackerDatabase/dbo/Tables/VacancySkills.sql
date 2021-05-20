CREATE TABLE [dbo].[VacancySkills] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [VacancyId] INT NOT NULL,
    [SkillId]   INT NOT NULL,
    CONSTRAINT [PK_VacancySkills] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_VacancySkills_Skills] FOREIGN KEY ([SkillId]) REFERENCES [dbo].[Skills] ([Id]),
    CONSTRAINT [FK_VacancySkills_Vacancies] FOREIGN KEY ([VacancyId]) REFERENCES [dbo].[Vacancies] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_VacancySkills_SkillId]
    ON [dbo].[VacancySkills]([SkillId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_VacancySkills_VacancyId]
    ON [dbo].[VacancySkills]([VacancyId] ASC);

