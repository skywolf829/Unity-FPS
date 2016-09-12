using UnityEngine;
using System.Collections;

public class NetworkCharacter : Photon.MonoBehaviour {

    private Vector3 realPos;
    private Quaternion realRot;

    float lastUpdateTime;

	// Use this for initialization
	void Start () {
        realPos = transform.position;
        realRot = transform.rotation;
	}
	
	// Update is called once per frame
	void Update () {
        if (photonView.isMine)
        {
            //Do nothing
        }
        else
        {
            //transform.position = Vector3.Lerp(transform.position, realPos, 0.0f);
            //transform.rotation = Quaternion.Lerp(transform.rotation, realRot, 0.0f);

            transform.position = realPos;
            transform.rotation = realRot;
        }
	}

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            //Our player, send our actual position to network
            stream.SendNext(transform.position);
            stream.SendNext(transform.position);
        }
        else
        {
            //Someone else's player, recieve their position (few ms late)
            realPos = (Vector3)stream.ReceiveNext();
            realRot = (Quaternion)stream.ReceiveNext();
            lastUpdateTime = Time.time;
        }
    }
}
