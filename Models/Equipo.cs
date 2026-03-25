using System;

namespace liga_betplay.models;

// Representa un equipo participante en el torneo Liga BetPlay.
// Almacena todas las estadísticas competitivas del equipo.
public class Equipo
{
    // ─── Propiedades 

    public string Nombre { get; set; } = "";
    public int PJ { get; set; } = 0;   // Partidos Jugados
    public int PG { get; set; } = 0;   // Partidos Ganados
    public int PE { get; set; } = 0;   // Partidos Empatados
    public int PP { get; set; } = 0;   // Partidos Perdidos
    public int GF { get; set; } = 0;   // Goles a Favor
    public int GC { get; set; } = 0;   // Goles en Contra

    // TP se calcula automáticamente: 3 puntos por victoria, 1 por empate
    public int TP => (PG * 3) + PE;

    // Diferencia de gol — criterio de desempate en la tabla
    public int DiferenciaGol => GF - GC;

    // ─── Constructor 

    public Equipo(string nombre)
    {
        Nombre = nombre;
    }

    // ─── Métodos 

    // Actualiza las estadísticas del equipo después de un partido.
    public void AgregarResultado(int golesFavor, int golesContra)
    {
        PJ++; // siempre suma un partido jugado
        GF += golesFavor;
        GC += golesContra;

        // Clasificamos el resultado
        if (golesFavor > golesContra)
            PG++; // victoria 3 puntos (calculado en TP)
        else if (golesFavor == golesContra)
            PE++; // empate 1 punto
        else
            PP++; // derrota 0 puntos
    }

    public override string ToString() => Nombre;
}