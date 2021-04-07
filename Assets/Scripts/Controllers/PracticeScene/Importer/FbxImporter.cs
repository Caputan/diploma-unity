using System.Collections;
using System.Collections.Generic;
using Diploma.Plugins.AssimpUnity;
using Assimp;
using Coroutine;
using GameObjectCreating;
using UnityEngine;
using UnityEngine.SceneManagement;
using Material = Assimp.Material;
using UnityMaterial = UnityEngine.Material;
using Mesh = Assimp.Mesh;

namespace Diploma.Controllers.Importer
{
    public class FbxImporter
    {
        private AssimpContext _assimpContext;
        
        private readonly List<Vector3> _modelVertices;
        private int[] _modelFaces;
        private readonly List<Vector3> _modelNormals;
        private string _modelName;

        private UnityMaterial _modelMaterial;


        public FbxImporter()
        {
            _modelVertices = new List<Vector3>();
            _modelNormals = new List<Vector3>();
        }

        public void StartParsing(string modelPath, GameObject parent, PoolOfObjects poolOfObjects, UnityMaterial modelMaterial)
        {
            _modelMaterial = modelMaterial;
            ParseModel(modelPath, parent, poolOfObjects).StartCoroutine(out _);
        }

        private IEnumerator ParseModel(string path, GameObject parent, PoolOfObjects poolOfObjects)
        {
            _assimpContext = new AssimpContext();
            var scene = _assimpContext.ImportFile(path);
            
            var assemblies = scene.Meshes;

            foreach (var assembly in assemblies)
            {
                SetVertices(assembly);
                
                SetNormals(assembly);
                
                SetFaces(assembly.Faces);

                _modelName = assembly.Name;
                SetMesh(parent, poolOfObjects, scene.Materials[assembly.MaterialIndex]).StartCoroutine(out _);
            }

            yield return new WaitForEndOfFrame();
        }

        private void SetVertices(Mesh assembly)
        {
            var modelsVerts = new List<Vector3D>();
            
            for (int j = 0; j < assembly.VertexCount; j++)
            {
                modelsVerts.Add(assembly.Vertices[j]);
            }
            

            foreach (var modelsVert in modelsVerts)
            {
                _modelVertices.Add(new Vector3(modelsVert.X, modelsVert.Y, modelsVert.Z));
            }
        }
        
        private void SetFaces(List<Face> assemblyFaces)
        {
            var modelFaces = new List<int>();

            foreach (var face in assemblyFaces)
            {
                for (int j = 0; j < face.IndexCount; j++)
                {
                    modelFaces.Add(face.Indices[j]);
                }
            }
            
            _modelFaces = modelFaces.ToArray();
        }
        
        private void SetNormals(Mesh assembly)
        {
            var modelsNormals = new List<Vector3D>();
            
            foreach (var normal in assembly.Normals)
            {
                modelsNormals.Add(normal);
            }

            foreach (var modelsNormal in modelsNormals)
            {
                _modelNormals.Add(new Vector3(modelsNormal.X, modelsNormal.Y, modelsNormal.Z));
            }
        }

        private IEnumerator SetMesh(GameObject parent, PoolOfObjects poolOfObjects, Material modelMaterial)
        {
            if (_modelName == "")
                yield break;
            
            GameObject gameObjectMesh = new GameObject(_modelName);
            gameObjectMesh.transform.parent = parent.transform;
            
            //тут пихаем в пул элементы
            poolOfObjects.AddInfoInPool(gameObjectMesh);
            
            gameObjectMesh.AddComponent<MeshFilter>();
            gameObjectMesh.AddComponent<MeshRenderer>();
            
            var meshFilter = gameObjectMesh.GetComponent<MeshFilter>().mesh;
            meshFilter.Clear();
            meshFilter.vertices = _modelVertices.ToArray();
            meshFilter.triangles = _modelFaces;
            meshFilter.normals = _modelNormals.ToArray();
            
            meshFilter.RecalculateBounds();
            
            gameObjectMesh.GetComponent<MeshCollider>().sharedMesh = gameObjectMesh.GetComponent<MeshFilter>().mesh;
            
            MeshRenderer meshRenderer = gameObjectMesh.GetComponent<MeshRenderer>();
            _modelMaterial.color = GetModelColor(modelMaterial);
            meshRenderer.material = _modelMaterial;

            yield return new WaitForEndOfFrame();
        }

        private Color GetModelColor(Material modelMaterial)
        {
            return new Color(modelMaterial.ColorAmbient.R, modelMaterial.ColorAmbient.G, modelMaterial.ColorAmbient.B, modelMaterial.ColorAmbient.A);
        }
    }
}