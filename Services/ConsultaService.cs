using System;

using liga_betplay.models;
namespace liga_betplay.Services;

// Servicio que centraliza todas las consultas LINQ sobre los equipos del torneo.
public class ConsultaService
{
    private readonly List<Equipo> _equipos;

    public ConsultaService(List<Equipo> equipos)
    {
        _equipos = equipos;
    }

    // Encabezado de la tabla de posiciones
    private static void ImprimirEncabezadoTabla()
    {
        Console.WriteLine();
        Console.WriteLine($"  {"#",-3} {"Equipo",-28} {"PJ",3} {"PG",3} {"PE",3} {"PP",3} {"GF",3} {"GC",3} {"DG",4} {"TP",4}");
        Console.WriteLine("  " + new string('─', 65));
    }

    // Imprime una fila de la tabla
    private static void ImprimirFilaTabla(int pos, Equipo e)
    {
        Console.WriteLine(
            $"  {pos,-3} {e.Nombre,-28} {e.PJ,3} {e.PG,3} {e.PE,3} {e.PP,3} " +
            $"{e.GF,3} {e.GC,3} {e.DiferenciaGol,4} {e.TP,4}");
    }

    // Verifica que haya equipos con partidos jugados antes de consultar
    private bool HayDatos()
    {
        if (!_equipos.Any(e => e.PJ > 0))
        {
            Console.WriteLine("\n Aún no hay partidos jugados. Simula partidos primero.");
            return false;
        }
        return true;
    }

    // ─── 1. Tabla de posiciones ordenada 

    // Muestra la tabla completa ordenada por: puntos, diferencia de gol, goles a favor, nombre.
    public void MostrarTabla()
    {
        // Criterios de ordenamiento según el enunciado
        var tabla = _equipos
            .Where(e => e.PJ > 0)
            .OrderByDescending(e => e.TP) // 1. Puntos
            .ThenByDescending(e => e.DiferenciaGol) // 2. Diferencia de gol
            .ThenByDescending(e => e.GF) // 3. Goles a favor
            .ThenBy(e => e.Nombre) // 4. Nombre alfabético
            .ToList();

        if (tabla.Count == 0) { Console.WriteLine("\n No hay partidos jugados."); return; }

        Console.WriteLine("\n =================================================================");
        Console.WriteLine("   |              TABLA DE POSICIONES — LIGA BETPLAY                 |");
        Console.WriteLine("    ================================================================");
        ImprimirEncabezadoTabla();

        for (int i = 0; i < tabla.Count; i++)
            ImprimirFilaTabla(i + 1, tabla[i]);
    }
}