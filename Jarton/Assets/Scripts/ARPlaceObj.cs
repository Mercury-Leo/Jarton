using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

[RequireComponent(typeof(ARRaycastManager))]
[System.Obsolete]
public class ARPlaceObj : MonoBehaviour
{

    public GameObject gameobjectToInstance;
    public GameObject character;
    public Text text;
    GameObject spawnedObj;
    ARRaycastManager _arRaycastManager;
    Vector2 touchPosition;
    private Pose placementPose;
    private Pose placementPoseMove;
    private Pose placementPoseMin;



    public GameObject loadding;
    public GameObject aim_Green;
    public GameObject aim_Red;


    float fill;
    float count;
     private Pose savePose;
    private bool aim = false;
    private bool once = false;
    private bool placementPoseIsValid = false;
    bool canupdate = false;


    static List<ARRaycastHit> hits = new List<ARRaycastHit>();

    private void Start()
    {
        Invoke("Initialized", 0.01f);      
    }
    private void Awake()
    {
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

   
    private void Initialized()
    {
        // spawnedObj =Instantiate(character, Vector3.zero, Quaternion.LookRotation(Vector3.forward));
        count = 0;
            fill = 0f;
            Invoke("startAim", 3f);
            loadding.GetComponent<MeshRenderer>().enabled = false;
            aim_Red = GameObject.Find("RedAim");
            aim_Green = GameObject.Find("GreenAim");

            aim_Red.active = true;
            aim_Red.GetComponent<MeshRenderer>().enabled = false;

            aim_Green.GetComponent<MeshRenderer>().enabled = false;
            aim_Green.active = false;
            
            

    }

    public void StartAim()
    {
        canupdate = true;
    }

    void startAim()
    {
        aim_Red.GetComponent<MeshRenderer>().enabled = true;
        aim_Green.GetComponent<MeshRenderer>().enabled = true;
        loadding.GetComponent<MeshRenderer>().enabled = true;
    }
    void stopAim()
    {
        aim_Red.GetComponent<MeshRenderer>().enabled = false;
        aim_Green.GetComponent<MeshRenderer>().enabled = false;
        loadding.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!canupdate)
            return;
            if (!once)
            {
                updatePlacementPos();
            }
            updatePlacementInDirector();
            if (fill >= 0.08 && !once && aim && placementPoseIsValid)
            {
                //t.text = "Select scene";
                savePose = placementPose;
                once = true;
                placementPoseIsValid = false;
                canupdate = false;
                 stopAim();
                spawnedObj = Instantiate(character, savePose.position, savePose.rotation);
            }
    }
      private void updatePlacementPos()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));

        // if(!tryGetTouchPosition(out Vector2 touchPosition))
        // {
        //     return;
        // }
        if(_arRaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinPolygon))
        {
             placementPoseIsValid = hits.Count > 0;
            if(placementPoseIsValid)
          { 
               placementPose = hits[0].pose;
                var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x,0,cameraForward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
            // t.text = placementPose.position.ToString()+"\n" + placementPose.rotation.ToString();

           if ((placementPoseMove.position.x <= placementPose.position.x + 0.05f && placementPoseMove.position.x >= placementPose.position.x - 0.05f) &&
                 (placementPoseMove.position.y <= placementPose.position.y + 0.05f && placementPoseMove.position.y >= placementPose.position.y - 0.05f) &&
                 (placementPoseMove.position.z <= placementPose.position.z + 0.05f && placementPoseMove.position.z >= placementPose.position.z - 0.05f))
            {

                fill += 0.01f * Time.deltaTime * 3;
            }
            else
            {

                placementPoseMove.position = placementPose.position;
                fill = 0;
            }


 if (placementPoseMin.position.y >= placementPose.position.y)
            {
                count = 0;
                placementPoseMin.position = placementPose.position;
                aim = true;

                aim_Red.active = false;
                aim_Green.active = true;
                //aim grean = true
                loadding.transform.localScale = new Vector3(fill, 0, fill);

            }
            else if(placementPose.position.y > placementPoseMin.position.y && placementPose.position.y < placementPoseMin.position.y +0.15f )
            {
                count = 0;
                placementPoseMin.position = placementPose.position; 
                aim = true;
                aim_Red.active = false;
                aim_Green.active = true;
                loadding.transform.localScale = new Vector3(fill, 0, fill);
            }
            else
            {

                loadding.transform.localScale = new Vector3(fill, 0, fill);
                aim = false;
                aim_Red.active = true;
                aim_Green.active = false;
                fill = 0f;
            }



            if ((placementPoseMove.position.x <= placementPose.position.x + 0.05f && placementPoseMove.position.x >= placementPose.position.x - 0.05f) &&
                 (placementPoseMove.position.y <= placementPose.position.y + 0.05f && placementPoseMove.position.y >= placementPose.position.y - 0.05f) &&
                 (placementPoseMove.position.z <= placementPose.position.z + 0.05f && placementPoseMove.position.z >= placementPose.position.z - 0.05f) && !aim)
            {
                count += Time.deltaTime;
            }

            if (count > 2)
            {
                count = 0;
                placementPoseMin.position = placementPose.position;
            }





            text.text = "x:"+placementPoseMove.position.x+"\ny: "+placementPoseMove.position.y+"\nz: "+placementPoseMove.position.z+"\ntime: "+fill;

          } 

            // if(spawnedObj == null)
            // {
            //     spawnedObj = Instantiate(gameobjectToInstance, hitPose.position, hitPose.rotation);
            // }
            // else
            // {
            //     spawnedObj.transform.position = hitPose.position;
            // }
        }
    }

     private void updatePlacementInDirector()
    {
        if(placementPoseIsValid)
        {
            gameobjectToInstance.SetActive(true);
            gameobjectToInstance.transform.SetPositionAndRotation(placementPose.position,placementPose.rotation);
        }
        else
        {
            gameobjectToInstance.SetActive(false);
        }
    }
}
