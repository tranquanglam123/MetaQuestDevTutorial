using Meta.XR.MRUtilityKit;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomotion1 : MonoBehaviour
{
    bool allowSwap = true;
    GameObject mruk;
    GameObject prefabSpawner;
    private void Start()
    {
        mruk = FindObjectOfType<MRUK>().gameObject;
        prefabSpawner = FindObjectOfType<AnchorPrefabSpawner>().gameObject;
    }
    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.X) && allowSwap)
        {
            bool temp = OVRManager.instance.isInsightPassthroughEnabled;
            OVRManager.instance.isInsightPassthroughEnabled = !temp;
            mruk.SetActive(!temp);
            prefabSpawner.SetActive(!temp);
            StartCoroutine(Delay());
        }
    }
    IEnumerator Delay()
    {
        allowSwap = false;
        yield return new WaitForSeconds(1.0f);
        allowSwap  = true;
    }
}
