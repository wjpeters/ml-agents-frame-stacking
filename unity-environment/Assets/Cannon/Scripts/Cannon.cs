using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private CannonBall ball;

    public float Speed = 0.25f;
    public float Force = 4f;

    private const float bounds = 8.9f;
    private Vector3 force;
    private Vector3 defaultPos;

    private void Awake()
    {
        defaultPos = transform.localPosition;
        force = new Vector3(0, 0, Force);
    }

    public void ResetPosition()
    {
        transform.localPosition = defaultPos;
    }

    public void Move(float incr = 0f)
    {
        float x = Mathf.Min(bounds, Mathf.Max(-bounds, transform.localPosition.x + incr));
        transform.localPosition = new Vector3(x, defaultPos.y, defaultPos.z);
    }

    public bool Shoot()
    {
        if (ball.ready)
        {
            ball.Shoot(transform.localPosition.x, force);
            return true;
        }
        return false;
    }
}
