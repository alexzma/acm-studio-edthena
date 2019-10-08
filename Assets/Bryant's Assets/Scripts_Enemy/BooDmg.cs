using UnityEngine;

public class BooDmg : MonoBehaviour
{
    public BoxCollider2D bc;
    SpriteControllerBryant sc;

    private void Awake()
    {
        bc = GetComponent<BoxCollider2D>();
        sc = GameObject.Find("Player").GetComponent<SpriteControllerBryant>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !sc.invincible)
        {
            sc.takeDamage();
        }
    }
}
