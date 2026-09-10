using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnInterval = 2f;
    public float spawnMargin = 1f;

    private float timer;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnInterval)
        {
            timer = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            return;
        }

        Vector3 spawnPosition = GetRandomOffScreenPosition();
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private Vector3 GetRandomOffScreenPosition()
    {
        Camera cam = Camera.main;
        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;
        Vector3 camPos = cam.transform.position;

        int side = Random.Range(0, 4);
        float x;
        float y;

        switch (side)
        {
            case 0: // 왼쪽
                x = -halfWidth - spawnMargin;
                y = Random.Range(-halfHeight, halfHeight);
                break;
            case 1: // 오른쪽
                x = halfWidth + spawnMargin;
                y = Random.Range(-halfHeight, halfHeight);
                break;
            case 2: // 위쪽
                x = Random.Range(-halfWidth, halfWidth);
                y = halfHeight + spawnMargin;
                break;
            default: // 아래쪽
                x = Random.Range(-halfWidth, halfWidth);
                y = -halfHeight - spawnMargin;
                break;
        }

        return new Vector3(camPos.x + x, camPos.y + y, 0f);
    }
}
