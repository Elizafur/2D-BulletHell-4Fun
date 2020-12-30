using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerDeathHandler : MonoBehaviour
{
    [Required]
    public Transform spawnPosition;
    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
    }

    public void Respawn()
    {
        player.SetActive(true);

        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerHealth>().HP = 100f;

        transform.position = spawnPosition.position;
    }
}
