using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleDamage : MonoBehaviour
{
    ParticleSystem particles;

    [SerializeField]
    PersonHealth health;

    public int damage = 2;
    public List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();


    void Start()
    {
        particles = GetComponent<ParticleSystem>();
    }


    private void OnParticleCollision(GameObject other)
    {
        int numCollisionEvents = particles.GetCollisionEvents(other, collisionEvents);
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Lost health");
            health.TakeDamage(damage);
        }
    }
}
