using Oculus.Interaction;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationHelper
{
    /// <summary>
    /// Return the list of all clips inside animation component 
    /// </summary>
    /// <param name="go">The gameobject contains animation component</param>
    /// <returns></returns>
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

    /// <summary>
    /// Play a specific clip in the animation
    /// </summary>
    /// <param name="animation">Animation component</param>
    /// <param name="animationName">The name of the specific clip </param>
    /// <returns>Play animation</returns>
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
    /// <summary>
    /// Play all clips inside the animation component
    /// </summary>
    /// <param name="animation"></param>
    /// <returns></returns>
    public IEnumerator PlayAllAssetAnimations(Animation animation)
    {
        // Get a list of animation clip names
        List<string> animationClipNames = GetAnimationStateNames(animation.gameObject);

        // Iterate through each animation clip name
        foreach (string clipName in animationClipNames)
        {
            // Play the current animation
            animation.Play(clipName);
            animation.wrapMode = WrapMode.Loop; // Loop the animation if desired

            // Wait for the animation to finish (optional)
            yield return new WaitForSeconds(animation.GetClip(clipName).length);
        }

        // Optionally, handle finishing all animations (e.g., stop the coroutine)
        Debug.Log("All animations played!");
    }

}
