using System.Collections;
using System.Collections.Generic;
using Diploma.Extensions;
using Tools;
using UnityEngine;

public class ScreenShotTesting : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private GameObject _gameObject;

    private float distance = 1.5f;
    // Start is called before the first frame update
    void Start()
    {
        SetCameraNearObject(_gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SetCameraNearObject(GameObject gameObject)
    {
        // gameObject.AddBoxCollider();
        // var renders = gameObject.GetComponentsInChildren<Renderer>();
        //
        // Vector3 Max = new Vector3(-9999,-9999,-9999);
        // foreach (var render in renders)
        // {
        //     if (render.bounds.max.y > Max.y)
        //     {
        //         Max = render.bounds.max;
        //     }
        //     
        // }

        // if (Max.y < _camera.transform.position.y)
        // {
        //     var positionY = new Vector3(gameObject.transform.position.x,0,gameObject.transform.position.z);
        //     positionY.y = _camera.transform.position.y / (Max.y+distance);
        //     gameObject.transform.position = positionY;
        // }
        // else
        // {
        //     var positionY = new Vector3(gameObject.transform.position.x,0,gameObject.transform.position.z);
        //     positionY.y = _camera.transform.position.y * (Max.y+distance);
        //     gameObject.transform.position = positionY;
        // }
        _camera.transform.LookAt(gameObject.transform);
        gameObject.transform.position = gameObject.transform.position.Change(z: 0f);
        //if (GeometryUtility.TestPlanesAABB(planes, obj.GetComponent<Collider>().bounds)) ;
    }

    public void TestAABB(Plane[] planes,Transform transform)
    {
        // Plane plane = new Plane();
        // plane.SetNormalAndPosition(
        //     _camera.transform.forward,
        //     _camera.transform.position);
        // Debug.Log(plane.GetDistanceToPoint(transform.position));
        
        if (
            GeometryUtility.TestPlanesAABB(planes, transform.GetComponent<Collider>().bounds)
        )
        {
            _camera.fieldOfView += 2;
            //_camera.farClipPlane += 0.01f;
            _camera.transform.localPosition += new Vector3(-0.05f, 0.05f, -0.05f);
            //Plane[] newPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
            //TestAABB(newPlanes, transform);
            return;
        }
        if (
            !GeometryUtility.TestPlanesAABB(planes, transform.GetComponent<Collider>().bounds)
        )
        {
            _camera.fieldOfView -= 2;
            _camera.transform.localPosition -= new Vector3(-0.05f, 0.05f, -0.05f);
            //Plane[] newPlanes = GeometryUtility.CalculateFrustumPlanes(Camera.main);
           // TestAABB(newPlanes, transform);
            return;
        }
        // if (GeometryUtility.TestPlanesAABB(plane5, transform.GetComponent<BoxCollider>().bounds))
        // {
        //     _camera.transform.localPosition += new Vector3(-0.05f, 0.05f, -0.05f);
        //     _camera.farClipPlane += 0.5f;
        //     TestAABB(planes, transform);
        // }
        return;
    }
}
