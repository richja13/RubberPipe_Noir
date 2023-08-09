using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightScript : MonoBehaviour
{

    public Light l;
    public bool FlashOff;
    public static FlashLightScript instance;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if(FlashOff)
        {
            l.enabled = false;
        }
        else
        {
            l.enabled = true;
        }

        if(Input.GetKeyDown(KeyCode.F))
        {
            if(FlashOff)
            {
                FlashOff = false;
            }
            else
            {
                FlashOff = true;
            }
        }
    }
}
