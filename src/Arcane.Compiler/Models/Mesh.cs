using System.Text.Json.Serialization;

namespace Arcane.Compiler.Models;

internal class Mesh
{
    [JsonPropertyName("vertices")]
    public List<Vertex> Vertices { get; init; }

    [JsonPropertyName("indices")]
    public List<int> Indices { get; init; }
}
