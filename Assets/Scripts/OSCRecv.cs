using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using extOSC;

public class OSCRecv : MonoBehaviour
{
        // Start is called before the first frame update
        void Start()
        {
            GameObject go = GameObject.Find("Main Camera");
            oscHandler = go.GetComponent<OSCHandler>();

            GameObject child = gameObject.transform.GetChild(0).gameObject;
            light = gameObject.transform.GetChild(1).gameObject.GetComponent<Light>();
    }

        // Update is called once per frame
        void Update()
        {            
            if ( oscHandler != null && dmxChannel > -1 && light !=null )
            {
                int r = oscHandler.dmxVals[ dmxChannel     ];
                int g = oscHandler.dmxVals[ dmxChannel + 1 ];
                int b = oscHandler.dmxVals[ dmxChannel + 2 ];

                light.color = new Color((float)r/255.0f, (float)g / 255.0f, (float)b / 255.0f, 1.0f);


                // set light based on channel
            }
        }
        OSCHandler oscHandler;
        Light light;


        public int dmxChannel = -1;
}
