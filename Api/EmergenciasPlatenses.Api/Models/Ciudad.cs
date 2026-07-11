namespace EmergenciasPlatenses.Api.Models;

public class Ciudad
{
    public int Id { get; set; }

    public string Nombre { get; set; } = string.Empty;

    public string Provincia { get; set; } = string.Empty;

    public string Pais { get; set; } = "Argentina";

    public bool Activa { get; set; } = true;
}
