using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

    public GameObject playerPrefab;
    public GameObject mainCamera;

    private bool dead;
    private float deathTimer = 10;
    private float deathTime;

    // Update is called once per frame
    void Update () {
        if (dead && Time.time > deathTime + deathTimer)
        {
            dead = false;
            SpawnMyPlayer();
        }
    }

    public void SpawnMyPlayer()
    {
        if (playerPrefab)
        {
            if (mainCamera)
            {
                mainCamera.SetActive(false);
            }
            GameObject myPlayer = (GameObject)PhotonNetwork.Instantiate(playerPrefab.name, new Vector3(0, 2, 0), Quaternion.identity, 0);
            if (myPlayer)
            {
                ((MonoBehaviour)myPlayer.GetComponent("FirstPersonController")).enabled = true;
                myPlayer.GetComponent<Boundaries>().enabled = true;
                myPlayer.GetComponent<PlayerShooting>().enabled = true;
                myPlayer.GetComponentInChildren<Camera>().enabled = true;
            }
        }
    }

    public void OnDeath()
    {
        if (mainCamera)
        {
            mainCamera.SetActive(true);
            deathTime = Time.time;
            dead = true;
        }
    }
}
