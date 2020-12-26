using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealth : MonoBehaviour
{
    GameObject player;
    PlayerHealth HP;

    Image healthBar;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        HP = player.GetComponent<PlayerHealth>();
        healthBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = HP.HP / 100;

        if(healthBar.fillAmount < 0.2f)
        {
            healthBar.color = Color.red;
        }
        else if(healthBar.fillAmount < 0.4f)
        {
            healthBar.color = Color.yellow;
        }
        else
        {
            healthBar.color = Color.green;
        }
    }
}
