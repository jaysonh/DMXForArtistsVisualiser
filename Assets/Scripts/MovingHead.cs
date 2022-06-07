using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingHead : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject go = GameObject.Find("OSCHandler");
        oscHandler = go.GetComponent<OSCHandler>();
        lightOutput = transform.Find("top/moving_head_head/lightOutput").gameObject.GetComponent<Light>(); ;
       
    }

    // Update is called once per frame
    void Update()
    {
        if (oscHandler != null && dmxAddress > -1 && lightOutput != null)
        {

            int c = oscHandler.getValue( 0, briAddr );     //oscHandler.dmxVals[ dmxAddress     ]; // brightness 
            int r = oscHandler.getValue( 0, redAddr ); //oscHandler.dmxVals[ dmxAddress + 1 ]; // red
            int g = oscHandler.getValue( 0, grnAddr ); //oscHandler.dmxVals[ dmxAddress + 2 ]; // green
            int b = oscHandler.getValue( 0, bluAddr ); //oscHandler.dmxVals[ dmxAddress + 3 ]; // blue

            float pan  = ( (float) oscHandler.getValue(0, panCoarseAddr) / 255.0f) * 180.0f - 180 -90.0f;
            float tilt = ( (float) oscHandler.getValue( 0, tiltCoarseAddr ) / 255.0f) * 120.0f - 90.0f;

            topSection = transform.Find("top").gameObject;
            topSection.transform.rotation = Quaternion.Euler(tilt, pan, 0.0f); ;// ((float)pan, 0.0f, 0.0f, Space.World);
            
            // make sure we are getting valid data
            if (c >= 0 && r >= 0 && g >= 0 && b >= 0)
            {
                float brightness = (float) c / 255.0f;

                // Set the light colour
                lightOutput.color = new Color(  ((float)r / 255.0f) * brightness,
                                                ((float)g / 255.0f) * brightness,
                                                ((float)b / 255.0f) * brightness,
                                                1.0f );
            }
        }
    }
    private const int panCoarseAddr  = 1;
    private const int panFineAddr    = 2;
    private const int tiltCoarseAddr = 3;
    private const int tiltFineAddr   = 4;
    private const int briAddr = 5;
    private const int redAddr = 6;
    private const int grnAddr = 7;
    private const int bluAddr = 8;

    public int dmxAddress = -1;

    OSCHandler oscHandler;
    Light lightOutput;
    GameObject topSection;
}
