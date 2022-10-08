using System.Collections;
using System.Linq;
using UnityEngine;
public class ActiveItem : MonoBehaviour
{
    [SerializeField] private Item item;
    private void OnTriggerEnter(Collider other)
    {
        Managers.main.ItemsManager.ActiveItem(item);

        StartCoroutine(Particles());
    }
    private IEnumerator Particles()
    {
        GetComponent<Animator>().enabled = false;
        GetComponentsInChildren<MeshRenderer>().ToList().ForEach(x => x.enabled = false);
        GetComponent<MeshCollider>().enabled = false;
        GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}