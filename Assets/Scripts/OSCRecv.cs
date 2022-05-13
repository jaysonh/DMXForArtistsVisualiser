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
                int r = oscHandler.dmxVals[ dmxAddress     ];
                int g = oscHandler.dmxVals[ dmxAddress + 1 ];
                int b = oscHandler.dmxVals[ dmxAddress + 2 ];

                lightOutput.color    = new Color((float)r / 255.0f, (float)g / 255.0f, (float)b / 255.0f, 1.0f);
                facePlateLight.color = new Color((float)r / 255.0f, (float)g / 255.0f, (float)b / 255.0f, 1.0f);


            // set light based on channel
            }
        }
        OSCHandler oscHandler;
        Light lightOutput;
        Light facePlateLight;

        public int dmxAddress = -1;
}
