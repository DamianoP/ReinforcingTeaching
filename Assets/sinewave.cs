using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class sinewave : MonoBehaviour {
	[Range(10, 500)]
	public int resolution = 10;
	public Material mat;
	private List<Vector3> points=new List<Vector3>();
	private List<int> triangles = new List<int>();
	private GameObject figure;
	private float step;
	private float x=0f,y=0f,z=0f,y1=0f,z1=0f;
	private float range=0.05f;
	private float factor=0;
	private MeshSuperclass meshHelper;

	void Start() {
		meshHelper = new MeshSuperclass ();
		step = 2f / resolution;
		z1=z-range;
		figure= gameObject;// new GameObject("Sinewave");
		//figure.AddComponent<MeshRenderer>();
		//figure.AddComponent<MeshFilter>();
		figure.GetComponent<MeshRenderer>().material=mat;
		//figure.transform.SetPositionAndRotation (transform.position, new Quaternion ());


	}

	float getY(float x){
		return Mathf.Sin(Mathf.PI * (x + factor+ Time.time/6));
	}


	void Update () {
		points=new List<Vector3>();			
		for (int i = 0; i < resolution; i++) {
			x = (i + 0.5f) * step - 1f;
			y = getY (x);
			y1=y-range;
			points.Add(new Vector3(x 	,y, 	 			z));
			points.Add(new Vector3(x 	,y, 				z1));
			points.Add(new Vector3(x 	,y1, 	z));
			points.Add(new Vector3(x 	,y1, 	z1));
		}
		figure.GetComponent<MeshFilter>().mesh = meshHelper.getMesh2D(points);    
		//figure.GetComponent<MeshRenderer>().enabled = true;	


	}
}
