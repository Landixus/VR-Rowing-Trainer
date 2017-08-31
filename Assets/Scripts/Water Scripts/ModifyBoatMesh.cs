using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyBoatMesh {
    //Get the transform of the boat in order to find the global position of each triangle's vertices
    private Transform boatTrans;
    
    //An array of coordinates of all vertices of the boat
    Vector3[] boatVertices;
    
    //Array indices for allVerticesArray used in the creation of triangles
    int[] boatTriangles;
    
    //An array of the global positions of all vertices
    public Vector3[] boatVerticesGlobal;
    
    //An array of distances from each triangle to the water
    float[] allDistancesToWater;

    //A list of triangles from the submerged parts of the boat
    public List<TriangleData> underWaterTriangleData = new List<TriangleData>();

	//Constructer for ModifyBoatMesh class
	public ModifyBoatMesh(GameObject boatObj)
    {
        //Get the transform of the boat
        boatTrans = boatObj.transform;

        //Init the arrays and lists
        boatVertices = boatObj.GetComponent<MeshFilter>().mesh.vertices;
        boatTriangles = boatObj.GetComponent<MeshFilter>().mesh.triangles;

        //The boat vertices in global position
        boatVerticesGlobal = new Vector3[boatVertices.Length];
        //Find the distances between the vertices of all triangles and the water. This is to account for some triangles that share vertices which are needed for reuse (otherwise they may be discarded and could result in missing triangles).
        allDistancesToWater = new float[boatVertices.Length];
    }

    //Generate the underwater mesh
    public void GenerateUnderwaterMesh()
    {
        //Clear triangle data in case any exist
        underWaterTriangleData.Clear();

        //Find all distances between the vertices of every triangle and the water. This accounts for vertices shared between some triangles which need to be reused.
        for (int j = 0; j < boatVertices.Length; j++)
        {
            //The coordinate should be in global position
            Vector3 globalPos = boatTrans.TransformPoint(boatVertices[j]);

            //Save the global position of each of the vertices
            boatVerticesGlobal[j] = globalPos;
			allDistancesToWater[j] = WaterController.current.DistanceToWater(globalPos);
        }

        //Add the triangles that are below the water
        AddTriangles();
    }

    //Add the triangles from the underwater mesh
    private void AddTriangles()
    {
        //List to sort vertices based on distance to water
        List<VertexData> vertexData = new List<VertexData>();

        //Initial data to replace
        vertexData.Add(new VertexData());
        vertexData.Add(new VertexData());
        vertexData.Add(new VertexData());

        //Loop through all triangles
        int i = 0;
        while (i < boatTriangles.Length)
        {
            //Loop through the three vertices
            for (int x = 0; x < 3; x++)
            {
                vertexData[x].distance = allDistancesToWater[boatTriangles[i]];
                vertexData[x].index = x;
                vertexData[x].globalVertexPos = boatVerticesGlobal[boatTriangles[i]];

                i++;
            }

            //Check if all vertices are above the water
            if (vertexData[0].distance > 0f && vertexData[1].distance > 0f && vertexData[2].distance > 0f)
            {
                continue;
            }

            //Create the triangles that are below the waterline

            //Check if all vertices are underwater. If so, save the entire triangle. Otherwise, break up the triangle into a smaller one
            if (vertexData[0].distance < 0f && vertexData[1].distance < 0f && vertexData[2].distance < 0f)
            {
                Vector3 p1 = vertexData[0].globalVertexPos;
                Vector3 p2 = vertexData[1].globalVertexPos;
                Vector3 p3 = vertexData[2].globalVertexPos;

                underWaterTriangleData.Add(new TriangleData(p1, p2, p3));
            }
            else
            {
                //Sort the vertices
                vertexData.Sort((x, y) => x.distance.CompareTo(y.distance));
                vertexData.Reverse();

                //One of the vertices is above the water
                if (vertexData[0].distance > 0f && vertexData[1].distance < 0f && vertexData[2].distance < 0f)
                {
                    AddTrianglesOneAboveWater(vertexData);
                }
                if (vertexData[0].distance > 0f && vertexData[1].distance > 0f && vertexData[2].distance < 0f)
                {
                    AddTrianglesTwoAboveWater(vertexData);
                }
            }
        }
    }
    
    //Build the new triangles where one of the old vertices is above the water
    private void AddTrianglesOneAboveWater(List<VertexData> vertexData)
    {
        //H is always at position 0
        Vector3 H = vertexData[0].globalVertexPos;

        //Left of H is M
        //Right of H is L
        //Find the index of M
        int M_index = vertexData[0].index - 1;
        if (M_index < 0)
        {
            M_index = 2;
        }

        //This finds the heights of the vertices to the water
        float h_H = vertexData[0].distance;
        float h_M = 0f;
        float h_L = 0f;

        Vector3 M = Vector3.zero;
        Vector3 L = Vector3.zero;

        //M is always at postion 1. This if statement makes sure of that.
        if (vertexData[1].index == M_index)
        {
            M = vertexData[1].globalVertexPos;
            L = vertexData[2].globalVertexPos;
            //These store the heights from the vertex to the water.
            h_M = vertexData[1].distance;
            h_L = vertexData[2].distance;
        }
        else
        {
            M = vertexData[2].globalVertexPos;
            L = vertexData[1].globalVertexPos;
            h_M = vertexData[2].distance;
            h_L = vertexData[1].distance;
        }

        //The following calculates where the new triangle should be cut
        //Calculation for point I_M
        Vector3 MH = H - M;
        float t_M = -h_M / (h_H - h_M);
        Vector3 MI_M = t_M * MH;
        Vector3 I_M = MI_M + M;

        //Point I_L
        Vector3 LH = H - L;
        float t_L = -h_L / (h_H - h_L);
        Vector3 LI_L = t_L * LH;
        Vector3 I_L = LI_L + L;

        //The following saves the resulting data
        underWaterTriangleData.Add(new TriangleData(M, I_M, I_L));
	}

    //Build the new triangles where two of the old vertices are above the water
    private void AddTrianglesTwoAboveWater(List<VertexData> vertexData)
    {
        Vector3 L = vertexData[2].globalVertexPos;

        //Find the index of H
        int H_index = vertexData[2].index + 1;
        if (H_index > 2)
        {
            H_index = 0;
        }


        //We also need the heights to water
        float h_L = vertexData[2].distance;
        float h_H = 0f;
        float h_M = 0f;

        Vector3 H = Vector3.zero;
        Vector3 M = Vector3.zero;

        //This means that H is at position 1 in the list
        if (vertexData[1].index == H_index)
        {
            H = vertexData[1].globalVertexPos;
            M = vertexData[0].globalVertexPos;

            h_H = vertexData[1].distance;
            h_M = vertexData[0].distance;
        }
        else
        {
            H = vertexData[0].globalVertexPos;
            M = vertexData[1].globalVertexPos;

            h_H = vertexData[0].distance;
            h_M = vertexData[1].distance;
        }


        //Now we can find where to cut the triangle

        //Point J_M
        Vector3 LM = M - L;

        float t_M = -h_L / (h_M - h_L);

        Vector3 LJ_M = t_M * LM;

        Vector3 J_M = LJ_M + L;


        //Point J_H
        Vector3 LH = H - L;

        float t_H = -h_L / (h_H - h_L);

        Vector3 LJ_H = t_H * LH;

        Vector3 J_H = LJ_H + L;


        //Save the data
        underWaterTriangleData.Add(new TriangleData(L, J_H, J_M));
	}

    //Help class to store triangle data so we can sort the distances
    private class VertexData
    {
        //The distance to water from this vertex
        public float distance;
        //An index so we can form clockwise triangles
        public int index;
        //The global Vector3 position of the vertex
        public Vector3 globalVertexPos;
    }

    //Display the underwater mesh
    public void DisplayMesh(Mesh mesh, string name, List<TriangleData> triangesData)
    {
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        //Build the mesh
        for (int i = 0; i < triangesData.Count; i++)
        {
            //From global coordinates to local coordinates
            Vector3 p1 = boatTrans.InverseTransformPoint(triangesData[i].p1);
            Vector3 p2 = boatTrans.InverseTransformPoint(triangesData[i].p2);
            Vector3 p3 = boatTrans.InverseTransformPoint(triangesData[i].p3);

            vertices.Add(p1);
            triangles.Add(vertices.Count - 1);

            vertices.Add(p2);
            triangles.Add(vertices.Count - 1);

            vertices.Add(p3);
            triangles.Add(vertices.Count - 1);
        }

        //Remove the old mesh
        mesh.Clear();

        //Give it a name
        mesh.name = name;

        //Add the new vertices and triangles
        mesh.vertices = vertices.ToArray();

        mesh.triangles = triangles.ToArray();

        mesh.RecalculateBounds();
    }
}