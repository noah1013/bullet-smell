using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletInstantiate : MonoBehaviour
{

    public static BulletInstantiate bulletInstance;
    

    [SerializeField]
    private GameObject bullet;

    private void Awake()
    {
        bulletInstance = this;
    }

    public GameObject GetBullet()
    {
        return Instantiate(bullet);
    }
}