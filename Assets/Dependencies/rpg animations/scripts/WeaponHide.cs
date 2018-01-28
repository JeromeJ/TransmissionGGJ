#pragma warning disable CS0108 // Member hides inherited member; missing new keyword
using UnityEngine;
using System.Collections;

public class WeaponHide : MonoBehaviour
{

    [SerializeField]
    Animator animator;

    [SerializeField]
    string tag;

    public bool turned = true;

    SkinnedMeshRenderer renderer;

    void Start()
    {
        renderer = GetComponent<SkinnedMeshRenderer>();
    }

    public bool isPlayingAnim(string s)
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName(s);
    }

    void Update()
    {
        if (!turned) return;
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag(tag))
        {
            renderer.enabled = true;
        } else
        {
            renderer.enabled = false;
        }
    }
}

#pragma warning restore CS0108 // Member hides inherited member; missing new keyword