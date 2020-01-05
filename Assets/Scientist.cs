using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scientist : TimeBehaviour
{
    private Animator _animator;
    private bool unlock = false;
    public AnimationClip clip;
    // Start is called before the first frame update

    private new void Start()
    {
        
        //base.Start();
        GameEvents.current.onTimeChange += swapTime;

        _animator = GetComponent<Animator>();

        if(clip)
            initAnimationClip();

        if (TimeHandler.Instance.time == true)
        {
            _animator.enabled = true;
        }
        else
        {
            //_animator.enabled = false;
            StartCoroutine("retardedStopAnimator");
        }

    }
    private  IEnumerator retardedStopAnimator()
    {
       yield return new  WaitForSeconds(.1f);
        _animator.enabled = false;


    }
    // Update is called once per frame
    void Update()
    {
        
       
        
    }
    override protected void swapTime()
    {

        if (_animator.enabled)
        {
            _animator.enabled = false;
        }
        else
        {
            _animator.enabled = true;

        }
    }

    void initAnimationClip()
    {
        if (clip)
        {
            AnimatorOverrideController aoc = new AnimatorOverrideController(_animator.runtimeAnimatorController);
            var anims = new List<KeyValuePair<AnimationClip, AnimationClip>>();
            foreach (var a in aoc.animationClips)
                anims.Add(new KeyValuePair<AnimationClip, AnimationClip>(a, clip));
            aoc.ApplyOverrides(anims);
            _animator.runtimeAnimatorController = aoc;
        }
    }
}
