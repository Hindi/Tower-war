using UnityEngine;
using System;
using System.Collections;

public class OneShotParticle : MonoBehaviour 
{
    [SerializeField]
    private ParticleSystem ps;
    [SerializeField]
    private GameObject model;

    public void Start()
    {

    }

    public void setInactive(Action effectEndCallback)
    {
        StartCoroutine(setInactiveCoroutine(effectEndCallback));
    }

    IEnumerator setInactiveCoroutine(Action effectEndCallback)
    {
        if (model)
            model.SetActive(false);
        ps.Play();
        while (ps.IsAlive())
        {
            yield return null;
        }
        effectEndCallback();
        if (model)
            model.SetActive(true);
    }
}
