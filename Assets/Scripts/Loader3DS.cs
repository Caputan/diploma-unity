using UnityEngine;
using System.Collections;
using System.IO;
using Coroutine;

public class Loader3DS : MonoBehaviour {

	public string modelPath = "C:/diploma-unity/Assets/Fence.3ds";
	public Shader modelShader;

	private string nameModel = "";
	private Vector3 [] verticesModel;
	private int [] facesModel;
	private Vector2[] uvsModel;
	private Vector3[] normalsModel;
	private string materialName = "";

	private string prevPartName = "";
	private Transform currentParent;
	private string hierarchyPos = "";
	
	
	public void StartParsing(string pathOfModel)
	{
		Loader(pathOfModel).StartCoroutine(out _);
	}

	private IEnumerator Loader (string path)
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

				switch (chunk_id) 
				{
					case 0x4d4d:
						// Debug.Log("Main chunk");
						break;
					
					case 0x3d3d:
						// Debug.Log("3D editor chunck");
						break;

					case 0x4000:
						nameModel = "";
						i = 0;
						do
						{
							charReader = myFileStream.ReadChar();
							nameModel += charReader.ToString();
						}
						while (charReader != '\0' && i<20);
						break;
					
					case 0x4100:
						break;
					
					case 0x4110:
						qty = myFileStream.ReadUInt16();
						verticesModel = new Vector3[qty];
						for (i=0; i<qty; i++)
						{
							verticesModel[i].x = myFileStream.ReadSingle();
							verticesModel[i].y = myFileStream.ReadSingle();
							verticesModel[i].z = myFileStream.ReadSingle();
						}
						break;
					
					case 0x4120:
						qty = myFileStream.ReadUInt16();
						facesModel = new int[qty * 3];
					
						for (i=0; i<qty*3; i++)
						{
							facesModel[i] = myFileStream.ReadUInt16();
							i++;
							facesModel[i] = myFileStream.ReadUInt16();
							i++;
							facesModel[i] = myFileStream.ReadUInt16();
							face_flags = myFileStream.ReadUInt16();
						}
						break;
					
					case 0x4140:
						qty = myFileStream.ReadUInt16();
						uvsModel = new Vector2[verticesModel.Length];
						for (i=0; i<qty; i++)
						{
							uvsModel[i].x = myFileStream.ReadSingle();
							uvsModel[i].y = myFileStream.ReadSingle();
						}
						break;
					
					case 0xAFFF:
						// Debug.Log("MATERIAL BLOCK Chunk");
						break;
					
					case 0xA000:
						do
						{
							charReader = myFileStream.ReadChar();
							materialName += charReader;
						} while (charReader != '\0');
						break;

					case 0xB000:
						// Debug.Log("KEYFRAMER Chunck");
						break;
						
					case 0xB002:
						// Debug.Log("FRAMES Chunck");
						break;

					case 0xB010:
						char chArr;
						string objName = "";
						do
						{
							chArr = myFileStream.ReadChar();
							objName += chArr;
						} while (chArr != '\0');
						break;
					
					default:
						myFileStream.BaseStream.Seek(chunk_lenght-6, SeekOrigin.Current);
						break;
				}

				SetMesh().StartCoroutine(out _);
			}

			myFileStream.Close ();
		}
		
		// yield return StartCoroutine (SetMesh());

		yield return new WaitForEndOfFrame();
	}

	private void CalculateNormals(Vector3[] vertices)
	{
		normalsModel = new Vector3[vertices.Length];
		for (int i = 2; i < vertices.Length; i++)
		{
			var firstVector = vertices[i - 1] - vertices[i];
			var secondVector = vertices[i - 2] - vertices[i];
			normalsModel[i] = Vector3.Cross(firstVector, secondVector).normalized;
		}
	}

	private IEnumerator SetMesh ()
	{
		if (nameModel == prevPartName || nameModel == "")
			yield break;
		
		CalculateNormals(verticesModel);
		
		GameObject gameObjectMesh = new GameObject(nameModel);
		gameObjectMesh.transform.parent = gameObject.transform;

		gameObjectMesh.AddComponent<MeshFilter>();
		gameObjectMesh.AddComponent<MeshRenderer>();

		Mesh meshFilter = gameObjectMesh.GetComponent<MeshFilter>().mesh;
		meshFilter.Clear();
		meshFilter.vertices = verticesModel;
		meshFilter.uv = uvsModel;
		meshFilter.triangles = facesModel;
		//meshFilter.no

		meshFilter.RecalculateBounds();
		
		Material modelMaterial = new Material (modelShader);
		
		// wwwTexture.LoadImageIntoTexture ((Texture2D) modelMaterial.mainTexture);
		// modelMaterial.mainTexture = wwwTexture.texture;
		Texture2D texture = Texture2D.normalTexture;
		modelMaterial.mainTexture = texture;
		MeshRenderer meshRenderer = gameObjectMesh.GetComponent<MeshRenderer>();
		meshRenderer.material = modelMaterial;

		prevPartName = nameModel;
		
		yield return new WaitForEndOfFrame();
	}

}
