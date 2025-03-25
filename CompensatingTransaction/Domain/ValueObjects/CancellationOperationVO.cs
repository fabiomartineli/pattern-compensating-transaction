namespace Domain.ValueObjects
{
    public readonly struct CancellationOperationVO<TData>
    {
        public required TData CancellationData { get; init; }
        public required string Note { get; init; }
    }
}
