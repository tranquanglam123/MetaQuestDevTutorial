using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnObjects : MonoBehaviour
{
    
    public UnityEngine.Object[] spawnObjects;
    public float spawnRadius = 10f; // Radius of the spawning area
    public int maxSpawn;

    //List control the spawn
    private List<GameObject> SpawnedObjects = new List<GameObject>();
    //Parent object of the spawned objects
    [SerializeField]
    Transform spawnParent;
    //Track the number of spawns
    private int spawnCount;

    //Handle the Animation
    private AnimationHelper spawnAnimation = new AnimationHelper();

    void Start()
    {
        StartCoroutine(SpawnObjectsToScene());
        spawnCount = 0;
    }
    private void Update()
    {
        // Spawn the objects
        if (OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch))
        {
            SpawnObjectsToScene();
        }
        //Clear the spawned
        if (OVRInput.Get(OVRInput.Button.Two, OVRInput.Controller.RTouch))
        {
            ClearCollection();
        }
        //Play the animation
        if (OVRInput.Get(OVRInput.Button.Start, OVRInput.Controller.LTouch))
        {
            try {
                foreach(var obj in spawnObjects)
                {
                    spawnAnimation.PlayAllAssetAnimations(obj.GetComponent<Animation>());
                }
            }
            catch (MissingReferenceException)
            {
                Debug.Log("Error playing the animations in spawned prefab");
            }

        }
    }

    public IEnumerator SpawnObjectsToScene()
    {
        while (spawnCount <= maxSpawn)
        {
            // Generate random position within the spawn radius
            float randomX = Random.Range(-spawnRadius, spawnRadius);
            float randomZ = Random.Range(-spawnRadius, spawnRadius);

            // Create a random prefab
            GameObject prefab = spawnObjects[Random.Range(0, spawnObjects.Length)] as GameObject;

            // Instantiate the prefab at the random position
            Instantiate(prefab, new Vector3(randomX, 1f, randomZ), Quaternion.identity, spawnParent);
            SpawnedObjects.Add(prefab);
            List<string> animationList = spawnAnimation.GetAnimationStateNames(prefab);
            spawnAnimation.PlayAssetAnimation(prefab.GetComponent<Animation>(), animationList[0]);
            spawnCount++;

            // Wait for a random interval before spawning the next object
            yield return new WaitForSeconds(Random.Range(1f, 3f));
        }
    }
    public void ClearCollection()
    {
        if (SpawnedObjects != null)
        {
            SpawnedObjects.Clear();
        }
        else
        {
            SpawnedObjects = new List<GameObject>();
        }
        foreach (Transform obj in spawnParent.transform)
        {
            GameObject.Destroy(obj.gameObject); 
        }
        Debug.Log("Collection Cleared: " + SpawnedObjects.Count);
    }
}
