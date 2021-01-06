using System.Collections;
using System.Collections.Generic;
using UnityChan;
using UnityEngine;

public class controllerAR : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject character;
    UnityChanControlScriptWithRgidBody controller;
    void Start()
    {
        controller = character.GetComponent<UnityChanControlScriptWithRgidBody>();
    }

    // Update is called once per frame
    bool play= false;
//     void Update()
//     {
//         if(Input.GetKeyDown("w"))
//         {
//             play=true;
//             controller.is_walke = true;
//             controller.speed = 0.2f;
//         }
//        if(Input.GetKeyDown("s"))
//        {
//            play=true;
//             controller.is_walke = true;
           
//             controller.speed = -0.2f;
//        }
//         if(Input.GetKeyDown("d"))
//        {
//             play=true;
//             controller.is_walke = true;
//             controller.circle=0.2f;
//        }
//        if(Input.GetKeyDown("a"))
//        {
//             play=true;
//             controller.is_walke = true;
//             controller.circle=-0.2f;
//        }



// if(Input.GetKeyUp("w"))
//         {
//              play=false;
//             controller.speed = 0f;
//             controller.is_walke = false;
//         }
//        if(Input.GetKeyUp("s"))
//        {
//            play=false;
//            controller.speed = 0f;
//             controller.is_walke = false;
            
//        }
//         if(Input.GetKeyUp("d"))
//        {
//            play=false;
//            controller.circle = 0f;
//             controller.is_walke = false;
//        }
//        if(Input.GetKeyUp("a"))
//        {
//            play=false;
//            controller.circle = 0f;
//             controller.is_walke = false;
//        }

//     }
}
