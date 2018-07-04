using UnityEngine;
using System;

public class CannonBall : Ball
{
    [HideInInspector] public bool ready = true;

    private Action callbackHit;
    private Rect bounds = new Rect(-9f, -3f, 18f, 8f);

    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        if (!bounds.Contains(new Vector2(transform.localPosition.x, transform.localPosition.z))
            || transform.position.y > 1f)
        {
            gameObject.SetActive(false);
            ready = true;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            if (contact.otherCollider.gameObject.name == "Target")
            {
                callbackHit();
            }
        }
    }

    public void RegisterCallback(Action hit)
    {
        callbackHit = hit;
    }

    public void Shoot(float x, Vector3 force)
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.rotation = Quaternion.identity;
        transform.localPosition = new Vector3(x, defaultPos.y, defaultPos.z);

        ready = false;
        gameObject.SetActive(true);
        rb.AddForce(force, ForceMode.Impulse);
    }
}
