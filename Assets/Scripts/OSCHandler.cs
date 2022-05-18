using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;


public class OSCHandler : MonoBehaviour
{

    class DmxUsbDevice
    {
        public DmxUsbDevice(int _id, int _numChannels)
        {
            id = _id;
            numChannels = _numChannels;

            dmxVals = new int[numChannels];
        }

        public void update(int[] _dmxVals)
        {
            dmxVals = _dmxVals;
        }

        public int numChannels;

        public int id;
        public int[] dmxVals;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting OSC Handler");

        var receiver = gameObject.AddComponent<OSCReceiver>();

        // Set local port.
        receiver.LocalPort = 7001;
        receiver.Bind("/DMXData", ReceivedDMXData);
        //receiver.Bind("/DMXInit", ReceivedDMXInit);
        Debug.Log("Connected to OSC");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void ReceivedDMXInit(OSCMessage message)
    {
        int deviceID    = message.Values[0].IntValue; // first value is device id
        int numChannels = message.Values[1].IntValue; // second value is num dmx channels
        
        DmxUsbDevice d = new DmxUsbDevice(deviceID, numChannels);
        deviceList.Add(d);

        Debug.Log("Init device: " + deviceID);
    }

    protected void ReceivedDMXData(OSCMessage message)
    {
        bool foundDevice = false;
        int  deviceID    = message.Values[0].IntValue; // First value is the device id

        //Debug.Log("recv msg for device: " + deviceID + " numVals: " + numVals);
        for ( int i = 0; i < deviceList.Count; i++ )
        {
            if(((DmxUsbDevice)deviceList[i]).id == deviceID )
            {
                foundDevice = true;

                setDmxData(i, message);
            }
        }    

        // If we couldn't find the device then add it to the list
        if(!foundDevice)
        {
            Debug.Log("couldn't find device: " + deviceID + ", adding it");
            int numChannels = message.Values.Count - 1;

            DmxUsbDevice d = new DmxUsbDevice(deviceID, numChannels);
            deviceList.Add(d);
        }
        
    }

    public void setDmxData(int deviceIndx, OSCMessage message)
    {
        //Debug.Log("setting dmx data for device: " + deviceIndx);
        //int deviceID = message.Values[0].IntValue; // First value is the device id
        int numChannels = message.Values.Count - 1;

        int[] dmxVals = new int[numChannels];

        for (int indx = 1; indx < numChannels; indx++)
        {
            dmxVals[indx] = message.Values[indx].IntValue;
            //Debug.Log(dmxVals[indx - 1])
        }

        ((DmxUsbDevice)deviceList[deviceIndx]).update(dmxVals);
    }
    public int getValue(int deviceID, int channel)
    {
        int res = -1;

        for (int i = 0; i < deviceList.Count; i++)
        {
            if (((DmxUsbDevice)deviceList[i]).id == deviceID)
            {
                res = ((DmxUsbDevice)deviceList[i]).dmxVals[channel];
            }
        }

        return res;
    }

    private ArrayList deviceList = new ArrayList();

}
