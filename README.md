# 🏆 Simulador Liga BetPlay - C#

Aplicación de consola desarrollada en **C#** que simula el funcionamiento de la **Liga BetPlay colombiana**, permitiendo gestionar equipos, simular partidos y consultar estadísticas del torneo de forma automatizada.

![.NET](https://img.shields.io/badge/.NET-10.0-purple?logo=dotnet)
![C#](https://img.shields.io/badge/C%23-Consola-blue?logo=csharp)
![POO](https://img.shields.io/badge/Paradigma-POO-green)
![LINQ](https://img.shields.io/badge/Consultas-LINQ-orange)

---

## 📋 Descripción

El sistema automatiza la gestión de una liga de fútbol desde consola, eliminando errores manuales en el cálculo de puntos, estadísticas y tabla de posiciones. Aplica **Programación Orientada a Objetos**, **listas en memoria** y **consultas LINQ** sobre colecciones de objetos.

---

## 🗂️ Estructura del proyecto

```
liga_betplay/
├── Models/
│   └── Equipo.cs                
├── Services/
│   ├── TorneoService.cs       
│   └── ConsultaService.cs     
├── Data/
│   └── DatosIniciales.cs      
└── Program.cs                  
```

---

## ⚙️ Funcionalidades

### 1. Gestión de equipos
- Listar todos los equipos registrados
- Registrar nuevos equipos al torneo

### 2. Simulación de partidos
- **Resultado aleatorio** — goles generados automáticamente (0 a 4)
- **Resultado manual** — el usuario ingresa los goles de cada equipo
- Actualización automática de estadísticas tras cada partido

### 3. Tabla de posiciones
Ordenada por los siguientes criterios en cascada:
1. Puntos (TP)
2. Diferencia de gol (DG)
3. Goles a favor (GF)
4. Nombre del equipo

Vistas disponibles: completa, resumida, Top 3 y ranking con zonas de clasificación.

### 4. Consultas LINQ disponibles

| # | Consulta |
|---|----------|
| 1 | Resumen general del torneo |
| 2 | Estadísticas destacadas |
| 3 | Líder del torneo |
| 4 | Equipo con más goles a favor |
| 5 | Equipo con menos goles en contra |
| 6 | Equipo con más victorias |
| 7 | Equipos con más empates |
| 8 | Equipos con más derrotas |
| 9 | Equipos invictos |
| 10 | Equipos sin victorias |
| 11 | Equipos con diferencia de gol positiva |
| 12 | Equipos con más de X puntos |
| 13 | Buscar equipo por nombre |
| 14 | Promedio de goles a favor |
| 15 | Promedio de goles en contra |
| 16 | Total de goles del torneo |
| 17 | Total de puntos del torneo |
| 18 | Equipos ordenados alfabéticamente |
| 19 | Equipos por debajo del promedio de puntos |

---

## 📊 Modelo de datos — `Equipo`

| Campo | Tipo | Descripción |
|-------|------|-------------|
| `Nombre` | `string` | Nombre del equipo |
| `PJ` | `int` | Partidos Jugados |
| `PG` | `int` | Partidos Ganados |
| `PE` | `int` | Partidos Empatados |
| `PP` | `int` | Partidos Perdidos |
| `GF` | `int` | Goles a Favor |
| `GC` | `int` | Goles en Contra |
| `DG` | `int` | Diferencia de Gol *(calculado: GF - GC)* |
| `TP` | `int` | Total de Puntos *(calculado: PG×3 + PE×1)* |

---

## 🚀 Cómo ejecutar el proyecto

### Requisitos previos
- [.NET SDK 10.0](https://dotnet.microsoft.com/download) o superior
- [Visual Studio Code](https://code.visualstudio.com/)

### Pasos

**1. Clona el repositorio**
```bash
git clone https://github.com/Sahiam1810/ejercicios-csharp.git
cd ejercicios-csharp/liga_betplay
```

**2. Ejecuta el proyecto**
```bash
dotnet run
```

---

## 🖥️ Menú principal

```
╔══════════════════════════════╗
║         MENÚ PRINCIPAL       ║
╠══════════════════════════════╣
║  1. Listar equipos           ║
║  2. Registrar equipo         ║
║  3. Simular partido          ║
║  4. Tabla de posiciones      ║
║  5. Estadísticas del torneo  ║
║  6. Salir                    ║
╚══════════════════════════════╝
```

---

## 🛠️ Tecnologías

- **Lenguaje:** C#
- **Framework:** .NET 10.0
- **Paradigma:** Programación Orientada a Objetos (POO)
- **Estructuras de datos:** Listas en memoria (`List<Equipo>`)
- **Consultas:** LINQ
- **Tipo:** Aplicación de consola

---

## 👤 Autor

**Sahiam1810**
[github.com/Sahiam1810](https://github.com/Sahiam1810)