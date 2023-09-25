using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingBall : MonoBehaviour
{

    public float Speed;
    public float JumpSpeed;

    private Rigidbody ridigbody;
    // Start is called before the first frame update
    void Start()
    {
            rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Vector3 movement = new Vector3(x*Time.deltaTime * Speed, rb.velocity.y, z*Time.deltaTime * Speed);

        Vector3 movement = new Vector3(x, 0 ,z) * Time.fixedDeltaTime * Speed;
        movement.y = GetComponent<Rigidbody>().velocity.y;

        GetComponent<Rigidbody>().velocity = movement;
        // rb.AddForce(movement);

        if(Input.GetKeyDown(KeyCode.Space)){
            GetComponent<Rigidbody>().AddForce(Vector3.up * JumpSpeed, ForceMode.Impulse);
        }
    }

}
