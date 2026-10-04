using Arcane.Compiler.Models;

using System.Text;
using System.Text.Json;

namespace Arcane.Compiler;

internal class Program
{
    public static void Main(string[] _)
    {
        CompileMesh("Cube");
        CompileMaterial("Default");
    }

    private static void CompileMesh(string name)
    {
        Console.WriteLine($"Compiling Mesh: {name}");
        var mesh = JsonSerializer.Deserialize<Mesh>(File.ReadAllText($@"C:\arcane\arcane-data\meshes\{name}.json"))!;

        using var stream = File.Create($@"C:\arcane\arcane-data\meshes\{name}.bin");
        using var writer = new BinaryWriter(stream);

        writer.Write(mesh.Vertices.Count);

        foreach (var v in mesh.Vertices)
        {
            writer.Write(v.X);
            writer.Write(v.Y);
            writer.Write(v.Z);
            writer.Write(v.NormalX);
            writer.Write(v.NormalY);
            writer.Write(v.NormalZ);
            writer.Write(v.U);
            writer.Write(v.V);
        }

        writer.Write(mesh.Indices.Count);

        foreach (var index in mesh.Indices)
        {
            writer.Write((uint)index);
        }
    }

    private static void CompileMaterial(string name)
    {
        Console.WriteLine($"Compiling Material: {name}");
        var material = JsonSerializer.Deserialize<Material>(File.ReadAllText($@"C:\arcane\arcane-data\materials\{name}.json"))!;

        using var stream = File.Create($@"C:\arcane\arcane-data\materials\{name}.bin");
        using var writer = new BinaryWriter(stream);

        var bytes = Encoding.UTF8.GetBytes(material.Shaders.Vertex);
        writer.Write(bytes.Length);
        writer.Write(bytes);

        bytes = Encoding.UTF8.GetBytes(material.Shaders.Pixel);
        writer.Write(bytes.Length);
        writer.Write(bytes);

        bytes = Encoding.UTF8.GetBytes(material.Textures.Albedo);
        writer.Write(bytes.Length);
        writer.Write(bytes);
    }
}