
IF OBJECT_ID('_ChangeLog') IS NOT NULL 
	DROP TABLE [dbo].[_ChangeLog]

IF OBJECT_ID('Asset') IS NOT NULL 
	DROP TABLE [dbo].[Asset]

IF OBJECT_ID('QueueDetail')	IS NOT NULL 
	DROP TABLE [dbo].[QueueDetail]

IF OBJECT_ID('QueueHeader') IS NOT NULL 
	DROP TABLE [dbo].[QueueHeader]

IF OBJECT_ID('QueueStatus')	IS NOT NULL 
	DROP TABLE [dbo].[QueueStatus]

IF OBJECT_ID('VendorUser') IS NOT NULL 
	DROP TABLE [dbo].[VendorUser]

IF OBJECT_ID('Person') IS NOT NULL 
	DROP TABLE [dbo].[Person]

IF OBJECT_ID('Vendor') IS NOT NULL 
	DROP TABLE [dbo].[Vendor]

IF OBJECT_ID('spTriggerGen_UpdateDelete') IS NOT NULL 
	DROP PROC [dbo].[spTriggerGen_UpdateDelete]

GO




CREATE PROC dbo.spTriggerGen_UpdateDelete (
	@tableName VARCHAR(100)
)
AS
IF NOT EXISTS(SELECT * FROM sys.tables WHERE name = @tableName)
BEGIN
	PRINT 'Table does not exist: ' + @tableName
	RETURN 0
END

SET @tableName = UPPER(SUBSTRING(@tableName, 1, 1)) + LOWER(SUBSTRING(@tableName, 2, 100))

DECLARE @tgName VARCHAR(100)
DECLARE @tgDef VARCHAR(MAX)
SET @tgName = 'tg' + @tableName + '_UpdateDelete';

IF OBJECT_ID(@tgName) IS NOT NULL
	EXEC('DROP TRIGGER ' + @tgName)

SET @tgDef = '
CREATE TRIGGER [dbo].[' + @tgName + '] ON [dbo].[' + @tableName + '] AFTER UPDATE, DELETE
AS

	DECLARE @id INT
	DECLARE @xml XML

	SELECT @id = id 
	FROM deleted

	SELECT @xml = (
		SELECT * 
		FROM deleted
		FOR XML AUTO
	)

	INSERT INTO dbo._ChangeLog (SourceTable, SourceRecordId, SourceRecord) 
	VALUES (''' + @tableName + ''', @id, @xml);
'

EXEC(@tgDef)

GO

--------------------------------------------------
--------------------------------------------------
--------------------------------------------------
--------------------------------------------------

CREATE TABLE [dbo].[_ChangeLog](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[SourceTable] [varchar](100) NOT NULL,
	[SourceRecordId] [int] NOT NULL,
	[SourceRecord] [xml] NOT NULL,
 CONSTRAINT [PK_ChangeLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[_ChangeLog] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO

CREATE TABLE [dbo].[Asset](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[ItemId] [int] NOT NULL,
	[ItemSource] [varchar](100) NOT NULL,
	[ItemDesc] [varchar](100) NOT NULL,
	[AssetName] [varchar](100) NOT NULL,
	[AssetType] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Asset] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Asset] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Asset] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO

CREATE NONCLUSTERED INDEX [IX_Asset] 
ON [dbo].[Asset] ([ItemId]) 
INCLUDE ([ItemSource], [ItemDesc], [AssetName], [AssetType]);
GO

EXEC dbo.spTriggerGen_UpdateDelete 'Asset'

--------------------------------------------------
--------------------------------------------------
--------------------------------------------------
--------------------------------------------------

CREATE TABLE [dbo].[Person](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[NameFirst] [nvarchar](100) NULL,
	[NameLast] [nvarchar](100) NULL,
	[TelSms] [nvarchar](100) NULL,
	[TelConfirmed] [bit] NOT NULL,
	[Email] [nvarchar](100) NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Person] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Person] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Person_TelSms] 
ON [dbo].[Person] ([TelSms]);
GO

INSERT [dbo].[Person] (NameFirst, NameLast, TelSms, TelConfirmed, Email) 
VALUES ('Joe', 'C', '+17148832837', 1, 'jsk.choi@gmail.com');

EXEC dbo.spTriggerGen_UpdateDelete 'Person'

--------------------------------------------------
--------------------------------------------------
--------------------------------------------------
--------------------------------------------------

CREATE TABLE [dbo].[Vendor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[VendorDesc] [nvarchar](100) NULL,
 CONSTRAINT [PK_Vendor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Vendor] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Vendor] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO

CREATE UNIQUE NONCLUSTERED INDEX [IX_Vendor] 
ON [dbo].[Vendor] ([VendorDesc]);
GO


INSERT [dbo].[Vendor] ([VendorDesc]) VALUES (N'esk8life')
GO

EXEC dbo.spTriggerGen_UpdateDelete 'Vendor'

--------------------------------------------------
--------------------------------------------------
--------------------------------------------------
--------------------------------------------------

CREATE TABLE [dbo].[VendorUser](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[VendorId] [int] NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_VendorUser] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[VendorUser] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[VendorUser] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[VendorUser]  WITH CHECK ADD  CONSTRAINT [FK_VendorUser_Vendor] FOREIGN KEY([VendorId])
REFERENCES [dbo].[Vendor] ([Id])
GO
ALTER TABLE [dbo].[VendorUser]  WITH CHECK ADD  CONSTRAINT [FK_VendorUser_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[_Users] ([Id])
GO

CREATE NONCLUSTERED INDEX [IX_VendorUser_VendorId]			ON [dbo].[VendorUser] ([VendorId]);
CREATE NONCLUSTERED INDEX [IX_VendorUser_UserId]			ON [dbo].[VendorUser] ([UserId]);
GO

INSERT [dbo].[VendorUser] ([VendorId], [UserId])
SELECT 1, Id FROM _Users
GO

EXEC dbo.spTriggerGen_UpdateDelete 'VendorUser'

--------------------------------------------------
--------------------------------------------------
--------------------------------------------------
--------------------------------------------------





--------------------------------------------------
--------------------------------------------------
--------------------------------------------------
--------------------------------------------------

CREATE TABLE [dbo].[QueueStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[QueueStatus] [nvarchar](100) NULL,
	[LongDesc] [nvarchar](300) NULL,
	[SortNo] [int] NULL,
 CONSTRAINT [PK_QueueStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[QueueStatus] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[QueueStatus] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO

INSERT [dbo].[QueueStatus] ([QueueStatus], [LongDesc], [SortNo]) VALUES 
	(N'In Line', N'Person is waiting in line', 10), 
	(N'Bumped', N'Person got to the front but did not appear', 20),
	(N'Done', N'Person is now out of line', 30),
	(N'Cancelled', N'Cancelled', 40)
GO

EXEC dbo.spTriggerGen_UpdateDelete 'QueueStatus'

--------------------------------------------------
--------------------------------------------------
--------------------------------------------------
--------------------------------------------------

CREATE TABLE [dbo].[QueueHeader](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[VendorId] [int] NOT NULL,
	[QueueName] [nvarchar](100) NULL,
	[QueueDesc] [nvarchar](500) NULL,
 CONSTRAINT [PK_QueueHeader] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[QueueHeader] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[QueueHeader] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO

INSERT [dbo].[QueueHeader] ([VendorId], [QueueName], [QueueDesc]) VALUES 
	(1, N'777pd Test Ride', N'Test ride the Dual 6374, 12s6p, 6shooter Pneumatic, Hummie cutout deck'), 
	(1, N'518d Test Ride', N'Test ride the Dual 6355, 12s4p, 97mm Abec11, Hummie cutout deck')
GO

EXEC dbo.spTriggerGen_UpdateDelete 'QueueHeader'

--------------------------------------------------
--------------------------------------------------
--------------------------------------------------
--------------------------------------------------

CREATE TABLE [dbo].[QueueDetail](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[QueueHeaderId] [int] NOT NULL,
	[QueueStatusId] [int] NOT NULL,
	[PersonId] [int] NOT NULL,
	[Sort] [decimal](8,2) NOT NULL,
 CONSTRAINT [PK_QueueDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[QueueDetail] ADD  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[QueueDetail] ADD  DEFAULT (getdate()) FOR [CreateDate]
GO

ALTER TABLE [dbo].[QueueDetail]  WITH CHECK ADD  CONSTRAINT [FK_QueueDetail_Person] FOREIGN KEY([PersonId])
REFERENCES [dbo].[Person] ([Id])
GO
ALTER TABLE [dbo].[QueueDetail]  WITH CHECK ADD  CONSTRAINT [FK_QueueDetail_QueueHeader] FOREIGN KEY([QueueHeaderId])
REFERENCES [dbo].[QueueHeader] ([Id])
GO
ALTER TABLE [dbo].[QueueDetail]  WITH CHECK ADD  CONSTRAINT [FK_QueueDetail_QueueStatus] FOREIGN KEY([QueueStatusId])
REFERENCES [dbo].[QueueStatus] ([Id])
GO

CREATE NONCLUSTERED INDEX [IX_QueueDetail_PersonId]			ON [dbo].[QueueDetail] ([PersonId]);
CREATE NONCLUSTERED INDEX [IX_QueueDetail_QueueHeaderId]	ON [dbo].[QueueDetail] ([QueueHeaderId]);
CREATE NONCLUSTERED INDEX [IX_QueueDetail_QueueStatusId]	ON [dbo].[QueueDetail] ([QueueStatusId]);
CREATE NONCLUSTERED INDEX [IX_QueueDetail_Sort]				ON [dbo].[QueueDetail] ([Sort]);
GO

EXEC dbo.spTriggerGen_UpdateDelete 'QueueDetail'

--------------------------------------------------
--------------------------------------------------
--------------------------------------------------
--------------------------------------------------

