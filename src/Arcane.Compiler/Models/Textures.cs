using System.Text.Json.Serialization;

namespace Arcane.Compiler.Models;

internal class Textures
{
    [JsonPropertyName("albedo")]
    public string Albedo { get; init; }
}
