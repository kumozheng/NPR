using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class main : MonoBehaviour
{
    public float playerSpeed = 0.1f;
    public Transform playerNode;
    public Transform GunNode;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (playerNode.position.z > 200f) {
            playerNode.position = new Vector3(playerNode.position.x, playerNode.position.y, 0);
        } else {
            playerNode.Translate(0, 0, playerSpeed);
        }
    }
}
