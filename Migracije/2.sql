IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [UserName] nvarchar(max) NULL,
    [PasswordHash] varbinary(max) NULL,
    [PasswordSalt] varbinary(max) NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191124161201_InitialCreate', N'2.1.11-servicing-32099');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191125000445_AddRolesAndUserRoles', N'2.1.11-servicing-32099');

GO

CREATE TABLE [Roles] (
    [Id] int NOT NULL IDENTITY,
    [Role] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [UserRoles] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191125000700_AddRolesAndUserRolesv2', N'2.1.11-servicing-32099');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191127233212_patients', N'2.1.11-servicing-32099');

GO

CREATE TABLE [Patients] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [DateOfBirth] datetime2 NOT NULL,
    [BloodType] nvarchar(max) NULL,
    CONSTRAINT [PK_Patients] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191127233307_patientsV2', N'2.1.11-servicing-32099');

GO

ALTER TABLE [Patients] ADD [RoleId] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Patients] ADD [UserId] int NOT NULL DEFAULT 0;

GO

CREATE TABLE [Doctors] (
    [Id] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [Title] nvarchar(max) NULL,
    CONSTRAINT [PK_Doctors] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [DoctorsPatients] (
    [Id] int NOT NULL IDENTITY,
    [DoctorId] int NOT NULL,
    [PatientId] int NOT NULL,
    CONSTRAINT [PK_DoctorsPatients] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [PatientData] (
    [Id] int NOT NULL IDENTITY,
    [DoctorsPatientsId] int NOT NULL,
    [IllnessName] nvarchar(max) NULL,
    [IllnessStatus] nvarchar(max) NULL,
    [IllnessTherapy] nvarchar(max) NULL,
    [Medication] nvarchar(max) NULL,
    [AlergicToPennicilin] bit NOT NULL,
    [AlergicToPolen] bit NOT NULL,
    [HasAsthma] bit NOT NULL,
    [HasDiabetes] bit NOT NULL,
    CONSTRAINT [PK_PatientData] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20191128221958_DoctorsPatientsPatientData', N'2.1.11-servicing-32099');

GO

ALTER TABLE [Patients] ADD [Gender] nvarchar(max) NULL;

GO

ALTER TABLE [Patients] ADD [JMBG] nvarchar(max) NULL;

GO

ALTER TABLE [Patients] ADD [RHFaktor] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200113205017_PatientsGenderRHFaktorJMBG', N'2.1.11-servicing-32099');

GO

