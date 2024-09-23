using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHelper
{
    public List<string> GetAnimationStateNames(GameObject go)
    {
        List<string> animationStateList = new List<string>();
        try
        {
            Animation anim = go.GetComponent<Animation>();
            if (anim == null)
            {
                Debug.Log("Asset has no Animation Component.");
            }
            else
            {
                int clipCount = anim.GetClipCount();
                if (clipCount > 0)
                {
                    foreach (AnimationState state in anim)
                    {
                        animationStateList.Add(state.name);
                    }
                }
                else
                {
                    Debug.Log("No animation clip found!");
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        return animationStateList;
    }

    public IEnumerator PlayAssetAnimation(Animation animation, string animationName)
    {
        while (true)
        {
            yield return null;
            if (!animation.isPlaying)
            {
                animation.Play(animationName);
                animation.wrapMode = WrapMode.Loop;
                break;
            }
        }
    }
}
