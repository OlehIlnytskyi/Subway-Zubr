using System.Collections;
using UnityEngine;
public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("You got a Coin!");
        StartCoroutine(Particles());
    }
    
    private IEnumerator Particles()
    {
        Debug.Log("BEbra");
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}