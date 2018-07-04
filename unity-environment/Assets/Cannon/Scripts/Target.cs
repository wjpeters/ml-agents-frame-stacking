using UnityEngine;

public class Target : Ball
{
    public float speed = 1.5f;

    private float xMin = -9f;
    private float xMax = 9f;
    private float zMin = -2f;
    private float zMax = 5f;
    private float zMaxT = 3f;

    private Rect bounds;

    private void Start()
    {
        gameObject.SetActive(false);
        bounds = new Rect(xMin, zMin, 2 * xMax, 7f);
    }

    private void Update()
    {
        if (!bounds.Contains(new Vector2(transform.localPosition.x, transform.localPosition.z))
            || transform.position.y > 1f)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.rotation = Quaternion.identity;

        int side = Random.Range(0, 3);
        Vector3 targetPos = new Vector3(0f, defaultPos.y, 0f);

        switch(side)
        {
            // top
            case 0:
                transform.localPosition = new Vector3(Random.Range(xMin, xMax), defaultPos.y, zMax);
                if (transform.localPosition.x < 0f)
                {
                    targetPos.x = Random.Range(0, xMax);
                    targetPos.z = Random.Range(zMin, zMaxT);
                }
                else
                {
                    targetPos.x = Random.Range(xMin, 0);
                    targetPos.z = Random.Range(zMin, zMaxT);
                }
                break;
            // left
            case 1:
                transform.localPosition = new Vector3(xMin, defaultPos.y, Random.Range(zMin, zMax));
                targetPos.x = Random.Range(0, xMax);
                targetPos.z = Random.Range(zMin, zMax);
                break;
            // right
            case 2:
                transform.localPosition = new Vector3(xMax, defaultPos.y, Random.Range(zMin, zMax));
                targetPos.x = Random.Range(xMin, 0);
                targetPos.z = Random.Range(zMin, zMax);
                break;

            default:
                transform.localPosition = defaultPos;
                break;
        }

        Vector3 force = targetPos - transform.localPosition;

        rb.AddForce(force * speed, ForceMode.Impulse);
    }

    public void SetActive(bool active)
    {
        if (active != gameObject.activeSelf)
        {
            gameObject.SetActive(active);

            if (active)
            {
                Spawn();
            }
        }
    }
}
