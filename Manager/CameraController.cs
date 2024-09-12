using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;

    public Transform room;
    public Transform activeRoom;

    public static CameraController instance;

    [Range(-5, 5)]
    public float minModX, maxModX, minModY, maxModY;



    private void Awake()
    {
        instance = this;
    }


    void Start()
    {

    }


    void Update()
    {
        if (player != null)
        {
            var minPosX = activeRoom.GetComponent<BoxCollider2D>().bounds.min.x + minModX;
            var maxPosX = activeRoom.GetComponent<BoxCollider2D>().bounds.max.x + maxModX;
            var minPosY = activeRoom.GetComponent<BoxCollider2D>().bounds.min.y + minModY;
            var maxPosY = activeRoom.GetComponent<BoxCollider2D>().bounds.max.y + maxModY;

            Vector3 clampedPos = new Vector3(Mathf.Clamp(player.position.x, minPosX, maxPosX), Mathf.Clamp(player.position.y, minPosY, maxPosY), Mathf.Clamp(player.position.z, -10, -10));

            transform.position = new Vector3(clampedPos.x, clampedPos.y, clampedPos.z);
        }
    }
}
