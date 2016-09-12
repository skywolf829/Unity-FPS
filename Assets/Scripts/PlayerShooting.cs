using UnityEngine;
using System.Collections;

public class PlayerShooting : MonoBehaviour {

    public float fireRate = 0.05f;
    public float fireDistance = 256f;
    public float damage = 5;

    private float cooldown = 0f;

	// Use this for initialization
	void Start () {
        
	}

    // Update is called once per frame
    void Update() {
        cooldown -= Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
	}
    void Shoot()
    {
        if (cooldown <= 0)
        {
            Ray ray = new Ray(transform.position, Camera.main.transform.forward);
            RaycastHit hitInfo = FindClosestHitInfo(ray);
            
            if(hitInfo.distance != -1)
            {
                Debug.Log("Hit: " + hitInfo.transform.name);
                Health h = hitInfo.transform.GetComponent<Health>();

                if(h != null)
                {
                    //h.TakeDamage(damage);
                    PhotonView pv = h.GetComponent<PhotonView>();
                    if (pv != null)
                    {
                        pv.RPC("TakeDamage", PhotonTargets.All, damage);
                    }
                }
            }

            cooldown = fireRate;
        }
    }

    RaycastHit FindClosestHitInfo(Ray ray)
    {

        RaycastHit closestHit = new RaycastHit();
        closestHit.distance = -1;

        RaycastHit[] hits = Physics.RaycastAll(ray);

        if(hits.Length > 0)
        {
            closestHit = hits[0];
        }
        for(int i = 1; i < hits.Length; i++)
        {
            if(hits[i].distance < closestHit.distance)
            {
                closestHit = hits[i];
            }
        }       

        return closestHit;
    }
}
