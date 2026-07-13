using System.Net;
using System.Globalization;
using System.Net.Http.Json;
using EmergenciasPlatenses.App.Models;

namespace EmergenciasPlatenses.App.Services;

public class EmergenciasApiClient(HttpClient httpClient) : IEmergenciasApiClient
{
    public async Task<IReadOnlyCollection<Categoria>> ObtenerCategoriasAsync(
        CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<Categoria[]>(
            "api/categorias",
            cancellationToken) ?? [];
    }

    public async Task<IReadOnlyCollection<ComercioResumen>> ObtenerComerciosAsync(
        int? categoriaId = null,
        int? ciudadId = null,
        bool? atiende24Horas = null,
        decimal? latitud = null,
        decimal? longitud = null,
        CancellationToken cancellationToken = default)
    {
        var parametros = new List<string>();

        if (categoriaId.HasValue)
        {
            parametros.Add($"categoriaId={categoriaId.Value}");
        }

        if (ciudadId.HasValue)
        {
            parametros.Add($"ciudadId={ciudadId.Value}");
        }

        if (atiende24Horas.HasValue)
        {
            parametros.Add(
                $"atiende24Horas={atiende24Horas.Value.ToString().ToLowerInvariant()}");
        }

        if (latitud.HasValue)
        {
            parametros.Add(
                $"latitud={latitud.Value.ToString(CultureInfo.InvariantCulture)}");
        }

        if (longitud.HasValue)
        {
            parametros.Add(
                $"longitud={longitud.Value.ToString(CultureInfo.InvariantCulture)}");
        }

        var ruta = "api/comercios";
        if (parametros.Count > 0)
        {
            ruta += $"?{string.Join("&", parametros)}";
        }

        return await httpClient.GetFromJsonAsync<ComercioResumen[]>(
            ruta,
            cancellationToken) ?? [];
    }

    public async Task<ComercioDetalle?> ObtenerComercioAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        using var respuesta = await httpClient.GetAsync(
            $"api/comercios/{id}",
            cancellationToken);

        if (respuesta.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        respuesta.EnsureSuccessStatusCode();

        return await respuesta.Content.ReadFromJsonAsync<ComercioDetalle>(
            cancellationToken: cancellationToken);
    }
}
