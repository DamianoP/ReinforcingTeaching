using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MeshSuperclass : MonoBehaviour {
    public void saveMeshAsAsset(Mesh mf, string textName){
        if (mf){
            var savePath = "Assets/"+ textName+".asset";

#if UNITY_EDITOR
			Debug.Log("Saved Mesh to:" + savePath);
            AssetDatabase.CreateAsset(mf, savePath);
#endif
		}
	}
    public void saveMeshAsObject(MeshFilter mf, string textName){
		using (StreamWriter sw = new StreamWriter(textName)){
		            sw.Write(MeshToString(mf));
		}
    }
	public Mesh getMesh2D(List<Vector3> points){
		List<int> triangles = new List<int>();
		int pointCounter = points.ToArray().Length;
		for (int i = 0; i < pointCounter-4; i=i+4) {
			/*


			i 				i+4
				i+1        		i+5
	
			i+2 			i+6
				i+3        		i+7		
			*/

			//top
			triangles.Add(i);
			triangles.Add(i+4);
			triangles.Add(i+1);


			triangles.Add(i+1);
			triangles.Add(i+4);
			triangles.Add(i+5);

			//right side
			triangles.Add(i+1);
			triangles.Add(i+5);
			triangles.Add(i+3);

			triangles.Add(i+3);
			triangles.Add(i+5);
			triangles.Add(i+7);

			//left side
			triangles.Add(i);
			triangles.Add(i+4);
			triangles.Add(i+2);

			triangles.Add(i+2);
			triangles.Add(i+4);
			triangles.Add(i+6);

			//bottom
			triangles.Add(i+2);
			triangles.Add(i+6);
			triangles.Add(i+3);

			triangles.Add(i+3);
			triangles.Add(i+6);
			triangles.Add(i+7);
		}
		triangles.Add(0);
		triangles.Add(1);
		triangles.Add(2);
		triangles.Add(2);
		triangles.Add(1);
		triangles.Add(3);


		triangles.Add(pointCounter-4);
		triangles.Add(pointCounter-3);
		triangles.Add(pointCounter-2);

		triangles.Add(pointCounter-2);
		triangles.Add(pointCounter-3);
		triangles.Add(pointCounter-1);

		Mesh mesh = new Mesh();    
		mesh.vertices = points.ToArray();
		mesh.triangles = triangles.ToArray();

		Vector2[] uvs = new Vector2[points.ToArray().Length];
		for (int i = 0; i < uvs.Length; i++)
		{
			//uvs[i] = new Vector2(points.ToArray()[i].x, points.ToArray()[i].z);
		}
		mesh.uv = uvs;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

		var vertices = mesh.vertices;
		var uv = mesh.uv;
		var normals2 = mesh.normals;
		var szV = vertices.Length;
		var newVerts = new Vector3[szV*2];
		var newUv = new Vector2[szV*2];
		var newNorms = new Vector3[szV*2];
		for (var j=0; j< szV; j++){
			// duplicate vertices and uvs:
			newVerts[j] = newVerts[j+szV] = vertices[j];
			newUv[j] = newUv[j+szV] = uv[j];
			// copy the original normals...
			newNorms[j] = normals2[j];
			// and revert the new ones
			newNorms[j+szV] = -normals2[j];
		}
		var triangles2 = mesh.triangles;
		var szT = triangles2.Length;
		var newTris = new int[szT*2]; // double the triangles
		for (var i=0; i< szT; i+=3){
			// copy the original triangle
			newTris[i] = triangles2[i];
			newTris[i+1] = triangles2[i+1];
			newTris[i+2] = triangles2[i+2];
			// save the new reversed triangle
			var j = i+szT; 
			newTris[j] = triangles2[i]+szV;
			newTris[j+2] = triangles2[i+1]+szV;
			newTris[j+1] = triangles2[i+2]+szV;
		}

		mesh.vertices = newVerts;
		mesh.uv = newUv;
		mesh.normals = newNorms;
		mesh.triangles = newTris; // assign triangles last!
		return mesh;
	}

	public Mesh get3Dmesh(List<Vector3> points,List<int> triangles){
		Mesh mesh = new Mesh();    
		mesh.vertices = points.ToArray();
		mesh.triangles = triangles.ToArray();

		Vector2[] uvs = new Vector2[points.ToArray().Length];
		for (int i = 0; i < uvs.Length; i++)
		{
			uvs[i] = new Vector2(points.ToArray()[i].x, points.ToArray()[i].z);
		}
		mesh.uv = uvs;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

		var vertices = mesh.vertices;
		var uv = mesh.uv;
		var normals2 = mesh.normals;
		var szV = vertices.Length;
		var newVerts = new Vector3[szV*2];
		var newUv = new Vector2[szV*2];
		var newNorms = new Vector3[szV*2];
		for (var j=0; j< szV; j++){
			// duplicate vertices and uvs:
			newVerts[j] = newVerts[j+szV] = vertices[j];
			newUv[j] = newUv[j+szV] = uv[j];
			// copy the original normals...
			newNorms[j] = normals2[j];
			// and revert the new ones
			newNorms[j+szV] = -normals2[j];
		}
		var triangles2 = mesh.triangles;
		var szT = triangles2.Length;
		var newTris = new int[szT*2]; // double the triangles
		for (var i=0; i< szT; i+=3){
			// copy the original triangle
			newTris[i] = triangles2[i];
			newTris[i+1] = triangles2[i+1];
			newTris[i+2] = triangles2[i+2];
			// save the new reversed triangle
			var j = i+szT; 
			newTris[j] = triangles2[i]+szV;
			newTris[j+2] = triangles2[i+1]+szV;
			newTris[j+1] = triangles2[i+2]+szV;
		}

		mesh.vertices = newVerts;
		mesh.uv = newUv;
		mesh.normals = newNorms;
		mesh.triangles = newTris; // assign triangles last!
		return mesh;
	}

    public static string MeshToString(MeshFilter mf) {
        Mesh m = mf.mesh;
        Material[] mats = mf.GetComponent<Renderer>().sharedMaterials;
 
        StringBuilder sb = new StringBuilder();
 
        sb.Append("g ").Append(mf.name).Append("\n");
        foreach(Vector3 v in m.vertices) {
            sb.Append(string.Format("v {0} {1} {2}\n",v.x,v.y,v.z));
        }
        sb.Append("\n");
        foreach(Vector3 v in m.normals) {
            sb.Append(string.Format("vn {0} {1} {2}\n",v.x,v.y,v.z));
        }
        sb.Append("\n");
        foreach(Vector3 v in m.uv) {
            sb.Append(string.Format("vt {0} {1}\n",v.x,v.y));
        }
        for (int material=0; material < m.subMeshCount; material ++) {
            sb.Append("\n");
            sb.Append("usemtl ").Append(mats[material].name).Append("\n");
            sb.Append("usemap ").Append(mats[material].name).Append("\n");
 
            int[] triangles = m.GetTriangles(material);
            for (int i=0;i<triangles.Length;i+=3) {
                sb.Append(string.Format("f {0}/{0}/{0} {1}/{1}/{1} {2}/{2}/{2}\n", 
                    triangles[i]+1, triangles[i+1]+1, triangles[i+2]+1));
            }
        }
        return sb.ToString();
    }
}
