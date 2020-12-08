using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Interaction : MonoBehaviour, IPointerClickHandler
{
    private Rigidbody _rigidBody;
    public UnityEvent OnClick;

    public bool _spinning = false;

    void Start()
    {
        _rigidBody = GetComponent<Rigidbody>();
        _spinning = false;
    }

    private void Update()
    {
        if(_spinning)
        {
            transform.Rotate(Vector3.down * Time.deltaTime * 100.0f);
        }
    }

    public void SetSpin()
    {
        _spinning = !_spinning;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.TargetInteraction = this.gameObject;
        OnClick.Invoke();
    }
}
