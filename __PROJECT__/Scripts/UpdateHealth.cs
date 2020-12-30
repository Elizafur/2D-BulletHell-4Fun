using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealth : MonoBehaviour
{
    GameObject player;
    PlayerHealth HP;

    Slider healthBar;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        HP = player.GetComponent<PlayerHealth>();
        healthBar = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value =  HP.HP / 100f;

        /*if(healthBar.value < 0.2f)
        {
            healthBar.colors = Color.red;
        }
        else if(healthBar.value < 0.4f)
        {
            healthBar.color = Color.yellow;
        }
        else
        {
            healthBar.color = Color.green;
        }*/
    }
}
