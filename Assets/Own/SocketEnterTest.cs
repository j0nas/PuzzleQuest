using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.Interaction.Toolkit;

public class SocketEnterTest : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("START!!");
    }

    public void CustomSelectTrigger()
    {
        //base.OnSelect(eventData);
        Debug.Log("TEST!!");
    }

    // Update is called once per frame
    void Update()
    {
    }
}
