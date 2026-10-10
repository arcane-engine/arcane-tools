import bpy
import json

obj = next((o for o in bpy.context.scene.objects if o.type == 'MESH'), None)
mesh = obj.data

mesh.calc_loop_triangles()

uv_layers = mesh.uv_layers
uv_layer_0 = uv_layers[0].data if len(uv_layers) > 0 else None
uv_layer_1 = uv_layers[1].data if len(uv_layers) > 1 else None

vertices = []
indices = []
vertex_cache = {}

for tri in mesh.loop_triangles:
    tri_indices = []

    for i in range(3):
        v_idx = tri.vertices[i]
        loop_idx = tri.loops[i]

        v_co = mesh.vertices[v_idx].co
        v_normal = mesh.loops[loop_idx].normal

        u0, v0 = 0.0, 0.0
        has_uv0 = uv_layer_0 is not None
        if has_uv0:
            uv0 = uv_layer_0[loop_idx].uv
            u0, v0 = uv0.x, uv0.y

        u1, v1 = 0.0, 0.0
        has_uv1 = uv_layer_1 is not None
        if has_uv1:
            uv1 = uv_layer_1[loop_idx].uv
            u1, v1 = uv1.x, uv1.y

        nx, ny, nz = round(v_normal.x, 5), round(v_normal.z, 5), round(v_normal.y, 5)

        cache_key = (v_idx, nx, ny, nz)
        if has_uv0:
            cache_key += (round(u0, 5), round(v0, 5))
        if has_uv1:
            cache_key += (round(u1, 5), round(v1, 5))

        if cache_key not in vertex_cache:
            new_idx = len(vertices)
            vertex_cache[cache_key] = new_idx

            vertex_data = {
                "position": { "x": v_co.x, "y": v_co.z, "z": v_co.y },
                "normal": { "x": v_normal.x, "y": v_normal.z, "z": v_normal.y }
            }
            if has_uv0:
                vertex_data["uv0"] = { "u": u0, "v": 1.0 - v0 }
            if has_uv1:
                vertex_data["uv1"] = { "u": u1, "v": 1.0 - v1 }

            vertices.append(vertex_data)
        else:
            new_idx = vertex_cache[cache_key]

        tri_indices.append(new_idx)

    indices.append(tri_indices[0])
    indices.append(tri_indices[2])
    indices.append(tri_indices[1])

data = {
    "vertices": vertices,
    "indices": indices
}

output_path = rf"C:\arcane\arcane-data\meshes\{obj.name}.json"
with open(output_path, "w") as f:
    json.dump(data, f, indent=2)