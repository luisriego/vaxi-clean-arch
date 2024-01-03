using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using Dapper;

namespace CleanArchitecture.Application.Alquileres.ObterAlquiler;

internal sealed class ObterAlquilerQueryHandler : IQueryHandler<ObterAlquilerQuery, AlquilerResponse>
{
    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public ObterAlquilerQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<Result<AlquilerResponse>> Handle(
        ObterAlquilerQuery request, 
        CancellationToken cancellationToken
        )
    {
        using var connection = _sqlConnectionFactory.CreateConnection();

        var sql = """
            SELECT
                id AS Id,
                vehiculo_id AS VehiculoId,
                usuario_id AS UsuarioId,
                status AS Status,
                precio_por_periodo AS PrecioAlquiler,
                precio_por_periodo_tipo_moneda AS TipoMonedaAlquiler,
                precio_mantenimiento AS PrecioMantenimiento,
                precio_mantenimiento_tipo_moneda AS TipoMonedaMantenimiento,
                Precio_accesorios AS PrecioAccesorios,
                precio_accesorios_tipo_moneda AS TipoMonedaAccesorios,
                precio_total AS PrecioTotal,
                precio_total_tipo_moneda AS TipoMonedaPrecioTotal,
                duracion_inicio AS DuracionInicio,
                duracion_final AS DuracionFinal,
                fecha_creacion AS FechaCreacion
            FROM alquileres
            WHERE id = @AlquilerId
        """;

        var alquiler = await connection.QueryFirstOrDefaultAsync<AlquilerResponse>(
            sql,
            new { request.AlquilerId }
            );

        return alquiler!;
    }
}
