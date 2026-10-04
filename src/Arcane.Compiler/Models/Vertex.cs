using System.Text.Json.Serialization;

namespace Arcane.Compiler.Models;

internal class Vertex
{
    [JsonPropertyName("x")]
    public float X { get; init; }

    [JsonPropertyName("y")]
    public float Y { get; init; }

    [JsonPropertyName("z")]
    public float Z { get; init; }

    [JsonPropertyName("nx")]
    public float NormalX { get; init; }

    [JsonPropertyName("ny")]
    public float NormalY { get; init; }

    [JsonPropertyName("nz")]
    public float NormalZ { get; init; }

    [JsonPropertyName("u")]
    public float U { get; init; }

    [JsonPropertyName("v")]
    public float V { get; init; }
}
