﻿using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Controller : MonoBehaviour {
    public GameObject PlayerToon, DungeonButton, LoginButton, LoginPanel, UIController, CreateAccountPanel, MasterConnectPanel, ConnectingPanel, CreateAccountButton, CreatePanel, GoodJobPanel, CharacterCreatePanel, PlayerMenu, Inventory, HealthPanel;
    public GameObject[] _loginInputs, SuccessOrFail, CreateInputs;
    public PhotonView myPhotonView;
    public bool loadInit = true;

    private GameObject _view;
    private bool UsernameAvailable, _create, okay;
    public Player _player;

    public GameObject curMenu;


	void Start () {
        DontDestroyOnLoad(this.gameObject);
        _create = false;
        okay = true;
	}
	
	void Update () {
        
	}

    public void takeDamage()
    {

    }

    public GameObject getCurMenu()
    {
        return curMenu;
    }

    public void setCurMenu(GameObject _value)
    {
        curMenu = _value;
    }

    public void StartGame()
    {
        this.gameObject.GetComponent<NetworkV2>().PhotonConnect();
        UsernameAvailable = false;
    }

    public void ReturnToLogin()
    {
        GoodJobPanel.SetActive(false);
        CreatePanel.SetActive(true);
        CreateAccountButton.SetActive(false);
        CreateAccountPanel.SetActive(false);
        LoginPanel.SetActive(true);
    }

    public void CreateScreen()
    {
        this.gameObject.GetComponent<NetworkV2>().PhotonConnect();
        _create = true;
        LoginPanel.SetActive(false);
        CreateAccountPanel.SetActive(true);
    }

    public void CreateAccount()
    {

        if (CreateInputs[1].GetComponent<InputField>().text == CreateInputs[2].GetComponent<InputField>().text)
        {
            if (UsernameAvailable)
            {
                myPhotonView.RPC("CreateAccount", PhotonTargets.MasterClient, CreateInputs[0].GetComponent<Text>().text, CreateInputs[1].GetComponent<InputField>().text);
                CreatePanel.SetActive(false);
                GoodJobPanel.SetActive(true);
                _create = false;
            }
        }
        else
        {
            CreateInputs[3].GetComponent<Text>().text = "Passwords do not match";
            //passwords do not match
        }
    }

    public void CheckUsernameReturn(bool _go)
    {
        if (_go)
        {
            CreateAccountButton.SetActive(true);
            UsernameAvailable = true;
            CreateInputs[3].GetComponent<Text>().text = "Username Available";
            CreateInputs[0].GetComponentInParent<InputField>().interactable = false;
            //Good Name
        }
        else
        {
            CreateInputs[3].GetComponent<Text>().text = "Username Not Available";
            //Bad Name
        }
    }

    public void CheckUsername()
    {
        if (CreateInputs[0].GetComponent<Text>().text.Contains(" "))
        {
            CreateInputs[3].GetComponent<Text>().text = "Please remove spaces";
        }
        else
        {
            myPhotonView.RPC("DbUsernameCheck", PhotonTargets.MasterClient, CreateInputs[0].GetComponent<Text>().text, myPhotonView.owner);
        }
    }

    public void ConnectionSuccesful()
    {
        _view = PhotonNetwork.Instantiate("playerTest", new Vector3(0, 0, 0), Quaternion.identity, 0);
        myPhotonView = _view.GetComponent<PhotonView>();
        if(_create){
        } else if (_loginInputs[0].GetComponent<Text>().text == "Master" && _loginInputs[1].GetComponent<InputField>().text == "")
        {
            LoginPanel.SetActive(false);
            MasterConnectPanel.SetActive(true);
            this.gameObject.GetComponent<Database>().MasterConnect();
        }
        else
        {
            string[] loginInfo = new string[2] { _loginInputs[0].GetComponent<Text>().text, _loginInputs[1].GetComponent<InputField>().text };
            _view.GetComponent<PhotonRPC>().Login(loginInfo);
        }
    }

    public void Login()
    {
        LoginButton.GetComponent<Button>().interactable = false;
        if (_loginInputs[0].GetComponent<Text>().text == "Master" && _loginInputs[1].GetComponent<InputField>().text == "")
        {
            this.gameObject.GetComponent<NetworkV2>().setAsMaster();
        }
        if (!PhotonNetwork.connected)
        {
            this.gameObject.GetComponent<NetworkV2>().PhotonConnect();
            UsernameAvailable = false;
        }
        else
        {
            if (_loginInputs[0].GetComponent<Text>().text == "Master" && _loginInputs[1].GetComponent<InputField>().text == "")
            {
                LoginPanel.SetActive(false);
                MasterConnectPanel.SetActive(true);
                //this.gameObject.GetComponent<Database>().MasterConnect();
            }
            else
            {
                string[] loginInfo = new string[2] { _loginInputs[0].GetComponent<Text>().text, _loginInputs[1].GetComponent<InputField>().text };
                _view.GetComponent<PhotonRPC>().Login(loginInfo);
            }
        }
    }

    public void LoginResult(bool _result)
    {
        Debug.Log("Recieved reply RPC");
        if (_result)
        {
            //Login Succesful
            SuccessOrFail[0].SetActive(true);
            
            myPhotonView.RPC("GetCharacter", PhotonTargets.MasterClient, _loginInputs[0].GetComponent<Text>().text, myPhotonView.owner);
        }
        else
        {
            //Login Fail
            SuccessOrFail[1].SetActive(true);
        }
    }

    public void NoCharacter()
    {
        LoginPanel.SetActive(false);
        CharacterCreatePanel.SetActive(true);
    }

    public void CoreReturn(int[] _stats){
        //Recieved Core stats for player
        _player = new Player(_loginInputs[0].GetComponent<Text>().text, _stats[0], _stats[1], _stats[2], _stats[3], _stats[4], _stats[5], _stats[6]);
        SuccessOrFail[0].GetComponent<Text>().text = "Player Loaded";
        myPhotonView.owner.name = _player.Username;
        myPhotonView.RPC("GetInventory", PhotonTargets.MasterClient, _loginInputs[0].GetComponent<Text>().text, myPhotonView.owner);
    }

    public void setInit(bool _value)
    {
        loadInit = _value;
    }

    public void CloseMasterLogin()
    {
        MasterConnectPanel.SetActive(false);
        if(!loadInit)
            this.gameObject.GetComponent<NetworkV2>().wait(10);
    }

    public void InvFilled()
    {
        Debug.Log("Controller.InvFilled");
        SuccessOrFail[0].GetComponent<Text>().text = "Inventory Loaded";
        HealthPanel.SetActive(true);
        PlayerToon = PhotonNetwork.Instantiate("SkeletonPlayer", GameObject.FindGameObjectWithTag("Spawnpoint").transform.position, Quaternion.identity, 0);
        PlayerToon.GetComponent<PhotonView>().owner.name = _player.Username; ;
        UIController.GetComponent<MenuController>().setClear(true);
        LoginPanel.SetActive(false);
        myPhotonView.RPC("SendPlayer", PhotonTargets.MasterClient, _player);
    }

    public void quitButton()
    {
        Application.Quit();
    }

    public void loadDungeonButton()
    {
        DungeonButton.GetComponent<Button>().interactable = false;
        DungeonButton.GetComponentInChildren<Text>().text = "Loading";
        myPhotonView.RPC("LoadDungeonLevel", PhotonTargets.MasterClient);
    }

    
}
