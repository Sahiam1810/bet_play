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

// ─── 8. Equipos invictos (no han perdido ningún partido) 

    public void MostrarInvictos()
    {
        if (!HayDatos()) return;

        // Invicto = ha jugado partidos Y no ha perdido ninguno
        var invictos = _equipos
            .Where(e => e.PJ > 0 && e.PP == 0)
            .OrderByDescending(e => e.TP)
            .ToList();

        Console.WriteLine("\n  Equipos invictos (PP = 0):");
        if (invictos.Count == 0) { Console.WriteLine("    (ningún equipo está invicto)"); return; }
        invictos.ForEach(e => Console.WriteLine($"    - {e.Nombre}: {e.PJ} PJ, {e.TP} pts"));
    }

    // ─── 9. Equipos sin victorias 

    public void MostrarSinVictorias()
    {
        if (!HayDatos()) return;

        var sinVictorias = _equipos
            .Where(e => e.PJ > 0 && e.PG == 0)
            .OrderBy(e => e.Nombre)
            .ToList();

        Console.WriteLine("\n  Equipos sin victorias (PG = 0):");
        if (sinVictorias.Count == 0) { Console.WriteLine("    (todos los equipos han ganado al menos un partido)"); return; }
        sinVictorias.ForEach(e => Console.WriteLine($"    - {e.Nombre}: {e.PJ} PJ, {e.PE} empate(s)"));
    }

    // ─── 10. Top 3 de la tabla 

    public void MostrarTop3()
    {
        if (!HayDatos()) return;

        // toma solo los 3 primeros de la tabla ordenada
        var top3 = _equipos
            .Where(e => e.PJ > 0)
            .OrderByDescending(e => e.TP)
            .ThenByDescending(e => e.DiferenciaGol)
            .ThenByDescending(e => e.GF)
            .Take(3)
            .ToList();

        Console.WriteLine("\n 🏅 Top 3 de la tabla:");
        string[] medallas = { "🥇", "🥈", "🥉" };
        for (int i = 0; i < top3.Count; i++)
            Console.WriteLine($" {medallas[i]} {top3[i].Nombre} — {top3[i].TP} pts, DG: {top3[i].DiferenciaGol}");
    }

    // ─── 11. Equipos con diferencia de gol positiva 

    public void MostrarDiferenciaPositiva()
    {
        if (!HayDatos()) return;

        var equipos = _equipos
            .Where(e => e.PJ > 0 && e.DiferenciaGol > 0)
            .OrderByDescending(e => e.DiferenciaGol)
            .ToList();

        Console.WriteLine("\n  Equipos con diferencia de gol positiva:");
        if (equipos.Count == 0) { Console.WriteLine("    (ninguno)"); return; }
        equipos.ForEach(e => Console.WriteLine($"    - {e.Nombre}: DG {e.DiferenciaGol:+#;-#;0}"));
    }

    // ─── 12. Equipos con más de N puntos 

    public void MostrarConMasDePuntos()
    {
        if (!HayDatos()) return;

        Console.Write("\n  ¿Mínimo de puntos? ");
        if (!int.TryParse(Console.ReadLine(), out int minPuntos) || minPuntos < 0)
        {
            Console.WriteLine("  Valor inválido.");
            return;
        }

        var equipos = _equipos
            .Where(e => e.TP > minPuntos)
            .OrderByDescending(e => e.TP)
            .ToList();

        Console.WriteLine($"\n  Equipos con más de {minPuntos} punto(s):");
        if (equipos.Count == 0) { Console.WriteLine(" (ninguno)"); return; }
        equipos.ForEach(e => Console.WriteLine($" - {e.Nombre}: {e.TP} pts"));
    }

    // ─── 13. Buscar equipo por nombre 

    public void BuscarPorNombre()
    {
        Console.Write("\n  Nombre a buscar: ");
        string busqueda = Console.ReadLine()?.Trim() ?? "";

        // FirstOrDefault con Contains para búsqueda parcial sin distinción de mayúsculas
        var equipo = _equipos.FirstOrDefault(e =>
            e.Nombre.Contains(busqueda, StringComparison.OrdinalIgnoreCase));

        if (equipo is null)
        {
            Console.WriteLine($"  ✗ No se encontró ningún equipo con \"{busqueda}\".");
            return;
        }

        Console.WriteLine($"\n  Equipo encontrado: {equipo.Nombre}");
        Console.WriteLine($" PJ:{equipo.PJ}  PG:{equipo.PG}  PE:{equipo.PE}  PP:{equipo.PP}");
        Console.WriteLine($" GF:{equipo.GF}  GC:{equipo.GC}  DG:{equipo.DiferenciaGol}  TP:{equipo.TP}");
    }

    // ─── 14. Promedio de goles a favor 

    public void MostrarPromedioGolesFavor()
    {
        if (!HayDatos()) return;

        // Average() calcula la media de los valores seleccionados
        double promedio = _equipos
            .Where(e => e.PJ > 0)
            .Average(e => e.GF);

        Console.WriteLine($"\n  Promedio de goles a favor por equipo: {promedio:F2}");
    }

    // ─── 15. Promedio de goles en contra 

    public void MostrarPromedioGolesContra()
    {
        if (!HayDatos()) return;

        double promedio = _equipos
            .Where(e => e.PJ > 0)
            .Average(e => e.GC);

        Console.WriteLine($"\n  Promedio de goles en contra por equipo: {promedio:F2}");
    }
// ─── 16. Total de goles en el torneo 

    public void MostrarTotalGoles()
    {
        // Sum() suma todos los goles de todos los equipos
        // Dividimos entre 2 porque cada gol se registra en los dos equipos del partido
        int totalGF = _equipos.Sum(e => e.GF);
        Console.WriteLine($"\n  Total de goles marcados en el torneo: {totalGF / 2}");
        Console.WriteLine($"  (GF acumulado de todos los equipos: {totalGF} / 2)");
    }

    // ─── 17. Total de puntos sumados 

    public void MostrarTotalPuntos()
    {
        int total = _equipos.Sum(e => e.TP);
        Console.WriteLine($"\n  Total de puntos sumados por todos los equipos: {total}");
    }

    // ─── 18. Tabla con proyección personalizada (Select) 

    public void MostrarProyeccionPersonalizada()
    {
        if (!HayDatos()) return;

        // Select proyecta solo los campos que nos interesan
        var proyeccion = _equipos
            .Where(e => e.PJ > 0)
            .OrderByDescending(e => e.TP)
            .Select(e => new
            {
                e.Nombre,
                e.TP,
                e.DiferenciaGol,
                Rendimiento = e.PJ > 0 ? (double)e.PG / e.PJ * 100 : 0
            })
            .ToList();

        Console.WriteLine("\n  Proyección personalizada (rendimiento %):");
        Console.WriteLine($"  {"Equipo",-28} {"TP",4} {"DG",4} {"Rend%",7}");
        Console.WriteLine("  " + new string('─', 48));
        proyeccion.ForEach(e =>
            Console.WriteLine($"  {e.Nombre,-28} {e.TP,4} {e.DiferenciaGol,4} {e.Rendimiento,6:F1}%"));
    }

    // ─── 19. Equipos ordenados alfabéticamente 

    public void MostrarAlfabeticamente()
    {
        Console.WriteLine("\n  Equipos ordenados alfabéticamente:");
        _equipos
            .OrderBy(e => e.Nombre)
            .ToList()
            .ForEach((e) => Console.WriteLine($"    - {e.Nombre}"));
    }

    // ─── 20. Resumen general del torneo 

    public void MostrarResumenGeneral()
    {
        int totalPartidos  = _equipos.Sum(e => e.PJ) / 2;
        int totalGoles     = _equipos.Sum(e => e.GF) / 2;
        int equiposConPJ   = _equipos.Count(e => e.PJ > 0);

        Console.WriteLine("\n  ──────────────────────────────────");
        Console.WriteLine("       Resumen general del torneo");
        Console.WriteLine("  ──────────────────────────────────");
        Console.WriteLine($"  Equipos registrados:  {_equipos.Count}");
        Console.WriteLine($"  Equipos con partidos: {equiposConPJ}");
        Console.WriteLine($"  Partidos jugados:     {totalPartidos}");
        Console.WriteLine($"  Goles marcados:       {totalGoles}");
        Console.WriteLine($"  Promedio goles/pdo:   {(totalPartidos > 0 ? (double)totalGoles / totalPartidos : 0):F2}");
        Console.WriteLine("  ──────────────────────────────────");
    }

    // ─── 21. Equipos por debajo del promedio de puntos 

    public void MostrarBajoPromedioPuntos()
    {
        if (!HayDatos()) return;

        double promedio = _equipos.Where(e => e.PJ > 0).Average(e => e.TP);

        var equipos = _equipos
            .Where(e => e.PJ > 0 && e.TP < promedio)
            .OrderBy(e => e.TP)
            .ToList();

        Console.WriteLine($"\n  Promedio de puntos: {promedio:F2}");
        Console.WriteLine("  Equipos por debajo del promedio:");
        if (equipos.Count == 0) { Console.WriteLine("    (ninguno)"); return; }
        equipos.ForEach(e => Console.WriteLine($"    - {e.Nombre}: {e.TP} pts"));
    }

    // ─── 22. Estadísticas destacadas 

    public void MostrarEstadisticasDestacadas()
    {
        if (!HayDatos()) return;

        var conPartidos = _equipos.Where(e => e.PJ > 0).ToList();

        var maxGF  = conPartidos.OrderByDescending(e => e.GF).First();
        var minGC  = conPartidos.OrderBy(e => e.GC).First();
        var maxPG  = conPartidos.OrderByDescending(e => e.PG).First();
        var maxPE  = conPartidos.OrderByDescending(e => e.PE).First();
        var maxPP  = conPartidos.OrderByDescending(e => e.PP).First();
        var lider  = conPartidos.OrderByDescending(e => e.TP)
                                .ThenByDescending(e => e.DiferenciaGol).First();

        Console.WriteLine("\n  --------------------------------");
        Console.WriteLine("       Estadísticas destacadas");
        Console.WriteLine("  ---------------------------------");
        Console.WriteLine($"   Líder:           {lider.Nombre} ({lider.TP} pts)");
        Console.WriteLine($"   Mejor ataque:    {maxGF.Nombre} ({maxGF.GF} GF)");
        Console.WriteLine($"   Mejor defensa:   {minGC.Nombre} ({minGC.GC} GC)");
        Console.WriteLine($"   Más victorias:   {maxPG.Nombre} ({maxPG.PG} PG)");
        Console.WriteLine($"   Más empates:     {maxPE.Nombre} ({maxPE.PE} PE)");
        Console.WriteLine($"   Más derrotas:    {maxPP.Nombre} ({maxPP.PP} PP)");
        Console.WriteLine("  ──────────────────────────────────");
    }

    // ─── 23. Ranking simple agrupado 

    public void MostrarRankingAgrupado()
    {
        if (!HayDatos()) return;

        // GroupBy agrupa equipos por cantidad de puntos
        // Luego ordenamos los grupos de mayor a menor
        var grupos = _equipos
            .Where(e => e.PJ > 0)
            .GroupBy(e => e.TP)
            .OrderByDescending(g => g.Key)
            .ToList();

        Console.WriteLine("\n  Ranking agrupado por puntos:");
        Console.WriteLine($"  {"Puntos",7}  Equipos");
        Console.WriteLine("  " + new string('─', 50));

        foreach (var grupo in grupos)
        {
            // Juntamos los nombres de los equipos con el mismo puntaje
            string nombres = string.Join(", ", grupo.Select(e => e.Nombre));
            Console.WriteLine($"  {grupo.Key,6} pts  {nombres}");
        }
    }
}