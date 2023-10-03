using UnityEngine;
using System.Collections.Generic;

public class lnx2y2 : MonoBehaviour {
  
	private int resolution = 80;
	public Material mat;
	private List<Vector3> points=new List<Vector3>();
	private List<int> triangles = new List<int>();
	private GameObject figure;	
	private MeshSuperclass meshHelper;
	void Start (){
		meshHelper = new MeshSuperclass ();
		int counter = 0;
		resolution=80; 
		int[,] matrixIndices = new int[resolution, resolution];
		for (int i = 0; i < resolution; i++) {
			for (int j = 0; j < resolution; j++) {
				matrixIndices [i, j] = counter;
				counter++;
			}
		}
		float x = 0, y = 0, z = 0, x1 = 0, y1 = 0, z1 = 0;
		float step = 0.1f;
		for (int i = 0; i < resolution; i++) {
			for (int j = 0; j < resolution; j++) {
				x = -4+(i*step);
				z = -4+(j*step);
				y =getY(x,z);
				points.Add (new Vector3 (x, y, z));

				if (i < resolution - 1 && j < resolution - 1) {
					/*
	            ij    ij1
	            i1j   i1j1
	          */
					triangles.Add (matrixIndices [i, j]);
					triangles.Add (matrixIndices [i, j + 1]);          
					triangles.Add (matrixIndices [i + 1, j]);  

					triangles.Add (matrixIndices [i + 1, j]);
					triangles.Add (matrixIndices [i, j + 1]);
					triangles.Add (matrixIndices [i + 1, j + 1]);
				}
			}
		}
		figure = gameObject;// GameObject ("3dMesh");
		//figure.AddComponent<MeshRenderer> ();
		//figure.AddComponent<MeshFilter> ();
		figure.GetComponent<MeshFilter> ().mesh = meshHelper.get3Dmesh (points, triangles);
		figure.GetComponent<MeshRenderer> ().material = mat;
		figure.transform.SetPositionAndRotation (transform.position, new Quaternion ());
        //figure.AddComponent<Light> ().color = Color.blue;
        //threeDSquare.GetComponent<Renderer> ().material.SetColor ("_EmissionColor", Color.blue);
        //meshHelper.saveMesh(figure.GetComponent<MeshFilter>().mesh, "bucoNero");
    }

	float getY(float x,float y){
		float x2=x*x;
		float y2=y*y;
		return  Mathf.Log(x2+y2);
	}
}