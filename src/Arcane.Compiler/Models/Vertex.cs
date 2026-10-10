using System.Text.Json.Serialization;

namespace Arcane.Compiler.Models;

internal struct Position
{
    [JsonPropertyName("x")]
    public float X { get; init; }

    [JsonPropertyName("y")]
    public float Y { get; init; }

    [JsonPropertyName("z")]
    public float Z { get; init; }
}

internal struct TextureCoordinate
{
    [JsonPropertyName("u")]
    public float U { get; init; }
    [JsonPropertyName("v")]
    public float V { get; init; }
}

internal struct Normal
{
    [JsonPropertyName("x")]
    public float X { get; init; }

    [JsonPropertyName("y")]
    public float Y { get; init; }

    [JsonPropertyName("z")]
    public float Z { get; init; }
}

internal class Vertex
{
    [JsonPropertyName("position")]
    public Position Position { get; init; }

    [JsonPropertyName("uv0")]
    public TextureCoordinate? TextureCoordinate0 { get; init; }

    [JsonPropertyName("uv1")]
    public TextureCoordinate? TextureCoordinate1 { get; init; }

    [JsonPropertyName("normal")]
    public Normal Normal { get; init; }
}
