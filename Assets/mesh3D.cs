﻿using UnityEngine;
using System.Collections.Generic;

public class mesh3D : MonoBehaviour {
  
	public int resolution = 10;
	public Material mat;
	private List<Vector3> points=new List<Vector3>();
	private List<int> triangles = new List<int>();
	private GameObject figure;	
	private MeshSuperclass meshHelper;

	void Start (){
		meshHelper = new MeshSuperclass ();
		int counter = 0;
		int[,] matrixIndices = new int[resolution, resolution];
		for (int i = 0; i < resolution; i++) {
			for (int j = 0; j < resolution; j++) {
				matrixIndices [i, j] = counter;
				counter++;
			}
		}
		float x = 0, y = 0, z = 0, x1 = 0, y1 = 0, z1 = 0;
		float step = 2f / resolution;
		for (int i = 0; i < resolution; i++) {
			for (int j = 0; j < resolution; j++) {
				x = (i + 0.5f) * step - 1f;
				z = (j + 0.5f) * step - 1f;
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
        //meshHelper.saveMesh(figure.GetComponent<MeshFilter>().mesh, "sella");
        //meshHelper.saveMeshAsObject(figure.GetComponent<MeshFilter>(), "sella2");
	}

	float getY(float x,float z){
		return  x * x - z * z + 1;
	}
}