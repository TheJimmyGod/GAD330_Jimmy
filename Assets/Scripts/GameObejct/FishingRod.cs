using System.Collections;
using System.Collections.Generic;
using Liminal.Core.Fader;
using Liminal.Platform.Experimental.App.Experiences;
using Liminal.SDK.Core;
using Liminal.SDK.VR;
using Liminal.SDK.VR.Avatars;
using Liminal.SDK.VR.Input;
using UnityEngine;

public class FishingRod : MonoBehaviour
{
    private static FishingRod _instance;

    public static FishingRod Instance { get { return _instance; } }

    public GameObject Rod;
    public GameObject line;

    public Lure lure;

    private void Awake()
    {
        if (_instance != null && _instance != this)
            Destroy(this.gameObject);
        else
            _instance = this;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
