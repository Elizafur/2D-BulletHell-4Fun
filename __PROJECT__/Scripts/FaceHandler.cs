using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FaceHandler : MonoBehaviour
{
    public Sprite lookRight;
    private Sprite lookLeft;//mirror

    public Sprite damage;
    public Sprite massiveDamage;

    public Sprite furious; //shooting for > some time
    public Sprite idle;


    private GameObject player;
    private PlayerHealth playerHealth;
    private PlayerAttackHandler playerAttack;

    private Image image;

    private float idleTime = 1f;

    private float lastFrameHP;
    private float framesShot;


    void Start()
    {
        player       = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        playerAttack = player.GetComponent<PlayerAttackHandler>();

        image        = GetComponent<Image>();
        lookLeft     = lookRight;

        lastFrameHP = playerHealth.HP;

        StartCoroutine(IdleLoop());
    }

    void Update()
    {
        if (playerAttack.shooting)
            ++framesShot;
        else
            framesShot = 0;

        if (framesShot > 135 || 
            (framesShot > 30 && playerHealth.HP < 60 && playerHealth.HP != lastFrameHP))
            ReplaceImage(furious, false);

        if (playerHealth.HP != lastFrameHP)
        {
            float r = Random.Range(0, 100);

            if (r > 70)
                ReplaceImage(damage, false);


            if (playerHealth.HP < lastFrameHP - 25)
                ReplaceImage(massiveDamage, false);
        }

        lastFrameHP = playerHealth.HP;
    }

    IEnumerator IdleLoop()
    {
        for (;;)
        {
            float randomChance = Random.Range(0, 100);

            if (randomChance > 60)
                ReplaceImage(idle, false);
            else if (randomChance > 30)
                ReplaceImage(lookLeft, true);
            else
                ReplaceImage(lookRight, false);

            yield return new WaitForSeconds(idleTime + Random.Range(0, 2));
        }
    }

    public void InterruptReplaceImage_DirectionHit(bool left)
    {
        if (left)
            ReplaceImage(lookLeft, true);
        else
            ReplaceImage(lookRight, false);
    }

    void ReplaceImage(Sprite s, bool flip)
    {
        image.sprite = s;
        if (flip)
            image.rectTransform.localScale = new Vector3(-1,1,1);
        else
            image.rectTransform.localScale = new Vector3(1,1,1);
    }

}
