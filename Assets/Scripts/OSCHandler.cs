using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class OSCHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting OSC Handler");

        var receiver = gameObject.AddComponent<OSCReceiver>();

        // Set local port.
        receiver.LocalPort = 7001;
        receiver.Bind("/DMXData", MessageReceived);
        Debug.Log("Connected to OSC");
        dmxVals = new int[NUM_DMX_CHANNELS];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void MessageReceived(OSCMessage message)
    {
        int numVals = message.Values.Count;
        test = "world";
        for ( int indx = 0; indx < NUM_DMX_CHANNELS && indx < numVals; indx++)
        {            
            dmxVals[ indx ] = message.Values[indx].IntValue; 
            //Debug.Log(indx + ": " + dmxVals[indx]);
        }           
    }

    //public int getDMXVals(int indx) { return dmxVals[indx];  }

    private const int NUM_DMX_CHANNELS = 255;
    public int[] dmxVals = new int[NUM_DMX_CHANNELS];
    public string test = "Hello";

}
