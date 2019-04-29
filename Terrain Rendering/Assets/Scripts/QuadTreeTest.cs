using UnityEngine;
using System.Collections;

public class TerrainRenderingTest : MonoBehaviour
{
    public class TestObject : IQuadTreeObject
    {
        private Vector3 m_vPosition;
        public TestObject(Vector3 position)
        {
            m_vPosition = position;
        }
        public Vector2 GetPosition()
        {
            //Ignore the Y position, Quad-trees operate on a 2D plane.
            return new Vector2(m_vPosition.x, m_vPosition.z);
        }
    }

    private QuadTree<Sector> terrainQuadTree;
    private Material terrainMaterial;

    void OnEnable()
    {
        terrainMaterial = (Material)Resources.Load("TerrainMaterial", typeof(Material)); ;
        terrainQuadTree = new QuadTree<Sector>(10, new Rect(-1000, -1000, 2000, 2000));
        uint sectorNum = 100;
        uint sectorSize = Sector.GetSectorSize();
        for(int i = 0; i < sectorNum; ++i)
        {
            for(int j = 0; j < sectorNum; ++j)
            {
                Sector sector = new Sector(new Vector2(i * sectorSize, j * sectorSize));
                terrainQuadTree.Insert(sector);
            }
        }
    }
    void OnDrawGizmos()
    {
        if (terrainQuadTree != null)
        {
            terrainQuadTree.DrawDebug();
        }

        foreach(var sector in terrainQuadTree.RetrieveObjectsInArea(new Rect(-1000, -1000, 2000, 2000)))
        {
            Graphics.DrawMesh(sector.GetSectorMesh(), Vector3.zero, Quaternion.identity, terrainMaterial, 0);
        }
    }
}
