using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

    public float hitPoints = 1000f;
    float currentHitPoints;

    // Use this for initialization
    void Start() {
        currentHitPoints = hitPoints;
    }

    [PunRPC]
	public void TakeDamage(float d)
    {
        Debug.Log(currentHitPoints + "");
        currentHitPoints -= d;
        if(currentHitPoints < 0)
        {
            Die();
        }
    }

    void Die()
    { 
        PlayerSpawner spawner = GameObject.FindGameObjectWithTag("GameLogic").GetComponent<PlayerSpawner>();
        if (spawner && gameObject.tag == "Player" && GetComponent<PhotonView>().instantiationId == 0)
        {
            spawner.OnDeath();
        }
        if (GetComponent<PhotonView>().instantiationId == 0)
        {
            Destroy(gameObject);
        }
        else if(PhotonNetwork.isMasterClient)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
