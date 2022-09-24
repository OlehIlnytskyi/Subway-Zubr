using System.Collections;
using UnityEngine;
public class ToiletPaper : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Managers.PlayerManager.ToiletPaper();
        StartCoroutine(Particles());
    }

    private IEnumerator Particles()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}