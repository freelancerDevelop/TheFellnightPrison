﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MasterPlayerHealth : MonoBehaviour
{

    public Player me;
    public bool GM;

    void Start()
    {
        if (!PhotonNetwork.isMasterClient)
        {
            this.enabled = false;
        }
        else
        {
            if (!GM)
            {
                foreach (Player _p in GameObject.FindWithTag("CarryData").GetComponent<CarryData>().players)
                {
                    if (_p.Username == this.gameObject.GetComponent<PhotonView>().owner.name)
                    {
                        Debug.Log("Found Owner");
                        me = _p;
                        break;
                    }
                }
                Debug.Log("Setting Health for: " + this.gameObject.GetComponent<PhotonView>().owner.name);
                this.gameObject.GetComponent<PhotonView>().RPC("PlayerSetHealth", this.gameObject.GetComponent<PhotonView>().owner, (float)me.MaxHp);
            }
        }
    }

    float timer;
    bool timeing = false;
    public float frame = 0;
    public int runFrames = 200;
    public float MS = 5f;
    public float VS = 3f;
    
    public BoxCollider sword;
    public Animator anim;

    void thing()
    {
        if (!GM)
        {
            foreach (Player _p in GameObject.FindWithTag("CarryData").GetComponent<CarryData>().players)
            {
                if (_p.Username == this.gameObject.GetComponent<PhotonView>().owner.name)
                {
                    Debug.Log("Found Owner");
                    me = _p;
                    break;
                }
            }
            Debug.Log("Setting Health for: " + this.gameObject.GetComponent<PhotonView>().owner.name);
            this.gameObject.GetComponent<PhotonView>().RPC("PlayerSetHealth", this.gameObject.GetComponent<PhotonView>().owner, (float)me.MaxHp);
        }
    }

    public void startCollider()
    {
        frame = Time.time;
        sword.enabled = true;
        MS = .5f;
        VS = 1;
    }
    void turnOffCollider()
    {
        anim.ResetTrigger("Attack");
        sword.enabled = false;
        frame = -1;
        MS = 5;
        VS = 3;
    }

    void wait(int time)
    {
        timer = time;
    }

    // Update is called once per frame
    void Update()
    {
        if (timeing)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timeing = false;
                thing();
            }
        }

        if (!GM)
        {
            if (frame >= 0)
            {
                if (Time.time - frame > 5)
                {
                    turnOffCollider();
                }
            }

            if (me.CurHp <= 0)
            {
                Debug.Log("You're Dead!");
            }
        }
    }

    public void setMe(Player _player)
    {
        me = _player;
    }

    public void takeDamage(float _value)
    {
        Debug.Log("MasterPlayerHealth.takeDamage(" + _value + ");");
        me.CurHp -= _value;
        this.gameObject.GetComponent<PhotonView>().RPC("PlayerTakeDamage", this.gameObject.GetComponent<PhotonView>().owner, _value);
    }
}
