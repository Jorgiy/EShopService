CREATE TABLE [PRODUCTS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [varchar](100) NOT NULL,
	[IMG_URI] [varchar](200) NOT NULL,
	[PRICE] [decimal](18, 2) NOT NULL,
	[DESCRIPTION] [text] NULL,
 CONSTRAINT [PK_PRODUCTS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY];

CREATE UNIQUE NONCLUSTERED INDEX [UNIQUE_NAME_IDX] ON [PRODUCTS]
(
	[NAME] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY];