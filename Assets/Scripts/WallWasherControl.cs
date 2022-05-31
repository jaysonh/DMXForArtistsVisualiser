using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallWasherControl : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("OSCHandler");
        oscHandler = go.GetComponent<OSCHandler>();

        foreach (Transform child in transform)
        {
            string objName = child.gameObject.name;
        
            if (objName.StartsWith("light"))
            {
                string [] i = objName.Split('_');
                if (i.Length > 0)
                {
                    int indx = Convert.ToInt32(i[1]);
                    barLEDS[indx] = child.gameObject.GetComponent<Light>();
                    barLEDS[indx].color = new Color(1.0f, 0.0f, 1.0f, 1.0f);
                }
            } else if ( objName.StartsWith("directionIndicator") )
            {
                child.gameObject.SetActive(false);
            }
        }            
    }

    // Update is called once per frame
    void Update()
    {
        if (oscHandler != null && dmxAddress > -1 )
        {
            int c = oscHandler.getValue(0, dmxAddress);     //oscHandler.dmxVals[ dmxAddress     ]; // brightness 
            int r = oscHandler.getValue(0, dmxAddress + 1); //oscHandler.dmxVals[ dmxAddress + 1 ]; // red
            int g = oscHandler.getValue(0, dmxAddress + 2); //oscHandler.dmxVals[ dmxAddress + 2 ]; // green
            int b = oscHandler.getValue(0, dmxAddress + 3); //oscHandler.dmxVals[ dmxAddress + 3 ]; // blue

            if (c >= 0 && r >= 0 && g >= b && b >= 0){
                for (int i = 0; i < numBarLEDS; i++) {
                    float brightness = (float)c / 255.0f;

                    barLEDS[i].color = new Color(((float)r / 255.0f) * brightness,
                                                 ((float)g / 255.0f) * brightness,
                                                 ((float)b / 255.0f) * brightness,
                                                 1.0f);
                }
            }
        }
    }
    OSCHandler oscHandler;

    Light [] barLEDS = new Light[numBarLEDS];

    static public int numBarLEDS = 7;
    public int dmxAddress = -1;
}
