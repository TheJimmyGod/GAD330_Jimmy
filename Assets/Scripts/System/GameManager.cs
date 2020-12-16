using Liminal.Core.Fader;
using Liminal.SDK.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }
    
    public Transform Target;

    public AudioClip Shoot;

    //public Text textTimer;

    public GameObject TargetInteraction;
    //public GameObject lure;

    private bool _isEnd = false;

    private float timer = 0.0f;

    public float maximumTimer = 360.0f;

    [Tooltip("Speed for throwing a Lure")]
    [SerializeField] private float Speed = 2.0f;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;
    }

    private void Start()
    {
        timer = 0.0f;
    }

    private void Update()
    {
        if (timer < maximumTimer)
            timer += Time.deltaTime;
        else
        {
            if(!_isEnd)
            {
                StartCoroutine(EndOfGame());
                _isEnd = true;
            }
        }

        //string str = string.Format("{0:0.#}", timer);

        //textTimer.text = str;
    }

    private IEnumerator EndOfGame()
    {
        ScreenFader.Instance.FadeTo(Color.black, duration: 1);
        yield return ScreenFader.Instance.WaitUntilFadeComplete();
        Debug.Log("End");
        ExperienceApp.End();
        Application.Quit();
    }

    public void ShootLure()
    {
        //if (Lure.Instance._isUsed)
        //    return;
        AudioManager.Instance.PlaySfx(Shoot);
        Target.GetComponent<Fish>().isCaptured = true;
        //Lure.Instance._isUsed = true;
        //Vector3 direction = Target.transform.position - Lure.Instance.transform.position;
        //Rigidbody _Rigidbody = Lure.Instance.GetComponent<Rigidbody>();

        //_Rigidbody.AddForce(direction * Speed, ForceMode.Impulse);
    }

    public void Rotating()
    {
        TargetInteraction.GetComponent<Interaction>().SetSpin();
    }
}
