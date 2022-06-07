using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using extOSC;

public class ParCanControl : MonoBehaviour
{
        // Start is called before the first frame update
        void Start()
        {
            foreach (Transform c in transform)
            {
                //Something(child.gameObject);
                if (c.gameObject.name == "directionIndicator")
                {
                    c.gameObject.SetActive(false);
                }
            }

            GameObject go = GameObject.Find("OSCHandler");
            oscHandler = go.GetComponent<OSCHandler>();

            GameObject child = gameObject.transform.GetChild(0).gameObject;
            lightOutput      = gameObject.transform.GetChild(1).gameObject.GetComponent<Light>();
            facePlateLight   = gameObject.transform.GetChild(2).gameObject.GetComponent<Light>();
        }

        // Update is called once per frame
        void Update()
        {
            if (oscHandler != null && dmxAddress > -1 && lightOutput != null)
            {
                
                int c = oscHandler.getValue(0, dmxAddress);     //oscHandler.dmxVals[ dmxAddress     ]; // brightness 
                int r = oscHandler.getValue(0, dmxAddress + 1); //oscHandler.dmxVals[ dmxAddress + 1 ]; // red
                int g = oscHandler.getValue(0, dmxAddress + 2); //oscHandler.dmxVals[ dmxAddress + 2 ]; // green
                int b = oscHandler.getValue(0, dmxAddress + 3); //oscHandler.dmxVals[ dmxAddress + 3 ]; // blue
                 
            // make sure we are getting valid data
                if (c >= 0 && r >= 0 && g >= 0 && b >= 0)
                {
                    float brightness = (float)c / 255.0f;

                    // Set the light colour
                    lightOutput.color = new Color(   ((float)r / 255.0f) * brightness,
                                                 ((float)g / 255.0f) * brightness,
                                                 ((float)b / 255.0f) * brightness,
                                                 1.0f);
                    facePlateLight.color = new Color(((float)r / 255.0f) * brightness,
                                                 ((float)g / 255.0f) * brightness,
                                                 ((float)b / 255.0f) * brightness,
                                                 1.0f);
                }
            }        
        }
        OSCHandler oscHandler;
        Light lightOutput;
        Light facePlateLight;

        public int dmxAddress = -1;
}
