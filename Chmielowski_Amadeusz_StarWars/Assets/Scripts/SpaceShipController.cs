using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipController : MonoBehaviour
{

    private float axisX;
    private float axisY;
    public float maxForce = 4.0f;

    public ParticleSystem EngineParticleEffect_UL;
    public ParticleSystem EngineParticleEffect_UR;
    public ParticleSystem EngineParticleEffect_DL;
    public ParticleSystem EngineParticleEffect_DR;
    public ParticleSystem EngineParticleEffect_LU;
    public ParticleSystem EngineParticleEffect_LD;
    public ParticleSystem EngineParticleEffect_RU;
    public ParticleSystem EngineParticleEffect_RD;
    public GameObject missile;

    private bool canShoot = false;

    public enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }


    // Start is called before the first frame update
    void Start()
    {
        axisX = 0;
        axisY = 0;
    }

    // Update is called once per frame
    void Update()
    {
        axisX = Input.GetAxisRaw("Horizontal") * Time.deltaTime;
        axisY = Input.GetAxisRaw("Vertical") * Time.deltaTime;

        if (axisX != 0 || axisY != 0)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(axisX * maxForce, axisY * maxForce, 0), ForceMode.Acceleration);
        }

        ParticleSystemEnablement(axisX, axisY);
        LunchMissile();
    }

    void LunchMissile()
    {

        if (Input.GetKeyUp(KeyCode.T) && !canShoot)
        {
            StartCoroutine( LunchMissileDirection(Direction.Up));
        }
        else if (Input.GetKeyUp(KeyCode.F) && !canShoot)
        {
            StartCoroutine(LunchMissileDirection(Direction.Left));
        }
        else if (Input.GetKeyUp(KeyCode.G) && !canShoot)
        {
            StartCoroutine(LunchMissileDirection(Direction.Right));
        }
        else if (Input.GetKeyUp(KeyCode.V) && !canShoot)
        {
            StartCoroutine(LunchMissileDirection(Direction.Down));
        }
    }

    public IEnumerator LunchMissileDirection(Direction direction)
    {
        canShoot = true;
        if (direction == Direction.Up)
        {
            Instantiate(missile, transform.position, Quaternion.Euler(0, 0, 0));
            yield return new WaitForSeconds(1);
        }
        if (direction == Direction.Left)
        {
            Instantiate(missile, transform.position, Quaternion.Euler(0, 0, 90));
            yield return new WaitForSeconds(1);
        }
        if (direction == Direction.Right)
        {
            Instantiate(missile, transform.position, Quaternion.Euler(0, 0, -90));
            yield return new WaitForSeconds(1);
        }
        if (direction == Direction.Down)
        {
            Instantiate(missile, transform.position, Quaternion.Euler(180, 0, 0));
            yield return new WaitForSeconds(1);
        }
        canShoot = false;

    }

    private void ParticleSystemEnablement(float inX, float inY)
    {
        if (axisX > 0)
        {
            SetParticleSystemPair(EngineParticleEffect_LU, EngineParticleEffect_LD, true);
            SetParticleSystemPair(EngineParticleEffect_RU, EngineParticleEffect_RD, false);
        }
        else if (axisX < 0)
        {
            SetParticleSystemPair(EngineParticleEffect_LU, EngineParticleEffect_LD, false);
            SetParticleSystemPair(EngineParticleEffect_RU, EngineParticleEffect_RD, true);
        }
        else
        {
            SetParticleSystemPair(EngineParticleEffect_RU, EngineParticleEffect_RD, false);
            SetParticleSystemPair(EngineParticleEffect_LU, EngineParticleEffect_LD, false);
        }
        if (axisY > 0)
        {
            SetParticleSystemPair(EngineParticleEffect_DL, EngineParticleEffect_DR, true);
            SetParticleSystemPair(EngineParticleEffect_UL, EngineParticleEffect_UR, false);
        }
        else if (axisY < 0)
        {
            SetParticleSystemPair(EngineParticleEffect_DL, EngineParticleEffect_DR, false);
            SetParticleSystemPair(EngineParticleEffect_UL, EngineParticleEffect_UR, true);
        }
        else
        {
            SetParticleSystemPair(EngineParticleEffect_DL, EngineParticleEffect_DR, false);
            SetParticleSystemPair(EngineParticleEffect_UL, EngineParticleEffect_UR, false);
        }
    }

    private void SetParticleSystemPair(ParticleSystem ps1, ParticleSystem ps2, bool setOnOrOff)
    {
        var em = ps1.emission;
        em.enabled = setOnOrOff;

        var em2 = ps2.emission;
        em2.enabled = setOnOrOff;
    }
}
