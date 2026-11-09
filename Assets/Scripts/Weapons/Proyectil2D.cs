
using UnityEngine;

public class Projectile2D : MonoBehaviour
{
    public float speed = 12f;
    public float lifetime = 2f;

    private Vector2 direction;

    public void SetDirection(Vector2 newDirection)
    {
        direction = newDirection.normalized;
    }

    void Update()
    {
        transform.Translate(
            direction * speed * Time.deltaTime,
            Space.World
        );

        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
