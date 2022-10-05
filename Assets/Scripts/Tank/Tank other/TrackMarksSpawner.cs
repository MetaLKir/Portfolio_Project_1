using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackMarksSpawner : MonoBehaviour
{
    [SerializeField] GameObject trackmark;
    List<GameObject> trackmarkPool = new List<GameObject>();
    [SerializeField] int trackmarkPoolSize = 50;

    Vector2 lastPosition;
    float trackmarkStep = 0.2f;

    private void Start()
    {
        lastPosition = transform.position;
        ObjectPooler.sharedInstance.CreatePool(trackmarkPool, trackmark, trackmarkPoolSize);
    }

    private void Update()
    {
        SpawnTrackmarks();
    }

    void SpawnTrackmarks()
    {
        var distancePassed = Vector2.Distance(transform.position, lastPosition);
        if (distancePassed > trackmarkStep)
        {
            lastPosition = transform.position;
            var trackmark = ObjectPooler.sharedInstance.GetPooledObject(trackmarkPool);
            trackmark.SetActive(true);
            trackmark.transform.position = transform.position;
            trackmark.transform.rotation = transform.rotation;
        }
    }

    private void OnDestroy()
    {
        foreach (var trackmark in trackmarkPool)
        {
            // When I stop the game in Unity it gives error that i trying to access to already destroyed object.
            // Probably Unity destroys objects one by one and can't destroys TrackmarksSpawner after Trackmarks, so there is error.
            // null check prevents this error.
            if (trackmark != null) 
            {
                if (!trackmark.activeSelf)
                    Destroy(trackmark);
                else
                {
                    var trackmarkScript = trackmark.GetComponent<Trackmark>();
                    trackmarkScript.isReadyBeDestroyed = true;
                }
            }
        }
    }
}
