using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxController : MonoBehaviour
{
    public int idPool = -1;
    public float timeLife = 2;
    ParticleSystem fx;

    void Awake()
    {
        fx = GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        StartCoroutine(Play());
    }

    IEnumerator Play()
    {
        if (fx != null) fx.Play();
        yield return new WaitForSeconds(timeLife);
        ObjectsPool.Despawn(idPool, gameObject);
        yield return null;
    }
}
