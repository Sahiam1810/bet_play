using System;

using liga_betplay.models;
namespace liga_betplay.Services;

// Gestiona el torneo: registro de equipos y simulación de partidos.
public class TorneoService
{
    // Lista en memoria con todos los equipos del torneo
    private readonly List<Equipo> _equipos;

    public TorneoService(List<Equipo> equipos)
    {
        _equipos = equipos;
    }

    // --- Listar equipos 

    // Muestra todos los equipos registrados numerados.
    public void ListarEquipos()
    {
        Console.WriteLine($"\n Equipos registrados ({_equipos.Count}):");
        Console.WriteLine("  " + new string('─', 35));

        // Recorremos la lista con índice para numerarlos
        for (int i = 0; i < _equipos.Count; i++)
            Console.WriteLine($"  {i + 1,2}. {_equipos[i].Nombre}");
    }

    // --- Registrar equipo 

    // Agrega un nuevo equipo al torneo si el nombre no existe.
    public void RegistrarEquipo()
    {
        Console.Write("\n  Nombre del equipo: ");
        string nombre = Console.ReadLine()?.Trim() ?? "";

        if (string.IsNullOrWhiteSpace(nombre))
        {
            Console.WriteLine("   El nombre no puede estar vacío.");
            return;
        }

        // Verificamos que no exista ya un equipo con ese nombre
        bool existe = _equipos.Any(e =>
            e.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));

        if (existe)
        {
            Console.WriteLine($" Ya existe un equipo llamado \"{nombre}\".");
            return;
        }

        _equipos.Add(new Equipo(nombre));
        Console.WriteLine($" \"{nombre}\" registrado. Total: {_equipos.Count} equipo(s).");
    }

    // --- Simular partido 

    // Muestra el menú de selección y simula un partido entre dos equipos.
    // Permite ingresar el resultado manualmente o generarlo de forma aleatoria.
    public void SimularPartido()
    {
        if (_equipos.Count < 2)
        {
            Console.WriteLine("\n   Se necesitan al menos 2 equipos para simular un partido.");
            return;
        }

        // Mostramos la lista para que el usuario elija
        ListarEquipos();

        // Selección del equipo local
        Console.Write("\n  Número del equipo LOCAL:    ");
        if (!int.TryParse(Console.ReadLine(), out int numLocal) ||
            numLocal < 1 || numLocal > _equipos.Count)
        {
            Console.WriteLine("   Número inválido.");
            return;
        }

        // Selección del equipo visitante
        Console.Write("  Número del equipo VISITANTE: ");
        if (!int.TryParse(Console.ReadLine(), out int numVisitante) ||
            numVisitante < 1 || numVisitante > _equipos.Count)
        {
            Console.WriteLine(" Número inválido.");
            return;
        }

        if (numLocal == numVisitante)
        {
            Console.WriteLine(" No puedes enfrentar un equipo consigo mismo.");
            return;
        }

        Equipo local = _equipos[numLocal - 1];
        Equipo visitante = _equipos[numVisitante - 1];

        // Preguntamos cómo se ingresa el resultado
        Console.WriteLine("\n  ¿Cómo deseas el resultado?");
        Console.WriteLine(" 1. Ingresar marcador manualmente");
        Console.WriteLine(" 2. Generar resultado aleatorio");
        Console.Write("  Opción: ");
        string opcion = Console.ReadLine() ?? "";

        int golesLocal, golesVisitante;

        if (opcion == "1")
        {
            // Resultado manual
            Console.Write($"  Goles de {local.Nombre}: ");
            if (!int.TryParse(Console.ReadLine(), out golesLocal) || golesLocal < 0)
            {
                Console.WriteLine(" Goles inválidos.");
                return;
            }

            Console.Write($" Goles de {visitante.Nombre}: ");
            if (!int.TryParse(Console.ReadLine(), out golesVisitante) || golesVisitante < 0)
            {
                Console.WriteLine(" Goles inválidos.");
                return;
            }
        }
        else
        {
            // Resultado aleatorio — máximo 5 goles por equipo
            var rng = new Random();
            golesLocal = rng.Next(0, 6);
            golesVisitante = rng.Next(0, 6);
        }

        // Actualizamos las estadísticas de ambos equipos
        local.AgregarResultado(golesLocal, golesVisitante);
        visitante.AgregarResultado(golesVisitante, golesLocal);

        // Mostramos el resultado
        string resultado = golesLocal > golesVisitante ? $"Ganó {local.Nombre}" :
                           golesLocal < golesVisitante ? $"Ganó {visitante.Nombre}" :
                           "Empate";

        Console.WriteLine($"\n  {local.Nombre} {golesLocal} - {golesVisitante} {visitante.Nombre}");
        Console.WriteLine($" Resultado: {resultado}");
    }
}