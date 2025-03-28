USE [HomeJourney]
GO
/****** Object:  Table [dbo].[Cargos]    Script Date: 26/2/2025 08:40:25 ******/
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
/****** Object:  Table [dbo].[Ciudades]    Script Date: 26/2/2025 08:40:26 ******/
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
/****** Object:  Table [dbo].[Colaboradores]    Script Date: 26/2/2025 08:40:26 ******/
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
/****** Object:  Table [dbo].[Colaboradoressucursales]    Script Date: 26/2/2025 08:40:26 ******/
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
/****** Object:  Table [dbo].[Departamentos]    Script Date: 26/2/2025 08:40:26 ******/
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
/****** Object:  Table [dbo].[Estados]    Script Date: 26/2/2025 08:40:26 ******/
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
/****** Object:  Table [dbo].[Estadosciviles]    Script Date: 26/2/2025 08:40:26 ******/
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
/****** Object:  Table [dbo].[Monedas]    Script Date: 26/2/2025 08:40:26 ******/
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
/****** Object:  Table [dbo].[Paises]    Script Date: 26/2/2025 08:40:26 ******/
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
/****** Object:  Table [dbo].[Pantallas]    Script Date: 26/2/2025 08:40:26 ******/
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
/****** Object:  Table [dbo].[Pantallasroles]    Script Date: 26/2/2025 08:40:26 ******/
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
/****** Object:  Table [dbo].[Personas]    Script Date: 26/2/2025 08:40:26 ******/
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
/****** Object:  Table [dbo].[Roles]    Script Date: 26/2/2025 08:40:26 ******/
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
/****** Object:  Table [dbo].[Serviciostransporte]    Script Date: 26/2/2025 08:40:26 ******/
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
/****** Object:  Table [dbo].[Solicitudesviajes]    Script Date: 26/2/2025 08:40:26 ******/
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
 CONSTRAINT [PK_Solicitudesviajes_Solicitudviaje_id] PRIMARY KEY CLUSTERED 
(
	[Solicitudviaje_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sucursales]    Script Date: 26/2/2025 08:40:26 ******/
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
 CONSTRAINT [PK_Sucursales_Sucursal_id] PRIMARY KEY CLUSTERED 
(
	[Sucursal_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transportistas]    Script Date: 26/2/2025 08:40:26 ******/
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
/****** Object:  Table [dbo].[Usuarios]    Script Date: 26/2/2025 08:40:26 ******/
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
/****** Object:  Table [dbo].[Valoracionesviajes]    Script Date: 26/2/2025 08:40:26 ******/
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
/****** Object:  Table [dbo].[Viajes]    Script Date: 26/2/2025 08:40:26 ******/
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
/****** Object:  Table [dbo].[Viajesdetalles]    Script Date: 26/2/2025 08:40:26 ******/
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

INSERT [dbo].[Cargos] ([Cargo_id], [Nombre]) VALUES (1, N'Admin')
INSERT [dbo].[Cargos] ([Cargo_id], [Nombre]) VALUES (2, N'Gerente Tienda')
INSERT [dbo].[Cargos] ([Cargo_id], [Nombre]) VALUES (3, N'Colaborador')
SET IDENTITY_INSERT [dbo].[Cargos] OFF
GO
SET IDENTITY_INSERT [dbo].[Ciudades] ON 

INSERT [dbo].[Ciudades] ([Ciudad_id], [Nombre], [Departamento_id], [Activo]) VALUES (1, N'San Pedro Sula', 1, 1)
SET IDENTITY_INSERT [dbo].[Ciudades] OFF
GO
SET IDENTITY_INSERT [dbo].[Colaboradores] ON 

INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (1, 1, 1, 1, 1, N'Honduras, Cortes, Col. La Pradera', 1, CAST(N'2025-02-20T00:00:00.000' AS DateTime), NULL, NULL, CAST(15.475127414100966 AS Decimal(19, 15)), CAST(-87.980954318082200 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (3, 6, 2, 2, 1, N'Hondruas, Cortes,La Lima', 1, CAST(N'2025-02-24T16:24:50.073' AS DateTime), NULL, NULL, CAST(15.435838366425624 AS Decimal(19, 15)), CAST(-87.935765635830600 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (5, 12, 2, 2, 1, N'Hondruas, Cortes,La Lima', 1, CAST(N'2025-02-25T14:42:40.137' AS DateTime), NULL, NULL, CAST(15.437985340382951 AS Decimal(19, 15)), CAST(-87.908503144171100 AS Decimal(19, 15)))
INSERT [dbo].[Colaboradores] ([Colaborador_id], [Persona_id], [Rol_id], [Cargo_id], [Activo], [Direccion], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (6, 13, 2, 2, 1, N'Hondruas, Cortes, San Pedro Sula', 1, CAST(N'2025-02-25T15:00:58.240' AS DateTime), NULL, NULL, CAST(15.582178244098625 AS Decimal(19, 15)), CAST(-88.023745412278590 AS Decimal(19, 15)))
SET IDENTITY_INSERT [dbo].[Colaboradores] OFF
GO
SET IDENTITY_INSERT [dbo].[Colaboradoressucursales] ON 

INSERT [dbo].[Colaboradoressucursales] ([Colaboradorsucursal_id], [Colaborador_id], [Sucursal_id], [Distanciakilometro], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (2, 1, 1, CAST(7.70 AS Decimal(5, 2)), 1, 1, CAST(N'2025-02-25T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Colaboradoressucursales] ([Colaboradorsucursal_id], [Colaborador_id], [Sucursal_id], [Distanciakilometro], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (3, 3, 1, CAST(15.10 AS Decimal(5, 2)), 1, 1, CAST(N'2025-02-25T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Colaboradoressucursales] ([Colaboradorsucursal_id], [Colaborador_id], [Sucursal_id], [Distanciakilometro], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (4, 5, 1, CAST(19.70 AS Decimal(5, 2)), 1, 1, CAST(N'2025-02-25T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Colaboradoressucursales] ([Colaboradorsucursal_id], [Colaborador_id], [Sucursal_id], [Distanciakilometro], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (5, 6, 1, CAST(12.60 AS Decimal(5, 2)), 1, 1, CAST(N'2025-02-25T00:00:00.000' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Colaboradoressucursales] OFF
GO
SET IDENTITY_INSERT [dbo].[Departamentos] ON 

INSERT [dbo].[Departamentos] ([Departamento_id], [Nombre], [Activo], [Pais_Id]) VALUES (1, N'Cortes', 1, 1)
SET IDENTITY_INSERT [dbo].[Departamentos] OFF
GO
SET IDENTITY_INSERT [dbo].[Estados] ON 

INSERT [dbo].[Estados] ([Estado_id], [Nombre], [Descripcion]) VALUES (1, N'SOLICITUD APROBADA', N'solicitud de viaje aprobada')
INSERT [dbo].[Estados] ([Estado_id], [Nombre], [Descripcion]) VALUES (2, N'SOLICITUD RECHAZADA', N'Solicitud de viaje rechazada por supervisor.')
INSERT [dbo].[Estados] ([Estado_id], [Nombre], [Descripcion]) VALUES (3, N'VIAJE EN PROCESO', N'Viaje estado en proceso.')
INSERT [dbo].[Estados] ([Estado_id], [Nombre], [Descripcion]) VALUES (4, N'VIAJE CERRADO', N'Viaje estado cerrado, no se pueden agregar mas empleados.')
SET IDENTITY_INSERT [dbo].[Estados] OFF
GO
SET IDENTITY_INSERT [dbo].[Estadosciviles] ON 

INSERT [dbo].[Estadosciviles] ([Estadocivil_id], [Nombre]) VALUES (1, N'Casado(A)')
INSERT [dbo].[Estadosciviles] ([Estadocivil_id], [Nombre]) VALUES (2, N'Soltero(A)')
INSERT [dbo].[Estadosciviles] ([Estadocivil_id], [Nombre]) VALUES (3, N'Viudo(A)')
SET IDENTITY_INSERT [dbo].[Estadosciviles] OFF
GO
SET IDENTITY_INSERT [dbo].[Monedas] ON 

INSERT [dbo].[Monedas] ([Moneda_Id], [Nombre], [Simbolo], [ValorLempiras]) VALUES (1, N'Lempira', N'L', 1.0000)
SET IDENTITY_INSERT [dbo].[Monedas] OFF
GO
SET IDENTITY_INSERT [dbo].[Paises] ON 

INSERT [dbo].[Paises] ([Pais_id], [Nombre], [Activo]) VALUES (1, N'Honduras', 1)
INSERT [dbo].[Paises] ([Pais_id], [Nombre], [Activo]) VALUES (4, N'Niger', 1)
INSERT [dbo].[Paises] ([Pais_id], [Nombre], [Activo]) VALUES (5, N'Uruguay', 1)
INSERT [dbo].[Paises] ([Pais_id], [Nombre], [Activo]) VALUES (6, N'Guatemala', 1)
INSERT [dbo].[Paises] ([Pais_id], [Nombre], [Activo]) VALUES (9, N'Costa Rica', 1)
INSERT [dbo].[Paises] ([Pais_id], [Nombre], [Activo]) VALUES (10, N'Madian', 0)
INSERT [dbo].[Paises] ([Pais_id], [Nombre], [Activo]) VALUES (12, N'Nicaragua', 1)
INSERT [dbo].[Paises] ([Pais_id], [Nombre], [Activo]) VALUES (13, N'Panama', 1)
INSERT [dbo].[Paises] ([Pais_id], [Nombre], [Activo]) VALUES (14, N'Mexico', 1)
INSERT [dbo].[Paises] ([Pais_id], [Nombre], [Activo]) VALUES (15, N'Colombia', 1)
INSERT [dbo].[Paises] ([Pais_id], [Nombre], [Activo]) VALUES (17, N'Argentina', 1)
SET IDENTITY_INSERT [dbo].[Paises] OFF
GO
SET IDENTITY_INSERT [dbo].[Personas] ON 

INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (1, N'Jason Jeremy', N'Villanueva Sanchez', N'M', N'jasonjeremy48jh@gmail.com', N'0501200403104', 1, 2, 1, 1, CAST(N'2025-02-20T00:00:00.000' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (6, N'Dany Allain', N'Franco Ortega', N'F', N'danyfranco@gmail.com', N'0501200403102', 1, 1, 1, 1, CAST(N'2025-02-24T16:24:49.663' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (12, N'Madian Alejandro', N'Reyes Velasquez', N'M', N'madianreyes@gmail.com', N'0502200500487', 1, 1, 1, 1, CAST(N'2025-02-25T14:42:40.120' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (13, N'Dania Cristel', N'Hernandez Hernandez', N'M', N'daniahernandez@gmail.com', N'0501200400458', 1, 1, 1, 1, CAST(N'2025-02-25T15:00:58.210' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (14, N'Chalino Josefino', N'Lopez Arriaga', N'M', N'Chalino@gmail.com', N'0501199705203', 0, NULL, 1, 1, CAST(N'2025-02-25T16:00:31.940' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (15, N'Angel Jose', N'Lopez Villalgar', N'M', N'Angelo@gmail.com', N'0501199801252', 0, NULL, 1, 1, CAST(N'2025-02-25T16:01:31.177' AS DateTime), NULL, NULL)
INSERT [dbo].[Personas] ([Persona_id], [Nombre], [Apelllido], [Sexo], [Email], [Documentonacionalidentificacion], [Activo], [Estadocivil_id], [Ciudad_id], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica]) VALUES (16, N'Juan Pancho', N'Perez Rivera', N'M', N'PanchoR@gmail.com', N'0501199601872', 0, NULL, 1, 1, CAST(N'2025-02-25T16:02:37.290' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[Personas] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([Rol_id], [Nombre], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Activo]) VALUES (1, N'SysAdmin', 1, CAST(N'2025-02-20T00:00:00.000' AS DateTime), NULL, NULL, 1)
INSERT [dbo].[Roles] ([Rol_id], [Nombre], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Activo]) VALUES (2, N'Gerente Tienda', 1, CAST(N'2025-02-24T00:00:00.000' AS DateTime), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[Serviciostransporte] ON 

INSERT [dbo].[Serviciostransporte] ([Serviciotransporte_id], [Nombre], [Descripcion], [Email], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Activo]) VALUES (3, N'Transportes Cholomeño', N'Transporte para empleados', N'cholomeño@gmail.com', 1, CAST(N'2024-02-24T00:00:00.000' AS DateTime), NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Serviciostransporte] OFF
GO
SET IDENTITY_INSERT [dbo].[Sucursales] ON 

INSERT [dbo].[Sucursales] ([Sucursal_id], [Nombre], [Direccion], [Activo], [Usuariocrea], [Fechacrea], [Usuariomodifica], [Fechamodifica], [Latitud], [Longitud]) VALUES (1, N'Sucursal Benque ', N'Barrio el Benque', 1, 1, CAST(N'2025-02-25T00:00:00.000' AS DateTime), NULL, NULL, CAST(15.502724055354715 AS Decimal(19, 15)), CAST(-88.027061317370450 AS Decimal(19, 15)))
SET IDENTITY_INSERT [dbo].[Sucursales] OFF
GO
SET IDENTITY_INSERT [dbo].[Transportistas] ON 

INSERT [dbo].[Transportistas] ([Transportista_id], [Serviciotransporte_id], [Tarifaporkilometro], [Activo], [Usuariocrea], [Fechacrea], [Persona_id], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (1, 3, CAST(25.00 AS Decimal(10, 2)), 0, 1, CAST(N'2025-02-25T16:00:32.373' AS DateTime), 14, NULL, NULL, 1)
INSERT [dbo].[Transportistas] ([Transportista_id], [Serviciotransporte_id], [Tarifaporkilometro], [Activo], [Usuariocrea], [Fechacrea], [Persona_id], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (2, 3, CAST(25.00 AS Decimal(10, 2)), 0, 1, CAST(N'2025-02-25T16:01:31.193' AS DateTime), 15, NULL, NULL, 1)
INSERT [dbo].[Transportistas] ([Transportista_id], [Serviciotransporte_id], [Tarifaporkilometro], [Activo], [Usuariocrea], [Fechacrea], [Persona_id], [Usuariomodifica], [Fechamodifica], [Moneda_Id]) VALUES (3, 3, CAST(24.00 AS Decimal(10, 2)), 0, 1, CAST(N'2025-02-25T16:02:37.307' AS DateTime), 16, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[Transportistas] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([Usuario_id], [Username], [Passwordhash], [Colaborador_id], [Esadmin], [Activo]) VALUES (1, N'pshkin', 0xA665A45920422F9D417E4867EFDC4FB8A04A1F3FFF1FA07E998E86F7F7A27AE3, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
/****** Object:  Index [UQ_Colaboradores_Persona_id]    Script Date: 26/2/2025 08:40:26 ******/
ALTER TABLE [dbo].[Colaboradores] ADD  CONSTRAINT [UQ_Colaboradores_Persona_id] UNIQUE NONCLUSTERED 
(
	[Persona_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_ColaboradorSucursal]    Script Date: 26/2/2025 08:40:26 ******/
ALTER TABLE [dbo].[Colaboradoressucursales] ADD  CONSTRAINT [UQ_ColaboradorSucursal] UNIQUE NONCLUSTERED 
(
	[Colaborador_id] ASC,
	[Sucursal_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Personas__A9D10534B64E690E]    Script Date: 26/2/2025 08:40:26 ******/
ALTER TABLE [dbo].[Personas] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Personas__A9D10534EF0F0958]    Script Date: 26/2/2025 08:40:26 ******/
ALTER TABLE [dbo].[Personas] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Servicio__A9D105340503BCB2]    Script Date: 26/2/2025 08:40:26 ******/
ALTER TABLE [dbo].[Serviciostransporte] ADD UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_Transportistas_Persona_id]    Script Date: 26/2/2025 08:40:26 ******/
ALTER TABLE [dbo].[Transportistas] ADD  CONSTRAINT [UQ_Transportistas_Persona_id] UNIQUE NONCLUSTERED 
(
	[Persona_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ__Usuarios__46FD4C9B3B5A1C28]    Script Date: 26/2/2025 08:40:26 ******/
ALTER TABLE [dbo].[Usuarios] ADD UNIQUE NONCLUSTERED 
(
	[Colaborador_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__Usuarios__536C85E429221941]    Script Date: 26/2/2025 08:40:26 ******/
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
