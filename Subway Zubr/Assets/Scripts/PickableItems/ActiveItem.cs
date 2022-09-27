using System.Collections;
using UnityEngine;
public class ActiveItem : MonoBehaviour
{
    [SerializeField] private Item item;
    private void OnTriggerEnter(Collider other)
    {
        switch (item)
        {
            case Item.Coin:
                Managers.PlayerManager.AddCoins(1);
                break;
            case Item.ToiletPaper:
                //Managers.PlayerManager.ToiletPaper();
                break;
            case Item.PinkWard:
                GetComponentsInChildren<MeshRenderer>()[1].enabled = false; // Пишемо так оскільки у батька також є MeshRenderer
                break;
        }

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
enum Item
{
    Coin,
    ToiletPaper,
    PinkWard
}