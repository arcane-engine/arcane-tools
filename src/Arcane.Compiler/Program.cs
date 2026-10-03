using Arcane.Compiler.Models;

using System.Text.Json;

namespace Arcane.Compiler
{
    internal class Program
    {
        public static void Main(string[] _)
        {
            var mesh = JsonSerializer.Deserialize<Mesh>(File.ReadAllText(@"C:\arcane\arcane-tools\data\Cube.json"))!;

            using var stream = File.OpenWrite(@"C:\arcane\arcane-tools\data\Cube.bin");
            using var writer = new BinaryWriter(stream);

            writer.Write(mesh.Vertices.Count);

            foreach (var v in mesh.Vertices)
            {
                writer.Write(v.X);
                writer.Write(v.Y);
                writer.Write(v.Z);
                writer.Write(v.U);
                writer.Write(v.V);
            }

            writer.Write(mesh.Indices.Count);

            foreach (var index in mesh.Indices)
            {
                writer.Write((uint)index);
            }
        }
    }
}
