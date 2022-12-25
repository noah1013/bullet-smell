using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPatternSpiral : MonoBehaviour //Double Spiral Pattern
{
    
    private float angle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("Fire", 0.3f, 0.3f);
    }

    private void Fire()
    {
        for(int i = 0; i <=1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin(((angle + 180f *i) * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos(((angle + 180f * i) * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletInstantiate.bulletInstance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetMovementVariables(bulDir, 5f, -0.5f, 60f, -10f);
        }

        angle += 10f;

        if (angle >= 360f)
        {
            angle = 0f;
        }

    }
}
