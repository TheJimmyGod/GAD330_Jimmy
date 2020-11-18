﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lure : MonoBehaviour
{
    private static Lure _instance;

    public static Lure Instance { get { return _instance; } }

    private GameObject Avatar;
    private GameObject CapturedFish;

    private float _Speed = 5.0f;
    private Rigidbody _Rigidbody;
    
    private Vector3 _Velocity;

    [SerializeField]
    private float _Availabletimer = 3.0f;
    private float _CurrentTimer = 0.0f;
    public bool _isUsed = false;
    public bool Captured = false;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

    }

    void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        Avatar = GameObject.FindGameObjectWithTag("Avatar").gameObject;
        transform.position = new Vector3(Avatar.transform.position.x, 1.0f, Avatar.transform.position.z);
        Captured = false;
    }

    void Update()
    {
        if(_isUsed)
        {

            _CurrentTimer += Time.deltaTime;
            if(_CurrentTimer >= _Availabletimer)
            {
                _CurrentTimer = 0.0f;
                _Rigidbody.velocity = Vector3.zero;
                Reel();
            }
            if (Captured)
            {
                _Rigidbody.velocity = Vector3.zero;
                _CurrentTimer = 0.0f;
                _isUsed = false;
                StartCoroutine(Capturing());
                return;
            }
        }
        else
        {
            if (Captured)
            {
                if (Vector3.Distance(Avatar.transform.position, transform.position) < 2.0f)
                {
                    Captured = false;
                    _Rigidbody.velocity = Vector3.zero;
                    GameObject EndofLine = FishingRod.Instance.line.transform.Find("Reset").gameObject;
                    transform.position = new Vector3(EndofLine.gameObject.transform.position.x, 
                        EndofLine.gameObject.transform.position.y, 
                        EndofLine.gameObject.transform.position.z);
                    transform.LookAt(Vector3.forward);
                }
            }
        }

    }

    private IEnumerator Capturing()
    {
        yield return new WaitForSeconds(1.0f);
        Reel();
    }

    public void Reel()
    {
        Vector3 direction = Avatar.transform.position - transform.position;
        _Rigidbody.AddForce(direction * 1.0f, ForceMode.Impulse);
        _isUsed = false;
    }
}
