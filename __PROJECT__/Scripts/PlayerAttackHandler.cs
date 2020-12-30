using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;

public class PlayerAttackHandler : MonoBehaviour
{
    public Transform firePoint;

    private PlayerMovement _PlayerMovement;
    private Controls Controls;

    [ShowInInspector, AssetsOnly]
    public GameObject bulletPrefab;

    public bool shooting = false;
    public float shotTime = .1f;

    void Start()
    {
        _PlayerMovement = GetComponent<PlayerMovement>();
        Controls = _PlayerMovement.PlayerControls;
    }

    void Update()
    {
        float   fire = Controls.Player.Fire.ReadValue<float>();
        
        
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
            
            Vector2 input = Util.GenerateMousePosition(Controls);
            
            bool useController = Gamepad.current != null;
            if (useController)
                input = Controls.Player.Look.ReadValue<Vector2>().normalized;
            
            shot.GetComponent<BulletHandler>().Direction = input;


            yield return new WaitForSeconds(shotTime);
        }

        yield return new WaitForEndOfFrame();
    }

    void TurnToCursor()
    {

    }
}
