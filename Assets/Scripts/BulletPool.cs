using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance { get; private set; }

    public GameObject bulletPrefab;
    public int initialPoolSize = 20;

    private readonly Queue<GameObject> pool = new Queue<GameObject>();

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < initialPoolSize; i++)
        {
            pool.Enqueue(CreateBullet());
        }
    }

    private GameObject CreateBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, transform);
        bullet.SetActive(false);
        return bullet;
    }

    public GameObject Get(Vector3 position, Quaternion rotation)
    {
        GameObject bullet = pool.Count > 0 ? pool.Dequeue() : CreateBullet();

        bullet.transform.SetPositionAndRotation(position, rotation);
        bullet.SetActive(true);
        return bullet;
    }

    public void Return(GameObject bullet)
    {
        bullet.SetActive(false);
        pool.Enqueue(bullet);
    }
}
