using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StreetNode : MonoBehaviour {

    Renderer rend;
    MeshFilter mf;

    public float scale = 1.0f;

    Mesh HalfSphere()
    {
        float radius = 1.0f;
        float depth = 1.0f;
        int depthSlices = 90;
        int radialSlices = 50;

        int VERTS_PER_RING = radialSlices + 1;
        int VERTS_PER_COL = depthSlices + 1;
        float COLUM_INC = Mathf.PI * 1.0f / Mathf.Clamp(depthSlices, 1.0f, float.MaxValue);
        float RADIAL_INC = Mathf.PI * 2.0f / Mathf.Clamp(radialSlices, 1.0f, float.MaxValue);

        float UV_HORIZONTAL_INC = 1.0f / radialSlices;
        float UV_VERTICAL_INC = 1.0f / depthSlices;

        List<Vector3> vertices = new List<Vector3>();
        List<Vector2> uvs = new List<Vector2>();
        float radial= 0.0f;
        for (int i = 0; i < VERTS_PER_RING; i++)
        {
            Matrix4x4 transform = Matrix4x4.TRS(Vector3.zero, Quaternion.Euler(0, radial * Mathf.Rad2Deg, 0), Vector3.one);

            float columCurr = Mathf.PI * -0.5f;
            for (int j = 0; j < VERTS_PER_COL; j++)
            {
                Vector3 point = new Vector3(Mathf.Cos(columCurr) * radius, Mathf.Sin(columCurr) * radius, 0.0f);
                

                point = transform * point;

                vertices.Add(point);

                Vector2 uv = new Vector2(i * UV_HORIZONTAL_INC, j * UV_VERTICAL_INC);

                uv.y = Mathf.Clamp(0.5f + ((uv.y - 0.5f) * scale), 0.0f, 1.0f);

                uvs.Add(uv);
                //bW.Add(bw);
                columCurr += COLUM_INC;
            }

            radial += RADIAL_INC;
        }

        List<int> triangles = new List<int>();
        for (int i = 0; i < (VERTS_PER_RING - 1); i++)
        {
            int botStart = i * VERTS_PER_COL;
            int topStart = botStart + VERTS_PER_COL;
            for (int j = 0; j < (VERTS_PER_COL - 1); j++)
            {
                triangles.Add(botStart + (j));
                triangles.Add(botStart + ((j + 1)));
                triangles.Add(topStart + ((j)));

                triangles.Add(botStart + ((j + 1)));
                triangles.Add(topStart + ((j + 1)));
                triangles.Add(topStart + (j));
            }
        }

        Mesh output = new Mesh();
        output.vertices = vertices.ToArray();
        output.triangles = triangles.ToArray();
        output.uv = uvs.ToArray();
        return output;
    }

    // Use this for initialization
    void Start () {

        rend = GetComponent<Renderer>();
        mf = GetComponent<MeshFilter>();


        mf.mesh = HalfSphere();
    }
	
	// Update is called once per frame
	void Update () {

    }
}
