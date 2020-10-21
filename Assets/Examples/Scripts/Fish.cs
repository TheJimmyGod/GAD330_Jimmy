using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Fish : MonoBehaviour, IPointerClickHandler
{
    public GameObject Avatar;

    public UnityEvent OnClick;
    private float _Angle = 0.0f;
    [SerializeField]
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
        _Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Avatar = GameObject.Find("Examples");
        if(!isPulling)
        {
            if (!isCaptured)
            {
                _Angle += ((_Speed * Mathf.PI) / 100.0f) * Time.deltaTime;
                _Velocity.x = Mathf.Cos(_Angle) * _Radius;
                _Velocity.y = 5.0f;
                _Velocity.z = Mathf.Sin(_Angle) * _Radius;
                _Rigidbody.MovePosition(_Velocity + transform.forward * Time.deltaTime);
                transform.LookAt(transform.position + _Velocity);
            }
            else
            {
                isPulling = true;
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

    public void OnPointerClick(PointerEventData eventData)
    {
        Avatar.GetComponent<Liminal.Examples.Examples>().Target = this.transform;
        OnClick.Invoke();
    }
}
