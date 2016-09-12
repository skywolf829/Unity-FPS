using UnityEngine;
using System.Collections;

public class NetworkManager : MonoBehaviour {

    private const string room = "mainRoom";
    private const string version = "0.1a";

    public bool offlineMode = true;

    private bool connecting = false;

    // Use this for initialization
    
    void Connect()
    {
        Debug.Log("Connected");
        PhotonNetwork.autoJoinLobby = true;
        PhotonNetwork.ConnectUsingSettings(version);        
    }

    void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        if (!PhotonNetwork.connected && !connecting)
        {
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Username: ");
            GUILayout.TextField("username");
            GUILayout.EndHorizontal();

            if (GUILayout.Button("Single Player"))
            {
                connecting = true;
                PhotonNetwork.offlineMode = true;
                OnJoinedLobby();
            }
            if(GUILayout.Button("Multi Player"))
            {
                connecting = true;
                Connect();
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }
    }
	
    void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
        PhotonNetwork.JoinRandomRoom();
    }

    void OnPhotonRandomJoinFailed()
    {
        Debug.Log("Join random failed");
        PhotonNetwork.CreateRoom(room);    
    }

    void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        gameObject.GetComponent<PlayerSpawner>().SpawnMyPlayer();
    }
    
}
