using UnityEngine;
using System.Collections;
using System.IO;
using Coroutine;
using GameObjectCreating;

public class Loader3DS {

	//public string modelPath = "C:/diploma-unity/Assets/Fence.3ds";
	public Shader modelShader;

	private string _nameModel = "";
	private Vector3 [] _verticesModel;
	private int [] _facesModel;
	private Vector2[] _uvsModel;
	private Vector3[] _normalsModel;
	private Material _modelMaterial;

	private string prevPartName = "";
	private Transform currentParent;
	private string hierarchyPos = "";

	public Vector3 LongestVert;
	
	
	public void StartParsing(string pathOfModel, GameObject parent, PoolOfObjects poolOfObjects, Material modelMaterial)
	{
		_modelMaterial = modelMaterial;
		Loader(pathOfModel, parent, poolOfObjects).StartCoroutine(out _);
	}

	private IEnumerator Loader (string path, GameObject parent, PoolOfObjects poolOfObjects)
	{
		
		if (!File.Exists (path)) 
		{
			Debug.LogError("File does not exist.");
			yield break;
		}
		
		ushort chunk_id;
		uint chunk_lenght;
		char charReader;
		ushort qty;
		ushort face_flags;
		int i;


		using (BinaryReader myFileStream = new BinaryReader (File.OpenRead (path))) 
		{
			

			while (myFileStream.BaseStream.Position < myFileStream.BaseStream.Length) 
			{
				chunk_id = myFileStream.ReadUInt16 ();
				chunk_lenght = myFileStream.ReadUInt32 ();
				// Debug.Log(chunk_id);
				switch (chunk_id) 
				{
					case 0x4d4d:
						// Debug.Log("Main chunk");
						break;
					
					case 0x3d3d:
						// Debug.Log("3D editor chunck");
						break;

					case 0x4000:
						_nameModel = "";
						i = 0;
						do
						{
							charReader = myFileStream.ReadChar();
							_nameModel += charReader.ToString();
						}
						while (charReader != '\0');
						break;

					case 0x4100:
						break;
					
					case 0x4110:
						qty = myFileStream.ReadUInt16();
						_verticesModel = new Vector3[qty];
						for (i = 0; i < qty; i++)
						{
							_verticesModel[i].x = myFileStream.ReadSingle();
							_verticesModel[i].y = myFileStream.ReadSingle();
							_verticesModel[i].z = myFileStream.ReadSingle();
						}
						break;
					
					case 0x4120:
						qty = myFileStream.ReadUInt16();
						_facesModel = new int[qty * 3];
					
						for (i = 0; i < qty * 3; i++)
						{
							_facesModel[i] = myFileStream.ReadUInt16();
							i++;
							_facesModel[i] = myFileStream.ReadUInt16();
							i++;
							_facesModel[i] = myFileStream.ReadUInt16();
							face_flags = myFileStream.ReadUInt16();
						}
						break;
					
					case 0x4140:
						qty = myFileStream.ReadUInt16();
						_uvsModel = new Vector2[_verticesModel.Length];
						for (i = 0; i < qty; i++)
						{
							_uvsModel[i].x = myFileStream.ReadSingle();
							_uvsModel[i].y = myFileStream.ReadSingle();
						}
						break;

					case 0xAFFF:
						// Debug.Log("MATERIAL BLOCK Chunk");
						break;

					default:
						myFileStream.BaseStream.Seek(chunk_lenght-6, SeekOrigin.Current);
						break;
				}

				SetMesh(parent, poolOfObjects).StartCoroutine(out _);
			}
			
			myFileStream.Close ();
		}

		yield return new WaitForEndOfFrame();
	}

	private void CalculateNormals(Vector3[] vertices)
	{
		if (vertices == null)
			return;

		LongestVert = _verticesModel[0];
		for (int j = 1; j < _verticesModel.Length; j++)
		{
			if (LongestVert.magnitude < _verticesModel[j].magnitude)
			{
				LongestVert = _verticesModel[j];
			}
		}


		_normalsModel = new Vector3[vertices.Length];
		for (int i = 0; i < vertices.Length - 2; i += 3)
		{
			var firstVector = vertices[i + 1] - vertices[i];
			var secondVector = vertices[i + 2] - vertices[i];
			_normalsModel[i] = Vector3.Cross(firstVector, secondVector).normalized;
		}
	}

	private IEnumerator SetMesh(GameObject parent, PoolOfObjects poolOfObjects)
	{
		if (_nameModel == prevPartName || _nameModel == "")
			yield break;
		
		CalculateNormals(_verticesModel);

		GameObject gameObjectMesh = new GameObject(_nameModel);
		gameObjectMesh.transform.parent = parent.transform;
		//тут пихаем в пул элементы
		poolOfObjects.AddInfoInPool(gameObjectMesh);

		gameObjectMesh.AddComponent<MeshFilter>();
		gameObjectMesh.AddComponent<MeshRenderer>();

		Mesh meshFilter = gameObjectMesh.GetComponent<MeshFilter>().mesh;
		meshFilter.Clear();
		meshFilter.vertices = _verticesModel;
		meshFilter.uv = _uvsModel;
		meshFilter.triangles = _facesModel;
		meshFilter.normals = _normalsModel;

		meshFilter.RecalculateBounds();

		gameObjectMesh.GetComponent<MeshCollider>().sharedMesh = gameObjectMesh.GetComponent<MeshFilter>().mesh;
		
		
		MeshRenderer meshRenderer = gameObjectMesh.GetComponent<MeshRenderer>();
		_modelMaterial.color = Color.gray;
		meshRenderer.material = _modelMaterial;

		prevPartName = _nameModel;
		
		yield return new WaitForEndOfFrame();
	}
}
