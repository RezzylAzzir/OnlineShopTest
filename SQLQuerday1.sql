CREATE TABLE PigeonsInOrders (
	[OrderId] INT Identity NOT NULL,
	[PigeonId] INT NULL,
	[Quantity] INT NULL,
	CONSTRAINT [PK_dbo.PigeonsInOrders] PRIMARY KEY CLUSTERED ([OrderId] ASC),
);