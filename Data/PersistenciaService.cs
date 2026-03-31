using System.Text.Json;           // ya viene por defecto en .NET 6+, no se necesita librería externa
using liga_betplay.models;

namespace liga_betplay.Services;

// Servicio que maneja la persistencia de datos del torneo en un archivo JSON.
public class PersistenciaService
{
    // Nombre del archivo JSON donde se guardan los equipos
    private const string ARCHIVO = "equipos.json";

    // Opciones del serializador JSON:
    private static readonly JsonSerializerOptions _opciones = new JsonSerializerOptions
    {
        WriteIndented = true // hace que el JSON sea legible (con sangría)
    };

    // Guarda la lista de equipos en el archivo JSON.
    // Se llama automáticamente después de cada acción que modifica datos.
    public static void Guardar(List<Equipo> equipos)
    {
        // Serializamos la lista de equipos a texto JSON
        string json = JsonSerializer.Serialize(equipos, _opciones);

        // Escribimos el texto en el archivo (lo crea si no existe, lo sobreescribe si ya existe)
        File.WriteAllText(ARCHIVO, json);

        Console.WriteLine($"Datos guardados en {ARCHIVO}");
    }

    // Carga la lista de equipos desde el archivo JSON.
    // Si el archivo no existe o está corrupto, retorna null para usar los datos iniciales.
    public static List<Equipo>? Cargar()
    {
        // Si el archivo no existe, retornamos null — se usarán los datos iniciales
        if (!File.Exists(ARCHIVO))
            return null;

        // Leemos el contenido del archivo
        string json = File.ReadAllText(ARCHIVO);

        // Si el archivo está vacío, retornamos null
        if (string.IsNullOrWhiteSpace(json))
            return null;

        // Deserializamos el JSON de vuelta a una lista de objetos Equipo
        List<Equipo>? equipos = JsonSerializer.Deserialize<List<Equipo>>(json);

        return equipos;
    }
}