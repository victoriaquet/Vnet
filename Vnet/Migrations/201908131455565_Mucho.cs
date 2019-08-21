namespace Vnet.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Mucho : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ArchivoEnvios",
                c => new
                    {
                        IdArchivoEnvio = c.Int(nullable: false, identity: true),
                        Numero = c.Int(nullable: false),
                        Fecha = c.DateTime(),
                        FechaCreacion = c.DateTime(),
                        FechaRespuesta = c.DateTime(),
                        Archivo = c.String(),
                        CantidadRegistros = c.Int(),
                        ImporteTotal = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.IdArchivoEnvio);
            
            CreateTable(
                "dbo.ArchivoImpresionTCs",
                c => new
                    {
                        IdArchivoImpresionTC = c.Int(nullable: false, identity: true),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        NombreArchivo = c.String(maxLength: 35),
                        Observacion = c.String(maxLength: 300),
                        Responsable = c.String(),
                        ArchivoTxt = c.Binary(),
                    })
                .PrimaryKey(t => t.IdArchivoImpresionTC);
            
            CreateTable(
                "dbo.TCporArchivoes",
                c => new
                    {
                        IdArchivoImpresionTC = c.Int(nullable: false),
                        IdTarjetaClub = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.IdArchivoImpresionTC, t.IdTarjetaClub })
                .ForeignKey("dbo.ArchivoImpresionTCs", t => t.IdArchivoImpresionTC, cascadeDelete: true)
                .ForeignKey("dbo.TarjetaClubs", t => t.IdTarjetaClub, cascadeDelete: true)
                .Index(t => t.IdArchivoImpresionTC)
                .Index(t => t.IdTarjetaClub);
            
            CreateTable(
                "dbo.TarjetaClubs",
                c => new
                    {
                        IdTarjetaClub = c.Int(nullable: false, identity: true),
                        Nombres = c.String(maxLength: 35),
                        Apellido = c.String(maxLength: 35),
                        DNI = c.Int(),
                        Email = c.String(),
                        TelefonoFijoNumero = c.String(),
                        TelefonoFijoArea = c.String(),
                        TelefonoMovilNumero = c.String(),
                        TelefonoMovilArea = c.String(),
                        FechaNacimiento = c.DateTime(),
                        IdSuscripcion = c.Int(nullable: false),
                        Numero = c.Long(nullable: false),
                        Precio = c.Decimal(precision: 18, scale: 2),
                        EstadoTarjetaClub = c.Int(nullable: false),
                        FechaSolicitud = c.DateTime(),
                        FechaSolicitudImpresion = c.DateTime(),
                        FechaGeneracionArchivo = c.DateTime(),
                        FechaImpresion = c.DateTime(),
                        FechaParaEntregar = c.DateTime(),
                        FechaEntrega = c.DateTime(),
                        TipoTarjetaClub = c.Int(nullable: false),
                        UbicacionTarjetaClub = c.Int(nullable: false),
                        TipoSexo = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdTarjetaClub)
                .ForeignKey("dbo.Suscripcions", t => t.IdSuscripcion, cascadeDelete: true)
                .Index(t => t.IdSuscripcion);
            
            CreateTable(
                "dbo.Suscripcions",
                c => new
                    {
                        IdSuscripcion = c.Int(nullable: false, identity: true),
                        FechaAlta = c.DateTime(nullable: false),
                        FechaSolicitudBaja = c.DateTime(),
                        FechaModificacion = c.DateTime(),
                        NumeroSuscripcion = c.Int(nullable: false),
                        IdOferta = c.Int(),
                        IdSuscriptor = c.Int(nullable: false),
                        IdCanillita = c.Int(),
                        TipoSuscripcion = c.Int(nullable: false),
                        Lunes = c.Boolean(nullable: false),
                        Martes = c.Boolean(nullable: false),
                        Miercoles = c.Boolean(nullable: false),
                        Jueves = c.Boolean(nullable: false),
                        Viernes = c.Boolean(nullable: false),
                        Sabado = c.Boolean(nullable: false),
                        Domingo = c.Boolean(nullable: false),
                        IdDatosFacturacion = c.Int(),
                        PrecioSuscripcion = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CantTarjetasExtras = c.Int(nullable: false),
                        TipoMedioPagoId = c.Int(),
                        TipoPeriodoEfectivo_PeriodoPagoEfectivoId = c.Int(),
                        UsuarioAlta_UserID = c.String(maxLength: 128),
                        UsuarioModificacion_UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdSuscripcion)
                .ForeignKey("dbo.Canillitas", t => t.IdCanillita)
                .ForeignKey("dbo.DatosFacturacions", t => t.IdDatosFacturacion)
                .ForeignKey("dbo.Ofertas", t => t.IdOferta)
                .ForeignKey("dbo.Suscriptors", t => t.IdSuscriptor, cascadeDelete: true)
                .ForeignKey("dbo.TipoMedioPagoes", t => t.TipoMedioPagoId)
                .ForeignKey("dbo.TipoPeriodoEfectivoes", t => t.TipoPeriodoEfectivo_PeriodoPagoEfectivoId)
                .ForeignKey("dbo.UserViews", t => t.UsuarioAlta_UserID)
                .ForeignKey("dbo.UserViews", t => t.UsuarioModificacion_UserID)
                .Index(t => t.IdOferta)
                .Index(t => t.IdSuscriptor)
                .Index(t => t.IdCanillita)
                .Index(t => t.IdDatosFacturacion)
                .Index(t => t.TipoMedioPagoId)
                .Index(t => t.TipoPeriodoEfectivo_PeriodoPagoEfectivoId)
                .Index(t => t.UsuarioAlta_UserID)
                .Index(t => t.UsuarioModificacion_UserID);
            
            CreateTable(
                "dbo.Canillitas",
                c => new
                    {
                        IdCanillita = c.Int(nullable: false, identity: true),
                        NroCanillita = c.Int(),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        NroTel = c.String(),
                        Activo = c.Boolean(nullable: false),
                        IdLocalidad = c.Int(),
                        IdDistribuidor = c.Int(),
                    })
                .PrimaryKey(t => t.IdCanillita)
                .ForeignKey("dbo.Distribuidors", t => t.IdDistribuidor)
                .ForeignKey("dbo.Localidads", t => t.IdLocalidad)
                .Index(t => t.IdLocalidad)
                .Index(t => t.IdDistribuidor);
            
            CreateTable(
                "dbo.DiaEntregas",
                c => new
                    {
                        IdDiaEntrega = c.Int(nullable: false, identity: true),
                        NombreDia = c.Int(nullable: false),
                        Habilitado = c.Boolean(nullable: false),
                        IdCanillita = c.Int(),
                        IdDomicilio = c.Int(nullable: false),
                        HorarioEntrega = c.String(),
                        Observacion = c.String(),
                        IdSuscripcion = c.Int(),
                        SuscriptorSecundario_IdSuscriptorSecundario = c.Int(),
                    })
                .PrimaryKey(t => t.IdDiaEntrega)
                .ForeignKey("dbo.Canillitas", t => t.IdCanillita)
                .ForeignKey("dbo.Domicilios", t => t.IdDomicilio, cascadeDelete: true)
                .ForeignKey("dbo.Suscripcions", t => t.IdSuscripcion)
                .ForeignKey("dbo.SuscriptorSecundarios", t => t.SuscriptorSecundario_IdSuscriptorSecundario)
                .Index(t => t.IdCanillita)
                .Index(t => t.IdDomicilio)
                .Index(t => t.IdSuscripcion)
                .Index(t => t.SuscriptorSecundario_IdSuscriptorSecundario);
            
            CreateTable(
                "dbo.Domicilios",
                c => new
                    {
                        IdDomicilio = c.Int(nullable: false, identity: true),
                        Calle = c.String(),
                        Altura = c.Int(),
                        Piso = c.String(),
                        Departamento = c.String(),
                        CalleLateral1 = c.String(),
                        CalleLateral2 = c.String(),
                        Barrio = c.String(),
                        IdLocalidad = c.Int(nullable: false),
                        Observaciones = c.String(),
                        TituloDom = c.String(),
                    })
                .PrimaryKey(t => t.IdDomicilio)
                .ForeignKey("dbo.Localidads", t => t.IdLocalidad, cascadeDelete: true)
                .Index(t => t.IdLocalidad);
            
            CreateTable(
                "dbo.Localidads",
                c => new
                    {
                        IdLocalidad = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Codigo = c.String(),
                        SoloParaPublicidad = c.Boolean(nullable: false),
                        CodPostal = c.String(),
                        IdProvincia = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdLocalidad)
                .ForeignKey("dbo.Provincias", t => t.IdProvincia, cascadeDelete: true)
                .Index(t => t.IdProvincia);
            
            CreateTable(
                "dbo.Provincias",
                c => new
                    {
                        IdProvincia = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        SoloParaPublicidad = c.Boolean(nullable: false),
                        SoloParaSuscripcion = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IdProvincia);
            
            CreateTable(
                "dbo.Distribuidors",
                c => new
                    {
                        IdDistribuidor = c.Int(nullable: false, identity: true),
                        Codigo = c.String(),
                        Nombre = c.String(),
                        NombreLocalidad = c.String(),
                        IdLocalidad = c.Int(),
                    })
                .PrimaryKey(t => t.IdDistribuidor)
                .ForeignKey("dbo.Localidads", t => t.IdLocalidad)
                .Index(t => t.IdLocalidad);
            
            CreateTable(
                "dbo.HojaDeRutas",
                c => new
                    {
                        IdHojaDeRuta = c.Int(nullable: false, identity: true),
                        IdCanillita = c.Int(nullable: false),
                        FechaEntrega = c.DateTime(nullable: false),
                        FechaGeneracion = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IdHojaDeRuta)
                .ForeignKey("dbo.Canillitas", t => t.IdCanillita, cascadeDelete: true)
                .Index(t => t.IdCanillita);
            
            CreateTable(
                "dbo.Lineas",
                c => new
                    {
                        IdLinea = c.Int(nullable: false, identity: true),
                        NumeroOrden = c.Int(nullable: false),
                        NumeroSuscriptor = c.Int(nullable: false),
                        NombreSuscriptor = c.String(),
                        Domicilio = c.String(),
                        Localidad = c.String(),
                        Lunes = c.Boolean(nullable: false),
                        Martes = c.Boolean(nullable: false),
                        Miercoles = c.Boolean(nullable: false),
                        Jueves = c.Boolean(nullable: false),
                        Viernes = c.Boolean(nullable: false),
                        Sabado = c.Boolean(nullable: false),
                        Domingo = c.Boolean(nullable: false),
                        PrimeraEntrega = c.Boolean(nullable: false),
                        Observacion = c.String(),
                        CodigoProducto = c.String(),
                        NombreProducto = c.String(),
                        NumeroSuscripcion = c.String(),
                        ObservacionLimpia = c.String(),
                        UltimaEntrega = c.Boolean(nullable: false),
                        IdHojaDeRuta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdLinea)
                .ForeignKey("dbo.HojaDeRutas", t => t.IdHojaDeRuta, cascadeDelete: true)
                .Index(t => t.IdHojaDeRuta);
            
            CreateTable(
                "dbo.SuscripcionHojaDeRutas",
                c => new
                    {
                        IdSuscripcionHojaDeRuta = c.Int(nullable: false, identity: true),
                        IdSuscripcion = c.Int(nullable: false),
                        IdHojaDeRuta = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdSuscripcionHojaDeRuta)
                .ForeignKey("dbo.HojaDeRutas", t => t.IdHojaDeRuta, cascadeDelete: true)
                .ForeignKey("dbo.Suscripcions", t => t.IdSuscripcion, cascadeDelete: true)
                .Index(t => t.IdSuscripcion)
                .Index(t => t.IdHojaDeRuta);
            
            CreateTable(
                "dbo.Casoes",
                c => new
                    {
                        IdCaso = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Observacion = c.String(),
                        FechaCarga = c.DateTime(nullable: false),
                        FechaCierre = c.DateTime(),
                        FechaVencimiento = c.DateTime(),
                        IdAsuntoCaso = c.Int(nullable: false),
                        IdEstadoCaso = c.Int(nullable: false),
                        IdSuscripcion = c.Int(nullable: false),
                        IdTipoCaso = c.Int(nullable: false),
                        IdCanillita = c.Int(),
                        IdMotivoCierreCaso = c.Int(),
                        UsuarioCarga = c.String(),
                    })
                .PrimaryKey(t => t.IdCaso)
                .ForeignKey("dbo.AsuntoCasoes", t => t.IdAsuntoCaso, cascadeDelete: true)
                .ForeignKey("dbo.Canillitas", t => t.IdCanillita)
                .ForeignKey("dbo.EstadoCasoes", t => t.IdEstadoCaso, cascadeDelete: true)
                .ForeignKey("dbo.MotivoCierreCasoes", t => t.IdMotivoCierreCaso)
                .ForeignKey("dbo.Suscripcions", t => t.IdSuscripcion, cascadeDelete: true)
                .ForeignKey("dbo.TipoCasoes", t => t.IdTipoCaso, cascadeDelete: true)
                .Index(t => t.IdAsuntoCaso)
                .Index(t => t.IdEstadoCaso)
                .Index(t => t.IdSuscripcion)
                .Index(t => t.IdTipoCaso)
                .Index(t => t.IdCanillita)
                .Index(t => t.IdMotivoCierreCaso);
            
            CreateTable(
                "dbo.AsuntoCasoes",
                c => new
                    {
                        IdAsuntoCaso = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        IdAreaCaso = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdAsuntoCaso)
                .ForeignKey("dbo.AreaCasoes", t => t.IdAreaCaso, cascadeDelete: true)
                .Index(t => t.IdAreaCaso);
            
            CreateTable(
                "dbo.AreaCasoes",
                c => new
                    {
                        IdAreaCaso = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.IdAreaCaso);
            
            CreateTable(
                "dbo.DiaDevolucionCasoes",
                c => new
                    {
                        IdDiaDevolucionCaso = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(),
                        Observaciones = c.String(),
                        UsuarioModificacion = c.String(),
                        FechaModificacion = c.DateTime(),
                        Caso_IdCaso = c.Int(),
                    })
                .PrimaryKey(t => t.IdDiaDevolucionCaso)
                .ForeignKey("dbo.Casoes", t => t.Caso_IdCaso)
                .Index(t => t.Caso_IdCaso);
            
            CreateTable(
                "dbo.EstadoCasoes",
                c => new
                    {
                        IdEstadoCaso = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Color = c.String(),
                    })
                .PrimaryKey(t => t.IdEstadoCaso);
            
            CreateTable(
                "dbo.HistorialCasoes",
                c => new
                    {
                        IdHistorialCaso = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(),
                        UsuarioModificacion = c.String(),
                        ObservacionesHistorial = c.String(),
                        LogTxt = c.String(),
                        FechaCarga = c.DateTime(nullable: false),
                        Caso_IdCaso = c.Int(),
                        EstadoCasoHistorial_IdEstadoCaso = c.Int(),
                    })
                .PrimaryKey(t => t.IdHistorialCaso)
                .ForeignKey("dbo.Casoes", t => t.Caso_IdCaso)
                .ForeignKey("dbo.EstadoCasoes", t => t.EstadoCasoHistorial_IdEstadoCaso)
                .Index(t => t.Caso_IdCaso)
                .Index(t => t.EstadoCasoHistorial_IdEstadoCaso);
            
            CreateTable(
                "dbo.MotivoCierreCasoes",
                c => new
                    {
                        IdMotivoCierreCaso = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.IdMotivoCierreCaso);
            
            CreateTable(
                "dbo.TipoCasoes",
                c => new
                    {
                        IdTipoCaso = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.IdTipoCaso);
            
            CreateTable(
                "dbo.CuentaBancarias",
                c => new
                    {
                        IdCuentaBancaria = c.Int(nullable: false, identity: true),
                        CBU = c.String(nullable: false, maxLength: 35),
                        NombreTitular = c.String(nullable: false, maxLength: 35),
                        DNITitular = c.Int(nullable: false),
                        IdDomicilio = c.Int(nullable: false),
                        CUITTitular = c.String(nullable: false, maxLength: 11),
                        AliasCuenta = c.String(),
                        Banco_IdEntidadBancaria = c.Int(),
                        Suscripcion_IdSuscripcion = c.Int(),
                        Suscriptor_IdSuscriptor = c.Int(),
                    })
                .PrimaryKey(t => t.IdCuentaBancaria)
                .ForeignKey("dbo.EntidadBancarias", t => t.Banco_IdEntidadBancaria)
                .ForeignKey("dbo.Suscripcions", t => t.Suscripcion_IdSuscripcion)
                .ForeignKey("dbo.Suscriptors", t => t.Suscriptor_IdSuscriptor)
                .Index(t => t.Banco_IdEntidadBancaria)
                .Index(t => t.Suscripcion_IdSuscripcion)
                .Index(t => t.Suscriptor_IdSuscriptor);
            
            CreateTable(
                "dbo.EntidadBancarias",
                c => new
                    {
                        IdEntidadBancaria = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.IdEntidadBancaria);
            
            CreateTable(
                "dbo.DatosFacturacions",
                c => new
                    {
                        IdDatosFacturacion = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                        Apellido = c.String(),
                        DNI = c.Int(nullable: false),
                        CUIT = c.String(),
                        RazonSocial = c.String(),
                        IdDomicilio = c.Int(nullable: false),
                        TipoIva = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdDatosFacturacion)
                .ForeignKey("dbo.Domicilios", t => t.IdDomicilio, cascadeDelete: true)
                .Index(t => t.IdDomicilio);
            
            CreateTable(
                "dbo.EstadoSuscripcionHistorials",
                c => new
                    {
                        IdEstadoSuscripcionHistorial = c.Int(nullable: false, identity: true),
                        EstadoSuscripcion = c.Int(nullable: false),
                        FechaModificacion = c.DateTime(),
                        MotivoEstadoSuscripcion = c.Int(nullable: false),
                        Observaciones = c.String(),
                        IdSuscripcion = c.Int(nullable: false),
                        Responsable = c.String(),
                    })
                .PrimaryKey(t => t.IdEstadoSuscripcionHistorial)
                .ForeignKey("dbo.Suscripcions", t => t.IdSuscripcion, cascadeDelete: true)
                .Index(t => t.IdSuscripcion);
            
            CreateTable(
                "dbo.Ofertas",
                c => new
                    {
                        IdOferta = c.Int(nullable: false, identity: true),
                        Codigo = c.Int(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 35),
                        Editorial = c.Int(nullable: false),
                        Descripcion = c.String(maxLength: 300),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CantDias = c.Int(nullable: false),
                        CantTarjetasAdicionales = c.Int(nullable: false),
                        IdEstadoOferta = c.Int(),
                        TipoOferta = c.Int(nullable: false),
                        FechaCarga = c.DateTime(nullable: false),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        FechaFin = c.DateTime(),
                        CodigoProductoServinor = c.String(),
                        NombreProductoServinor = c.String(),
                        UsuarioCarga_UserID = c.String(maxLength: 128),
                        UsuarioModificacion_UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdOferta)
                .ForeignKey("dbo.EstadoOfertas", t => t.IdEstadoOferta)
                .ForeignKey("dbo.UserViews", t => t.UsuarioCarga_UserID)
                .ForeignKey("dbo.UserViews", t => t.UsuarioModificacion_UserID)
                .Index(t => t.IdEstadoOferta)
                .Index(t => t.UsuarioCarga_UserID)
                .Index(t => t.UsuarioModificacion_UserID);
            
            CreateTable(
                "dbo.EstadoOfertas",
                c => new
                    {
                        IdEstadoOferta = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.IdEstadoOferta);
            
            CreateTable(
                "dbo.HistorialPrecioOfertas",
                c => new
                    {
                        IdHistorialPrecioOferta = c.Int(nullable: false, identity: true),
                        Precio = c.Decimal(nullable: false, precision: 18, scale: 2),
                        FechaInicio = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        IdOferta = c.Int(nullable: false),
                        UsuarioModificacion_UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdHistorialPrecioOferta)
                .ForeignKey("dbo.Ofertas", t => t.IdOferta, cascadeDelete: true)
                .ForeignKey("dbo.UserViews", t => t.UsuarioModificacion_UserID)
                .Index(t => t.IdOferta)
                .Index(t => t.UsuarioModificacion_UserID);
            
            CreateTable(
                "dbo.UserViews",
                c => new
                    {
                        UserID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Email = c.String(),
                        Role_RoleID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.RoleViews", t => t.Role_RoleID)
                .Index(t => t.Role_RoleID);
            
            CreateTable(
                "dbo.RoleViews",
                c => new
                    {
                        RoleID = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        UserView_UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RoleID)
                .ForeignKey("dbo.UserViews", t => t.UserView_UserID)
                .Index(t => t.UserView_UserID);
            
            CreateTable(
                "dbo.ProductoOfertas",
                c => new
                    {
                        IdProducto = c.Int(nullable: false),
                        IdOferta = c.Int(nullable: false),
                        Copias = c.Int(),
                    })
                .PrimaryKey(t => new { t.IdProducto, t.IdOferta })
                .ForeignKey("dbo.Ofertas", t => t.IdOferta, cascadeDelete: true)
                .ForeignKey("dbo.Productoes", t => t.IdProducto, cascadeDelete: true)
                .Index(t => t.IdProducto)
                .Index(t => t.IdOferta);
            
            CreateTable(
                "dbo.Productoes",
                c => new
                    {
                        IdProducto = c.Int(nullable: false, identity: true),
                        Codigo = c.Int(),
                        Nombre = c.String(nullable: false, maxLength: 35),
                        Descripcion = c.String(),
                        Monto = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.IdProducto);
            
            CreateTable(
                "dbo.Ordens",
                c => new
                    {
                        IdOrden = c.Int(nullable: false, identity: true),
                        Numero = c.Long(nullable: false),
                        Fecha = c.DateTime(nullable: false),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SuscripcionInicio = c.DateTime(),
                        SuscripcionFin = c.DateTime(),
                        FechaLibro = c.DateTime(),
                        NroLegalLetra = c.String(),
                        NroLegalPuntoVenta = c.Int(),
                        NroLegalNumero = c.Int(),
                        ReciboNroLegalLetra = c.String(),
                        ReciboNroLegalPuntoVenta = c.Int(),
                        ReciboNroLegalNumero = c.Int(),
                        Observaciones = c.String(),
                        IdSuscripcion = c.Int(nullable: false),
                        EstadoOrden = c.Int(nullable: false),
                        DatosFacturacion_IdDatosFacturacion = c.Int(),
                    })
                .PrimaryKey(t => t.IdOrden)
                .ForeignKey("dbo.DatosFacturacions", t => t.DatosFacturacion_IdDatosFacturacion)
                .ForeignKey("dbo.Suscripcions", t => t.IdSuscripcion, cascadeDelete: true)
                .Index(t => t.IdSuscripcion)
                .Index(t => t.DatosFacturacion_IdDatosFacturacion);
            
            CreateTable(
                "dbo.Pagoes",
                c => new
                    {
                        IdPago = c.Int(nullable: false, identity: true),
                        Monto = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Fecha = c.DateTime(nullable: false),
                        IdTarjetaPago = c.Int(),
                        IdCuentaBancaria = c.Int(),
                        IdOrden = c.Int(nullable: false),
                        EstadoPago_IdEstadoPago = c.Int(),
                        MedioPago_TipoMedioPagoId = c.Int(),
                    })
                .PrimaryKey(t => t.IdPago)
                .ForeignKey("dbo.CuentaBancarias", t => t.IdCuentaBancaria)
                .ForeignKey("dbo.EstadoPagoes", t => t.EstadoPago_IdEstadoPago)
                .ForeignKey("dbo.TipoMedioPagoes", t => t.MedioPago_TipoMedioPagoId)
                .ForeignKey("dbo.Ordens", t => t.IdOrden, cascadeDelete: true)
                .ForeignKey("dbo.TarjetaPagoes", t => t.IdTarjetaPago)
                .Index(t => t.IdTarjetaPago)
                .Index(t => t.IdCuentaBancaria)
                .Index(t => t.IdOrden)
                .Index(t => t.EstadoPago_IdEstadoPago)
                .Index(t => t.MedioPago_TipoMedioPagoId);
            
            CreateTable(
                "dbo.Envios",
                c => new
                    {
                        IdEnvio = c.Int(nullable: false, identity: true),
                        FechaEnvio = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.IdEnvio);
            
            CreateTable(
                "dbo.EstadoPagoes",
                c => new
                    {
                        IdEstadoPago = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.IdEstadoPago);
            
            CreateTable(
                "dbo.TipoMedioPagoes",
                c => new
                    {
                        TipoMedioPagoId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        GeneraArchivo = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.TipoMedioPagoId);
            
            CreateTable(
                "dbo.TarjetaPagoes",
                c => new
                    {
                        IdTarjetaPago = c.Int(nullable: false, identity: true),
                        Numero = c.String(nullable: false),
                        NombreExacto = c.String(nullable: false),
                        MesVencimiento = c.Int(nullable: false),
                        AñoVencimiento = c.Int(nullable: false),
                        CodigoSeguridad = c.String(nullable: false),
                        Banco_IdEntidadBancaria = c.Int(),
                        TipoTarjeta_TipoTarjetaId = c.Int(),
                        Suscriptor_IdSuscriptor = c.Int(),
                        Suscripcion_IdSuscripcion = c.Int(),
                    })
                .PrimaryKey(t => t.IdTarjetaPago)
                .ForeignKey("dbo.EntidadBancarias", t => t.Banco_IdEntidadBancaria)
                .ForeignKey("dbo.TipoTarjetas", t => t.TipoTarjeta_TipoTarjetaId)
                .ForeignKey("dbo.Suscriptors", t => t.Suscriptor_IdSuscriptor)
                .ForeignKey("dbo.Suscripcions", t => t.Suscripcion_IdSuscripcion)
                .Index(t => t.Banco_IdEntidadBancaria)
                .Index(t => t.TipoTarjeta_TipoTarjetaId)
                .Index(t => t.Suscriptor_IdSuscriptor)
                .Index(t => t.Suscripcion_IdSuscripcion);
            
            CreateTable(
                "dbo.TipoTarjetas",
                c => new
                    {
                        TipoTarjetaId = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.TipoTarjetaId);
            
            CreateTable(
                "dbo.Premios",
                c => new
                    {
                        IdPremio = c.Int(nullable: false, identity: true),
                        FechaPremio = c.DateTime(nullable: false),
                        IdSuscriptor = c.Int(nullable: false),
                        IdCatalogoPremio = c.Int(nullable: false),
                        Suscripcion_IdSuscripcion = c.Int(),
                    })
                .PrimaryKey(t => t.IdPremio)
                .ForeignKey("dbo.CatalogoPremios", t => t.IdCatalogoPremio, cascadeDelete: true)
                .ForeignKey("dbo.Suscriptors", t => t.IdSuscriptor, cascadeDelete: true)
                .ForeignKey("dbo.Suscripcions", t => t.Suscripcion_IdSuscripcion)
                .Index(t => t.IdSuscriptor)
                .Index(t => t.IdCatalogoPremio)
                .Index(t => t.Suscripcion_IdSuscripcion);
            
            CreateTable(
                "dbo.CatalogoPremios",
                c => new
                    {
                        IdCatalogoPremio = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        FechaCarga = c.DateTime(),
                        FechaBaja = c.DateTime(),
                    })
                .PrimaryKey(t => t.IdCatalogoPremio);
            
            CreateTable(
                "dbo.Suscriptors",
                c => new
                    {
                        IdSuscriptor = c.Int(nullable: false, identity: true),
                        EstadoSuscriptor = c.Int(nullable: false),
                        Nombre = c.String(nullable: false),
                        Apellido = c.String(nullable: false),
                        FechaNacimiento = c.DateTime(),
                        NumeroSuscriptor = c.Int(nullable: false),
                        DNI = c.Int(nullable: false),
                        TipoSexo = c.Int(nullable: false),
                        Email = c.String(),
                        IdDomicilio = c.Int(nullable: false),
                        TelefonoFijoNumero = c.String(),
                        TelefonoFijoArea = c.String(),
                        TelefonoMovilNumero = c.String(),
                        TelefonoMovilArea = c.String(),
                        CUIT = c.String(),
                        RazonSocial = c.String(),
                        TipoSuscriptor = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.IdSuscriptor)
                .ForeignKey("dbo.Domicilios", t => t.IdDomicilio, cascadeDelete: true)
                .Index(t => t.IdDomicilio);
            
            CreateTable(
                "dbo.SuscriptorSecundarios",
                c => new
                    {
                        IdSuscriptorSecundario = c.Int(nullable: false, identity: true),
                        IdSuscriptor = c.Int(nullable: false),
                        IdTarjetaClub = c.Int(nullable: false),
                        Suscripcion_IdSuscripcion = c.Int(),
                    })
                .PrimaryKey(t => t.IdSuscriptorSecundario)
                .ForeignKey("dbo.Suscriptors", t => t.IdSuscriptor, cascadeDelete: true)
                .ForeignKey("dbo.TarjetaClubs", t => t.IdTarjetaClub, cascadeDelete: false)
                .ForeignKey("dbo.Suscripcions", t => t.Suscripcion_IdSuscripcion)
                .Index(t => t.IdSuscriptor)
                .Index(t => t.IdTarjetaClub)
                .Index(t => t.Suscripcion_IdSuscripcion);
            
            CreateTable(
                "dbo.Suspensions",
                c => new
                    {
                        IdSuspension = c.Int(nullable: false, identity: true),
                        FechaDesde = c.DateTime(nullable: false),
                        FechaHasta = c.DateTime(),
                        Observaciones = c.String(),
                        FechaCarga = c.DateTime(nullable: false),
                        FechaModificacion = c.DateTime(),
                        IdSuscripcion = c.Int(nullable: false),
                        MotivoSuspension_IdMotivoSuspension = c.Int(),
                        UsuarioCarga_UserID = c.String(maxLength: 128),
                        UsuarioModificacion_UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdSuspension)
                .ForeignKey("dbo.MotivoSuspensions", t => t.MotivoSuspension_IdMotivoSuspension)
                .ForeignKey("dbo.Suscripcions", t => t.IdSuscripcion, cascadeDelete: true)
                .ForeignKey("dbo.UserViews", t => t.UsuarioCarga_UserID)
                .ForeignKey("dbo.UserViews", t => t.UsuarioModificacion_UserID)
                .Index(t => t.IdSuscripcion)
                .Index(t => t.MotivoSuspension_IdMotivoSuspension)
                .Index(t => t.UsuarioCarga_UserID)
                .Index(t => t.UsuarioModificacion_UserID);
            
            CreateTable(
                "dbo.MotivoSuspensions",
                c => new
                    {
                        IdMotivoSuspension = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.IdMotivoSuspension);
            
            CreateTable(
                "dbo.TipoPeriodoEfectivoes",
                c => new
                    {
                        PeriodoPagoEfectivoId = c.Int(nullable: false, identity: true),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.PeriodoPagoEfectivoId);
            
            CreateTable(
                "dbo.Comercios",
                c => new
                    {
                        IdComercio = c.Int(nullable: false, identity: true),
                        Nombre = c.String(nullable: false),
                        RazonSocial = c.String(),
                        Direccion = c.String(),
                        Localidad = c.String(),
                        Establecimiento = c.Int(),
                        Posnet = c.String(),
                        Lapos = c.String(),
                        Beneficio = c.String(),
                        Email = c.String(nullable: false),
                        FechaAdhesion = c.DateTime(nullable: false),
                        AdhesionActiva = c.Boolean(nullable: false),
                        IdUsuarioAlta = c.String(),
                        FechaAlta = c.DateTime(),
                        Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdComercio)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Ventas",
                c => new
                    {
                        IdVenta = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                        Monto = c.Decimal(precision: 18, scale: 2),
                        Fecha = c.DateTime(),
                        IdComercio = c.Int(nullable: false),
                        IdTarjetaClub = c.Int(nullable: false),
                        IdUsuarioAlta = c.String(),
                        FechaAlta = c.DateTime(),
                    })
                .PrimaryKey(t => t.IdVenta)
                .ForeignKey("dbo.Comercios", t => t.IdComercio, cascadeDelete: true)
                .ForeignKey("dbo.TarjetaClubs", t => t.IdTarjetaClub, cascadeDelete: true)
                .Index(t => t.IdComercio)
                .Index(t => t.IdTarjetaClub);
            
            CreateTable(
                "dbo.Respuestas",
                c => new
                    {
                        IdRespuesta = c.Int(nullable: false, identity: true),
                        Fecha = c.DateTime(nullable: false),
                        Aprobado = c.Boolean(nullable: false),
                        Observaciones = c.String(),
                        Manual = c.Boolean(nullable: false),
                        IdEnvio = c.Int(nullable: false),
                        UsuarioCarga_UserID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.IdRespuesta)
                .ForeignKey("dbo.Envios", t => t.IdEnvio, cascadeDelete: true)
                .ForeignKey("dbo.UserViews", t => t.UsuarioCarga_UserID)
                .Index(t => t.IdEnvio)
                .Index(t => t.UsuarioCarga_UserID);
            
            CreateTable(
                "dbo.EnvioPagoes",
                c => new
                    {
                        Envio_IdEnvio = c.Int(nullable: false),
                        Pago_IdPago = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Envio_IdEnvio, t.Pago_IdPago })
                .ForeignKey("dbo.Envios", t => t.Envio_IdEnvio, cascadeDelete: true)
                .ForeignKey("dbo.Pagoes", t => t.Pago_IdPago, cascadeDelete: true)
                .Index(t => t.Envio_IdEnvio)
                .Index(t => t.Pago_IdPago);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Respuestas", "UsuarioCarga_UserID", "dbo.UserViews");
            DropForeignKey("dbo.Respuestas", "IdEnvio", "dbo.Envios");
            DropForeignKey("dbo.Ventas", "IdTarjetaClub", "dbo.TarjetaClubs");
            DropForeignKey("dbo.Ventas", "IdComercio", "dbo.Comercios");
            DropForeignKey("dbo.Comercios", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TCporArchivoes", "IdTarjetaClub", "dbo.TarjetaClubs");
            DropForeignKey("dbo.Suscripcions", "UsuarioModificacion_UserID", "dbo.UserViews");
            DropForeignKey("dbo.Suscripcions", "UsuarioAlta_UserID", "dbo.UserViews");
            DropForeignKey("dbo.Suscripcions", "TipoPeriodoEfectivo_PeriodoPagoEfectivoId", "dbo.TipoPeriodoEfectivoes");
            DropForeignKey("dbo.Suscripcions", "TipoMedioPagoId", "dbo.TipoMedioPagoes");
            DropForeignKey("dbo.TarjetaPagoes", "Suscripcion_IdSuscripcion", "dbo.Suscripcions");
            DropForeignKey("dbo.TarjetaClubs", "IdSuscripcion", "dbo.Suscripcions");
            DropForeignKey("dbo.Suspensions", "UsuarioModificacion_UserID", "dbo.UserViews");
            DropForeignKey("dbo.Suspensions", "UsuarioCarga_UserID", "dbo.UserViews");
            DropForeignKey("dbo.Suspensions", "IdSuscripcion", "dbo.Suscripcions");
            DropForeignKey("dbo.Suspensions", "MotivoSuspension_IdMotivoSuspension", "dbo.MotivoSuspensions");
            DropForeignKey("dbo.SuscriptorSecundarios", "Suscripcion_IdSuscripcion", "dbo.Suscripcions");
            DropForeignKey("dbo.SuscriptorSecundarios", "IdTarjetaClub", "dbo.TarjetaClubs");
            DropForeignKey("dbo.SuscriptorSecundarios", "IdSuscriptor", "dbo.Suscriptors");
            DropForeignKey("dbo.DiaEntregas", "SuscriptorSecundario_IdSuscriptorSecundario", "dbo.SuscriptorSecundarios");
            DropForeignKey("dbo.Premios", "Suscripcion_IdSuscripcion", "dbo.Suscripcions");
            DropForeignKey("dbo.Premios", "IdSuscriptor", "dbo.Suscriptors");
            DropForeignKey("dbo.TarjetaPagoes", "Suscriptor_IdSuscriptor", "dbo.Suscriptors");
            DropForeignKey("dbo.Suscripcions", "IdSuscriptor", "dbo.Suscriptors");
            DropForeignKey("dbo.Suscriptors", "IdDomicilio", "dbo.Domicilios");
            DropForeignKey("dbo.CuentaBancarias", "Suscriptor_IdSuscriptor", "dbo.Suscriptors");
            DropForeignKey("dbo.Premios", "IdCatalogoPremio", "dbo.CatalogoPremios");
            DropForeignKey("dbo.Ordens", "IdSuscripcion", "dbo.Suscripcions");
            DropForeignKey("dbo.Pagoes", "IdTarjetaPago", "dbo.TarjetaPagoes");
            DropForeignKey("dbo.TarjetaPagoes", "TipoTarjeta_TipoTarjetaId", "dbo.TipoTarjetas");
            DropForeignKey("dbo.TarjetaPagoes", "Banco_IdEntidadBancaria", "dbo.EntidadBancarias");
            DropForeignKey("dbo.Pagoes", "IdOrden", "dbo.Ordens");
            DropForeignKey("dbo.Pagoes", "MedioPago_TipoMedioPagoId", "dbo.TipoMedioPagoes");
            DropForeignKey("dbo.Pagoes", "EstadoPago_IdEstadoPago", "dbo.EstadoPagoes");
            DropForeignKey("dbo.EnvioPagoes", "Pago_IdPago", "dbo.Pagoes");
            DropForeignKey("dbo.EnvioPagoes", "Envio_IdEnvio", "dbo.Envios");
            DropForeignKey("dbo.Pagoes", "IdCuentaBancaria", "dbo.CuentaBancarias");
            DropForeignKey("dbo.Ordens", "DatosFacturacion_IdDatosFacturacion", "dbo.DatosFacturacions");
            DropForeignKey("dbo.Ofertas", "UsuarioModificacion_UserID", "dbo.UserViews");
            DropForeignKey("dbo.Ofertas", "UsuarioCarga_UserID", "dbo.UserViews");
            DropForeignKey("dbo.Suscripcions", "IdOferta", "dbo.Ofertas");
            DropForeignKey("dbo.ProductoOfertas", "IdProducto", "dbo.Productoes");
            DropForeignKey("dbo.ProductoOfertas", "IdOferta", "dbo.Ofertas");
            DropForeignKey("dbo.HistorialPrecioOfertas", "UsuarioModificacion_UserID", "dbo.UserViews");
            DropForeignKey("dbo.RoleViews", "UserView_UserID", "dbo.UserViews");
            DropForeignKey("dbo.UserViews", "Role_RoleID", "dbo.RoleViews");
            DropForeignKey("dbo.HistorialPrecioOfertas", "IdOferta", "dbo.Ofertas");
            DropForeignKey("dbo.Ofertas", "IdEstadoOferta", "dbo.EstadoOfertas");
            DropForeignKey("dbo.EstadoSuscripcionHistorials", "IdSuscripcion", "dbo.Suscripcions");
            DropForeignKey("dbo.Suscripcions", "IdDatosFacturacion", "dbo.DatosFacturacions");
            DropForeignKey("dbo.DatosFacturacions", "IdDomicilio", "dbo.Domicilios");
            DropForeignKey("dbo.CuentaBancarias", "Suscripcion_IdSuscripcion", "dbo.Suscripcions");
            DropForeignKey("dbo.CuentaBancarias", "Banco_IdEntidadBancaria", "dbo.EntidadBancarias");
            DropForeignKey("dbo.Casoes", "IdTipoCaso", "dbo.TipoCasoes");
            DropForeignKey("dbo.Casoes", "IdSuscripcion", "dbo.Suscripcions");
            DropForeignKey("dbo.Casoes", "IdMotivoCierreCaso", "dbo.MotivoCierreCasoes");
            DropForeignKey("dbo.HistorialCasoes", "EstadoCasoHistorial_IdEstadoCaso", "dbo.EstadoCasoes");
            DropForeignKey("dbo.HistorialCasoes", "Caso_IdCaso", "dbo.Casoes");
            DropForeignKey("dbo.Casoes", "IdEstadoCaso", "dbo.EstadoCasoes");
            DropForeignKey("dbo.DiaDevolucionCasoes", "Caso_IdCaso", "dbo.Casoes");
            DropForeignKey("dbo.Casoes", "IdCanillita", "dbo.Canillitas");
            DropForeignKey("dbo.Casoes", "IdAsuntoCaso", "dbo.AsuntoCasoes");
            DropForeignKey("dbo.AsuntoCasoes", "IdAreaCaso", "dbo.AreaCasoes");
            DropForeignKey("dbo.Suscripcions", "IdCanillita", "dbo.Canillitas");
            DropForeignKey("dbo.Canillitas", "IdLocalidad", "dbo.Localidads");
            DropForeignKey("dbo.SuscripcionHojaDeRutas", "IdSuscripcion", "dbo.Suscripcions");
            DropForeignKey("dbo.SuscripcionHojaDeRutas", "IdHojaDeRuta", "dbo.HojaDeRutas");
            DropForeignKey("dbo.Lineas", "IdHojaDeRuta", "dbo.HojaDeRutas");
            DropForeignKey("dbo.HojaDeRutas", "IdCanillita", "dbo.Canillitas");
            DropForeignKey("dbo.Distribuidors", "IdLocalidad", "dbo.Localidads");
            DropForeignKey("dbo.Canillitas", "IdDistribuidor", "dbo.Distribuidors");
            DropForeignKey("dbo.DiaEntregas", "IdSuscripcion", "dbo.Suscripcions");
            DropForeignKey("dbo.DiaEntregas", "IdDomicilio", "dbo.Domicilios");
            DropForeignKey("dbo.Domicilios", "IdLocalidad", "dbo.Localidads");
            DropForeignKey("dbo.Localidads", "IdProvincia", "dbo.Provincias");
            DropForeignKey("dbo.DiaEntregas", "IdCanillita", "dbo.Canillitas");
            DropForeignKey("dbo.TCporArchivoes", "IdArchivoImpresionTC", "dbo.ArchivoImpresionTCs");
            DropIndex("dbo.EnvioPagoes", new[] { "Pago_IdPago" });
            DropIndex("dbo.EnvioPagoes", new[] { "Envio_IdEnvio" });
            DropIndex("dbo.Respuestas", new[] { "UsuarioCarga_UserID" });
            DropIndex("dbo.Respuestas", new[] { "IdEnvio" });
            DropIndex("dbo.Ventas", new[] { "IdTarjetaClub" });
            DropIndex("dbo.Ventas", new[] { "IdComercio" });
            DropIndex("dbo.Comercios", new[] { "Id" });
            DropIndex("dbo.Suspensions", new[] { "UsuarioModificacion_UserID" });
            DropIndex("dbo.Suspensions", new[] { "UsuarioCarga_UserID" });
            DropIndex("dbo.Suspensions", new[] { "MotivoSuspension_IdMotivoSuspension" });
            DropIndex("dbo.Suspensions", new[] { "IdSuscripcion" });
            DropIndex("dbo.SuscriptorSecundarios", new[] { "Suscripcion_IdSuscripcion" });
            DropIndex("dbo.SuscriptorSecundarios", new[] { "IdTarjetaClub" });
            DropIndex("dbo.SuscriptorSecundarios", new[] { "IdSuscriptor" });
            DropIndex("dbo.Suscriptors", new[] { "IdDomicilio" });
            DropIndex("dbo.Premios", new[] { "Suscripcion_IdSuscripcion" });
            DropIndex("dbo.Premios", new[] { "IdCatalogoPremio" });
            DropIndex("dbo.Premios", new[] { "IdSuscriptor" });
            DropIndex("dbo.TarjetaPagoes", new[] { "Suscripcion_IdSuscripcion" });
            DropIndex("dbo.TarjetaPagoes", new[] { "Suscriptor_IdSuscriptor" });
            DropIndex("dbo.TarjetaPagoes", new[] { "TipoTarjeta_TipoTarjetaId" });
            DropIndex("dbo.TarjetaPagoes", new[] { "Banco_IdEntidadBancaria" });
            DropIndex("dbo.Pagoes", new[] { "MedioPago_TipoMedioPagoId" });
            DropIndex("dbo.Pagoes", new[] { "EstadoPago_IdEstadoPago" });
            DropIndex("dbo.Pagoes", new[] { "IdOrden" });
            DropIndex("dbo.Pagoes", new[] { "IdCuentaBancaria" });
            DropIndex("dbo.Pagoes", new[] { "IdTarjetaPago" });
            DropIndex("dbo.Ordens", new[] { "DatosFacturacion_IdDatosFacturacion" });
            DropIndex("dbo.Ordens", new[] { "IdSuscripcion" });
            DropIndex("dbo.ProductoOfertas", new[] { "IdOferta" });
            DropIndex("dbo.ProductoOfertas", new[] { "IdProducto" });
            DropIndex("dbo.RoleViews", new[] { "UserView_UserID" });
            DropIndex("dbo.UserViews", new[] { "Role_RoleID" });
            DropIndex("dbo.HistorialPrecioOfertas", new[] { "UsuarioModificacion_UserID" });
            DropIndex("dbo.HistorialPrecioOfertas", new[] { "IdOferta" });
            DropIndex("dbo.Ofertas", new[] { "UsuarioModificacion_UserID" });
            DropIndex("dbo.Ofertas", new[] { "UsuarioCarga_UserID" });
            DropIndex("dbo.Ofertas", new[] { "IdEstadoOferta" });
            DropIndex("dbo.EstadoSuscripcionHistorials", new[] { "IdSuscripcion" });
            DropIndex("dbo.DatosFacturacions", new[] { "IdDomicilio" });
            DropIndex("dbo.CuentaBancarias", new[] { "Suscriptor_IdSuscriptor" });
            DropIndex("dbo.CuentaBancarias", new[] { "Suscripcion_IdSuscripcion" });
            DropIndex("dbo.CuentaBancarias", new[] { "Banco_IdEntidadBancaria" });
            DropIndex("dbo.HistorialCasoes", new[] { "EstadoCasoHistorial_IdEstadoCaso" });
            DropIndex("dbo.HistorialCasoes", new[] { "Caso_IdCaso" });
            DropIndex("dbo.DiaDevolucionCasoes", new[] { "Caso_IdCaso" });
            DropIndex("dbo.AsuntoCasoes", new[] { "IdAreaCaso" });
            DropIndex("dbo.Casoes", new[] { "IdMotivoCierreCaso" });
            DropIndex("dbo.Casoes", new[] { "IdCanillita" });
            DropIndex("dbo.Casoes", new[] { "IdTipoCaso" });
            DropIndex("dbo.Casoes", new[] { "IdSuscripcion" });
            DropIndex("dbo.Casoes", new[] { "IdEstadoCaso" });
            DropIndex("dbo.Casoes", new[] { "IdAsuntoCaso" });
            DropIndex("dbo.SuscripcionHojaDeRutas", new[] { "IdHojaDeRuta" });
            DropIndex("dbo.SuscripcionHojaDeRutas", new[] { "IdSuscripcion" });
            DropIndex("dbo.Lineas", new[] { "IdHojaDeRuta" });
            DropIndex("dbo.HojaDeRutas", new[] { "IdCanillita" });
            DropIndex("dbo.Distribuidors", new[] { "IdLocalidad" });
            DropIndex("dbo.Localidads", new[] { "IdProvincia" });
            DropIndex("dbo.Domicilios", new[] { "IdLocalidad" });
            DropIndex("dbo.DiaEntregas", new[] { "SuscriptorSecundario_IdSuscriptorSecundario" });
            DropIndex("dbo.DiaEntregas", new[] { "IdSuscripcion" });
            DropIndex("dbo.DiaEntregas", new[] { "IdDomicilio" });
            DropIndex("dbo.DiaEntregas", new[] { "IdCanillita" });
            DropIndex("dbo.Canillitas", new[] { "IdDistribuidor" });
            DropIndex("dbo.Canillitas", new[] { "IdLocalidad" });
            DropIndex("dbo.Suscripcions", new[] { "UsuarioModificacion_UserID" });
            DropIndex("dbo.Suscripcions", new[] { "UsuarioAlta_UserID" });
            DropIndex("dbo.Suscripcions", new[] { "TipoPeriodoEfectivo_PeriodoPagoEfectivoId" });
            DropIndex("dbo.Suscripcions", new[] { "TipoMedioPagoId" });
            DropIndex("dbo.Suscripcions", new[] { "IdDatosFacturacion" });
            DropIndex("dbo.Suscripcions", new[] { "IdCanillita" });
            DropIndex("dbo.Suscripcions", new[] { "IdSuscriptor" });
            DropIndex("dbo.Suscripcions", new[] { "IdOferta" });
            DropIndex("dbo.TarjetaClubs", new[] { "IdSuscripcion" });
            DropIndex("dbo.TCporArchivoes", new[] { "IdTarjetaClub" });
            DropIndex("dbo.TCporArchivoes", new[] { "IdArchivoImpresionTC" });
            DropTable("dbo.EnvioPagoes");
            DropTable("dbo.Respuestas");
            DropTable("dbo.Ventas");
            DropTable("dbo.Comercios");
            DropTable("dbo.TipoPeriodoEfectivoes");
            DropTable("dbo.MotivoSuspensions");
            DropTable("dbo.Suspensions");
            DropTable("dbo.SuscriptorSecundarios");
            DropTable("dbo.Suscriptors");
            DropTable("dbo.CatalogoPremios");
            DropTable("dbo.Premios");
            DropTable("dbo.TipoTarjetas");
            DropTable("dbo.TarjetaPagoes");
            DropTable("dbo.TipoMedioPagoes");
            DropTable("dbo.EstadoPagoes");
            DropTable("dbo.Envios");
            DropTable("dbo.Pagoes");
            DropTable("dbo.Ordens");
            DropTable("dbo.Productoes");
            DropTable("dbo.ProductoOfertas");
            DropTable("dbo.RoleViews");
            DropTable("dbo.UserViews");
            DropTable("dbo.HistorialPrecioOfertas");
            DropTable("dbo.EstadoOfertas");
            DropTable("dbo.Ofertas");
            DropTable("dbo.EstadoSuscripcionHistorials");
            DropTable("dbo.DatosFacturacions");
            DropTable("dbo.EntidadBancarias");
            DropTable("dbo.CuentaBancarias");
            DropTable("dbo.TipoCasoes");
            DropTable("dbo.MotivoCierreCasoes");
            DropTable("dbo.HistorialCasoes");
            DropTable("dbo.EstadoCasoes");
            DropTable("dbo.DiaDevolucionCasoes");
            DropTable("dbo.AreaCasoes");
            DropTable("dbo.AsuntoCasoes");
            DropTable("dbo.Casoes");
            DropTable("dbo.SuscripcionHojaDeRutas");
            DropTable("dbo.Lineas");
            DropTable("dbo.HojaDeRutas");
            DropTable("dbo.Distribuidors");
            DropTable("dbo.Provincias");
            DropTable("dbo.Localidads");
            DropTable("dbo.Domicilios");
            DropTable("dbo.DiaEntregas");
            DropTable("dbo.Canillitas");
            DropTable("dbo.Suscripcions");
            DropTable("dbo.TarjetaClubs");
            DropTable("dbo.TCporArchivoes");
            DropTable("dbo.ArchivoImpresionTCs");
            DropTable("dbo.ArchivoEnvios");
        }
    }
}
