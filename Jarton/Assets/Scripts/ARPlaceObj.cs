using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
public class ARPlaceObj : MonoBehaviour
{

    public GameObject gameobjectToInstance;
    
    public Text log;
    float latitude;
    float longitude;

    GameObject spawnedObj;
    ARRaycastManager _arRaycastManager;
    Vector2 touchPosition;

    public bool firstClick;

    static List<ARRaycastHit> hits = new List<ARRaycastHit>();


    private void Awake()
    {
        firstClick=true;
        _arRaycastManager = GetComponent<ARRaycastManager>();
    }

    bool tryGetTouchPosition(out Vector2 touchPosition)
    {
        if(Input.touchCount > 0)
        {
            touchPosition = Input.GetTouch(0).position;
            return true;
        }

        touchPosition = default;
        return false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!tryGetTouchPosition(out Vector2 touchPosition))
        {
            return;
        }
        if(_arRaycastManager.Raycast(touchPosition, hits, TrackableType.PlaneWithinPolygon))
        {
            var hitPose = hits[0].pose;
            Vector3 position;
            if(spawnedObj == null)
            {
                
                if(firstClick)
                {
                    
                    if(GPS.Instance.latitude!=0 && GPS.Instance.longitude!=0)
                    {
                        firstClick=false;
                       latitude =  GPS.Instance.latitude;
                       longitude = GPS.Instance.longitude;
                    }
                    
                }
                else{
                     position = createPoint(latitude,longitude);
                    spawnedObj = Instantiate(gameobjectToInstance, position, hitPose.rotation);
                }
                
                position = createPoint(latitude,longitude);
                log.text = position.ToString();
            }
            else
            {
                 position = createPoint(latitude,longitude);
                 log.text = position.ToString();
                // Vector3 position =createPoint(GPS.Instance.latitude,GPS.Instance.longitude);
                spawnedObj = Instantiate(gameobjectToInstance, position, hitPose.rotation);
                // spawnedObj.transform.position = hitPose.position;
            }
        }
    }



    public Vector3 createPoint(float latitude,float longitude)
    {
        var radio = 6367;
        Vector3 vect=new Vector3();
        latitude = Mathf.Deg2Rad*latitude;
        longitude = Mathf.Deg2Rad*longitude;
        float x = radio * Mathf.Cos(latitude) * Mathf.Cos(longitude);
        float y = radio * Mathf.Cos(latitude) * Mathf.Sin(longitude);  
        float z = radio * Mathf.Sin(latitude);
        vect.x = x;
        vect.y = y;
        vect.z = z;
        return vect; 
    }

}
