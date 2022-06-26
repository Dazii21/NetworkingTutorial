using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    void HandleMovement()
    {
        if (isLocalPlayer)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal * 0.1f, moveVertical * 0.1f, 0);
            transform.position = transform.position + movement;
        }
    }
    
    void Update()
    {
        HandleMovement();

        if (isLocalPlayer && Input.GetKeyDown(KeyCode.X))
        {
            Debug.Log("Sending Hola to Server!");
            Hola();
        }

        if (isServer && transform.position.y > 50)
        {
            TooHigh();
        }
    }
    
    [Command]
    void Hola()
    {
        Debug.Log("Received Hola from Client!");
        ReplyHola();
    }

    [TargetRpc]
    void ReplyHola()
    {
        Debug.Log("Recieved Hola from server!");
    }

    [ClientRpc]
    void TooHigh()
    {
        Debug.Log("Too High!");
    }
}
