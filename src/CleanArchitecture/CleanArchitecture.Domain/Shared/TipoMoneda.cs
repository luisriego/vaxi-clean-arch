namespace CleanArchitecture.Domain.Shared;

public record TipoMoneda
{
    public static readonly TipoMoneda None = new("");
    public static readonly TipoMoneda Usd = new("USD");
    public static readonly TipoMoneda Eur = new("EUR");

    private TipoMoneda(string? codigo) => Codigo = codigo;

    public string? Codigo { get; init; }

    public static readonly IReadOnlyCollection<TipoMoneda> All = new[]
    {
        None,
        Usd,
        Eur
    };

    public static TipoMoneda FromCodigo(string? codigo) =>
        All.FirstOrDefault(c => c.Codigo == codigo) ??
        throw new ArgumentException($"No existe el tipo de moneda con código {codigo}.");   
};