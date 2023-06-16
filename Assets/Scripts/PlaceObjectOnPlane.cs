using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARRaycastManager))]
public class PlaceObjectOnPlane : MonoBehaviour
{

    [SerializeField] private GameObject prefab;
    private GameObject spawnedObject;

    private ARRaycastManager racaster;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    // Start is called before the first frame update
    void Start()
    {
        racaster = GetComponent<ARRaycastManager>();
    }

    // Update is called once per frame
    public void OnPlaceObject(InputValue value)
    {
        // Get the screen touch position
        Vector2 touchPosition = value.Get<Vector2>();
        // Perform a raycast from the touchPosition into the 3D scene to look into
        if(racaster.Raycast(touchPosition, hits, UnityEngine.XR.ARSubsystems.TrackableType.PlaneWithinPolygon))
        {
            // Get the hit point (pose) on the plane
            Pose hitPoint = hits[0].pose;
            // Is this the first time we will place an object?
            if(spawnedObject != null)
            {
                //Instantiate our own prefab
                spawnedObject = Instantiate(prefab, hitPoint.position, hitPoint.rotation);
            }
            else
            {
                //if the object is SqlAlreadyFilledException existing. We move it.

                spawnedObject.transform.SetPositionAndRotation(hitPoint.position,hitPoint.rotation);
            }

        }
    }
}
