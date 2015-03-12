using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Activity))]
public class OneShotParticle : MonoBehaviour 
{
    private ParticleSystem ps;
    private Activity activity;

    public void Start()
    {
        ps = GetComponent<ParticleSystem>();
        activity = GetComponent<Activity>();
        StartCoroutine(setInactiveCoroutine());
    }

    IEnumerator setInactiveCoroutine()
    {
        yield return new WaitForSeconds(1);
        if(ps)
        {
            while (ps.IsAlive())
            {
                yield return new WaitForSeconds(1);
            }
        }
        activity.Active = false;
    }
}
