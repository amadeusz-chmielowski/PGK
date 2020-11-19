using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : MonoBehaviour
{
    public float missileSpeed = 1.0f;
    public float missileAcceleration = 1.0f;
    public RaycastHit hit;
    public ParticleSystem explosion;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(AutoDestruct(3));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(transform.up * missileSpeed * Time.deltaTime, Space.World);
        missileSpeed += missileAcceleration * Time.deltaTime;
        Debug.DrawRay(transform.position, transform.up);

        if(Physics.Raycast(transform.position, transform.up, out hit, 1.0f/(2*missileSpeed)))
        {
            Explosion();
        }
    }

    private void Explosion()
    {
        StartCoroutine(Explode());
    }

    public IEnumerator Explode()
    {
        var em = explosion.emission;
        em.enabled = true;
        yield return new WaitForSeconds(0.05f);
        em.enabled = false;
        Destroy(gameObject);
    }

    public IEnumerator AutoDestruct(int time)
    {
        yield return new WaitForSeconds(time);
        Explosion();
    }

}
