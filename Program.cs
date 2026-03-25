using liga_betplay.Data;

// para verificar funcionamiento, deben cargarse lo 20 equipos.
var equipos = DatosIniciales.ObtenerEquipos();
Console.WriteLine($"Equipos cargados: {equipos.Count}");
foreach (var e in equipos)
    Console.WriteLine(e.Nombre);