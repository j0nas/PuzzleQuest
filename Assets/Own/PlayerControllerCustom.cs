using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerCustom : MonoBehaviour
{

    public int speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      float moveHorizontal = Input.GetAxis("Horizontal");
      float moveVertical = Input.GetAxis("Vertical");
      // update player position based on input
      Vector3 position = transform.position;
      position.x += moveHorizontal * speed * Time.deltaTime;
      position.z += moveVertical * speed * Time.deltaTime;
      transform.position = position;
    }
}
