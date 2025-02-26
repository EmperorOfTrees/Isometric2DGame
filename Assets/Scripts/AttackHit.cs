using UnityEngine;

public class AttackHit : MonoBehaviour
{
    [SerializeField] private string targetTag;
    [SerializeField] private float timeToLive;
    private float timer;
    [SerializeField] private int damage;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeToLive)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if ("Enemy" == targetTag)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
        else if (collision.CompareTag("Player"))
        {
            if ("Player" == targetTag)
            {
                collision.gameObject.GetComponent<Health>().TakeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
