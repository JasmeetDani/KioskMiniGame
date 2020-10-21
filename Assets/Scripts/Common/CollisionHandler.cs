using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float randomFactor = 30.0f;
    [SerializeField] private float thresholdVel = 30.0f;
    [SerializeField] private Rigidbody body;

    void OnCollisionEnter(Collision collision)
    {
        RandomVelocityTweak();
    }

    private void RandomVelocityTweak()
    {
        if (body.velocity.magnitude < thresholdVel)
        {
            float velocityTweak = Random.Range(randomFactor, randomFactor * 2);
            body.AddForce(velocityTweak * body.velocity.normalized, ForceMode.Impulse);
        }
    }
}