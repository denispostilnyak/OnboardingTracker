CREATE TABLE [dbo].[CandidateSkills] (
    [Id]          INT IDENTITY (1, 1) NOT NULL,
    [CandidateId] INT NOT NULL,
    [SkillId]     INT NOT NULL,
    CONSTRAINT [PK_CandidateSkills] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_CandidateSkills_Candidates] FOREIGN KEY ([CandidateId]) REFERENCES [dbo].[Candidates] ([Id]),
    CONSTRAINT [FK_CandidateSkills_Skills] FOREIGN KEY ([SkillId]) REFERENCES [dbo].[Skills] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_CandidateSkills_CandidateId]
    ON [dbo].[CandidateSkills]([CandidateId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_CandidateSkills_SkillId]
    ON [dbo].[CandidateSkills]([SkillId] ASC);

