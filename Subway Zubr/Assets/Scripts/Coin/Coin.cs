using System.Collections;
using UnityEngine;
public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Managers.PlayerManager.AddCoins(1);
        StartCoroutine(Particles());
    }
    
    private IEnumerator Particles()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}