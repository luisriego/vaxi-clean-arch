using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Domain.Alquileres;
using Dapper;
using MediatR;

namespace CleanArchitecture.Application.Vehiculos.BuscarVehiculos;

internal sealed class BuscarVehiculoQueryHandler : IQueryHandler<BuscarVehiculoQuery, IReadOnlyList<VehiculoResponse>>
{
    private static readonly int[] ActiveAlquilerStatuses = 
    {
        (int)AlquilerStatus.Reservado,
        (int)AlquilerStatus.Confirmado,
        (int)AlquilerStatus.Completado
    };

    private readonly ISqlConnectionFactory _sqlConnectionFactory;

    public BuscarVehiculoQueryHandler(ISqlConnectionFactory sqlConnectionFactory)
    {
        _sqlConnectionFactory = sqlConnectionFactory;
    }

    public async Task<IReadOnlyList<VehiculoResponse>> Handle(BuscarVehiculoQuery request, CancellationToken cancellationToken)
    {
        if (request.fechaInicio > request.fechaFin)
        {
            return new List<VehiculoResponse>();
        }
        
        using var connection = _sqlConnectionFactory.CreateConnection();

        const string sql = """
            SELECT
                v.id AS Id,
                v.modelo AS Modelo,
                v.vin AS Vin,
                v.precio_monto AS Precio,
                v.precio_tipo_moneda AS TipoMoneda,
                v.direccion_pais AS Pais,
                v.direccion_departamento AS Departamento,
                v.direccion_provincia AS Provincia,
                v.direccion_ciudad AS Ciudad,
                v.direccion_calle AS Calle
            FROM vehiculos AS v
            WHERE NOT EXISTS (
                SELECT 1
                FROM alquileres AS a
                WHERE 
                    a.vehiculo_id = v.id
                    a.duracion_inicio <= @EndDate AND
                    a.duracion_final >= @StartDate AND
                    b.status = ANY(@ActiveAlquilerStatuses)
            )
        """;

        var vehiculos = await connection.QueryAsync<VehiculoResponse, DireccionResponse, VehiculoResponse>(
            sql,
            (vehiculo, direccion) =>
            {
                vehiculo.Direccion = direccion;
                return vehiculo;
            },
            new
            {
                StartDate = request.fechaInicio,
                EndDate = request.fechaFin,
                ActiveAlquilerStatuses
            },
            splitOn: "Pais"
        );

        return vehiculos.ToList();
    }

    Task<Result<IReadOnlyList<VehiculoResponse>>> IRequestHandler<BuscarVehiculoQuery, Result<IReadOnlyList<VehiculoResponse>>>.Handle(BuscarVehiculoQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}