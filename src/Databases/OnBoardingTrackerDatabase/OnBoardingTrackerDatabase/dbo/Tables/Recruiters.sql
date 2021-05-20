CREATE TABLE [dbo].[Recruiters] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (50) NOT NULL,
    [LastName]  NVARCHAR (50) NOT NULL,
    [Created]   DATETIME2 (7) DEFAULT ('0001-01-01T00:00:00.0000000') NOT NULL,
    [CreatedBy] INT           DEFAULT ((0)) NOT NULL,
    [IsDeleted] BIT           DEFAULT (CONVERT([bit],(0))) NOT NULL,
    [Updated]   DATETIME2 (7) NULL,
    [UpdatedBy] INT           NULL,
    CONSTRAINT [PK_Recruiters] PRIMARY KEY CLUSTERED ([Id] ASC)
);

