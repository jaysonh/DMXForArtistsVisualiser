using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using extOSC;

public class OSCRecv : MonoBehaviour
{
        // Start is called before the first frame update
        void Start()
        {
            GameObject go = GameObject.Find("OSCHandler");
            oscHandler = go.GetComponent<OSCHandler>();

            GameObject child = gameObject.transform.GetChild(0).gameObject;
            lightOutput      = gameObject.transform.GetChild(1).gameObject.GetComponent<Light>();
            facePlateLight   = gameObject.transform.GetChild(2).gameObject.GetComponent<Light>();
    }

        // Update is called once per frame
        void Update()
        {            
            if ( oscHandler != null && dmxAddress > -1 && lightOutput != null )
            {
                int c = oscHandler.dmxVals[ dmxAddress     ];
                int r = oscHandler.dmxVals[ dmxAddress + 1 ];
                int g = oscHandler.dmxVals[ dmxAddress + 2 ];
                int b = oscHandler.dmxVals[ dmxAddress + 3 ];

                float brightness = (float)c / 255.0f;

                lightOutput.color    = new Color((float)r / 255.0f, (float)g / 255.0f, (float)b / 255.0f, brightness);
                facePlateLight.color = new Color((float)r / 255.0f, (float)g / 255.0f, (float)b / 255.0f, brightness);


            // set light based on channel
            }
        }
        OSCHandler oscHandler;
        Light lightOutput;
        Light facePlateLight;

        public int dmxAddress = -1;
}
