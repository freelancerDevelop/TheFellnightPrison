﻿using UnityEngine;
using System.Collections;

public class GlobalFunctions :  MonoBehaviour{

	private bool _ready = false;
    private bool _connection = false;


    public void Start()
    {
        //if (!PhotonNetwork.connected) { PhotonNetwork.ConnectUsingSettings("V0.0"); }
    }

    public void Update()
    {

    }

	public void SpawnPlayer(){
		GameObject _player = PhotonNetwork.Instantiate("NetworkPlayer", GameObject.FindGameObjectWithTag("Spawnpoint").transform.position , Quaternion.identity, 0);
		if(_player.GetPhotonView().isMine) { _player.SetActive(true); }
	}

    public bool PhotonConnection()
    {
        return _connection;
    }

	public bool CheckReady(){
		return _ready;
	}
	
	public void SetReady(bool _bool){
		_ready = _bool;
	}
	
	public static void PhotonConnect(){
		bool _connect = PhotonNetwork.ConnectUsingSettings("V0.0");
	}

    void OnFailedToConnectToPhoton(){
        Debug.Log("OnFailedToConnectToPhoton()");
    }

    void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster()");
    }

    void OnConnectionFail()
    {
        Debug.Log("OnConnectionFail()");
    }

    void OnDisconnectedFromPhoton()
    {
        Debug.Log("OnDisconnectedFromPhoton()");
    }

	void OnConnectedToPhoton()
	{
        _connection = true;
		Debug.Log("Connected to Photon Server");
        PhotonNetwork.JoinLobby();
	}
	
	void OnJoinedLobby(){
		Debug.Log("OnJoinedLobby");
        PhotonNetwork.JoinRoom("FillerRoom-");
        // + Random.Range(1, 100000)
	}
	
	void OnPhotonJoinRoomFailed(){
		Debug.Log ("OnJoinRoomFailed");
		PhotonNetwork.CreateRoom("FillerRoom-");
        // + Random.Range(1, 100000)
	}
	
	void OnPhotonCreateRoomFailed(){
		Debug.Log("OnCreateRoomFailed");
	}
	
	void OnCreatedRoom(){
		Debug.Log("OnCreatedRoom");
	}
	
	void OnJoinedRoom ()
	{
		Debug.Log("OnJoinedRoom: " + PhotonNetwork.room.ToString());
        this.gameObject.GetComponent<MainController>().LaunchGenerator();
	}
}
