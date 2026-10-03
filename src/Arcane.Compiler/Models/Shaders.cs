using System.Text.Json.Serialization;

namespace Arcane.Compiler.Models;

internal class Shaders
{
    [JsonPropertyName("vertex")]
    public string Vertex { get; init; }

    [JsonPropertyName("pixel")]
    public string Pixel { get; init; }
}
