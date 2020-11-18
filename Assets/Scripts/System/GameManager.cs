using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }
    
    public Transform Target;
    public GameObject lure;
    [Tooltip("Speed for throwing a Lure")]
    [SerializeField] private float Speed = 2.0f;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

    }

    public void ShootLure()
    {
        Lure.Instance._isUsed = true;
        Vector3 direction = Target.transform.position - Lure.Instance.transform.position;
        Rigidbody _Rigidbody = Lure.Instance.GetComponent<Rigidbody>();

        _Rigidbody.AddForce(direction * Speed, ForceMode.Impulse);
    }
}
