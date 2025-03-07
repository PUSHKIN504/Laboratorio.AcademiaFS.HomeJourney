USE [HomeJourney]
GO
/****** Object:  Table [dbo].[Cargos]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cargos](
	[Cargo_id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](25) NULL,
 CONSTRAINT [PK_Cargos_Cargo_id] PRIMARY KEY CLUSTERED 
(
	[Cargo_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ciudades]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ciudades](
	[Ciudad_id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](75) NOT NULL,
	[Departamento_id] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Ciudades_Ciudad_id] PRIMARY KEY CLUSTERED 
(
	[Ciudad_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colaboradores]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colaboradores](
	[Colaborador_id] [int] IDENTITY(1,1) NOT NULL,
	[Persona_id] [int] NOT NULL,
	[Rol_id] [int] NOT NULL,
	[Cargo_id] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[Direccion] [varchar](150) NOT NULL,
	[Usuariocrea] [int] NOT NULL,
	[Fechacrea] [datetime] NOT NULL,
	[Usuariomodifica] [int] NULL,
	[Fechamodifica] [datetime] NULL,
	[Latitud] [decimal](19, 15) NULL,
	[Longitud] [decimal](19, 15) NULL,
 CONSTRAINT [PK_Colaboradores_Colaborador_id] PRIMARY KEY CLUSTERED 
(
	[Colaborador_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Colaboradoressucursales]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Colaboradoressucursales](
	[Colaboradorsucursal_id] [int] IDENTITY(1,1) NOT NULL,
	[Colaborador_id] [int] NOT NULL,
	[Sucursal_id] [int] NOT NULL,
	[Distanciakilometro] [decimal](5, 2) NOT NULL,
	[Activo] [bit] NOT NULL,
	[Usuariocrea] [int] NOT NULL,
	[Fechacrea] [datetime] NOT NULL,
	[Usuariomodifica] [int] NULL,
	[Fechamodifica] [datetime] NULL,
 CONSTRAINT [PK_Colaboradoressucursales_Colaboradorsucursal_id] PRIMARY KEY CLUSTERED 
(
	[Colaboradorsucursal_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departamentos]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departamentos](
	[Departamento_id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](75) NOT NULL,
	[Activo] [bit] NOT NULL,
	[Pais_Id] [int] NULL,
 CONSTRAINT [PK_Departamentos_Departamento_id] PRIMARY KEY CLUSTERED 
(
	[Departamento_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estados]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estados](
	[Estado_id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](255) NOT NULL,
 CONSTRAINT [PK_Estados_Estado_id] PRIMARY KEY CLUSTERED 
(
	[Estado_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estadosciviles]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estadosciviles](
	[Estadocivil_id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](45) NOT NULL,
 CONSTRAINT [PK_Estadosciviles_Estadocivil_id] PRIMARY KEY CLUSTERED 
(
	[Estadocivil_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Monedas]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Monedas](
	[Moneda_Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](25) NULL,
	[Simbolo] [nvarchar](1) NULL,
	[ValorLempiras] [smallmoney] NULL,
 CONSTRAINT [PK_Monedas_Moneda_Id] PRIMARY KEY CLUSTERED 
(
	[Moneda_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Paises]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Paises](
	[Pais_id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NULL,
	[Activo] [bit] NULL,
 CONSTRAINT [PK_Paises_Pais_id] PRIMARY KEY CLUSTERED 
(
	[Pais_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pantallas]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pantallas](
	[Pantalla_id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Usuariocrea] [int] NOT NULL,
	[Fechacrea] [datetime] NOT NULL,
	[Usuariomodifica] [int] NULL,
	[Fechamodifica] [datetime] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Pantallas_Pantalla_id] PRIMARY KEY CLUSTERED 
(
	[Pantalla_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pantallasroles]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pantallasroles](
	[Pantallarol_id] [int] IDENTITY(1,1) NOT NULL,
	[Rol_id] [int] NOT NULL,
	[Pantalla_id] [int] NOT NULL,
	[Fechacrea] [datetime] NOT NULL,
 CONSTRAINT [PK_Pantallasroles_Pantallarol_id] PRIMARY KEY CLUSTERED 
(
	[Pantallarol_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Personas]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Personas](
	[Persona_id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Apelllido] [varchar](100) NOT NULL,
	[Sexo] [varchar](1) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Documentonacionalidentificacion] [varchar](15) NOT NULL,
	[Activo] [bit] NOT NULL,
	[Estadocivil_id] [int] NULL,
	[Ciudad_id] [int] NOT NULL,
	[Usuariocrea] [int] NOT NULL,
	[Fechacrea] [datetime] NOT NULL,
	[Usuariomodifica] [int] NULL,
	[Fechamodifica] [datetime] NULL,
 CONSTRAINT [PK_Personas_Persona_id] PRIMARY KEY CLUSTERED 
(
	[Persona_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[Rol_id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Usuariocrea] [int] NOT NULL,
	[Fechacrea] [datetime] NOT NULL,
	[Usuariomodifica] [int] NULL,
	[Fechamodifica] [datetime] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Roles_Rol_id] PRIMARY KEY CLUSTERED 
(
	[Rol_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Serviciostransporte]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Serviciostransporte](
	[Serviciotransporte_id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](150) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[Usuariocrea] [int] NOT NULL,
	[Fechacrea] [datetime] NOT NULL,
	[Usuariomodifica] [int] NULL,
	[Fechamodifica] [datetime] NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Serviciostrasnporte_Serviciotransporte_id] PRIMARY KEY CLUSTERED 
(
	[Serviciotransporte_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Solicitudesviajes]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Solicitudesviajes](
	[Solicitudviaje_id] [int] IDENTITY(1,1) NOT NULL,
	[Colaborador_id] [int] NOT NULL,
	[Fechasolicitud] [date] NOT NULL,
	[Viaje_id] [int] NOT NULL,
	[Estado_id] [int] NOT NULL,
	[Comentarios] [varchar](150) NULL,
	[Activo] [bit] NOT NULL,
	[Usuariocrea] [int] NOT NULL,
	[Fechacrea] [datetime] NOT NULL,
	[Usuariomodifica] [int] NULL,
	[Fechamodifica] [datetime] NULL,
	[FechaAprobacion] [date] NOT NULL,
	[Supervisor_Id] [int] NOT NULL,
 CONSTRAINT [PK_Solicitudesviajes_Solicitudviaje_id] PRIMARY KEY CLUSTERED 
(
	[Solicitudviaje_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sucursales]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sucursales](
	[Sucursal_id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](100) NOT NULL,
	[Direccion] [varchar](100) NOT NULL,
	[Activo] [bit] NOT NULL,
	[Usuariocrea] [int] NOT NULL,
	[Fechacrea] [datetime] NOT NULL,
	[Usuariomodifica] [int] NULL,
	[Fechamodifica] [datetime] NULL,
	[Latitud] [decimal](19, 15) NULL,
	[Longitud] [decimal](19, 15) NULL,
	[Jefe_id] [int] NULL,
 CONSTRAINT [PK_Sucursales_Sucursal_id] PRIMARY KEY CLUSTERED 
(
	[Sucursal_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transportistas]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transportistas](
	[Transportista_id] [int] IDENTITY(1,1) NOT NULL,
	[Serviciotransporte_id] [int] NOT NULL,
	[Tarifaporkilometro] [decimal](10, 2) NOT NULL,
	[Activo] [bit] NOT NULL,
	[Usuariocrea] [int] NOT NULL,
	[Fechacrea] [datetime] NOT NULL,
	[Persona_id] [int] NOT NULL,
	[Usuariomodifica] [int] NULL,
	[Fechamodifica] [datetime] NULL,
	[Moneda_Id] [int] NULL,
 CONSTRAINT [PK_Transportista_Transportista_id] PRIMARY KEY CLUSTERED 
(
	[Transportista_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Usuario_id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[Passwordhash] [varbinary](275) NULL,
	[Colaborador_id] [int] NULL,
	[Esadmin] [bit] NOT NULL,
	[Activo] [bit] NOT NULL,
 CONSTRAINT [PK_Usuarios_Usuario_id] PRIMARY KEY CLUSTERED 
(
	[Usuario_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Valoracionesviajes]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Valoracionesviajes](
	[Valoracionviaje_id] [int] IDENTITY(1,1) NOT NULL,
	[Valoracionnota] [tinyint] NOT NULL,
	[Colaborador_id] [int] NOT NULL,
	[Viaje_id] [int] NOT NULL,
 CONSTRAINT [PK_Valoracionesviajes_Valoracionviaje_id] PRIMARY KEY CLUSTERED 
(
	[Valoracionviaje_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Viajes]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Viajes](
	[Viaje_id] [int] IDENTITY(1,1) NOT NULL,
	[Sucursal_id] [int] NOT NULL,
	[Transportista_id] [int] NOT NULL,
	[Estado_id] [int] NOT NULL,
	[Viajehora] [time](7) NOT NULL,
	[Viajefecha] [date] NOT NULL,
	[Totalkilometros] [decimal](5, 2) NOT NULL,
	[Totalpagar] [decimal](10, 2) NOT NULL,
	[Activo] [bit] NOT NULL,
	[Usuariocrea] [int] NOT NULL,
	[Fechacrea] [datetime] NOT NULL,
	[Usuariomodifica] [int] NULL,
	[Fechamodifica] [datetime] NULL,
	[Moneda_Id] [int] NULL,
 CONSTRAINT [PK_Viajes_Viajes_id] PRIMARY KEY CLUSTERED 
(
	[Viaje_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Viajesdetalles]    Script Date: 27/2/2025 16:48:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Viajesdetalles](
	[Viajedetalle_id] [int] IDENTITY(1,1) NOT NULL,
	[Viaje_id] [int] NOT NULL,
	[Colaborador_id] [int] NOT NULL,
	[Distanciakilometros] [decimal](5, 2) NOT NULL,
	[Totalpagar] [decimal](10, 2) NOT NULL,
	[Colaboradorsucursal_id] [int] NOT NULL,
	[Activo] [bit] NOT NULL,
	[Usuariocrea] [int] NOT NULL,
	[Fechacrea] [datetime] NOT NULL,
	[Usuariomodifica] [int] NULL,
	[Fechamodifica] [datetime] NULL,
	[Moneda_Id] [int] NULL,
 CONSTRAINT [PK_Viajesdetalles_Viajedetalle_id] PRIMARY KEY CLUSTERED 
(
	[Viajedetalle_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Cargos] ON 

INSERT [dbo].[Cargos] ([Cargo_id], [Nombre]) VALUES (1, N'SysAdmin')
INSERT [dbo].[Cargos] ([Cargo_id], [Nombre]) VALUES (2, N'Colaborador Tienda')
INSERT [dbo].[Cargos] ([Cargo_id], [Nombre]) VALUES (3, N'Gerente Tienda')
INSERT [dbo].[Cargos] ([Cargo_id], [Nombre]) VALUES (4, N'TEST')
SET IDENTITY_INSERT [dbo].[Cargos] OFF
GO
SET IDENTITY_INSERT [dbo].[Ciudades] ON 

INSERT [dbo].[Ciudades] ([Ciudad_id], [Nombre], [Departamento_id], [Activo]) VALUES (1, N'San Pedro Sula', 1, 1)
INSERT [dbo].[Ciudades] ([Ciudad_id], [Nombre], [Departamento_id], [Activo]) VALUES (2, N'Caragol', 1, 1)
SET IDENTITY_INSERT [dbo].[Ciudades] OFF
GO
SET IDENTITY_INSERT [dbo].[Colaboradores] ON 

INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (1, 1, 1, 1, 1, N'Honduras, Cortes, San Pedro Sula', 1, CAST(N'2025-02-20T00:00:00.000' AS DateTime), NULL, NULL, CAST(15.475146740703817 AS Decimal(19, 15)), CAST(-87.981048186841330 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (2, 14, 1, 1, 1, N'Honduras, Cortes, San Pedro Sula, La pradera', 1, CAST(N'2025-02-24T21:38:05.557' AS DateTime), NULL, NULL, CAST(15.475165044215684 AS Decimal(19, 15)), CAST(-87.980962306614130 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (3, 17, 2, 3, 1, N'Colonia Fesitranh, San Pedro Sula', 1, CAST(N'2025-02-25T19:43:52.263' AS DateTime), NULL, NULL, CAST(15.557985970431286 AS Decimal(19, 15)), CAST(-87.993208536194170 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (4, 18, 2, 3, 1, N'Colonia Fesitranh, Calle Principal', 1, CAST(N'2025-02-25T19:44:21.900' AS DateTime), NULL, NULL, CAST(15.558123456789012 AS Decimal(19, 15)), CAST(-87.992876543210980 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (5, 19, 3, 2, 1, N'Colonia Fesitranh, Avenida 1', 1, CAST(N'2025-02-25T19:44:51.600' AS DateTime), NULL, NULL, CAST(15.556789123456789 AS Decimal(19, 15)), CAST(-87.994321098765430 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (6, 20, 2, 3, 1, N'Colonia Fesitranh, Sector Norte', 1, CAST(N'2025-02-25T19:45:29.937' AS DateTime), NULL, NULL, CAST(15.559876543210987 AS Decimal(19, 15)), CAST(-87.991234567890120 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (7, 21, 3, 2, 1, N'Barrio Guamilito, cerca de Fesitranh', 1, CAST(N'2025-02-25T19:46:45.273' AS DateTime), NULL, NULL, CAST(15.560123456789012 AS Decimal(19, 15)), CAST(-87.990876543210980 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (8, 22, 3, 2, 1, N'Colonia Río de Piedras, cerca de Fesitranh', 1, CAST(N'2025-02-25T19:47:09.667' AS DateTime), NULL, NULL, CAST(15.554321098765432 AS Decimal(19, 15)), CAST(-87.996789123456780 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (9, 23, 3, 2, 1, N'Barrio El Centro, cerca de Fesitranh', 1, CAST(N'2025-02-25T19:47:39.133' AS DateTime), NULL, NULL, CAST(15.561234567890123 AS Decimal(19, 15)), CAST(-87.989654321098760 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (10, 24, 3, 2, 1, N'Colonia La Pradera, cerca de Fesitranh', 1, CAST(N'2025-02-25T19:48:09.610' AS DateTime), NULL, NULL, CAST(15.553210987654321 AS Decimal(19, 15)), CAST(-87.997890123456780 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (11, 25, 3, 2, 1, N'Colonia Trejo, cerca de Fesitranh', 1, CAST(N'2025-02-25T19:48:34.570' AS DateTime), NULL, NULL, CAST(15.562345678901234 AS Decimal(19, 15)), CAST(-87.988543210987650 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (12, 26, 3, 2, 1, N'Barrio Suyapa, cerca de Fesitranh', 1, CAST(N'2025-02-25T19:49:04.113' AS DateTime), NULL, NULL, CAST(15.552109876543210 AS Decimal(19, 15)), CAST(-87.998901234567890 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (13, 27, 3, 2, 1, N'Colonia Moderna, cerca de Fesitranh', 1, CAST(N'2025-02-25T19:49:25.380' AS DateTime), NULL, NULL, CAST(15.563456789012345 AS Decimal(19, 15)), CAST(-87.987432109876540 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (14, 28, 3, 2, 1, N'Colonia Los Laureles, cerca de Fesitranh', 1, CAST(N'2025-02-25T19:50:33.503' AS DateTime), NULL, NULL, CAST(15.551098765432109 AS Decimal(19, 15)), CAST(-87.999012345678900 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (15, 29, 3, 2, 1, N'Barrio Las Acacias, cerca de Fesitranh', 1, CAST(N'2025-02-25T19:51:16.913' AS DateTime), NULL, NULL, CAST(15.564567890123456 AS Decimal(19, 15)), CAST(-87.986321098765430 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (16, 30, 3, 2, 1, N'Colonia Fesitranh, Sector Este', 1, CAST(N'2025-02-25T19:51:34.367' AS DateTime), NULL, NULL, CAST(15.557890123456789 AS Decimal(19, 15)), CAST(-87.994567890123450 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (18, 48, 3, 2, 1, N'Colonia La Pradera', 1, CAST(N'2025-02-26T10:50:55.980' AS DateTime), NULL, NULL, CAST(15.476651292960746 AS Decimal(19, 15)), CAST(-87.983238958584420 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (19, 51, 3, 2, 1, N'Colonia La Pradera', 1, CAST(N'2025-02-26T10:52:08.860' AS DateTime), NULL, NULL, CAST(15.476651292960746 AS Decimal(19, 15)), CAST(-87.983238958584420 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (20, 52, 3, 2, 1, N'Caracol, Cortes', 1, CAST(N'2025-02-27T16:28:04.187' AS DateTime), NULL, NULL, CAST(15.125903632187681 AS Decimal(19, 15)), CAST(-87.939753601623500 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (21, 53, 3, 2, 1, N'Caracol, Cortes', 1, CAST(N'2025-02-27T16:29:15.307' AS DateTime), NULL, NULL, CAST(15.062812532339288 AS Decimal(19, 15)), CAST(-87.926383649985070 AS Decimal(19, 15)))
SET IDENTITY_INSERT [dbo].[Colaboradores] OFF
GO
SET IDENTITY_INSERT [dbo].[Colaboradoressucursales] ON 

INSERT [dbo].[Colaboradoressucursales] ([Colaboradorsucursal_id], [Colaborador_id], [Sucursal_id], [Distanciakilometro], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (1, 1, 1, CAST(7.70 AS Decimal(5, 2)), 1, 1, CAST(N'2025-02-24T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Colaboradoressucursales] ([Colaboradorsucursal_id], [Colaborador_id], [Sucursal_id], [Distanciakilometro], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (4, 2, 1, CAST(5.82 AS Decimal(5, 2)), 1, 1, CAST(N'2025-02-26T02:56:16.450' AS DateTime), 1, CAST(N'2025-02-26T02:56:16.450' AS DateTime))
INSERT [dbo].[Colaboradoressucursales] ([Colaboradorsucursal_id], [Colaborador_id], [Sucursal_id], [Distanciakilometro], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (5, 8, 1, CAST(6.57 AS Decimal(5, 2)), 1, 1, CAST(N'2025-02-26T03:15:21.650' AS DateTime), NULL, NULL)
INSERT [dbo].[Colaboradoressucursales] ([Colaboradorsucursal_id], [Colaborador_id], [Sucursal_id], [Distanciakilometro], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (6, 7, 1, CAST(7.45 AS Decimal(5, 2)), 1, 1, CAST(N'2025-02-26T03:15:21.650' AS DateTime), NULL, NULL)
INSERT [dbo].[Colaboradoressucursales] ([Colaboradorsucursal_id], [Colaborador_id], [Sucursal_id], [Distanciakilometro], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (7, 6, 1, CAST(6.94 AS Decimal(5, 2)), 1, 1, CAST(N'2025-02-26T03:15:21.650' AS DateTime), NULL, NULL)
INSERT [dbo].[Colaboradoressucursales] ([Colaboradorsucursal_id], [Colaborador_id], [Sucursal_id], [Distanciakilometro], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (8, 19, 1, CAST(7.70 AS Decimal(5, 2)), 1, 1, CAST(N'2025-02-26T16:52:29.523' AS DateTime), NULL, NULL)
INSERT [dbo].[Colaboradoressucursales] ([Colaboradorsucursal_id], [Colaborador_id], [Sucursal_id], [Distanciakilometro], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (9, 9, 1, CAST(10.50 AS Decimal(5, 2)), 1, 1, CAST(N'2025-02-26T16:52:29.523' AS DateTime), NULL, NULL)
INSERT [dbo].[Colaboradoressucursales] ([Colaboradorsucursal_id], [Colaborador_id], [Sucursal_id], [Distanciakilometro], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (10, 18, 1, CAST(7.40 AS Decimal(5, 2)), 1, 1, CAST(N'2025-02-27T17:00:10.913' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Colaboradoressucursales] OFF
GO
SET IDENTITY_INSERT [dbo].[Departamentos] ON 

INSERT [dbo].[Departamentos] ([Departamento_id], [Nombre], [Activo], [Pais_Id]) VALUES (1, N'Cortes', 1, 1)
SET IDENTITY_INSERT [dbo].[Departamentos] OFF
GO
SET IDENTITY_INSERT [dbo].[Estados] ON 

INSERT [dbo].[Estados] ([Estado_id], [Nombre], [Descripcion]) VALUES (1, N'Solicitud Aprobada', N'Solicitud de viaje aprobada.')
INSERT [dbo].[Estados] ([Estado_id], [Nombre], [Descripcion]) VALUES (2, N'Solicitud Denegada', N'Solicitud de viaje cancelada por supervisor.')
INSERT [dbo].[Estados] ([Estado_id], [Nombre], [Descripcion]) VALUES (3, N'Solicitud Cancelada', N'Solicitud viaje Cancelada por empleado.')
INSERT [dbo].[Estados] ([Estado_id], [Nombre], [Descripcion]) VALUES (4, N'Viaje Cerrado', N'No se pueden agregar mas colaboradores al viaje.')
INSERT [dbo].[Estados] ([Estado_id], [Nombre], [Descripcion]) VALUES (5, N'Viaje Abierto', N'Todavia se pueden asignar colaboradores al viaje.')
SET IDENTITY_INSERT [dbo].[Estados] OFF
GO
SET IDENTITY_INSERT [dbo].[Estadosciviles] ON 

INSERT [dbo].[Estadosciviles] ([Estadocivil_id], [Nombre]) VALUES (1, N'Casado(A)')
INSERT [dbo].[Estadosciviles] ([Estadocivil_id], [Nombre]) VALUES (2, N'Soltero(A)')
SET IDENTITY_INSERT [dbo].[Estadosciviles] OFF
GO
SET IDENTITY_INSERT [dbo].[Monedas] ON 

INSERT [dbo].[Monedas] ([Moneda_Id], [Nombre], [Simbolo], [ValorLempiras]) VALUES (1, N'Lempira', N'L', 1.0000)
SET IDENTITY_INSERT [dbo].[Monedas] OFF
GO
SET IDENTITY_INSERT [dbo].[Paises] ON 

INSERT [dbo].[Paises] ([Pais_id], [Nombre], [Activo]) VALUES (1, N'Honduras', 1)
INSERT [dbo].[Paises] ([Pais_id], [Nombre], [Activo]) VALUES (4, N'Belice', 1)
SET IDENTITY_INSERT [dbo].[Paises] OFF
GO
SET IDENTITY_INSERT [dbo].[Personas] ON 

INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (1, N'Jason Jeremy', N'Villanueva Sanchez', N'M', N'jasonjeremy504@gmail.com', N'0501200403104', 1, 1, 1, 1, CAST(N'2025-02-20T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (9, N'Jose Chalino', N'Paz Arteaga', N'M', N'Chalino@gmail.com', N'0501199703105', 0, NULL, 1, 1, CAST(N'2025-02-24T21:03:12.853' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (10, N'Pancho Francisco', N'Acuario Benitez', N'M', N'Francisco@gmail.com', N'0501199703102', 0, NULL, 1, 1, CAST(N'2025-02-24T21:20:42.123' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (14, N'Jason Jeremy', N'Villanueva Sanchez', N'M', N'jasonjeremy48j@gmail.com', N'0501200403104', 1, 1, 1, 1, CAST(N'2025-02-24T21:38:05.507' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (17, N'Juan', N'Pérez', N'M', N'juan.perez@example.com', N'0801199012345', 1, 1, 1, 1, CAST(N'2025-02-25T19:43:51.877' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (18, N'María', N'Gómez', N'F', N'maria.gomez@example.com', N'0801199254321', 1, 2, 1, 1, CAST(N'2025-02-25T19:44:21.890' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (19, N'Carlos', N'Rodríguez', N'M', N'carlos.rodriguez@example.com', N'0801198898765', 1, 1, 1, 1, CAST(N'2025-02-25T19:44:51.583' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (20, N'Ana', N'López', N'F', N'ana.lopez@example.com', N'0801199545678', 1, 2, 1, 1, CAST(N'2025-02-25T19:45:29.923' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (21, N'Sofía', N'Mejia', N'F', N'sofia.mejia@example.com', N'0801199333445', 1, 2, 1, 1, CAST(N'2025-02-25T19:46:45.253' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (22, N'Luis', N'Cruz', N'M', N'luis.cruz@example.com', N'0801199166778', 1, 1, 1, 1, CAST(N'2025-02-25T19:47:09.643' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (23, N'Elena', N'Ramírez', N'F', N'elena.ramirez@example.com', N'0801198988990', 1, 2, 1, 1, CAST(N'2025-02-25T19:47:39.120' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (24, N'José', N'Flores', N'M', N'jose.flores@example.com', N'0801198722334', 1, 1, 1, 1, CAST(N'2025-02-25T19:48:09.597' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (25, N'Carmen', N'Ortiz', N'F', N'carmen.ortiz@example.com', N'0801199455667', 1, 2, 1, 1, CAST(N'2025-02-25T19:48:34.557' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (26, N'Miguel', N'Reyes', N'M', N'miguel.reyes@example.com', N'0801199077889', 1, 1, 1, 1, CAST(N'2025-02-25T19:49:04.100' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (27, N'Laura', N'Vásquez', N'F', N'laura.vasquez@example.com', N'0801199699001', 1, 2, 1, 1, CAST(N'2025-02-25T19:49:25.363' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (28, N'Diego', N'Santos', N'M', N'diego.santos@example.com', N'0801198611234', 1, 1, 1, 1, CAST(N'2025-02-25T19:50:33.463' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (29, N'Karla', N'Mendoza', N'F', N'karla.mendoza@example.com', N'0801199233456', 1, 2, 1, 1, CAST(N'2025-02-25T19:51:16.900' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (30, N'Roberto', N'García', N'M', N'roberto.garcia@example.com', N'0801198455678', 1, 1, 1, 1, CAST(N'2025-02-25T19:51:34.350' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (31, N'José', N'Martínez', N'M', N'jose.martinez@example.com', N'0801198512345', 0, NULL, 1, 1, CAST(N'2025-02-25T20:16:40.863' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (35, N'Gabriela', N'Rivas', N'F', N'gabriela.rivas@example.com', N'0801199356789', 0, NULL, 1, 1, CAST(N'2025-02-25T20:23:23.087' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (37, N'Isabel', N'Pineda', N'F', N'isabel.pineda@example.com', N'0801199623456', 0, NULL, 1, 1, CAST(N'2025-02-25T20:23:45.377' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (39, N'Ricardo', N'Soto', N'M', N'ricardo.soto@example.com', N'0801198317890', 0, NULL, 1, 1, CAST(N'2025-02-25T20:24:33.240' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (40, N'Lucía', N'Mejía', N'F', N'lucia.mejia@example.com', N'0801199418901', 0, NULL, 1, 1, CAST(N'2025-02-25T20:24:46.317' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (41, N'Héctor', N'Aguilar', N'M', N'hector.aguilar@example.com', N'0801198923456', 0, NULL, 1, 1, CAST(N'2025-02-25T20:24:58.950' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (42, N'Rosa', N'Castro', N'F', N'rosa.castro@example.com', N'0801199756789', 0, NULL, 1, 1, CAST(N'2025-02-25T20:25:11.047' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (44, N'Eduardo', N'Guzmán', N'M', N'eduardo.guzman@example.com', N'0801198612345', 0, NULL, 1, 1, CAST(N'2025-02-25T20:25:38.787' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (45, N'Beatriz', N'Reyes', N'F', N'beatriz.reyes@example.com', N'0801199834567', 0, NULL, 1, 1, CAST(N'2025-02-25T20:25:55.247' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (48, N'José', N'Flores', N'M', N'jose.flores2@example.com', N'0801198722554', 1, 1, 1, 1, CAST(N'2025-02-26T10:50:55.397' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (51, N'José', N'Flores Garcia', N'M', N'jose.floresgarcia@example.com', N'0801198234554', 1, 1, 1, 1, CAST(N'2025-02-26T10:52:08.833' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (52, N'Sua ', N'Salgado', N'F', N'sua.salgado@gmail.com', N'0502200536978', 1, 1, 2, 1, CAST(N'2025-02-27T16:28:04.013' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (53, N'Danilo', N'Barahona', N'F', N'danilo.barahona@gmail.com', N'0502200536987', 1, 1, 2, 1, CAST(N'2025-02-27T16:29:15.250' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Personas] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Rol_id], [Nombre], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Activo]) VALUES (1, N'Admin', 1, CAST(N'2025-02-20T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Roles] ([Rol_id], [Nombre], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Activo]) VALUES (2, N'Gerente Tienda', 1, CAST(N'2025-02-25T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Roles] ([Rol_id], [Nombre], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Activo]) VALUES (3, N'Colaborador Tienda', 1, CAST(N'2025-02-25T00:00:00.000' AS DateTime), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Serviciostransporte] ON 

INSERT [dbo].[Serviciostransporte] ([Serviciotransporte_id], [Nombre], [Descripcion], [Email], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Activo]) VALUES (1, N'test', N'string', N'string@gmail.com', 1, CAST(N'2025-02-22T12:46:02.163' AS DateTime), NULL, CAST(N'2025-02-22T12:46:02.163' AS DateTime), 1)
INSERT [dbo].[Serviciostransporte] ([Serviciotransporte_id], [Nombre], [Descripcion], [Email], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Activo]) VALUES (2, N'Transportes Rápidos', N'Servicio de transporte urgente en San Pedro Sula', N'rapidos@example.com', 1, CAST(N'2025-02-26T01:54:19.617' AS DateTime), NULL, CAST(N'2025-02-26T01:54:19.617' AS DateTime), 1)
INSERT [dbo].[Serviciostransporte] ([Serviciotransporte_id], [Nombre], [Descripcion], [Email], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Activo]) VALUES (4, N'Logística Cortés', N'Transporte de carga pesada en Cortés', N'cortes.logistica@example.com', 1, CAST(N'2025-02-26T01:54:19.617' AS DateTime), NULL, CAST(N'2025-02-26T01:54:19.617' AS DateTime), 1)
INSERT [dbo].[Serviciostransporte] ([Serviciotransporte_id], [Nombre], [Descripcion], [Email], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Activo]) VALUES (5, N'Viajes El Norte', N'Servicio de pasajeros en el norte de Honduras', N'viajesnorte@example.com', 1, CAST(N'2025-02-26T01:54:19.617' AS DateTime), NULL, CAST(N'2025-02-26T01:54:19.617' AS DateTime), 1)
INSERT [dbo].[Serviciostransporte] ([Serviciotransporte_id], [Nombre], [Descripcion], [Email], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Activo]) VALUES (6, N'Carga Express', N'Entrega rápida de mercancías', N'cargaexpress@example.com', 1, CAST(N'2025-02-26T01:54:19.617' AS DateTime), NULL, CAST(N'2025-02-26T01:54:19.617' AS DateTime), 1)
INSERT [dbo].[Serviciostransporte] ([Serviciotransporte_id], [Nombre], [Descripcion], [Email], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Activo]) VALUES (7, N'Transportes Unidos', N'Servicio de transporte compartido', N'unidos@example.com', 1, CAST(N'2025-02-26T01:54:19.617' AS DateTime), NULL, CAST(N'2025-02-26T01:54:19.617' AS DateTime), 1)
INSERT [dbo].[Serviciostransporte] ([Serviciotransporte_id], [Nombre], [Descripcion], [Email], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Activo]) VALUES (8, N'Fletes San Pedro', N'Fletes locales en San Pedro Sula', N'fletes.sp@example.com', 1, CAST(N'2025-02-26T01:54:19.617' AS DateTime), NULL, CAST(N'2025-02-26T01:54:19.617' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Serviciostransporte] OFF
GO
SET IDENTITY_INSERT [dbo].[Sucursales] ON 

INSERT [dbo].[Sucursales] ([Sucursal_id], [Nombre], [Direccion], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud], [Jefe_id]) VALUES (1, N'Sucursal Barrio Benque', N'Barrio el benqye san pedro sula', 1, 1, CAST(N'2025-02-24T00:00:00.000' AS DateTime), NULL, NULL, CAST(15.502893036915468 AS Decimal(19, 15)), CAST(-88.026969291986460 AS Decimal(19, 15)), 3)
SET IDENTITY_INSERT [dbo].[Sucursales] OFF
GO
SET IDENTITY_INSERT [dbo].[Transportistas] ON 

INSERT [dbo].[Transportistas] ([Transportista_id], [Serviciotransporte_id], [Tarifaporkilometro], [Activo], [Usuariocrea], [Fechacrea], [Persona_id], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (1, 1, CAST(23.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-24T21:03:15.097' AS DateTime), 9, NULL, NULL, 1)
INSERT [dbo].[Transportistas] ([Transportista_id], [Serviciotransporte_id], [Tarifaporkilometro], [Activo], [Usuariocrea], [Fechacrea], [Persona_id], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (2, 1, CAST(25.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-24T21:20:42.133' AS DateTime), 10, NULL, NULL, 1)
INSERT [dbo].[Transportistas] ([Transportista_id], [Serviciotransporte_id], [Tarifaporkilometro], [Activo], [Usuariocrea], [Fechacrea], [Persona_id], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (3, 1, CAST(2.50 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-25T20:16:40.893' AS DateTime), 31, NULL, NULL, 1)
INSERT [dbo].[Transportistas] ([Transportista_id], [Serviciotransporte_id], [Tarifaporkilometro], [Activo], [Usuariocrea], [Fechacrea], [Persona_id], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (4, 2, CAST(3.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-25T20:23:23.103' AS DateTime), 35, NULL, NULL, 1)
INSERT [dbo].[Transportistas] ([Transportista_id], [Serviciotransporte_id], [Tarifaporkilometro], [Activo], [Usuariocrea], [Fechacrea], [Persona_id], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (5, 4, CAST(2.70 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-25T20:23:45.390' AS DateTime), 37, NULL, NULL, 1)
INSERT [dbo].[Transportistas] ([Transportista_id], [Serviciotransporte_id], [Tarifaporkilometro], [Activo], [Usuariocrea], [Fechacrea], [Persona_id], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (6, 5, CAST(3.20 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-25T20:24:33.257' AS DateTime), 39, NULL, NULL, 1)
INSERT [dbo].[Transportistas] ([Transportista_id], [Serviciotransporte_id], [Tarifaporkilometro], [Activo], [Usuariocrea], [Fechacrea], [Persona_id], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (7, 6, CAST(2.90 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-25T20:24:46.330' AS DateTime), 40, NULL, NULL, 1)
INSERT [dbo].[Transportistas] ([Transportista_id], [Serviciotransporte_id], [Tarifaporkilometro], [Activo], [Usuariocrea], [Fechacrea], [Persona_id], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (8, 7, CAST(3.10 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-25T20:24:58.957' AS DateTime), 41, NULL, NULL, 1)
INSERT [dbo].[Transportistas] ([Transportista_id], [Serviciotransporte_id], [Tarifaporkilometro], [Activo], [Usuariocrea], [Fechacrea], [Persona_id], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (9, 8, CAST(2.60 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-25T20:25:11.057' AS DateTime), 42, NULL, NULL, 1)
INSERT [dbo].[Transportistas] ([Transportista_id], [Serviciotransporte_id], [Tarifaporkilometro], [Activo], [Usuariocrea], [Fechacrea], [Persona_id], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (11, 4, CAST(3.30 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-25T20:25:38.793' AS DateTime), 44, NULL, NULL, 1)
INSERT [dbo].[Transportistas] ([Transportista_id], [Serviciotransporte_id], [Tarifaporkilometro], [Activo], [Usuariocrea], [Fechacrea], [Persona_id], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (12, 4, CAST(2.40 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-25T20:25:55.257' AS DateTime), 45, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Transportistas] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([Usuario_id], [Username], [Passwordhash], [Colaborador_id], [Esadmin], [Activo]) VALUES (1, N'pshkin', 0xA665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3, 1, 1, 1)
INSERT [dbo].[Usuarios] ([Usuario_id], [Username], [Passwordhash], [Colaborador_id], [Esadmin], [Activo]) VALUES (2, N'juan', 0xA665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3, 3, 0, 1)
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
SET IDENTITY_INSERT [dbo].[Viajes] ON 

INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (5, 1, 1, 1, CAST(N'21:00:00' AS Time), CAST(N'2025-02-25' AS Date), CAST(21.00 AS Decimal(5, 2)), CAST(483.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-24T22:13:05.230' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (6, 1, 1, 4, CAST(N'22:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(5.82 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-25T22:22:06.980' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (7, 1, 2, 4, CAST(N'22:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(14.02 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-25T22:22:06.980' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (9, 1, 1, 5, CAST(N'16:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(10.50 AS Decimal(5, 2)), CAST(241.50 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-26T11:49:07.847' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (16, 1, 4, 4, CAST(N'16:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(24.68 AS Decimal(5, 2)), CAST(74.03 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-26T11:56:54.490' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (17, 1, 2, 4, CAST(N'16:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(8.33 AS Decimal(5, 2)), CAST(208.15 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-26T11:56:54.640' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (18, 1, 3, 4, CAST(N'16:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(9.24 AS Decimal(5, 2)), CAST(23.09 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-26T11:56:54.793' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (37, 1, 4, 4, CAST(N'16:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(30.50 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-26T15:45:34.740' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (38, 1, 2, 4, CAST(N'16:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(6.94 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-26T15:45:34.740' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (39, 1, 3, 4, CAST(N'16:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(6.57 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-26T15:45:34.740' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (40, 1, 3, 4, CAST(N'16:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(30.50 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-26T16:26:26.140' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (41, 1, 4, 4, CAST(N'16:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(6.94 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-26T16:26:26.140' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (42, 1, 2, 4, CAST(N'16:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(6.57 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-26T16:26:26.140' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (43, 1, 4, 4, CAST(N'16:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(30.50 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-26T16:31:11.300' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (44, 1, 3, 4, CAST(N'16:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(6.94 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-26T16:31:11.300' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (45, 1, 2, 4, CAST(N'16:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(6.57 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-26T16:31:11.300' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (46, 1, 3, 4, CAST(N'16:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(30.50 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-26T16:34:27.090' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (47, 1, 4, 4, CAST(N'16:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(6.94 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-26T16:34:27.090' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (48, 1, 2, 4, CAST(N'16:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(6.57 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-26T16:34:27.090' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (49, 1, 2, 4, CAST(N'16:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(30.50 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-26T16:35:59.157' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (50, 1, 4, 4, CAST(N'16:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(6.94 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-26T16:36:06.870' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (51, 1, 3, 4, CAST(N'16:00:00' AS Time), CAST(N'2025-02-26' AS Date), CAST(6.57 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, CAST(N'2025-02-26T16:36:12.007' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (70, 1, 2, 5, CAST(N'16:00:00' AS Time), CAST(N'2025-02-27' AS Date), CAST(30.50 AS Decimal(5, 2)), CAST(762.50 AS Decimal(10, 2)), 1, 2, CAST(N'2025-02-27T15:26:23.800' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (71, 1, 2, 5, CAST(N'16:00:00' AS Time), CAST(N'2025-02-27' AS Date), CAST(6.94 AS Decimal(5, 2)), CAST(173.50 AS Decimal(10, 2)), 1, 2, CAST(N'2025-02-27T15:26:23.807' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajes] ([Viaje_id], [Sucursal_id], [Transportista_id], [Estado_id], [Viajehora], [Viajefecha], [Totalkilometros], [Totalpagar], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (72, 1, 4, 5, CAST(N'16:00:00' AS Time), CAST(N'2025-02-27' AS Date), CAST(6.57 AS Decimal(5, 2)), CAST(19.71 AS Decimal(10, 2)), 1, 2, CAST(N'2025-02-27T15:26:23.813' AS DateTime), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Viajes] OFF
GO
SET IDENTITY_INSERT [dbo].[Viajesdetalles] ON 

INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (2, 5, 1, CAST(21.00 AS Decimal(5, 2)), CAST(483.00 AS Decimal(10, 2)), 1, 1, 1, CAST(N'2025-02-24T22:13:05.230' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (3, 6, 2, CAST(5.82 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 4, 1, 1, CAST(N'2025-02-25T22:22:06.977' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (4, 7, 8, CAST(6.57 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 5, 1, 1, CAST(N'2025-02-25T22:22:06.980' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (5, 7, 7, CAST(7.45 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 6, 1, 1, CAST(N'2025-02-25T22:22:06.980' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (6, 9, 9, CAST(10.50 AS Decimal(5, 2)), CAST(241.50 AS Decimal(10, 2)), 9, 1, 1, CAST(N'2025-02-26T11:49:07.847' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (11, 16, 1, CAST(7.71 AS Decimal(5, 2)), CAST(23.12 AS Decimal(10, 2)), 1, 1, 1, CAST(N'2025-02-26T11:56:54.220' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (12, 16, 2, CAST(7.72 AS Decimal(5, 2)), CAST(23.15 AS Decimal(10, 2)), 4, 1, 1, CAST(N'2025-02-26T11:56:54.350' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (13, 16, 19, CAST(9.25 AS Decimal(5, 2)), CAST(27.75 AS Decimal(10, 2)), 8, 1, 1, CAST(N'2025-02-26T11:56:54.490' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (14, 17, 6, CAST(8.33 AS Decimal(5, 2)), CAST(208.15 AS Decimal(10, 2)), 7, 1, 1, CAST(N'2025-02-26T11:56:54.640' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (15, 18, 8, CAST(9.24 AS Decimal(5, 2)), CAST(23.09 AS Decimal(10, 2)), 5, 1, 1, CAST(N'2025-02-26T11:56:54.793' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (16, 37, 1, CAST(7.70 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, 1, CAST(N'2025-02-26T15:45:34.740' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (17, 37, 2, CAST(15.10 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 4, 1, 1, CAST(N'2025-02-26T15:45:34.740' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (18, 37, 19, CAST(7.70 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 8, 1, 1, CAST(N'2025-02-26T15:45:34.740' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (19, 38, 6, CAST(6.94 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 7, 1, 1, CAST(N'2025-02-26T15:45:34.740' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (20, 39, 8, CAST(6.57 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 5, 1, 1, CAST(N'2025-02-26T15:45:34.740' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (21, 40, 1, CAST(7.70 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, 1, CAST(N'2025-02-26T16:26:26.137' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (22, 40, 2, CAST(15.10 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 4, 1, 1, CAST(N'2025-02-26T16:26:26.140' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (23, 40, 19, CAST(7.70 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 8, 1, 1, CAST(N'2025-02-26T16:26:26.140' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (24, 41, 6, CAST(6.94 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 7, 1, 1, CAST(N'2025-02-26T16:26:26.140' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (25, 42, 8, CAST(6.57 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 5, 1, 1, CAST(N'2025-02-26T16:26:26.140' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (26, 43, 1, CAST(7.70 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, 1, CAST(N'2025-02-26T16:31:11.300' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (27, 43, 2, CAST(15.10 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 4, 1, 1, CAST(N'2025-02-26T16:31:11.300' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (28, 43, 19, CAST(7.70 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 8, 1, 1, CAST(N'2025-02-26T16:31:11.300' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (29, 44, 6, CAST(6.94 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 7, 1, 1, CAST(N'2025-02-26T16:31:11.300' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (30, 45, 8, CAST(6.57 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 5, 1, 1, CAST(N'2025-02-26T16:31:11.300' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (31, 46, 1, CAST(7.70 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, 1, CAST(N'2025-02-26T16:34:27.087' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (32, 46, 2, CAST(15.10 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 4, 1, 1, CAST(N'2025-02-26T16:34:27.087' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (33, 46, 19, CAST(7.70 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 8, 1, 1, CAST(N'2025-02-26T16:34:27.087' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (34, 47, 6, CAST(6.94 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 7, 1, 1, CAST(N'2025-02-26T16:34:27.090' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (35, 48, 8, CAST(6.57 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 5, 1, 1, CAST(N'2025-02-26T16:34:27.090' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (36, 49, 1, CAST(7.70 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 1, 1, 1, CAST(N'2025-02-26T16:35:50.437' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (37, 49, 2, CAST(15.10 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 4, 1, 1, CAST(N'2025-02-26T16:35:53.723' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (38, 49, 19, CAST(7.70 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 8, 1, 1, CAST(N'2025-02-26T16:35:54.853' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (39, 50, 6, CAST(6.94 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 7, 1, 1, CAST(N'2025-02-26T16:36:03.477' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (40, 51, 8, CAST(6.57 AS Decimal(5, 2)), CAST(0.00 AS Decimal(10, 2)), 5, 1, 1, CAST(N'2025-02-26T16:36:09.390' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (71, 70, 1, CAST(7.70 AS Decimal(5, 2)), CAST(192.50 AS Decimal(10, 2)), 1, 1, 2, CAST(N'2025-02-27T15:26:23.793' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (72, 70, 2, CAST(15.10 AS Decimal(5, 2)), CAST(377.50 AS Decimal(10, 2)), 4, 1, 2, CAST(N'2025-02-27T15:26:23.793' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (73, 70, 19, CAST(7.70 AS Decimal(5, 2)), CAST(192.50 AS Decimal(10, 2)), 8, 1, 2, CAST(N'2025-02-27T15:26:23.793' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (74, 71, 6, CAST(6.94 AS Decimal(5, 2)), CAST(173.50 AS Decimal(10, 2)), 7, 1, 2, CAST(N'2025-02-27T15:26:23.807' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Viajesdetalles] ([Viajedetalle_id], [Viaje_id], [Colaborador_id], [Distanciakilometros], [Totalpagar], [Colaboradorsucursal_id], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (75, 72, 8, CAST(6.57 AS Decimal(5, 2)), CAST(19.71 AS Decimal(10, 2)), 5, 1, 2, CAST(N'2025-02-27T15:26:23.813' AS DateTime), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Viajesdetalles] OFF
GO
/****** Object:  Index [UQ_Colaboradores_Persona_id]    Script Date: 27/2/2025 16:48:19 ******/
ALTER TABLE [dbo].[Colaboradores] ADD  CONSTRAINT [UQ_Colaboradores_Persona_id] UNIQUE NONCLUSTERED 
(
	[Persona_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_ColaboradorSucursal]    Script Date: 27/2/2025 16:48:19 ******/
ALTER TABLE [dbo].[Colaboradoressucursales] ADD  CONSTRAINT [UQ_ColaboradorSucursal] UNIQUE NONCLUSTERED 
(
	[Colaborador_id] ASC,
	[Sucursal_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Personas__A9D1053437290ADF]    Script Date: 27/2/2025 16:48:19 ******/
ALTER TABLE [dbo].[Personas] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Personas__A9D10534BCFC2EB2]    Script Date: 27/2/2025 16:48:19 ******/
ALTER TABLE [dbo].[Personas] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Servicio__A9D1053460E68BBE]    Script Date: 27/2/2025 16:48:19 ******/
ALTER TABLE [dbo].[Serviciostransporte] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_Transportistas_Persona_id]    Script Date: 27/2/2025 16:48:19 ******/
ALTER TABLE [dbo].[Transportistas] ADD  CONSTRAINT [UQ_Transportistas_Persona_id] UNIQUE NONCLUSTERED 
(
	[Persona_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Usuarios__46FD4C9BEDA41A3C]    Script Date: 27/2/2025 16:48:19 ******/
ALTER TABLE [dbo].[Usuarios] ADD UNIQUE NONCLUSTERED 
(
	[Colaborador_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Usuarios__536C85E4F21FC609]    Script Date: 27/2/2025 16:48:19 ******/
ALTER TABLE [dbo].[Usuarios] ADD UNIQUE NONCLUSTERED 
(
	[Username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Ciudades] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Colaboradores] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Colaboradoressucursales] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Departamentos] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Paises] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Pantallas] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Personas] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Roles] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Serviciostransporte] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Solicitudesviajes] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Sucursales] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Transportistas] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((0)) FOR [Esadmin]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Viajes] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Viajesdetalles] ADD  DEFAULT ((1)) FOR [Activo]
GO
ALTER TABLE [dbo].[Ciudades]  WITH CHECK ADD  CONSTRAINT [FK_Ciudades_Departamentos_Departamento_id] FOREIGN KEY([Departamento_id])
REFERENCES [dbo].[Departamentos] ([Departamento_id])
GO
ALTER TABLE [dbo].[Ciudades] CHECK CONSTRAINT [FK_Ciudades_Departamentos_Departamento_id]
GO
ALTER TABLE [dbo].[Colaboradores]  WITH CHECK ADD  CONSTRAINT [FK_Colaboradores_Cargos_Cargo_id] FOREIGN KEY([Cargo_id])
REFERENCES [dbo].[Cargos] ([Cargo_id])
GO
ALTER TABLE [dbo].[Colaboradores] CHECK CONSTRAINT [FK_Colaboradores_Cargos_Cargo_id]
GO
ALTER TABLE [dbo].[Colaboradores]  WITH CHECK ADD  CONSTRAINT [FK_Colaboradores_Personas_Persona_id] FOREIGN KEY([Persona_id])
REFERENCES [dbo].[Personas] ([Persona_id])
GO
ALTER TABLE [dbo].[Colaboradores] CHECK CONSTRAINT [FK_Colaboradores_Personas_Persona_id]
GO
ALTER TABLE [dbo].[Colaboradores]  WITH CHECK ADD  CONSTRAINT [FK_Colaboradores_Roles_Rol_id] FOREIGN KEY([Rol_id])
REFERENCES [dbo].[Roles] ([Rol_id])
GO
ALTER TABLE [dbo].[Colaboradores] CHECK CONSTRAINT [FK_Colaboradores_Roles_Rol_id]
GO
ALTER TABLE [dbo].[Colaboradores]  WITH CHECK ADD  CONSTRAINT [FK_Colaboradores_Usuarios_Usuariocrea] FOREIGN KEY([Usuariocrea])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Colaboradores] CHECK CONSTRAINT [FK_Colaboradores_Usuarios_Usuariocrea]
GO
ALTER TABLE [dbo].[Colaboradores]  WITH CHECK ADD  CONSTRAINT [FK_Colaboradores_Usuarios_Usuariomodifica] FOREIGN KEY([Usuariomodifica])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Colaboradores] CHECK CONSTRAINT [FK_Colaboradores_Usuarios_Usuariomodifica]
GO
ALTER TABLE [dbo].[Colaboradoressucursales]  WITH CHECK ADD  CONSTRAINT [FK_Colaboradores] FOREIGN KEY([Colaborador_id])
REFERENCES [dbo].[Colaboradores] ([Colaborador_id])
GO
ALTER TABLE [dbo].[Colaboradoressucursales] CHECK CONSTRAINT [FK_Colaboradores]
GO
ALTER TABLE [dbo].[Colaboradoressucursales]  WITH CHECK ADD  CONSTRAINT [FK_Colaboradoressucursales_Usuarios_Usuariocrea] FOREIGN KEY([Usuariocrea])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Colaboradoressucursales] CHECK CONSTRAINT [FK_Colaboradoressucursales_Usuarios_Usuariocrea]
GO
ALTER TABLE [dbo].[Colaboradoressucursales]  WITH CHECK ADD  CONSTRAINT [FK_Colaboradoressucursales_Usuarios_Usuariomodifica] FOREIGN KEY([Usuariomodifica])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Colaboradoressucursales] CHECK CONSTRAINT [FK_Colaboradoressucursales_Usuarios_Usuariomodifica]
GO
ALTER TABLE [dbo].[Colaboradoressucursales]  WITH CHECK ADD  CONSTRAINT [FK_Sucursales] FOREIGN KEY([Sucursal_id])
REFERENCES [dbo].[Sucursales] ([Sucursal_id])
GO
ALTER TABLE [dbo].[Colaboradoressucursales] CHECK CONSTRAINT [FK_Sucursales]
GO
ALTER TABLE [dbo].[Departamentos]  WITH CHECK ADD  CONSTRAINT [FK_Departamento_Paises_Pais_Id] FOREIGN KEY([Pais_Id])
REFERENCES [dbo].[Paises] ([Pais_id])
GO
ALTER TABLE [dbo].[Departamentos] CHECK CONSTRAINT [FK_Departamento_Paises_Pais_Id]
GO
ALTER TABLE [dbo].[Pantallas]  WITH CHECK ADD  CONSTRAINT [FK_Pantallas_Usuarios_Usuariocrea] FOREIGN KEY([Usuariocrea])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Pantallas] CHECK CONSTRAINT [FK_Pantallas_Usuarios_Usuariocrea]
GO
ALTER TABLE [dbo].[Pantallas]  WITH CHECK ADD  CONSTRAINT [FK_Pantallas_Usuarios_Usuariomodifica] FOREIGN KEY([Usuariomodifica])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Pantallas] CHECK CONSTRAINT [FK_Pantallas_Usuarios_Usuariomodifica]
GO
ALTER TABLE [dbo].[Pantallasroles]  WITH CHECK ADD  CONSTRAINT [FK_Pantallas_Roles_Pantallas_Pantalla_id] FOREIGN KEY([Pantalla_id])
REFERENCES [dbo].[Pantallas] ([Pantalla_id])
GO
ALTER TABLE [dbo].[Pantallasroles] CHECK CONSTRAINT [FK_Pantallas_Roles_Pantallas_Pantalla_id]
GO
ALTER TABLE [dbo].[Pantallasroles]  WITH CHECK ADD  CONSTRAINT [FK_Pantallas_Roles_Roles_Rol_id] FOREIGN KEY([Rol_id])
REFERENCES [dbo].[Roles] ([Rol_id])
GO
ALTER TABLE [dbo].[Pantallasroles] CHECK CONSTRAINT [FK_Pantallas_Roles_Roles_Rol_id]
GO
ALTER TABLE [dbo].[Personas]  WITH CHECK ADD  CONSTRAINT [FK_Personas_Ciudades_Ciudad_id] FOREIGN KEY([Ciudad_id])
REFERENCES [dbo].[Ciudades] ([Ciudad_id])
GO
ALTER TABLE [dbo].[Personas] CHECK CONSTRAINT [FK_Personas_Ciudades_Ciudad_id]
GO
ALTER TABLE [dbo].[Personas]  WITH CHECK ADD  CONSTRAINT [FK_Personas_Estadosciviles_Estadocivil_id] FOREIGN KEY([Estadocivil_id])
REFERENCES [dbo].[Estadosciviles] ([Estadocivil_id])
GO
ALTER TABLE [dbo].[Personas] CHECK CONSTRAINT [FK_Personas_Estadosciviles_Estadocivil_id]
GO
ALTER TABLE [dbo].[Personas]  WITH CHECK ADD  CONSTRAINT [FK_Personas_Usuarios_Usuariocrea] FOREIGN KEY([Usuariocrea])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Personas] CHECK CONSTRAINT [FK_Personas_Usuarios_Usuariocrea]
GO
ALTER TABLE [dbo].[Personas]  WITH CHECK ADD  CONSTRAINT [FK_Personas_Usuarios_Usuariomodifica] FOREIGN KEY([Usuariomodifica])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Personas] CHECK CONSTRAINT [FK_Personas_Usuarios_Usuariomodifica]
GO
ALTER TABLE [dbo].[Roles]  WITH CHECK ADD  CONSTRAINT [FK_Roles_Usuarios_Usuariocrea] FOREIGN KEY([Usuariocrea])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Roles] CHECK CONSTRAINT [FK_Roles_Usuarios_Usuariocrea]
GO
ALTER TABLE [dbo].[Roles]  WITH CHECK ADD  CONSTRAINT [FK_Roles_Usuarios_Usuariomodifica] FOREIGN KEY([Usuariomodifica])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Roles] CHECK CONSTRAINT [FK_Roles_Usuarios_Usuariomodifica]
GO
ALTER TABLE [dbo].[Serviciostransporte]  WITH CHECK ADD  CONSTRAINT [FK_Serviciostransporte_Usuarios_Usuariocrea] FOREIGN KEY([Usuariocrea])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Serviciostransporte] CHECK CONSTRAINT [FK_Serviciostransporte_Usuarios_Usuariocrea]
GO
ALTER TABLE [dbo].[Serviciostransporte]  WITH CHECK ADD  CONSTRAINT [FK_Serviciostransporte_Usuarios_Usuariomodifica] FOREIGN KEY([Usuariomodifica])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Serviciostransporte] CHECK CONSTRAINT [FK_Serviciostransporte_Usuarios_Usuariomodifica]
GO
ALTER TABLE [dbo].[Solicitudesviajes]  WITH CHECK ADD  CONSTRAINT [FK_SolicitudesViaje_Colaboradores_Colaborador_id] FOREIGN KEY([Colaborador_id])
REFERENCES [dbo].[Colaboradores] ([Colaborador_id])
GO
ALTER TABLE [dbo].[Solicitudesviajes] CHECK CONSTRAINT [FK_SolicitudesViaje_Colaboradores_Colaborador_id]
GO
ALTER TABLE [dbo].[Solicitudesviajes]  WITH CHECK ADD  CONSTRAINT [FK_SolicitudesViaje_EstadosSolicitud_Estadosolicitud_id] FOREIGN KEY([Estado_id])
REFERENCES [dbo].[Estados] ([Estado_id])
GO
ALTER TABLE [dbo].[Solicitudesviajes] CHECK CONSTRAINT [FK_SolicitudesViaje_EstadosSolicitud_Estadosolicitud_id]
GO
ALTER TABLE [dbo].[Solicitudesviajes]  WITH CHECK ADD  CONSTRAINT [FK_SolicitudesViaje_Usuarios_Usuariocrea_Usuariocrea] FOREIGN KEY([Usuariocrea])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Solicitudesviajes] CHECK CONSTRAINT [FK_SolicitudesViaje_Usuarios_Usuariocrea_Usuariocrea]
GO
ALTER TABLE [dbo].[Solicitudesviajes]  WITH CHECK ADD  CONSTRAINT [FK_SolicitudesViaje_Usuarios_Usuariomodifica_Usuariomodifica] FOREIGN KEY([Usuariomodifica])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Solicitudesviajes] CHECK CONSTRAINT [FK_SolicitudesViaje_Usuarios_Usuariomodifica_Usuariomodifica]
GO
ALTER TABLE [dbo].[Solicitudesviajes]  WITH CHECK ADD  CONSTRAINT [FK_SolicitudesViaje_Viajes_Viaje_id] FOREIGN KEY([Viaje_id])
REFERENCES [dbo].[Viajes] ([Viaje_id])
GO
ALTER TABLE [dbo].[Solicitudesviajes] CHECK CONSTRAINT [FK_SolicitudesViaje_Viajes_Viaje_id]
GO
ALTER TABLE [dbo].[Solicitudesviajes]  WITH CHECK ADD  CONSTRAINT [FK_Solicitudesviajes_Colaboradores_Supervisor_id] FOREIGN KEY([Supervisor_Id])
REFERENCES [dbo].[Colaboradores] ([Colaborador_id])
GO
ALTER TABLE [dbo].[Solicitudesviajes] CHECK CONSTRAINT [FK_Solicitudesviajes_Colaboradores_Supervisor_id]
GO
ALTER TABLE [dbo].[Sucursales]  WITH CHECK ADD  CONSTRAINT [FK_Sucursales_Colaboradores_Jefe_id] FOREIGN KEY([Jefe_id])
REFERENCES [dbo].[Colaboradores] ([Colaborador_id])
GO
ALTER TABLE [dbo].[Sucursales] CHECK CONSTRAINT [FK_Sucursales_Colaboradores_Jefe_id]
GO
ALTER TABLE [dbo].[Sucursales]  WITH CHECK ADD  CONSTRAINT [FK_Sucursales_Usuarios_Usuariocrea] FOREIGN KEY([Usuariocrea])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Sucursales] CHECK CONSTRAINT [FK_Sucursales_Usuarios_Usuariocrea]
GO
ALTER TABLE [dbo].[Sucursales]  WITH CHECK ADD  CONSTRAINT [FK_Sucursales_Usuarios_Usuariomodifica] FOREIGN KEY([Usuariomodifica])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Sucursales] CHECK CONSTRAINT [FK_Sucursales_Usuarios_Usuariomodifica]
GO
ALTER TABLE [dbo].[Transportistas]  WITH CHECK ADD  CONSTRAINT [FK_Transportistas_Personas_Persona_id] FOREIGN KEY([Persona_id])
REFERENCES [dbo].[Personas] ([Persona_id])
GO
ALTER TABLE [dbo].[Transportistas] CHECK CONSTRAINT [FK_Transportistas_Personas_Persona_id]
GO
ALTER TABLE [dbo].[Transportistas]  WITH CHECK ADD  CONSTRAINT [FK_Transportistas_Serviciostransporte] FOREIGN KEY([Serviciotransporte_id])
REFERENCES [dbo].[Serviciostransporte] ([Serviciotransporte_id])
GO
ALTER TABLE [dbo].[Transportistas] CHECK CONSTRAINT [FK_Transportistas_Serviciostransporte]
GO
ALTER TABLE [dbo].[Transportistas]  WITH CHECK ADD  CONSTRAINT [FK_Transportistas_Usuarios_Usuariocrea] FOREIGN KEY([Usuariocrea])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Transportistas] CHECK CONSTRAINT [FK_Transportistas_Usuarios_Usuariocrea]
GO
ALTER TABLE [dbo].[Transportistas]  WITH CHECK ADD  CONSTRAINT [FK_Transportistas_Usuarios_Usuariomodifica] FOREIGN KEY([Usuariomodifica])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Transportistas] CHECK CONSTRAINT [FK_Transportistas_Usuarios_Usuariomodifica]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Colaboradores_Colaborador_id] FOREIGN KEY([Colaborador_id])
REFERENCES [dbo].[Colaboradores] ([Colaborador_id])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Colaboradores_Colaborador_id]
GO
ALTER TABLE [dbo].[Valoracionesviajes]  WITH CHECK ADD  CONSTRAINT [FK_Valoracionviaje_Colaboradores_Colaborador_id] FOREIGN KEY([Colaborador_id])
REFERENCES [dbo].[Colaboradores] ([Colaborador_id])
GO
ALTER TABLE [dbo].[Valoracionesviajes] CHECK CONSTRAINT [FK_Valoracionviaje_Colaboradores_Colaborador_id]
GO
ALTER TABLE [dbo].[Valoracionesviajes]  WITH CHECK ADD  CONSTRAINT [FK_Valoracionviaje_Viajes_Viaje_id] FOREIGN KEY([Viaje_id])
REFERENCES [dbo].[Viajes] ([Viaje_id])
GO
ALTER TABLE [dbo].[Valoracionesviajes] CHECK CONSTRAINT [FK_Valoracionviaje_Viajes_Viaje_id]
GO
ALTER TABLE [dbo].[Viajes]  WITH CHECK ADD FOREIGN KEY([Sucursal_id])
REFERENCES [dbo].[Sucursales] ([Sucursal_id])
GO
ALTER TABLE [dbo].[Viajes]  WITH CHECK ADD FOREIGN KEY([Transportista_id])
REFERENCES [dbo].[Transportistas] ([Transportista_id])
GO
ALTER TABLE [dbo].[Viajes]  WITH CHECK ADD  CONSTRAINT [FK_Transportista_Monedas_Moneda_Id] FOREIGN KEY([Moneda_Id])
REFERENCES [dbo].[Monedas] ([Moneda_Id])
GO
ALTER TABLE [dbo].[Viajes] CHECK CONSTRAINT [FK_Transportista_Monedas_Moneda_Id]
GO
ALTER TABLE [dbo].[Viajes]  WITH CHECK ADD  CONSTRAINT [FK_Viajes_Estados_Estado_id] FOREIGN KEY([Estado_id])
REFERENCES [dbo].[Estados] ([Estado_id])
GO
ALTER TABLE [dbo].[Viajes] CHECK CONSTRAINT [FK_Viajes_Estados_Estado_id]
GO
ALTER TABLE [dbo].[Viajes]  WITH CHECK ADD  CONSTRAINT [FK_Viajes_Monedas_Moneda_Id] FOREIGN KEY([Moneda_Id])
REFERENCES [dbo].[Monedas] ([Moneda_Id])
GO
ALTER TABLE [dbo].[Viajes] CHECK CONSTRAINT [FK_Viajes_Monedas_Moneda_Id]
GO
ALTER TABLE [dbo].[Viajes]  WITH CHECK ADD  CONSTRAINT [FK_Viajes_Usuarios_Usuariocrea] FOREIGN KEY([Usuariocrea])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Viajes] CHECK CONSTRAINT [FK_Viajes_Usuarios_Usuariocrea]
GO
ALTER TABLE [dbo].[Viajes]  WITH CHECK ADD  CONSTRAINT [FK_Viajes_Usuarios_Usuariomodifica] FOREIGN KEY([Usuariomodifica])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Viajes] CHECK CONSTRAINT [FK_Viajes_Usuarios_Usuariomodifica]
GO
ALTER TABLE [dbo].[Viajesdetalles]  WITH CHECK ADD FOREIGN KEY([Viaje_id])
REFERENCES [dbo].[Viajes] ([Viaje_id])
GO
ALTER TABLE [dbo].[Viajesdetalles]  WITH CHECK ADD  CONSTRAINT [FK_Viajesdetalles_Colaboradores_por_sucursales_Colaboradorsucursal_id] FOREIGN KEY([Colaboradorsucursal_id])
REFERENCES [dbo].[Colaboradoressucursales] ([Colaboradorsucursal_id])
GO
ALTER TABLE [dbo].[Viajesdetalles] CHECK CONSTRAINT [FK_Viajesdetalles_Colaboradores_por_sucursales_Colaboradorsucursal_id]
GO
ALTER TABLE [dbo].[Viajesdetalles]  WITH CHECK ADD  CONSTRAINT [FK_Viajesdetalles_Monedas_Moneda_Id] FOREIGN KEY([Moneda_Id])
REFERENCES [dbo].[Monedas] ([Moneda_Id])
GO
ALTER TABLE [dbo].[Viajesdetalles] CHECK CONSTRAINT [FK_Viajesdetalles_Monedas_Moneda_Id]
GO
ALTER TABLE [dbo].[Viajesdetalles]  WITH CHECK ADD  CONSTRAINT [FK_Viajesdetalles_Usuarios_Usuariocrea] FOREIGN KEY([Usuariocrea])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Viajesdetalles] CHECK CONSTRAINT [FK_Viajesdetalles_Usuarios_Usuariocrea]
GO
ALTER TABLE [dbo].[Viajesdetalles]  WITH CHECK ADD  CONSTRAINT [FK_Viajesdetalles_Usuarios_Usuariomodifica] FOREIGN KEY([Usuariomodifica])
REFERENCES [dbo].[Usuarios] ([Usuario_id])
GO
ALTER TABLE [dbo].[Viajesdetalles] CHECK CONSTRAINT [FK_Viajesdetalles_Usuarios_Usuariomodifica]
GO
ALTER TABLE [dbo].[Colaboradoressucursales]  WITH CHECK ADD CHECK  (([Distanciakilometro]>(0) AND [Distanciakilometro]<=(50)))
GO
ALTER TABLE [dbo].[Personas]  WITH CHECK ADD  CONSTRAINT [chk_Sexo] CHECK  (([Sexo]='M' OR [Sexo]='F'))
GO
ALTER TABLE [dbo].[Personas] CHECK CONSTRAINT [chk_Sexo]
GO
ALTER TABLE [dbo].[Viajesdetalles]  WITH CHECK ADD  CONSTRAINT [chk_distancia] CHECK  (([Distanciakilometros]<=(50)))
GO
ALTER TABLE [dbo].[Viajesdetalles] CHECK CONSTRAINT [chk_distancia]
GO
ALTER TABLE [dbo].[Viajesdetalles]  WITH CHECK ADD CHECK  (([Distanciakilometros]>(0) AND [Distanciakilometros]<=(50)))
GO
