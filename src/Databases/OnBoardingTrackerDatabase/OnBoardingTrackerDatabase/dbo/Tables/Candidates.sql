CREATE TABLE [dbo].[Candidates] (
    [Id]                    INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]             NVARCHAR (50)  NOT NULL,
    [LastName]              NVARCHAR (50)  NOT NULL,
    [PhoneNumber]           NVARCHAR (50)  NOT NULL,
    [Email]                 NVARCHAR (50)  NOT NULL,
    [OriginId]              INT            NOT NULL,
    [YearsOfExperience]     FLOAT (53)     NOT NULL,
    [CurrentJobInformation] NVARCHAR (250) NOT NULL,
    [Created]               DATETIME2 (7)  DEFAULT ('0001-01-01T00:00:00.0000000') NOT NULL,
    [CreatedBy]             INT            DEFAULT ((0)) NOT NULL,
    [IsDeleted]             BIT            DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Updated]               DATETIME2 (7)  NULL,
    [UpdatedBy]             INT            NULL,
    CONSTRAINT [PK_Candidates] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Candidates_CandidateOrigins] FOREIGN KEY ([OriginId]) REFERENCES [dbo].[CandidateOrigins] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_Candidates_OriginId]
    ON [dbo].[Candidates]([OriginId] ASC);

