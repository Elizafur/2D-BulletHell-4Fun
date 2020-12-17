using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerAttackHandler : MonoBehaviour
{
    public Transform firePoint;

    private PlayerMovement _PlayerMovement;
    private Controls Controls;

    [ShowInInspector, AssetsOnly]
    public GameObject bulletPrefab;

    private bool shooting = false;
    public float shotTime = .1f;

    void Start()
    {
        _PlayerMovement = GetComponent<PlayerMovement>();
        Controls = _PlayerMovement.PlayerControls;
    }

    void Update()
    {
        float   fire = Controls.Player.Fire.ReadValue<float>();
        Vector2 look = Controls.Player.Look.ReadValue<Vector2>();

        
        if (fire == 1)
        {
            if (!shooting)
            {
                shooting = true;
                StartCoroutine(CreateBullet());
            }
        }
        else
        {
            StopCoroutine(CreateBullet());
            shooting = false;
        }
    }

    IEnumerator CreateBullet()
    {
        for(;;) 
        {
            if (!shooting) break;
            GameObject shot = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            shot.GetComponent<BulletHandler>().Direction = Controls.Player.Look.ReadValue<Vector2>();


            yield return new WaitForSeconds(shotTime);
        }

        yield return new WaitForEndOfFrame();
    }

    void TurnToCursor()
    {

    }
}
