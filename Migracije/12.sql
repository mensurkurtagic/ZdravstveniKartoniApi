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

ALTER TABLE [Patients] ADD [Address] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200126123604_addaddresstopatient', N'2.1.11-servicing-32099');

GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Patients]') AND [c].[name] = N'DateOfBirth');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Patients] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Patients] ALTER COLUMN [DateOfBirth] nvarchar(max) NULL;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200126195922_changeDateTimeToString', N'2.1.11-servicing-32099');

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200126200801_DiseaseRecords', N'2.1.11-servicing-32099');

GO

CREATE TABLE [DiseaseRecords] (
    [Id] int NOT NULL IDENTITY,
    [DateOfVisit] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Diagnose] nvarchar(max) NULL,
    [Therapy] nvarchar(max) NULL,
    [Order] nvarchar(max) NULL,
    [CanWork] nvarchar(max) NULL,
    CONSTRAINT [PK_DiseaseRecords] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200126200848_DiseaseRecords1', N'2.1.11-servicing-32099');

GO

ALTER TABLE [DiseaseRecords] ADD [PatientId] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200126200948_patientIdToDiseaseRecords', N'2.1.11-servicing-32099');

GO

EXEC sp_rename N'[DiseaseRecords].[PatientId]', N'UserId', N'COLUMN';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200126202128_patientIdtoUserId', N'2.1.11-servicing-32099');

GO

EXEC sp_rename N'[DiseaseRecords].[Order]', N'OrderToDoctor', N'COLUMN';

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200126203054_OrderToDoctor', N'2.1.11-servicing-32099');

GO

CREATE TABLE [Receipts] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [MedicineDescription] nvarchar(max) NULL,
    [Therapy] nvarchar(max) NULL,
    [UserId] int NOT NULL,
    [DoctorId] int NOT NULL,
    CONSTRAINT [PK_Receipts] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200126220043_receipt', N'2.1.11-servicing-32099');

GO

ALTER TABLE [Receipts] ADD [Status] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200127205155_StatusToReceipt', N'2.1.11-servicing-32099');

GO

CREATE TABLE [Appointments] (
    [Id] int NOT NULL IDENTITY,
    [DoctorsName] nvarchar(max) NULL,
    [DateOfAppointment] nvarchar(max) NULL,
    [ReasonForVisit] nvarchar(max) NULL,
    [Status] int NOT NULL,
    CONSTRAINT [PK_Appointments] PRIMARY KEY ([Id])
);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200127210332_Appointments', N'2.1.11-servicing-32099');

GO

ALTER TABLE [Appointments] ADD [DoctorId] int NOT NULL DEFAULT 0;

GO

ALTER TABLE [Appointments] ADD [UserId] int NOT NULL DEFAULT 0;

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200127210657_userIdDoctorIdAppointments', N'2.1.11-servicing-32099');

GO

