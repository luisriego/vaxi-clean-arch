using CleanArchitecture.Domain.Shared;
using CleanArchitecture.Domain.Vehiculos;

namespace CleanArchitecture.Domain.Alquileres;

public class PrecioService
{
    public PrecioDetalle CalcularPrecio(Vehiculo vehiculo, DateRange periodo)
    {
        var tipoMoneda = vehiculo.Precio!.TipoMoneda;
        var precioPorPeriodo = new Moneda(periodo.CantidadDias * vehiculo.Precio!.Monto, tipoMoneda);
        
        decimal porcentageChange = 0;

        foreach (var accesorio in vehiculo.Accesorios)
        {
            porcentageChange += accesorio switch
            {
                Accesorio.AppleCar or Accesorio.AndroidAuto => 0.05m,
                Accesorio.AireAcondicionado => 0.01m,
                Accesorio.Mapas => 0.02m,
                Accesorio.WiFi => 0.03m,
                _ => 0
            };
        }

        var accesorioCharges = Moneda.Cero(tipoMoneda);

        if (porcentageChange > 0)
        {
            accesorioCharges = new Moneda(precioPorPeriodo.Monto * porcentageChange, tipoMoneda);
        }

        var PrecioTotal = Moneda.Cero();
        PrecioTotal += precioPorPeriodo;

        if (!vehiculo!.Mantenimiento!.EsCero())
        {
            PrecioTotal += vehiculo.Mantenimiento;
        }

        PrecioTotal += accesorioCharges;

        return new PrecioDetalle(precioPorPeriodo, vehiculo.Mantenimiento, accesorioCharges, PrecioTotal);
    }
}