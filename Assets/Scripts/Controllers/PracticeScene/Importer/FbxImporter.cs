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
using Autodesk.Fbx;
using Quaternion = UnityEngine.Quaternion;


namespace Diploma.Controllers.Importer
{
    public class FbxImporter
    {
        private AssimpContext _assimpContext;
        
        private Vector3[] _modelVertices;
        private int[] _modelTriangles;
        private Vector4[] _modelTangents;
        private Vector3[] _modelNormals;
        private Vector2[] _modelUVs;
        private FbxNode _currentNodeParent;
        private FbxNode _prevNodeParent;
        private GameObject _currentUnityParent;
        private string _modelName;

        private Vector3 _modelPosition;
        private Vector3 _modelRotation;

        private UnityMaterial _modelMaterial;
        
        public void StartParsing(string modelPath, GameObject parent, PoolOfObjects poolOfObjects, UnityMaterial modelMaterial)
        {
            _currentUnityParent = parent;
            
            _modelMaterial = modelMaterial;
            ImportModel(modelPath, parent, poolOfObjects).StartCoroutine(out _);
        }

        private IEnumerator ImportModel(string path, GameObject parent, PoolOfObjects poolOfObjects)
        {
            FbxManager fbxManager = FbxManager.Create();

            FbxIOSettings fbxIOSettings = FbxIOSettings.Create(fbxManager, Application.dataPath);
            fbxManager.SetIOSettings(fbxIOSettings);
            
            Autodesk.Fbx.FbxImporter fbxImporter = Autodesk.Fbx.FbxImporter.Create(fbxManager, "");

            fbxImporter.Initialize(path, -1, fbxIOSettings);
            
            FbxScene fbxScene = FbxScene.Create(fbxManager, "myScene");
            
            _assimpContext = new AssimpContext();
            var scene = _assimpContext.ImportFile(path, PostProcessSteps.Triangulate);

            Debug.Log(scene.RootNode.Children[0].Children[0].Children[0].Children[0].Children[0].Children[0]);
            
            var assemblies = scene.Meshes;

            if (fbxImporter.Import(fbxScene))
            {
                _currentNodeParent = fbxScene.GetRootNode().GetChild(0);
                // _currentUnityParent = CreateNewParent(fbxScene.GetRootNode().GetChild(0).GetName(), parent.transform);
                for (var i = 0; i < assemblies.Count; i++)
                {
                    // if (_currentNodeParent.GetChildCount() == 1)
                    // {
                    //     GetBodyChild(_currentNodeParent.GetChild(0));
                    //     SetGeometryData(assemblies[i], _currentUnityParent, poolOfObjects);
                    // }
                    // else if (_currentNodeParent.GetChildCount() > 1)
                    // {
                    //     GetBodyChild(_currentNodeParent.GetChild(i));
                    SetGeometryData(assemblies[i], _currentUnityParent, poolOfObjects);
                    // }
                }
            }

            yield return new WaitForEndOfFrame();
        }

        #region Assimp Geometry Data Set

        private void SetGeometryData(Mesh assembly, GameObject newParent, PoolOfObjects poolOfObjects)
        {
            SetVertices(assembly);
            
            SetNormals(assembly);
            
            SetTangents(assembly);
            
            SetTriangles(assembly.Faces);
            
            
            SetMesh(newParent, poolOfObjects).StartCoroutine(out _);
        }
        
        private void SetVertices(Mesh assembly)
        {
            var modelsVerts = new List<Vector3D>();
            
            for (var i = 0; i < assembly.VertexCount; i++)
            {
                modelsVerts.Add(assembly.Vertices[i]);
            }

            _modelVertices = new Vector3[modelsVerts.Count];

            for (int i = 0; i < modelsVerts.Count; i++)
            {
                _modelVertices[i].x = modelsVerts[i].X;
                _modelVertices[i].y = modelsVerts[i].Y;
                _modelVertices[i].z = modelsVerts[i].Z;
            }
        }
        
        private void SetTriangles(List<Face> assemblyFaces)
        {
            var modelFaces = new List<int>();
            
            foreach (var face in assemblyFaces)
            {
                modelFaces.AddRange(face.Indices);
            }

            _modelTriangles = new int[modelFaces.Count];

            _modelTriangles = modelFaces.ToArray();
        }
        
        private void SetTangents(Mesh assembly)
        {
            var modelsTangents = new List<Vector3D>();

            foreach (var tangent in assembly.Tangents)
            {
                modelsTangents.Add(tangent);
            }

            _modelTangents = new Vector4[modelsTangents.Count];

            for (int i = 0; i < modelsTangents.Count; i++)
            {
                _modelTangents[i].x = modelsTangents[i].X;
                _modelTangents[i].y = modelsTangents[i].Y;
                _modelTangents[i].z = modelsTangents[i].Z;
            }

        }
        
        private void SetNormals(Mesh assembly)
        {
            var modelsNormals = new List<Vector3D>();
            
            foreach (var normal in assembly.Normals)
            {
                modelsNormals.Add(normal);
            }
            
            _modelNormals = new Vector3[modelsNormals.Count];

            for(var i = 0; i < modelsNormals.Count; i++)
            {
                _modelNormals[i].x = modelsNormals[i].X;
                _modelNormals[i].y = modelsNormals[i].Y;
                _modelNormals[i].z = modelsNormals[i].Z;
            }
        }

        #endregion

        #region Autodesk Geometry Data Set

        private void SetAutodeskVertices(FbxNode assembly)
        {
            var geometry = assembly.GetGeometry();
            Debug.Log(geometry.GetName());
            _modelVertices = new Vector3[geometry.GetControlPointsCount()];

            for (var i = 0; i < geometry.GetControlPointsCount(); i++)
            {
                _modelVertices[i].x = (float) geometry.GetControlPointAt(i).X;
                _modelVertices[i].y = (float) geometry.GetControlPointAt(i).Y;
                _modelVertices[i].z = (float) geometry.GetControlPointAt(i).Z;
            }
        }

        private void SetAutodeskNormals(FbxNode assembly)
        {
            var geometry = assembly.GetGeometry();
            var geometryNormals = geometry.GetLayer(0).GetNormals();
            
            _modelNormals = new Vector3[geometryNormals.GetIndexArray().GetCount()];

            for (var i = 0; i < geometryNormals.GetIndexArray().GetCount(); i++)
            {
                _modelNormals[i].x = (float) geometryNormals.GetDirectArray().GetAt(i).X;
                _modelNormals[i].y = (float) geometryNormals.GetDirectArray().GetAt(i).Y;
                _modelNormals[i].z = (float) geometryNormals.GetDirectArray().GetAt(i).Z;
            }
        }

        // private void SetAutodeskTangents(FbxNode assembly)
        // {
        //     var geometry = assembly.GetGeometry();
        //     var geometryTangents = geometry.GetLayer(0).;
        //     
        //     Debug.Log(geometryTangents);
        //     
        //     _modelNormals = new Vector3[geometryTangents.GetDirectArray().GetCount()];
        //
        //     for (var i = 0; i < geometryTangents.GetIndexArray().GetCount(); i++)
        //     {
        //         _modelTangents[i].x = (float) geometryTangents.GetDirectArray().GetAt(i).X;
        //         _modelTangents[i].y = (float) geometryTangents.GetDirectArray().GetAt(i).Y;
        //         _modelTangents[i].z = (float) geometryTangents.GetDirectArray().GetAt(i).Z;
        //         _modelTangents[i].w = (float) geometryTangents.GetDirectArray().GetAt(i).W;
        //     }
        // }

        private void SetAutodeskUV(FbxNode assembly)
        {
            var geometry = assembly.GetGeometry();
            var geometryUVs = geometry.GetLayer(0).GetUVs();

            _modelUVs = new Vector2[geometry.GetControlPointsCount()];

            for (var i = 0; i < _modelUVs.Length; i++)
            {
                _modelUVs[i].x = (float) geometryUVs.GetDirectArray().GetAt(i).X;
                _modelUVs[i].y = (float) geometryUVs.GetDirectArray().GetAt(i).Y;
            }
        }

        private void SetAutodeskTriangles(FbxNode assembly)
        {
            var modelTriangles = new List<int>();

            for (int i = 0; i < _modelTriangles.Length; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    // modelTriangles.
                }
            }
        }
        
        #endregion

        private IEnumerator SetMesh(GameObject parent, PoolOfObjects poolOfObjects)
        {
            if (_modelName == "")
                yield break;

            var gameObjectMesh = new GameObject(_modelName);
            gameObjectMesh.transform.parent = parent.transform;
            
            // gameObjectMesh.transform.localPosition = _modelPosition;
            // gameObjectMesh.transform.localRotation = Quaternion.Euler(_modelRotation);

            // gameObjectMesh.transform.SetPositionAndRotation(_modelPosition, Quaternion.Euler(_modelRotation));

            //тут пихаем в пул элементы
            poolOfObjects.AddInfoInPool(gameObjectMesh);
            
            gameObjectMesh.AddComponent<MeshFilter>();
            gameObjectMesh.AddComponent<MeshRenderer>();
            
            var meshFilter = gameObjectMesh.GetComponent<MeshFilter>().mesh;
            meshFilter.Clear();
            meshFilter.vertices = _modelVertices;
            meshFilter.tangents = _modelTangents;
            meshFilter.normals = _modelNormals;
            meshFilter.triangles = _modelTriangles;

            meshFilter.RecalculateNormals();
            meshFilter.RecalculateTangents();

            meshFilter.RecalculateBounds();
            
            // gameObjectMesh.GetComponent<MeshCollider>().sharedMesh = gameObjectMesh.GetComponent<MeshFilter>().mesh;
            // gameObjectMesh.GetComponent<MeshCollider>().convex = true;
            
            MeshRenderer meshRenderer = gameObjectMesh.GetComponent<MeshRenderer>();
            _modelMaterial.color = Color.gray;
            meshRenderer.material = _modelMaterial;

            yield return new WaitForEndOfFrame();
        }

        private Color GetModelColor(Material modelMaterial)
        {
            return new Color(modelMaterial.ColorAmbient.R, modelMaterial.ColorAmbient.G, modelMaterial.ColorAmbient.B, modelMaterial.ColorAmbient.A);
        }

        private GameObject CreateNewParent(string parentName, Transform parent)
        {
            GameObject parentGameObject = new GameObject(parentName);
            parentGameObject.transform.parent = parent;

            return parentGameObject;
        }

        private void GetBodyChild(FbxNode parentNode)
        {
            while (true)
            {
                if (parentNode.GetChildCount() > 1)
                {
                    _currentNodeParent = parentNode;
                    _prevNodeParent = parentNode.GetParent().GetParent();
                }
                if (parentNode.GetChild(0).GetName() == "Body1")
                {
                    _modelName = parentNode.GetName();
                    SetPositionAndRotation(parentNode.GetParent().LclTranslation.Get(), parentNode.GetParent().LclRotation.Get());
                }
                else
                {
                    parentNode = parentNode.GetChild(0);
                    continue;
                }
                break;
            }
        }

        private void SetPositionAndRotation(FbxDouble3 position, FbxDouble3 rotation)
        {
            _modelPosition.x = (float) position.X;
            _modelPosition.y = (float) position.Y;
            _modelPosition.z = (float) position.Z;

            _modelRotation.x = (float) rotation.X;
            _modelRotation.y = (float) rotation.Y;
            _modelRotation.z = (float) rotation.Z;
        }
    }
}