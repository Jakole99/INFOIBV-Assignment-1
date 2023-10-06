namespace INFOIBV.Framework;

/// <summary>
/// Pipeline for bitmap conversion using filters
/// </summary>
public sealed class FilterCollection : IImageProcessor
{
    public readonly List<IImageProcessor> ImageProcessors = new();

    public string DisplayName { get; }

    public FilterCollection(string displayName = "Collection")
    {
        DisplayName = displayName;
    }

    public static FilterCollection From(IImageProcessor process, string displayName = "Collection")
    {
        return new FilterCollection(displayName).AddProcess(process);
    }

    /// <summary>
    /// Queue a filter to the pipeline
    /// </summary>
    public FilterCollection AddProcess(IImageProcessor process)
    {
        ImageProcessors.Add(process);
        return this;
    }

    public byte[,] Process(byte[,] input)
    {
        if (!ImageProcessors.Any())
            return input;

        var output = ImageProcessors.First().Process(input);
        return ImageProcessors.Skip(1).Aggregate(output, (current, processor) => processor.Process(current));
    }

    public override string ToString()
    {
        return DisplayName;
    }
}