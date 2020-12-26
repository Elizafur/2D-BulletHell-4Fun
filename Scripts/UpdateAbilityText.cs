using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpdateAbilityText : MonoBehaviour
{
    PlayerAbilityHandler playerAbility;
    TextMeshProUGUI text;
    // Start is called before the first frame update
    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        playerAbility = player.GetComponent<PlayerAbilityHandler>();

        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = (playerAbility.PulseCDTimer <= 0) ? "READY" : Mathf.Round(playerAbility.PulseCDTimer).ToString();
    }
}
