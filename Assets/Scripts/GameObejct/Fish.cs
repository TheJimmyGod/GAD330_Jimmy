using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Fish : MonoBehaviour, IPointerClickHandler
{
    private enum Fish_Type { Big, Small }

    public GameObject Avatar;
    public GameObject Spawner;

    public UnityEvent OnClick;
    private float _Angle = 0.0f;
    private float _Speed = 0.75f;
    private Vector3 _Velocity = Vector3.zero;
    private Rigidbody _Rigidbody;
    [Tooltip("Area length that the fish moving around")]
    [SerializeField] private float _Radius = 5.0f;

    public bool isCaptured = false;
    public bool isPulling = false;
    
    
    // Start is called before the first frame update
    void Start()
    {
        isCaptured = false;
        isPulling = false;
        _Rigidbody = GetComponent<Rigidbody>();
        _Rigidbody.velocity = Vector3.zero;
        Avatar = GameObject.FindGameObjectWithTag("Avatar").gameObject;

        foreach (GameObject fish in GameObject.FindGameObjectsWithTag("Fish"))
        {
            Physics.IgnoreCollision(fish.GetComponent<Collider>(), GetComponent<Collider>());
        }

    }

    public Fish Initialize(GameObject spawner)
    {
        Spawner = spawner.gameObject;
        float randomNumber = Random.Range(-1.0f, 1.0f);
        _Velocity.y = randomNumber;
        transform.position = new Vector3(transform.position.x, randomNumber, transform.position.z);
        _Speed = Random.Range(1.5f, 3.5f);
        OnClick.AddListener(() => GameManager.Instance.ShootLure());
        return this;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPulling)
        {
            if (!isCaptured)
            {
                _Angle += ((_Speed * Mathf.PI) / 100.0f) * Time.deltaTime;
                _Velocity.x = Mathf.Cos(_Angle) * _Radius;
                _Velocity.z = Mathf.Sin(_Angle) * _Radius;
                _Rigidbody.MovePosition(_Velocity + transform.forward * Time.deltaTime);
                transform.LookAt(transform.position + _Velocity);
            }
            else
            {
                isPulling = true;
                _Rigidbody.velocity = Vector3.zero;
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(pull());
            }
        }

        if(Vector3.Distance(Avatar.transform.position,transform.position) < 1.0f)
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator pull()
    {
        yield return new WaitForSeconds(1.0f);
        Vector3 Direction = Avatar.transform.position - transform.position;
        _Rigidbody.AddForce(Direction * 1.0f, ForceMode.Impulse);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lure")
        {
            isCaptured = true;
            other.gameObject.GetComponent<Lure>().Captured = true;
        }
    }

    private void OnDestroy()
    {
        if(this.gameObject.activeSelf)
            Spawner.GetComponent<Spawner>()._activeFishes.Remove(this.gameObject);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.Target = this.transform;
        OnClick.Invoke();
    }
}
