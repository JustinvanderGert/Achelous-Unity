using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimelineTriggerBox : MonoBehaviour
{
    [SerializeField] private PlayableDirector director;

    // Update is called once per frame
    void Update()
    {
  

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            director.Play();
        }
    }
    private void Awake()
    {
        director.played += Director_Played;
        director.stopped += Director_Stopped;
    }

    private void Director_Played(PlayableDirector playableDirector)
    {
        // Do something on start of the timeline
    }

    private void Director_Stopped(PlayableDirector playableDirector)
    {
        // Do something at the end of the timeline
    }

}
