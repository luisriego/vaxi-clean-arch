using System.Runtime.ConstrainedExecution;

namespace CleanArchitecture.Domain.Vehiculos;

public record Moneda(decimal Monto, TipoMoneda TipoMoneda)
{
    public static Moneda operator +(Moneda primero, Moneda segundo)
    {
        if (primero.TipoMoneda != segundo.TipoMoneda)
        {
            throw new InvalidOperationException("No se pueden sumar montos de distintas monedas");
        }

        return new Moneda(primero.Monto + segundo.Monto, primero.TipoMoneda);
    }

    public static Moneda Cero() => new(0, TipoMoneda.None);

    public static Moneda Cero(TipoMoneda TipoMoneda) => new(0, TipoMoneda);

    public bool EsCero() => this == Cero(TipoMoneda);
}