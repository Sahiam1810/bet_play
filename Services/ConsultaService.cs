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

    // ─── 2. Líder del torneo 

    public void MostrarLider()
    {
        if (!HayDatos()) return;

        // FirstOrDefault con el mismo ordenamiento de la tabla
        var lider = _equipos
            .Where(e => e.PJ > 0)
            .OrderByDescending(e => e.TP)
            .ThenByDescending(e => e.DiferenciaGol)
            .ThenByDescending(e => e.GF)
            .FirstOrDefault();

        Console.WriteLine($"\n  🏆 Líder actual: {lider?.Nombre} — {lider?.TP} puntos");
    }

    // ─── 3. Equipos con más goles a favor 

    public void MostrarMasGolesAFavor()
    {
        if (!HayDatos()) return;

        // Máximo de goles a favor en el torneo
        int maxGF = _equipos.Max(e => e.GF);

        var equipos = _equipos
            .Where(e => e.GF == maxGF)
            .OrderBy(e => e.Nombre)
            .ToList();

        Console.WriteLine($"\n  Equipos con más goles a favor ({maxGF} goles):");
        equipos.ForEach(e => Console.WriteLine($"    - {e.Nombre}: {e.GF} GF"));
    }

    // ─── 4. Equipos con menos goles en contra 

    public void MostrarMenosGolesEnContra()
    {
        if (!HayDatos()) return;

        int minGC = _equipos.Where(e => e.PJ > 0).Min(e => e.GC);

        var equipos = _equipos
            .Where(e => e.PJ > 0 && e.GC == minGC)
            .OrderBy(e => e.Nombre)
            .ToList();

        Console.WriteLine($"\n  Equipos con menos goles en contra ({minGC} goles recibidos):");
        equipos.ForEach(e => Console.WriteLine($"    - {e.Nombre}: {e.GC} GC"));
    }

    // ─── 5. Equipos con más partidos ganados 

    public void MostrarMasGanados()
    {
        if (!HayDatos()) return;

        int maxPG = _equipos.Max(e => e.PG);

        var equipos = _equipos
            .Where(e => e.PG == maxPG)
            .OrderBy(e => e.Nombre)
            .ToList();

        Console.WriteLine($"\n  Equipos con más victorias ({maxPG} ganados):");
        equipos.ForEach(e => Console.WriteLine($"    - {e.Nombre}: {e.PG} PG"));
    }

    // ─── 6. Equipos con más empates 

    public void MostrarMasEmpates()
    {
        if (!HayDatos()) return;

        // OrderByDescending para mostrar del mayor al menor
        var equipos = _equipos
            .Where(e => e.PE > 0)
            .OrderByDescending(e => e.PE)
            .ThenBy(e => e.Nombre)
            .ToList();

        Console.WriteLine("\n  Equipos con más empates:");
        if (equipos.Count == 0) { Console.WriteLine("    (ningún equipo ha empatado aún)"); return; }
        equipos.ForEach(e => Console.WriteLine($"    - {e.Nombre}: {e.PE} empate(s)"));
    }

    // ─── 7. Equipos con más derrotas 

    public void MostrarMasDerrotas()
    {
        if (!HayDatos()) return;

        var equipos = _equipos
            .Where(e => e.PP > 0)
            .OrderByDescending(e => e.PP)
            .ThenBy(e => e.Nombre)
            .ToList();

        Console.WriteLine("\n  Equipos con más derrotas:");
        if (equipos.Count == 0) { Console.WriteLine("    (ningún equipo ha perdido aún)"); return; }
        equipos.ForEach(e => Console.WriteLine($"    - {e.Nombre}: {e.PP} derrota(s)"));
    }

}