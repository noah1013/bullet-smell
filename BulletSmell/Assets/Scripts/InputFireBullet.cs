using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFireBullet : MonoBehaviour //Spreadshot
{
    public SpriteRenderer spriteRenderer;
    public Sprite ShootSprite;
    public Sprite IdleSprite;


    [SerializeField]
    private int bulletsAmount = 10;

    [SerializeField]
    private float startAngle = 90f, endAngle = 270f;

    private Vector2 bulletMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        InvokeRepeating("Fire", 0f, 2f);
        //Invoke("ChangeSprite", 2f);
    }

    void ChangeSprite()
    {
        spriteRenderer.sprite = ShootSprite;
    }

    void revertSprite()
    {
        spriteRenderer.sprite = IdleSprite;
    }

    private void Fire()
    {
        ChangeSprite();
        Invoke("revertSprite", 0.5f);
        float angleStep = (endAngle - startAngle) / bulletsAmount;
        float angle = startAngle;

        for (int i = 0; i < bulletsAmount + 1; i++)
        {
            float bulDirX = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180f);
            float bulDirY = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180f);

            Vector3 bulMoveVector = new Vector3(bulDirX, bulDirY, 0f);
            Vector2 bulDir = (bulMoveVector - transform.position).normalized;

            GameObject bul = BulletInstantiate.bulletInstance.GetBullet();
            bul.transform.position = transform.position;
            bul.transform.rotation = transform.rotation;
            bul.SetActive(true);
            bul.GetComponent<Bullet>().SetMovementVariables(bulDir, 5f);

            angle += angleStep;
        }
    }
}