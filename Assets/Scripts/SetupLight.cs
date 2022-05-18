using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            //Something(child.gameObject);
            if (child.gameObject.name == "directionIndicator")
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
