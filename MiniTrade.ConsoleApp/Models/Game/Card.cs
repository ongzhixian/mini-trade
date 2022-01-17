namespace MiniTrade.ConsoleApp.Models.Game;

public interface ICard
{
    uint Id { get; init; }
}

internal record Card : ICard
{
    public uint Id { get; init; }
}
