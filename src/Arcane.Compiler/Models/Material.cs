using System.Text.Json.Serialization;

namespace Arcane.Compiler.Models;

internal class Material
{
    [JsonPropertyName("name")]
    public string Name { get; init; }

    [JsonPropertyName("shaders")]
    public Shaders Shaders { get; init; }

    [JsonPropertyName("textures")]
    public Textures Textures { get; init; }
}
