namespace Infra.Data.Settings
{
    public record DatabaseContextSettings
    {
        public required string ConnectionString { get; init; }
        public required string DataBase { get; init; }
    }
}
