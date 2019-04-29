using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : IQuadTreeObject
{
    private Vector2 m_offset = new Vector3(0.0f, 0.0f);
    private static uint m_sectorSize = 64;
    private static float m_terrainResolution = 0.5f;
    private Mesh m_sectorMesh;

    public Sector(Vector2 offset, uint sectorSize = 64u, float terrainResolution = 0.5f)
    {
        m_offset = offset;
        m_sectorSize = sectorSize;
        m_terrainResolution = terrainResolution;
        CreateSectorMesh();
    }

    public static uint GetSectorSize()
    {
        return m_sectorSize;
    }

    public Mesh GetSectorMesh()
    {
        return m_sectorMesh;
    }

    public Vector2 GetPosition()
    {
        return m_offset;
    }

    private void CreateSectorMesh()
    {
        uint meshVerticesNum = (uint)((float)m_sectorSize / m_terrainResolution);
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uv0 = new List<Vector2>();
        Vector3 offset = new Vector3(m_offset.x, 0.0f, m_offset.y);
        int triangleIndex = 0;
        for(uint width = 0; width < meshVerticesNum; ++width)
        {
            for(uint length = 0; length < meshVerticesNum; ++length)
            {
                Vector3 pos0 = new Vector3(width * m_terrainResolution, 0.0f, length * m_terrainResolution);
                pos0 += offset;
                Vector3 pos1 = new Vector3(width * m_terrainResolution, 0.0f, (length + 1) * m_terrainResolution);
                pos1 += offset;
                Vector3 pos2 = new Vector3((width + 1) * m_terrainResolution, 0.0f, (length + 1) * m_terrainResolution);
                pos2 += offset;
                Vector3 pos3 = new Vector3((width + 1) * m_terrainResolution, 0.0f, length * m_terrainResolution);
                pos3 += offset;
                vertices.Add(pos0); vertices.Add(pos1); vertices.Add(pos2); vertices.Add(pos3);
                uv0.Add(new Vector2(0.0f, 0.0f)); uv0.Add(new Vector2(0.0f, 1.0f));
                uv0.Add(new Vector2(1.0f, 1.0f)); uv0.Add(new Vector2(1.0f, 0.0f));
                triangles.Add(triangleIndex); triangles.Add(triangleIndex + 1); triangles.Add(triangleIndex + 2);
                triangles.Add(triangleIndex); triangles.Add(triangleIndex + 2); triangles.Add(triangleIndex + 3);
                triangleIndex += 4;
            }
        }
        m_sectorMesh.SetVertices(vertices);
        m_sectorMesh.SetTriangles(triangles, 0);
        m_sectorMesh.SetUVs(0, uv0);
    }
}
