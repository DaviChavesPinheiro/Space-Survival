using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshRenderer))]
public class Chunk : MonoBehaviour
{
    public int[,] map;
    public int heigth = 50, width = 50;
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    List<Vector2> uvs = new List<Vector2>();

    float textureOffset = 1f/16f;
    Mesh mesh;
    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        map = new int[width, heigth];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < heigth; y++)
            {
                map[x,y] = 1;
            }
        }
        CalculateMash();
    }

    public void CalculateMash(){
         
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < heigth; y++)
            {
                if(map[x,y] != 0){
                    AddSquare(x , y);
                }
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.Optimize();

        GetComponent<MeshCollider>().sharedMesh = mesh;
        GetComponent<MeshFilter>().mesh = mesh;
        print(vertices.ToArray().Length);
    }

    public void AddSquare(int x, int y){
        x = x + Mathf.FloorToInt(transform.position.x);
        y = y + Mathf.FloorToInt(transform.position.y);

        triangles.Add(0 + vertices.Count);
        triangles.Add(1 + vertices.Count);
        triangles.Add(2 + vertices.Count);

        triangles.Add(0 + vertices.Count);
        triangles.Add(2 + vertices.Count);
        triangles.Add(3 + vertices.Count);

        uvs.Add(new Vector2(textureOffset * 1, textureOffset * 15));
        uvs.Add(new Vector2(textureOffset * 1, textureOffset * 16));
        uvs.Add(new Vector2(textureOffset * 2, textureOffset * 16));
        uvs.Add(new Vector2(textureOffset * 2, textureOffset * 15));

        vertices.Add(new Vector3(x + 0, y + 0, 0));
        vertices.Add(new Vector3(x + -1, y + 0, 0));
        vertices.Add(new Vector3(x + -1, y + 1, 0));
        vertices.Add(new Vector3(x + 0, y + 1, 0));
    }
}
