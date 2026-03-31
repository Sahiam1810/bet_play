using liga_betplay.Data;
using liga_betplay.Services;

Console.WriteLine("|----------------------------------------------|");
Console.WriteLine("|      SIMULADOR LIGA BETPLAY DIMAYOR          |");
Console.WriteLine("|----------------------------------------------|\n");

// Intentamos cargar los equipos desde el archivo JSON
// Si no existe el archivo, cargamos los 20 equipos iniciales

List<liga_betplay.models.Equipo> equipos;

var equiposCargados = PersistenciaService.Cargar();

if (equiposCargados != null)
{
    // Había datos guardados — los usamos
    equipos = equiposCargados;
    Console.WriteLine($"  ✅ Datos cargados desde equipos.json ({equipos.Count} equipos).");
}
else
{
    // Primera vez que corre — cargamos los equipos iniciales
    equipos = DatosIniciales.ObtenerEquipos();
    Console.WriteLine($"  🆕 Cargando datos iniciales ({equipos.Count} equipos).");

    // Guardamos de una vez el archivo inicial
    PersistenciaService.Guardar(equipos);
}


var torneo   = new TorneoService(equipos);
var consultas = new ConsultaService(equipos);

Console.WriteLine($"  {equipos.Count} equipos listos.\n");

string opcion;

// Menú principal con do-while — se repite hasta que el usuario elija "Salir"
do
{
    Console.WriteLine("\n|--------------------------------------------|");
    Console.WriteLine("|                  MENÚ PRINCIPAL              |");
    Console.WriteLine("|----------------------------------------------|");
    Console.WriteLine("|  GESTIÓN                                      |");
    Console.WriteLine("|  1. Listar equipos                            |");
    Console.WriteLine("|  2. Registrar nuevo equipo                    |");
    Console.WriteLine("|  3. Simular partido                           |");
    Console.WriteLine("|  4. Ver tabla de posiciones                   |");
    Console.WriteLine("|-----------------------------------------------|");
    Console.WriteLine("|  CONSULTAS LINQ                               |");
    Console.WriteLine("|  5.  Líder del torneo                         |");
    Console.WriteLine("|  6.  Más goles a favor                        |");
    Console.WriteLine("|  7.  Menos goles en contra                    |");
    Console.WriteLine("|  8.  Más partidos ganados                     |");
    Console.WriteLine("|  9.  Más empates                              |");
    Console.WriteLine("|  10. Más derrotas                             |");
    Console.WriteLine("|  11. Equipos invictos                         |");
    Console.WriteLine("|  12. Equipos sin victorias                    |");
    Console.WriteLine("|  13. Top 3 de la tabla                        |");
    Console.WriteLine("|  14. Diferencia de gol positiva               |");
    Console.WriteLine("|  15. Equipos con más de N puntos              |");
    Console.WriteLine("|  16. Buscar equipo por nombre                 |");
    Console.WriteLine("|  17. Promedio goles a favor                   |");
    Console.WriteLine("|  18. Promedio goles en contra                 |");
    Console.WriteLine("|  19. Total de goles en el torneo              |");
    Console.WriteLine("|  20. Total de puntos acumulados               |");
    Console.WriteLine("|  21. Tabla con proyección personalizada       |");
    Console.WriteLine("|  22. Equipos ordenados alfabéticamente        |");
    Console.WriteLine("|  23. Resumen general del torneo               |");
    Console.WriteLine("|  24. Equipos bajo el promedio de puntos       |");
    Console.WriteLine("|  25. Estadísticas destacadas                  |");
    Console.WriteLine("|  26. Ranking agrupado por puntos              |");
    Console.WriteLine("|-----------------------------------------------|");
    Console.WriteLine("|  0. Salir                                     |");
    Console.WriteLine("|-----------------------------------------------|");
    Console.Write("\nElige una opción: ");

    opcion = Console.ReadLine() ?? "";
    Console.WriteLine();
    // switch con todas las opciones del menú
    switch (opcion)
    {
        case "1": 
            torneo.ListarEquipos(); 
            break;

        case "2": 
            torneo.RegistrarEquipo(); 
            // Guardamos después de registrar un equipo nuevo
            PersistenciaService.Guardar(equipos);
            break;


        case "3": 
            torneo.SimularPartido(); 
            // Guardamos después de simular — las estadísticas cambiaron
            PersistenciaService.Guardar(equipos);
            break;

        case "4": consultas.MostrarTabla(); break;
        case "5": consultas.MostrarLider(); break;
        case "6": consultas.MostrarMasGolesAFavor(); break;
        case "7": consultas.MostrarMenosGolesEnContra(); break;
        case "8": consultas.MostrarMasGanados(); break;
        case "9": consultas.MostrarMasEmpates(); break;
        case "10": consultas.MostrarMasDerrotas(); break;
        case "11": consultas.MostrarInvictos(); break;
        case "12": consultas.MostrarSinVictorias(); break;
        case "13": consultas.MostrarTop3(); break;
        case "14": consultas.MostrarDiferenciaPositiva(); break;
        case "15": consultas.MostrarConMasDePuntos(); break;
        case "16": consultas.BuscarPorNombre(); break;
        case "17": consultas.MostrarPromedioGolesFavor(); break;
        case "18": consultas.MostrarPromedioGolesContra(); break;
        case "19": consultas.MostrarTotalGoles(); break;
        case "20": consultas.MostrarTotalPuntos(); break;
        case "21": consultas.MostrarProyeccionPersonalizada(); break;
        case "22": consultas.MostrarAlfabeticamente(); break;
        case "23": consultas.MostrarResumenGeneral(); break;
        case "24": consultas.MostrarBajoPromedioPuntos(); break;
        case "25": consultas.MostrarEstadisticasDestacadas(); break;
        case "26": consultas.MostrarRankingAgrupado(); break;

        case "0":  
            // Guardado final antes de salir
            PersistenciaService.Guardar(equipos);
            Console.WriteLine("¡Hasta luego!"); 
            break;

        default:  
            Console.WriteLine("Opción inválida. Elige entre 0 y 26."); 
            break;
    }

} while (opcion != "0");