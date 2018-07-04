using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Magnet[] magnets;

    protected Rigidbody rb;
    protected Vector3 defaultPos;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        defaultPos = transform.localPosition;
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < magnets.Length; i++)
        {
            Vector3 delta = magnets[i].transform.position - transform.position;
            float force = Mathf.Pow(4f / Vector3.Distance(magnets[i].transform.position, 
                                         transform.position), 3f) * magnets[i].strength;
            rb.AddForce(delta * force, ForceMode.Force);
        }

        if (rb.velocity.magnitude < 2f)
        {
            rb.velocity *= 1.1f;
        }
    }
}
