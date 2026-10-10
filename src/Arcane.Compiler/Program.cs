using Arcane.Compiler.Models;

using System.Text;
using System.Text.Json;

namespace Arcane.Compiler;

internal class Program
{
    public static void Main(string[] _)
    {
        CompileMesh("Cube");
        CompileMesh("Grid");
        CompileMaterial("Default");
    }

    private static void CompileMesh(string name)
    {
        Console.WriteLine($"Compiling Mesh: {name}");
        var mesh = JsonSerializer.Deserialize<Mesh>(File.ReadAllText($@"C:\arcane\arcane-data\meshes\{name}.json"))!;

        using var stream = File.Create($@"C:\arcane\arcane-data\meshes\{name}.bin");
        using var writer = new BinaryWriter(stream);

        writer.Write(mesh.Vertices.Count);
        if (mesh.Vertices[0].TextureCoordinate1 is { })
        {
            writer.Write(4 * 10);
        }
        else
        {
            writer.Write(4 * 8);
        }
        writer.Write(mesh.Indices.Count);

        foreach (var v in mesh.Vertices)
        {
            writer.Write(v.Position.X);
            writer.Write(v.Position.Y);
            writer.Write(v.Position.Z);
            writer.Write(v.Normal.X);
            writer.Write(v.Normal.Y);
            writer.Write(v.Normal.Z);
            if (v.TextureCoordinate0 is { } uv0)
            {
                writer.Write(uv0.U);
                writer.Write(uv0.V);
            }
            if (v.TextureCoordinate1 is { } uv1)
            {
                writer.Write(uv1.U);
                writer.Write(uv1.V);
            }
        }

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