import bpy
import json

obj = next((o for o in bpy.context.scene.objects if o.type == 'MESH'), None)
mesh = obj.data

mesh.calc_loop_triangles()

uv_layer = mesh.uv_layers.active.data if mesh.uv_layers else None

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
        
        if uv_layer:
            uv = uv_layer[loop_idx].uv
            u, v = uv.x, uv.y
        else:
            u, v = 0.0, 0.0
            
        nx, ny, nz = round(v_normal.x, 5), round(v_normal.z, 5), round(v_normal.y, 5)
        cache_key = (v_idx, round(u, 5), round(v, 5), nx, ny, nz)
        
        if cache_key not in vertex_cache:
            new_idx = len(vertices)
            vertex_cache[cache_key] = new_idx
            vertices.append({
                "x": v_co.x,
                "y": v_co.z,
                "z": v_co.y,
                "u": u,
                "v": 1.0 - v,
                "nx": v_normal.x,
                "ny": v_normal.z,
                "nz": v_normal.y
            })
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