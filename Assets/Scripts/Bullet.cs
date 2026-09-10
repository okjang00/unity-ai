using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
    }

    private void OnBecameInvisible()
    {
        BulletPool.Instance.Return(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Destroy(other.gameObject);
            BulletPool.Instance.Return(gameObject);
        }
    }
}
