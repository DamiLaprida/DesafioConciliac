USE [Tareas]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 15/03/2024 0:49:30 ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tareas]    Script Date: 15/03/2024 0:49:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tareas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [nvarchar](max) NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Estado] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_Tareas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240313180458_PrimeraMigracion', N'6.0.28')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240314055837_CambioTipoDatoEstado', N'6.0.28')
GO
SET IDENTITY_INSERT [dbo].[Tareas] ON 

INSERT [dbo].[Tareas] ([Id], [Titulo], [Descripcion], [Estado]) VALUES (3, N'Hacer compras modificada', N'Comprar lecha,pan, y vino', N'Completada')
INSERT [dbo].[Tareas] ([Id], [Titulo], [Descripcion], [Estado]) VALUES (4, N'Hacer tarea matemática', N'Realizar actividades 1,2 y 3 de funciones lineales', N'Completada')
INSERT [dbo].[Tareas] ([Id], [Titulo], [Descripcion], [Estado]) VALUES (7, N'Ordenar la pieza', N'Hacer la cama, limpiar el piso y ordenar la ropa.', N'Pendiente')
SET IDENTITY_INSERT [dbo].[Tareas] OFF
GO
