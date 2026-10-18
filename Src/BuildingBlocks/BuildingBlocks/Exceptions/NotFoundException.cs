namespace BuildingBlocks.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string Name, object key) : base($"Entity \"{Name}\" ({key}) was not found.")
    {
    }
}
