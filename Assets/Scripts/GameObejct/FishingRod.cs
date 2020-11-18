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
        var avatar = VRAvatar.Active;
        var lure = Lure.Instance;

        if (avatar == null)
            return;

        if (lure == null)
            return;

        if(lure._isUsed == false && lure.Captured == false)
        {
            Rod.transform.LookAt(new Vector3(avatar.LookForward.x, avatar.LookForward.y + 0.1f, avatar.LookForward.z));
            float step = 1.1f * Time.deltaTime;
            Vector3 dir = Vector3.RotateTowards(transform.forward, (this.transform.position - lure.transform.position), step, 0.0f);
            transform.rotation = Quaternion.LookRotation(dir);
        }
    }
}
