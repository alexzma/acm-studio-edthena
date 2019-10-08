using UnityEngine;

public class InvisBlock : MonoBehaviour
{
    public Rigidbody2D playerRB;
    public BoxCollider2D m_triggerT;
    public BoxCollider2D m_triggerB;

    SpriteRenderer sr;
    BoxCollider2D bc;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        bc = GetComponent<BoxCollider2D>();
        sr.enabled = false;
        bc.enabled = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (!m_triggerT.IsTouching(col) && m_triggerB.IsTouching(col) && playerRB.velocity.y > 0)
            {
                sr.enabled = true;
                bc.enabled = true;
                m_triggerT.enabled = false;
                m_triggerB.enabled = false;
            }
        }
    }
}
