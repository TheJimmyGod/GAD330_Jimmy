using Liminal.SDK.VR.Avatars;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lure : MonoBehaviour
{
    private static Lure _instance;

    public static Lure Instance { get { return _instance; } }

    private GameObject Avatar;

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
        var collider = transform.GetComponent<Collider>();
        GameObject EndofLine = FishingRod.Instance.Reset.gameObject;

        if(GameManager.Instance.Target == null)
        {
            transform.position = new Vector3(EndofLine.gameObject.transform.position.x,
    EndofLine.gameObject.transform.position.y,
    EndofLine.gameObject.transform.position.z);
            transform.LookAt(Vector3.forward);
            _Rigidbody.velocity = Vector3.zero;
        }

        if(_isUsed)
        {
            collider.enabled = true;
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
                    transform.position = new Vector3(EndofLine.gameObject.transform.position.x, 
                        EndofLine.gameObject.transform.position.y, 
                        EndofLine.gameObject.transform.position.z);
                    transform.LookAt(Vector3.forward);
                    _Rigidbody.velocity = Vector3.zero;

                }
            }
            else
            {
                collider.enabled = false;
                _Rigidbody.velocity = Vector3.zero;
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
        Vector3 direction = new Vector3(Avatar.transform.position.x - transform.position.x,
            transform.position.y
            , Avatar.transform.position.z - transform.position.z);
        _Rigidbody.AddForce(direction * 0.5f, ForceMode.Impulse);
        _isUsed = false;
    }
}
