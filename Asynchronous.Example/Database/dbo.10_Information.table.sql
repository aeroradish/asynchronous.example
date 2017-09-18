IF not EXISTS (
				SELECT	*
				FROM	INFORMATION_SCHEMA.COLUMNS 
				WHERE	TABLE_NAME = 'Information'
			   )
BEGIN

	CREATE TABLE [dbo].[Information](
		[InformationId] [int] IDENTITY(1,1) NOT NULL,
		[Description] [nvarchar](100) NULL,
		[Col1] [nvarchar](25) NULL,
		[Col2] [nvarchar](25) NULL,
		[Col3] [nvarchar](25) NULL,
		[Col4] [nvarchar](25) NULL,
		[Col5] [nvarchar](25) NULL,
		[Col6] [nvarchar](25) NULL,
		[Col7] [nvarchar](25) NULL,
		[Col8] [nvarchar](25) NULL,
		[SystemDateTime] DateTime NULL default(getdate())
	PRIMARY KEY CLUSTERED 
	(
		[InformationId] ASC
	)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
	) ON [PRIMARY]
END
