using System;

using liga_betplay.models;
namespace liga_betplay.Data;

/// Provee los 20 equipos de la Liga BetPlay 
public static class DatosIniciales
{
    // Retorna la lista con los 20 equipos participantes del torneo.
    public static List<Equipo> ObtenerEquipos() => new()
    {
        new Equipo("Atlético Nacional"),
        new Equipo("Millonarios FC"),
        new Equipo("América de Cali"),
        new Equipo("Independiente Santa Fe"),
        new Equipo("Deportivo Cali"),
        new Equipo("Junior FC"),
        new Equipo("Deportivo Pereira"),
        new Equipo("Atlético Bucaramanga"),
        new Equipo("Envigado FC"),
        new Equipo("Once Caldas"),
        new Equipo("Independiente Medellín"),
        new Equipo("Alianza FC"),
        new Equipo("Deportivo Pasto"),
        new Equipo("La Equidad"),
        new Equipo("Jaguares de Córdoba"),
        new Equipo("Boyacá Chicó"),
        new Equipo("Patriotas Boyacá"),
        new Equipo("Unión Magdalena"),
        new Equipo("Real Cartagena"),
        new Equipo("Fortaleza CEIF"),
    };
}