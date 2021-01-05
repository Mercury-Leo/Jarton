using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class findLocation : MonoBehaviour
{

    public Text TextToShow;
    public InputField inputFieldText;
    string text;
    // Start is called before the first frame update
    void Start()
    {
        text = inputFieldText.text;
    }

    // Update is called once per frame
    void Update()
    {
       if(text != inputFieldText.text)
       {
           TextToShow.text="";
           for(int i=0;i<inputFieldText.text.Length;i++)
           {
               TextToShow.text+=inputFieldText.text[inputFieldText.text.Length-1-i];
           }
           text=inputFieldText.text;
       }
    }


   
}
