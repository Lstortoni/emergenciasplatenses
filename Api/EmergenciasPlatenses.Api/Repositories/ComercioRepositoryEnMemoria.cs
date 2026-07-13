using EmergenciasPlatenses.Api.Models;

namespace EmergenciasPlatenses.Api.Repositories;

public class ComercioRepositoryEnMemoria : IComercioRepository
{
    private static readonly Ciudad LaPlata = new()
    {
        Id = 1,
        Nombre = "La Plata",
        Provincia = "Buenos Aires"
    };

    private static readonly IReadOnlyCollection<Categoria> Categorias =
    [
        new() { Id = 1, Nombre = "Cerrajerías", Descripcion = "Aperturas y reparaciones de urgencia", Icono = "key" },
        new() { Id = 2, Nombre = "Veterinarias", Descripcion = "Atención veterinaria de urgencia", Icono = "pets" },
        new() { Id = 3, Nombre = "Farmacias", Descripcion = "Farmacias y servicios farmacéuticos", Icono = "pharmacy" },
        new() { Id = 4, Nombre = "Electricistas", Descripcion = "Emergencias eléctricas", Icono = "bolt" },
        new() { Id = 5, Nombre = "Plomeros", Descripcion = "Reparaciones sanitarias urgentes", Icono = "plumbing" },
        new() { Id = 6, Nombre = "Gomerías", Descripcion = "Auxilio y reparación de neumáticos", Icono = "tire_repair" },
        new() { Id = 7, Nombre = "Remolques", Descripcion = "Auxilio mecánico y traslado de vehículos", Icono = "tow_truck" },
        new() { Id = 8, Nombre = "Gasistas", Descripcion = "Atención de emergencias de gas", Icono = "gas_meter" }
    ];

    private static readonly IReadOnlyCollection<Comercio> Comercios =
    [
        CrearComercio(
            1, "Farmacia Central", "Farmacia con atención durante las 24 horas.",
            "2214123456", "5492214123456", "Abierto las 24 horas", true, 3,
            "7", "1234", "Entre 57 y 58", -34.921456m, -57.954321m),
        CrearComercio(
            2, "Veterinaria del Bosque", "Guardia veterinaria para perros y gatos.",
            "2214234567", "5492214234567", "Todos los días de 8:00 a 24:00", true, 2,
            "1", "678", "Frente al bosque", -34.908812m, -57.939721m),
        CrearComercio(
            3, "Cerrajería 24 Horas", "Apertura de puertas y cambio de cerraduras.",
            "2214345678", "5492214345678", "Atención las 24 horas", true, 1,
            "44", "950", "Entre 13 y 14", -34.920183m, -57.959602m),
        CrearComercio(
            4, "Auxilio Platense", "Remolques y auxilio mecánico en La Plata.",
            "2214456789", "5492214456789", "Todos los días, las 24 horas", true, 7,
            "31", "1820", null, -34.936420m, -57.978510m),
        CrearComercio(
            5, "Electricidad Segura", "Reparaciones eléctricas domiciliarias.",
            "2214567890", null, "Lunes a sábado de 8:00 a 20:00", false, 4,
            "19", "1450", null, -34.928310m, -57.967540m)
    ];

    public Task<IReadOnlyCollection<Categoria>> ObtenerCategoriasAsync()
    {
        IReadOnlyCollection<Categoria> resultado = Categorias
            .Where(categoria => categoria.Activa)
            .OrderBy(categoria => categoria.Nombre)
            .ToArray();

        return Task.FromResult(resultado);
    }

    public Task<IReadOnlyCollection<Comercio>> ObtenerComerciosAsync(
        int? categoriaId,
        int? ciudadId,
        bool? atiende24Horas)
    {
        var consulta = Comercios.Where(comercio => comercio.Activo);

        if (categoriaId.HasValue)
        {
            consulta = consulta.Where(comercio => comercio.CategoriaId == categoriaId.Value);
        }

        if (ciudadId.HasValue)
        {
            consulta = consulta.Where(comercio => comercio.Direccion.CiudadId == ciudadId.Value);
        }

        if (atiende24Horas.HasValue)
        {
            consulta = consulta.Where(
                comercio => comercio.Atiende24Horas == atiende24Horas.Value);
        }

        IReadOnlyCollection<Comercio> resultado = consulta
            .OrderBy(comercio => comercio.Nombre)
            .ToArray();

        return Task.FromResult(resultado);
    }

    public Task<Comercio?> ObtenerPorIdAsync(int id)
    {
        var comercio = Comercios.FirstOrDefault(comercio => comercio.Id == id && comercio.Activo);
        return Task.FromResult(comercio);
    }

    private static Comercio CrearComercio(
        int id,
        string nombre,
        string descripcion,
        string telefono,
        string? whatsApp,
        string horario,
        bool atiende24Horas,
        int categoriaId,
        string calle,
        string numero,
        string? referencia,
        decimal latitud,
        decimal longitud)
    {
        var categoria = Categorias.First(item => item.Id == categoriaId);

        return new Comercio
        {
            Id = id,
            Nombre = nombre,
            Descripcion = descripcion,
            Telefono = telefono,
            WhatsApp = whatsApp,
            Horario = horario,
            Atiende24Horas = atiende24Horas,
            CategoriaId = categoria.Id,
            Categoria = categoria,
            Direccion = new Direccion
            {
                Id = id,
                ComercioId = id,
                Calle = calle,
                Numero = numero,
                CodigoPostal = "1900",
                Referencia = referencia,
                CiudadId = LaPlata.Id,
                Ciudad = LaPlata,
                Latitud = latitud,
                Longitud = longitud
            }
        };
    }
}
