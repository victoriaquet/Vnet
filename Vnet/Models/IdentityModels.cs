using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Vnet.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<VnetRegistro> VnetRegistros { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.ArchivoEnvio> ArchivoEnvios { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.AreaCaso> AreaCasos { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.EstadoPago> EstadoPagos { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.AsuntoCaso> AsuntoCasos { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Canillita> Canillitas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.Comercio> Comercios { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.ProductoOferta> ProductoOfertas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.ArchivoImpresionTC> ArchivoImpresionTcs { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Caso> Casos { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.Venta> Ventas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.TCporArchivo> TCporArchivos { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.SuscriptorSecundario> SuscriptorSecundarios { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.EstadoCaso> EstadoCasos { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Suscriptor> Suscriptores { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.TipoCaso> TipoCasos { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Oferta> Ofertas { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Orden> Ordenes { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Suscripcion> Suscripciones { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Pago> Pagos { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Premio> Premios { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.CatalogoPremio> CatalogoPremios { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Producto> Productos { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Provincia> Provincias { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Respuesta> Respuestas { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Envio> Envios { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.TarjetaClub> TarjetaClubes { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.TarjetaPago> TarjetaPagos { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.CuentaBancaria> CuentaBancarias { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.DatosFacturacion> DatosFacturaciones { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.DiaDevolucionCaso> DiaDevolucionCasos { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.DiaEntrega> DiaEntregas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.Domicilio> Domicilios { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.EntidadBancaria> EntidadBancarias { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.EstadoOferta> EstadoOfertas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.EstadoSuscripcionHistorial> EstadoSuscripcionHistoriales { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.HistorialPrecioOferta> HistorialPrecioOfertas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.Localidad> Localidades { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.MotivoCierreCaso> MotivoCierreCasos { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.MotivoSuspension> MotivosSuspensiones { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.Linea> Lineas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.HojaDeRuta> HojaDeRutas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.SuscripcionHojaDeRuta> SuscripcionHojaDeRutas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.HistorialCaso> HistorialCasos { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.Distribuidor> Distribuidores { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.TipoMedioPago> TipoMedioPagos { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.TipoTarjeta> TiposTarjetas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.TipoPeriodoEfectivo> TiposPeriodoEfectivo { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }

    public class SNCContext : IdentityDbContext<ApplicationUser>
    {

        public SNCContext()
            : base("SNCContext", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Suscripcion>().HasMany(p => p.TarjetaClub).WithRequired(p => p.Suscripcion).WillCascadeOnDelete(false);
            //modelBuilder.Entity<Caso>().HasMany(p => p.DiasDevoluciones).WithRequired(p => p.Caso).WillCascadeOnDelete(true);
            modelBuilder.Entity<Caso>().HasMany(p => p.HistorialCaso).WithRequired(p => p.Caso).WillCascadeOnDelete(true);
            //modelBuilder.Entity<Suscripcion>().HasMany(p => p.DiaEntregas).WithRequired(p => p.Suscripcion).WillCascadeOnDelete(true);
        }
        public static SNCContext Create()
        {

            return new SNCContext();
        }

        public System.Data.Entity.DbSet<Vnet.Models.ArchivoEnvio> ArchivoEnvios { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.AreaCaso> AreaCasos { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.EstadoPago> EstadoPagos { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.AsuntoCaso> AsuntoCasos { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Canillita> Canillitas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.Comercio> Comercios { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.ProductoOferta> ProductoOfertas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.ArchivoImpresionTC> ArchivoImpresionTcs { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Caso> Casos { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.Venta> Ventas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.TCporArchivo> TCporArchivos { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.SuscriptorSecundario> SuscriptorSecundarios { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.EstadoCaso> EstadoCasos { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Suscriptor> Suscriptores { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.TipoCaso> TipoCasos { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Oferta> Ofertas { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Orden> Ordenes { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Suscripcion> Suscripciones { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Pago> Pagos { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Premio> Premios { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.CatalogoPremio> CatalogoPremios { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Producto> Productos { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Provincia> Provincias { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Respuesta> Respuestas { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.Envio> Envios { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.TarjetaClub> TarjetaClubes { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.TarjetaPago> TarjetaPagos { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.CuentaBancaria> CuentaBancarias { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.DatosFacturacion> DatosFacturaciones { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.DiaDevolucionCaso> DiaDevolucionCasos { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.DiaEntrega> DiaEntregas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.Domicilio> Domicilios { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.EntidadBancaria> EntidadBancarias { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.EstadoOferta> EstadoOfertas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.EstadoSuscripcionHistorial> EstadoSuscripcionHistoriales { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.HistorialPrecioOferta> HistorialPrecioOfertas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.Localidad> Localidades { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.MotivoCierreCaso> MotivoCierreCasos { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.MotivoSuspension> MotivosSuspensiones { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.Linea> Lineas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.HojaDeRuta> HojaDeRutas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.SuscripcionHojaDeRuta> SuscripcionHojaDeRutas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.HistorialCaso> HistorialCasos { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.Distribuidor> Distribuidores { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.TipoMedioPago> TipoMedioPagos { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.TipoTarjeta> TiposTarjetas { get; set; }
        public System.Data.Entity.DbSet<Vnet.Models.TipoPeriodoEfectivo> TiposPeriodoEfectivo { get; set; }

        public System.Data.Entity.DbSet<Vnet.Models.VnetRegistro> VnetRegistros { get; set; }


    }
}