using liga_betplay.Data;
using liga_betplay.Services;
// para verificar funcionamiento, deben cargarse lo 20 equipos.
var equipos = DatosIniciales.ObtenerEquipos();
var torneoService = new TorneoService(equipos);
torneoService.ListarEquipos();

torneoService.SimularPartido("Atlético Nacional", "Millonarios FC");
torneoService.SimularPartido("América de Cali", "Junior de Barranquilla");

var consultaService = new ConsultaService(equipos);

consultaService.MostrarTabla();
// consultaService.MostrarEstadisticasDestacadas();