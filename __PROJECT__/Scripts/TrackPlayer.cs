using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackPlayer : MonoBehaviour
{
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag ("Player").transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPos = new Vector3 (player.position.x, player.position.y, transform.position.z);
        transform.position = newPos;
    }
}
