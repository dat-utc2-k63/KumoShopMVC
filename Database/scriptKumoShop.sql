USE [KumoShop]
GO
/****** Object:  Table [dbo].[ProductSize]    Script Date: 12/11/2024 9:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductSize](
	[ProductSizeId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[SizeId] [int] NULL,
 CONSTRAINT [PK_ProductSize] PRIMARY KEY CLUSTERED 
(
	[ProductSizeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductColor]    Script Date: 12/11/2024 9:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductColor](
	[ProductColorId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[ColorId] [int] NULL,
 CONSTRAINT [PK_ProductColor] PRIMARY KEY CLUSTERED 
(
	[ProductColorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 12/11/2024 9:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[NameProduct] [nvarchar](50) NULL,
	[DescProduct] [ntext] NULL,
	[Price] [float] NULL,
	[Brands] [nvarchar](50) NULL,
	[Gender] [bit] NULL,
	[Status] [bit] NULL,
	[CategoryId] [int] NULL,
	[CreateDate] [datetime] NULL,
	[IsNew] [bit] NULL,
	[IsHot] [bit] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Color]    Script Date: 12/11/2024 9:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Color](
	[ColorId] [int] IDENTITY(1,1) NOT NULL,
	[ColorName] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Color] PRIMARY KEY CLUSTERED 
(
	[ColorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Size]    Script Date: 12/11/2024 9:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Size](
	[SizeId] [int] IDENTITY(1,1) NOT NULL,
	[SizeNumber] [int] NOT NULL,
 CONSTRAINT [PK_Size] PRIMARY KEY CLUSTERED 
(
	[SizeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Image]    Script Date: 12/11/2024 9:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image](
	[ImageId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[ImageUrl] [nvarchar](50) NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[ProductDetailsView]    Script Date: 12/11/2024 9:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[ProductDetailsView]
AS
SELECT  p.ProductId, p.NameProduct, p.DescProduct, p.Price, p.Brands, p.Gender, c.ColorName, s.SizeNumber, p.Status, p.CreateDate, p.CategoryId, img.ImageUrl, p.IsNew, p.IsHot
FROM      dbo.Products AS p LEFT OUTER JOIN
                 dbo.ProductColor AS pc ON p.ProductId = pc.ProductId LEFT OUTER JOIN
                 dbo.Color AS c ON pc.ColorId = c.ColorId LEFT OUTER JOIN
                 dbo.ProductSize AS ps ON p.ProductId = ps.ProductId LEFT OUTER JOIN
                 dbo.Size AS s ON ps.SizeId = s.SizeId LEFT OUTER JOIN
                 dbo.Image AS img ON p.ProductId = img.ProductId
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 12/11/2024 9:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 12/11/2024 9:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[NameCategory] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contact]    Script Date: 12/11/2024 9:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contact](
	[ContactId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[Subject] [nvarchar](50) NULL,
	[DescContact] [ntext] NULL,
	[Status] [bit] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_Feedback] PRIMARY KEY CLUSTERED 
(
	[ContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Favourite]    Script Date: 12/11/2024 9:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Favourite](
	[FavouriteId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[DescFavourite] [ntext] NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK_Favourite] PRIMARY KEY CLUSTERED 
(
	[FavouriteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FavouriteItem]    Script Date: 12/11/2024 9:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FavouriteItem](
	[FavouriteItemId] [int] IDENTITY(1,1) NOT NULL,
	[FavouriteId] [int] NOT NULL,
	[ProductId] [int] NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_FavouriteItem] PRIMARY KEY CLUSTERED 
(
	[FavouriteItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 12/11/2024 9:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[OrderItemId] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NULL,
	[Color] [nvarchar](50) NULL,
	[Size] [int] NULL,
	[Image] [nvarchar](50) NULL,
	[Quantity] [int] NULL,
	[NameProduct] [nvarchar](50) NULL,
	[Price] [float] NULL,
	[IsRating] [bit] NULL,
 CONSTRAINT [PK_order_item] PRIMARY KEY CLUSTERED 
(
	[OrderItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 12/11/2024 9:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[OrderDate] [datetime] NULL,
	[DescOrder] [ntext] NULL,
	[StatusId] [int] NULL,
	[Phone] [nvarchar](50) NULL,
	[Fullname] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[PaymentMethode] [nvarchar](50) NULL,
 CONSTRAINT [PK_orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RatingImage]    Script Date: 12/11/2024 9:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RatingImage](
	[RatingImageId] [int] NOT NULL,
	[RatingId] [int] NOT NULL,
	[ImageUrl] [nvarchar](50) NULL,
 CONSTRAINT [PK_RatingImage] PRIMARY KEY CLUSTERED 
(
	[RatingImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RatingProduct]    Script Date: 12/11/2024 9:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RatingProduct](
	[RatingId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[ProductId] [int] NULL,
	[DescRating] [ntext] NULL,
	[RatePoint] [int] NULL,
	[CreateDate] [datetime] NULL,
	[Fullname] [nvarchar](50) NULL,
 CONSTRAINT [PK_Rating_product] PRIMARY KEY CLUSTERED 
(
	[RatingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 12/11/2024 9:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[NameRole] [nvarchar](50) NULL,
	[CreateDate] [datetime] NULL,
 CONSTRAINT [PK_roles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StatusShipping]    Script Date: 12/11/2024 9:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusShipping](
	[StatusId] [int] NOT NULL,
	[NameStatus] [nvarchar](50) NULL,
	[DescShipping] [nvarchar](50) NULL,
 CONSTRAINT [PK_StatusShipping] PRIMARY KEY CLUSTERED 
(
	[StatusId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/11/2024 9:02:19 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[Fullname] [nvarchar](50) NULL,
	[Avatar] [nvarchar](50) NULL,
	[Status] [bit] NULL,
	[RoleId] [int] NULL,
	[CreateDate] [datetime] NULL,
	[Phone] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[RandomKey] [nvarchar](50) NULL,
	[AboutUs] [nvarchar](50) NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241010122441_AddProductColorProductSizeTables', N'8.0.8')
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([CategoryId], [NameCategory], [CreateDate]) VALUES (1, N'Men Clothing', NULL)
INSERT [dbo].[Categories] ([CategoryId], [NameCategory], [CreateDate]) VALUES (2, N'Women Clothing', NULL)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Color] ON 

INSERT [dbo].[Color] ([ColorId], [ColorName]) VALUES (1, N'Red')
INSERT [dbo].[Color] ([ColorId], [ColorName]) VALUES (2, N'Blue')
INSERT [dbo].[Color] ([ColorId], [ColorName]) VALUES (3, N'Green')
INSERT [dbo].[Color] ([ColorId], [ColorName]) VALUES (4, N'Yellow')
INSERT [dbo].[Color] ([ColorId], [ColorName]) VALUES (5, N'Black')
INSERT [dbo].[Color] ([ColorId], [ColorName]) VALUES (6, N'White')
INSERT [dbo].[Color] ([ColorId], [ColorName]) VALUES (7, N'Orange')
INSERT [dbo].[Color] ([ColorId], [ColorName]) VALUES (8, N'Purple')
INSERT [dbo].[Color] ([ColorId], [ColorName]) VALUES (9, N'Pink')
INSERT [dbo].[Color] ([ColorId], [ColorName]) VALUES (10, N'Gray')
SET IDENTITY_INSERT [dbo].[Color] OFF
GO
SET IDENTITY_INSERT [dbo].[Contact] ON 

INSERT [dbo].[Contact] ([ContactId], [Name], [Email], [Subject], [DescContact], [Status], [CreateDate]) VALUES (1, NULL, NULL, NULL, NULL, 0, CAST(N'2024-12-11T10:42:52.540' AS DateTime))
SET IDENTITY_INSERT [dbo].[Contact] OFF
GO
SET IDENTITY_INSERT [dbo].[Image] ON 

INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (1, 1, N'menTshirt.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (2, 2, N'womenDress.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (3, 3, N'menJean.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (4, 4, N'sportsShorts.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (5, 5, N'Skirt.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (6, 6, N'MenShoes.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (7, 7, N'womenJean.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (8, 8, N'womenTrouser.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (9, 9, N'womenshort.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (10, 10, N'quanthunnu.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (11, 11, N'MenPoloShirt.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (12, 12, N'WomenSkirt.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (13, 13, N'giaynu.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (14, 14, N'SportsTights.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (15, 15, N'thunnam.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (16, 16, N'menShoes1.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (17, 17, N'womenJacket.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (18, 18, N'sominam.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (19, 19, N'RunningShoes.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (20, 20, N'sominu.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (21, 1, N'menTshirt1.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (22, 2, N'womenDress1.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (23, 3, N'menJean1.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (24, 4, N'sportsShorts1.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (25, 5, N'Skirt1.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (27, 7, N'womenJean1.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (28, 8, N'womenTrouser1.jpg')
INSERT [dbo].[Image] ([ImageId], [ProductId], [ImageUrl]) VALUES (35, 15, N'thunnam1.jpg')
SET IDENTITY_INSERT [dbo].[Image] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderItem] ON 

INSERT [dbo].[OrderItem] ([OrderItemId], [OrderId], [ProductId], [Color], [Size], [Image], [Quantity], [NameProduct], [Price], [IsRating]) VALUES (2, 2, 2, N'Pink', 36, N'womenDress.jpg', 2, N'Women Dress', 12.300000190734863, 0)
INSERT [dbo].[OrderItem] ([OrderItemId], [OrderId], [ProductId], [Color], [Size], [Image], [Quantity], [NameProduct], [Price], [IsRating]) VALUES (3, 3, 2, N'Pink', 34, N'womenDress.jpg', 1, N'Women Dress', 12.300000190734863, 1)
INSERT [dbo].[OrderItem] ([OrderItemId], [OrderId], [ProductId], [Color], [Size], [Image], [Quantity], [NameProduct], [Price], [IsRating]) VALUES (4, 4, 2, N'Pink', 35, N'womenDress.jpg', 1, N'Women Dress', 12.300000190734863, 1)
INSERT [dbo].[OrderItem] ([OrderItemId], [OrderId], [ProductId], [Color], [Size], [Image], [Quantity], [NameProduct], [Price], [IsRating]) VALUES (5, 5, 2, N'Pink', 33, N'womenDress.jpg', 3, N'Women Dress', 12.399999618530273, 1)
INSERT [dbo].[OrderItem] ([OrderItemId], [OrderId], [ProductId], [Color], [Size], [Image], [Quantity], [NameProduct], [Price], [IsRating]) VALUES (6, 6, 3, N'FFFF00', 34, N'womenDress.jpg', 1, N'Men Jean', 12.300000190734863, 1)
INSERT [dbo].[OrderItem] ([OrderItemId], [OrderId], [ProductId], [Color], [Size], [Image], [Quantity], [NameProduct], [Price], [IsRating]) VALUES (7, 7, 2, N'FFFF00', 35, N'womenDress.jpg', 3, N'Women Dress', 12.300000190734863, 0)
INSERT [dbo].[OrderItem] ([OrderItemId], [OrderId], [ProductId], [Color], [Size], [Image], [Quantity], [NameProduct], [Price], [IsRating]) VALUES (8, 8, 2, N'Pink', 34, N'womenDress.jpg', 2, N'Women Dress', 12.300000190734863, 0)
INSERT [dbo].[OrderItem] ([OrderItemId], [OrderId], [ProductId], [Color], [Size], [Image], [Quantity], [NameProduct], [Price], [IsRating]) VALUES (9, 9, 2, N'FFFF00', 34, N'womenDress.jpg', 1, N'Women Dress', 12.300000190734863, 0)
INSERT [dbo].[OrderItem] ([OrderItemId], [OrderId], [ProductId], [Color], [Size], [Image], [Quantity], [NameProduct], [Price], [IsRating]) VALUES (10, 10, 2, N'FFFF00', 30, N'womenDress.jpg', 4, N'Women Dress', 12.300000190734863, 0)
INSERT [dbo].[OrderItem] ([OrderItemId], [OrderId], [ProductId], [Color], [Size], [Image], [Quantity], [NameProduct], [Price], [IsRating]) VALUES (11, 11, 3, N'Pink', 33, N'menJean.jpg', 3, N'Men Jean', 12.399999618530273, 0)
INSERT [dbo].[OrderItem] ([OrderItemId], [OrderId], [ProductId], [Color], [Size], [Image], [Quantity], [NameProduct], [Price], [IsRating]) VALUES (12, 12, 3, N'FFFF00', 33, N'menJean.jpg', 2, N'Men Jean', 12.399999618530273, 0)
SET IDENTITY_INSERT [dbo].[OrderItem] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 

INSERT [dbo].[Orders] ([OrderId], [UserId], [OrderDate], [DescOrder], [StatusId], [Phone], [Fullname], [Address], [PaymentMethode]) VALUES (2, 2, CAST(N'2024-11-26T13:59:59.167' AS DateTime), NULL, 1, N'151673', N'Nguyễn Văn A', N'1289 Lê Văn Việt', N'COD')
INSERT [dbo].[Orders] ([OrderId], [UserId], [OrderDate], [DescOrder], [StatusId], [Phone], [Fullname], [Address], [PaymentMethode]) VALUES (3, 2, CAST(N'2024-11-26T14:02:19.913' AS DateTime), NULL, 1, N'151673', N'Nguyễn Văn B', N'98 Võ Văn Ngân', N'COD')
INSERT [dbo].[Orders] ([OrderId], [UserId], [OrderDate], [DescOrder], [StatusId], [Phone], [Fullname], [Address], [PaymentMethode]) VALUES (4, 2, CAST(N'2024-11-26T18:20:39.853' AS DateTime), NULL, 2, N'151673', N'Nguyễn Thị C', N'83 Lý Thường Kiệt', N'COD')
INSERT [dbo].[Orders] ([OrderId], [UserId], [OrderDate], [DescOrder], [StatusId], [Phone], [Fullname], [Address], [PaymentMethode]) VALUES (5, 2, CAST(N'2024-11-26T18:22:30.640' AS DateTime), NULL, 2, N'151673', N'Nguyễn Thị C', N'82 Võ Văn Dũng', N'COD')
INSERT [dbo].[Orders] ([OrderId], [UserId], [OrderDate], [DescOrder], [StatusId], [Phone], [Fullname], [Address], [PaymentMethode]) VALUES (6, 2, CAST(N'2024-11-27T08:59:51.047' AS DateTime), NULL, 1, N'151673', N'Nguyễn Văn Minh', N'13 Nguyễn Huệ', N'COD')
INSERT [dbo].[Orders] ([OrderId], [UserId], [OrderDate], [DescOrder], [StatusId], [Phone], [Fullname], [Address], [PaymentMethode]) VALUES (7, 2, CAST(N'2024-11-27T09:00:14.740' AS DateTime), NULL, 3, N'151673', N'Nguyễn Văn A', N'82 Võ Văn Dũng', N'COD')
INSERT [dbo].[Orders] ([OrderId], [UserId], [OrderDate], [DescOrder], [StatusId], [Phone], [Fullname], [Address], [PaymentMethode]) VALUES (8, 2, CAST(N'2024-11-27T09:03:11.640' AS DateTime), NULL, 3, N'151673', N'Nguyễn Văn B', N'98 Võ Văn Ngân', N'COD')
INSERT [dbo].[Orders] ([OrderId], [UserId], [OrderDate], [DescOrder], [StatusId], [Phone], [Fullname], [Address], [PaymentMethode]) VALUES (9, 2, CAST(N'2024-12-06T14:20:02.417' AS DateTime), N'Dễ vỡ!', 1, N'151673', N'Nguyễn Thị Ái', N'98 Võ Văn Ngân', N'COD')
INSERT [dbo].[Orders] ([OrderId], [UserId], [OrderDate], [DescOrder], [StatusId], [Phone], [Fullname], [Address], [PaymentMethode]) VALUES (10, 2, CAST(N'2024-12-06T14:33:40.720' AS DateTime), N'Cute', 1, N'0915787193', N'Nguyễn Văn Minh', N'82 Võ Văn Dũng', N'VnPay')
INSERT [dbo].[Orders] ([OrderId], [UserId], [OrderDate], [DescOrder], [StatusId], [Phone], [Fullname], [Address], [PaymentMethode]) VALUES (11, 3, CAST(N'2024-12-06T14:38:00.530' AS DateTime), N'Cẩn thận', 3, N'0915787193', N'Nguyễn Thị Ái', N'82 Võ Văn Dũng', N'VnPay')
INSERT [dbo].[Orders] ([OrderId], [UserId], [OrderDate], [DescOrder], [StatusId], [Phone], [Fullname], [Address], [PaymentMethode]) VALUES (12, 3, CAST(N'2024-12-06T14:40:57.707' AS DateTime), N'edf', 1, N'0915787193', N'Nguyễn Thị Ái', N'98 Võ Văn Ngân', N'VnPay')
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductColor] ON 

INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (1, 1, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (2, 2, 2)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (3, 3, 3)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (4, 4, 4)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (5, 5, 5)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (6, 6, 7)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (7, 7, 6)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (8, 8, 7)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (9, 9, 8)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (10, 10, 9)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (11, 11, 10)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (12, 12, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (13, 13, 2)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (14, 14, 3)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (15, 15, 4)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (16, 16, 5)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (17, 17, 6)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (18, 18, 7)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (19, 19, 8)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (20, 20, 9)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (21, 1, 2)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (22, 2, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (23, 3, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (24, 4, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (25, 5, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (26, 6, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (27, 7, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (28, 8, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (29, 9, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (30, 10, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (31, 11, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (32, 12, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (33, 13, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (34, 14, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (35, 15, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (36, 16, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (37, 17, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (38, 18, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (39, 19, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (40, 20, 1)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (41, 20, 9)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (42, 1, 3)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (43, 3, 4)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (44, 4, 4)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (45, 5, 4)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (46, 6, 4)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (47, 7, 4)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (48, 8, 4)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (49, 9, 4)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (50, 10, 4)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (51, 11, 4)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (52, 12, 4)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (53, 13, 4)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (54, 14, 4)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (55, 15, 4)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (56, 16, 4)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (57, 17, 4)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (58, 18, 4)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (59, 19, 4)
INSERT [dbo].[ProductColor] ([ProductColorId], [ProductId], [ColorId]) VALUES (60, 20, 4)
SET IDENTITY_INSERT [dbo].[ProductColor] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (1, N'Men T-Shirt', N'Comfortable men''s t-shirt.', 100, N'Brand1', 1, 1, 1, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (2, N'Women Dress', N'Elegant women''s dress.', 200, N'Brand2', 0, 1, 2, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 0, 1)
INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (3, N'Men Jean', N'jean for men', 150, N'Brand3', 1, 1, 1, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (4, N'Sports Shorts', N'Lightweight sports shorts.', 250, N'Brand4', 0, 1, 1, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 0, 1)
INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (5, N'Women''s skirt', N'skirt for women', 300, N'Brand5', 1, 1, 2, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 1, 1)
INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (6, N'Men Shoes', N'Stylish men''s shoes.', 350, N'Brand6', 0, 1, 1, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 0, 0)
INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (7, N'Women Jean', N'Trendy women''s bag.', 400, N'Brand7', 1, 1, 2, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (8, N'Women''s trousers', N'Trouserl for women.', 450, N'Brand8', 0, 1, 2, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 0, 1)
INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (9, N'women short', N'Breathable sports cap.', 500, N'Brand9', 1, 1, 2, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (10, N'Women shirt', N'UV-protected sunglasses.', 550, N'Brand10', 0, 1, 2, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 0, 1)
INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (11, N'Men Polo Shirt', N'Classic men''s polo shirt.', 120, N'Brand11', 1, 1, 1, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (12, N'Women Skirt', N'Floral women''s skirt.', 180, N'Brand12', 0, 1, 2, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 0, 1)
INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (13, N'Women Shoes', N'Shoes for women', 160, N'Brand13', 1, 1, 2, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (14, N'Sports Tights', N'Flexible sports tights.', 260, N'Brand14', 0, 1, 2, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 0, 1)
INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (15, N'men elastic', N'men''s elastic for men', 310, N'Brand15', 1, 1, 1, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 1, 1)
INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (16, N'Men Shoes', N'Stylish men''s Shoes.', 360, N'Brand16', 0, 1, 1, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 0, 0)
INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (17, N'Women Jacket', N'Elegant women''s jacket.', 410, N'Brand17', 1, 1, 2, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (18, N'
men''s shirt', N'Stylish men shirt', 460, N'Brand18', 0, 1, 1, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 0, 1)
INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (19, N'Running Shoes', N'Lightweight running shoes.', 510, N'Brand19', 1, 1, 2, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 1, 0)
INSERT [dbo].[Products] ([ProductId], [NameProduct], [DescProduct], [Price], [Brands], [Gender], [Status], [CategoryId], [CreateDate], [IsNew], [IsHot]) VALUES (20, N'Women', N'Luxury wristwatch.', 560, N'Brand20', 0, 1, 2, CAST(N'2024-12-11T09:30:26.603' AS DateTime), 0, 1)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductSize] ON 

INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (1, 1, 1)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (2, 2, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (3, 3, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (4, 4, 4)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (5, 5, 5)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (6, 6, 7)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (7, 7, 6)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (8, 8, 7)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (9, 9, 8)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (10, 10, 9)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (11, 11, 10)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (12, 12, 11)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (13, 13, 12)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (14, 14, 13)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (15, 15, 14)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (16, 16, 15)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (17, 17, 1)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (18, 18, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (19, 19, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (20, 20, 4)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (21, 1, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (22, 2, 1)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (23, 3, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (24, 4, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (25, 5, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (26, 6, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (27, 7, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (28, 8, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (29, 9, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (30, 10, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (31, 11, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (32, 12, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (33, 13, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (34, 14, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (35, 15, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (36, 16, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (37, 17, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (38, 18, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (39, 19, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (40, 20, 2)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (41, 1, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (42, 2, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (43, 3, 1)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (44, 4, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (45, 5, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (46, 6, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (47, 7, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (48, 8, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (49, 9, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (50, 10, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (51, 11, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (52, 12, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (53, 13, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (54, 14, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (55, 15, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (56, 16, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (57, 17, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (58, 18, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (59, 19, 3)
INSERT [dbo].[ProductSize] ([ProductSizeId], [ProductId], [SizeId]) VALUES (60, 20, 3)
SET IDENTITY_INSERT [dbo].[ProductSize] OFF
GO
SET IDENTITY_INSERT [dbo].[RatingProduct] ON 

INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (1, NULL, 2, N'trrtrt', 4, CAST(N'2024-10-24T13:48:11.740' AS DateTime), N'15')
INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (2, 2, 2, N'datqw', 5, CAST(N'2024-11-26T10:01:09.283' AS DateTime), N'1rdyhfg136')
INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (3, 2, 2, N'211ewsf', 5, CAST(N'2024-11-26T14:06:40.563' AS DateTime), N'da')
INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (4, 2, 2, N'add', 4, CAST(N'2024-11-26T14:09:15.980' AS DateTime), N'124')
INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (5, 2, 3, N'dadd', 5, CAST(N'2024-11-26T18:22:49.930' AS DateTime), N'1rdyhfg136')
INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (6, 2, 3, N'gdgg', 5, CAST(N'2024-11-26T18:24:12.070' AS DateTime), N'12')
INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (7, 2, 2, N'12', 3, CAST(N'2024-11-26T18:51:12.360' AS DateTime), N'412')
INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (8, 2, 2, N'12', 3, CAST(N'2024-11-26T18:51:38.840' AS DateTime), N'412')
INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (9, 2, 2, N'12', 3, CAST(N'2024-11-26T18:52:08.423' AS DateTime), N'412')
INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (10, 3, 2, N'21safg', 3, CAST(N'2024-11-26T19:21:01.277' AS DateTime), N'214')
INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (11, 3, 2, N'124551', 3, CAST(N'2024-11-26T19:27:01.813' AS DateTime), N'124')
INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (12, 3, 2, N'124551', 3, CAST(N'2024-11-26T19:27:29.357' AS DateTime), N'124')
INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (13, 3, 2, N'1516', 3, CAST(N'2024-11-26T19:29:03.670' AS DateTime), N'ewt')
INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (14, 3, 2, N'124gg', 3, CAST(N'2024-11-26T19:30:42.820' AS DateTime), N'124')
INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (15, 3, 2, N'1jifewg', 3, CAST(N'2024-11-26T19:33:10.130' AS DateTime), N'agg')
INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (16, 3, 2, N'1jifewg', 3, CAST(N'2024-11-26T19:33:27.897' AS DateTime), N'agg')
INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (17, 3, 2, N'1egery', 4, CAST(N'2024-11-26T19:37:32.577' AS DateTime), N'2rg')
INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (18, 3, 2, N'1egery', 4, CAST(N'2024-11-26T19:39:45.263' AS DateTime), N'2rg')
INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (19, 3, 2, N'à', 2, CAST(N'2024-11-26T19:43:08.257' AS DateTime), N'gae')
INSERT [dbo].[RatingProduct] ([RatingId], [UserId], [ProductId], [DescRating], [RatePoint], [CreateDate], [Fullname]) VALUES (20, 3, 2, N'21gdg', 3, CAST(N'2024-11-26T19:44:39.923' AS DateTime), N'124')
SET IDENTITY_INSERT [dbo].[RatingProduct] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([RoleId], [NameRole], [CreateDate]) VALUES (1, N'Customer', NULL)
INSERT [dbo].[Roles] ([RoleId], [NameRole], [CreateDate]) VALUES (2, N'Admin', NULL)
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Size] ON 

INSERT [dbo].[Size] ([SizeId], [SizeNumber]) VALUES (1, 30)
INSERT [dbo].[Size] ([SizeId], [SizeNumber]) VALUES (2, 31)
INSERT [dbo].[Size] ([SizeId], [SizeNumber]) VALUES (3, 32)
INSERT [dbo].[Size] ([SizeId], [SizeNumber]) VALUES (4, 33)
INSERT [dbo].[Size] ([SizeId], [SizeNumber]) VALUES (5, 34)
INSERT [dbo].[Size] ([SizeId], [SizeNumber]) VALUES (6, 35)
INSERT [dbo].[Size] ([SizeId], [SizeNumber]) VALUES (7, 36)
INSERT [dbo].[Size] ([SizeId], [SizeNumber]) VALUES (8, 37)
INSERT [dbo].[Size] ([SizeId], [SizeNumber]) VALUES (9, 38)
INSERT [dbo].[Size] ([SizeId], [SizeNumber]) VALUES (10, 39)
INSERT [dbo].[Size] ([SizeId], [SizeNumber]) VALUES (11, 40)
INSERT [dbo].[Size] ([SizeId], [SizeNumber]) VALUES (12, 38)
INSERT [dbo].[Size] ([SizeId], [SizeNumber]) VALUES (13, 40)
INSERT [dbo].[Size] ([SizeId], [SizeNumber]) VALUES (14, 42)
INSERT [dbo].[Size] ([SizeId], [SizeNumber]) VALUES (15, 44)
SET IDENTITY_INSERT [dbo].[Size] OFF
GO
INSERT [dbo].[StatusShipping] ([StatusId], [NameStatus], [DescShipping]) VALUES (1, N'Cancel', NULL)
INSERT [dbo].[StatusShipping] ([StatusId], [NameStatus], [DescShipping]) VALUES (2, N'Preparing', NULL)
INSERT [dbo].[StatusShipping] ([StatusId], [NameStatus], [DescShipping]) VALUES (3, N'Payment', NULL)
INSERT [dbo].[StatusShipping] ([StatusId], [NameStatus], [DescShipping]) VALUES (4, N'Wait Shipping', NULL)
INSERT [dbo].[StatusShipping] ([StatusId], [NameStatus], [DescShipping]) VALUES (5, N'Complete', NULL)
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Email], [Password], [Fullname], [Avatar], [Status], [RoleId], [CreateDate], [Phone], [Address], [RandomKey], [AboutUs]) VALUES (1, N'thanhdat41044@gmail.com', N'7d9b18954615220753faa95fe179ae1d', N'Nguyen Thanh Dat', N'863afa8f-56a7-49f0-a00e-8207e4eae620.jpg', 1, 2, NULL, N'151673', N'Thu Duc city', N'aSoKd', N'gfhh')
INSERT [dbo].[Users] ([UserId], [Email], [Password], [Fullname], [Avatar], [Status], [RoleId], [CreateDate], [Phone], [Address], [RandomKey], [AboutUs]) VALUES (2, N'thanhnhan4104@gmail.com', N'f414408170530634f603194b5d71fa0b', N'Pham Thanh Nhan', N'f6412d68-6963-402d-b117-eed76b01abc7.png', 1, 1, NULL, N'152613', N'Quang Ngai city', N'xKvxE', N'u65t44')
INSERT [dbo].[Users] ([UserId], [Email], [Password], [Fullname], [Avatar], [Status], [RoleId], [CreateDate], [Phone], [Address], [RandomKey], [AboutUs]) VALUES (3, N'thanhphuc410445313@gmail.com', N'abcdef', N'Pham Thanh Phuc', N'29386965-68a3-47f0-b9da-4ebdbe9fbf88.png', 1, 1, CAST(N'2024-12-06T19:31:40.473' AS DateTime), N'0192566136', N'Khanh Hoa', NULL, NULL)
INSERT [dbo].[Users] ([UserId], [Email], [Password], [Fullname], [Avatar], [Status], [RoleId], [CreateDate], [Phone], [Address], [RandomKey], [AboutUs]) VALUES (7, N'nhanpham.04122004@gmail.com', N'1e7762fee1acfcce3895487e5fe3e1f2', N'Nhan', N'bann2.jpg', 1, 1, CAST(N'2024-12-11T20:02:18.630' AS DateTime), N'123-345-6789', N'101 Guest Lane HCM', N'DjizI', N'hello')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
ALTER TABLE [dbo].[Favourite]  WITH CHECK ADD  CONSTRAINT [FK_Favourite_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Favourite] CHECK CONSTRAINT [FK_Favourite_Users]
GO
ALTER TABLE [dbo].[FavouriteItem]  WITH CHECK ADD  CONSTRAINT [FK_FavouriteItem_Favourite] FOREIGN KEY([FavouriteId])
REFERENCES [dbo].[Favourite] ([FavouriteId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FavouriteItem] CHECK CONSTRAINT [FK_FavouriteItem_Favourite]
GO
ALTER TABLE [dbo].[FavouriteItem]  WITH CHECK ADD  CONSTRAINT [FK_FavouriteItem_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[FavouriteItem] CHECK CONSTRAINT [FK_FavouriteItem_Products]
GO
ALTER TABLE [dbo].[Image]  WITH CHECK ADD  CONSTRAINT [FK_Images_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Image] CHECK CONSTRAINT [FK_Images_Product]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Orders] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([OrderId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Orders]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Products]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_StatusShipping] FOREIGN KEY([StatusId])
REFERENCES [dbo].[StatusShipping] ([StatusId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_StatusShipping]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Users]
GO
ALTER TABLE [dbo].[ProductColor]  WITH CHECK ADD  CONSTRAINT [FK_ProductColor_Color] FOREIGN KEY([ColorId])
REFERENCES [dbo].[Color] ([ColorId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[ProductColor] CHECK CONSTRAINT [FK_ProductColor_Color]
GO
ALTER TABLE [dbo].[ProductColor]  WITH CHECK ADD  CONSTRAINT [FK_ProductColor_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductColor] CHECK CONSTRAINT [FK_ProductColor_Products]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Categories] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Categories] ([CategoryId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Categories]
GO
ALTER TABLE [dbo].[ProductSize]  WITH CHECK ADD  CONSTRAINT [FK_ProductSize_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[ProductSize] CHECK CONSTRAINT [FK_ProductSize_Products]
GO
ALTER TABLE [dbo].[ProductSize]  WITH CHECK ADD  CONSTRAINT [FK_ProductSize_Size] FOREIGN KEY([SizeId])
REFERENCES [dbo].[Size] ([SizeId])
GO
ALTER TABLE [dbo].[ProductSize] CHECK CONSTRAINT [FK_ProductSize_Size]
GO
ALTER TABLE [dbo].[RatingImage]  WITH CHECK ADD  CONSTRAINT [FK_RatingImage_RatingProduct] FOREIGN KEY([RatingId])
REFERENCES [dbo].[RatingProduct] ([RatingId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RatingImage] CHECK CONSTRAINT [FK_RatingImage_RatingProduct]
GO
ALTER TABLE [dbo].[RatingProduct]  WITH CHECK ADD  CONSTRAINT [FK_RatingProduct_Products] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[RatingProduct] CHECK CONSTRAINT [FK_RatingProduct_Products]
GO
ALTER TABLE [dbo].[RatingProduct]  WITH CHECK ADD  CONSTRAINT [FK_RatingProduct_Users] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[RatingProduct] CHECK CONSTRAINT [FK_RatingProduct_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Roles] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Roles] ([RoleId])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Roles]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = -240
         Left = -15
      End
      Begin Tables = 
         Begin Table = "p"
            Begin Extent = 
               Top = 7
               Left = 48
               Bottom = 170
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 6
         End
         Begin Table = "pc"
            Begin Extent = 
               Top = 175
               Left = 48
               Bottom = 294
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 294
               Left = 48
               Bottom = 413
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ps"
            Begin Extent = 
               Top = 413
               Left = 48
               Bottom = 532
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "s"
            Begin Extent = 
               Top = 532
               Left = 48
               Bottom = 651
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "img"
            Begin Extent = 
               Top = 651
               Left = 48
               Bottom = 792
               Right = 242
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1180' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductDetailsView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1360
         SortOrder = 1420
         GroupBy = 1350
         Filter = 1360
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductDetailsView'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'ProductDetailsView'
GO
