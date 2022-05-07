using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using System.Linq;

public class TimeLineManager : MonoBehaviour
{
    // This is to assign ANOTHER timeline (not just to one on this Game Object)
    // b/c This game object is a trigger - someting you mouse up or gaze @ -
    //  that will take you to a specific "Marker"
    // NOTE: the Marker # is not necessarily sequential... Marker # may have to do with the order in which they are created
    public PlayableDirector playableDirector;
    bool pause = false;
    public static bool isPlaying = false;
    // The # of the Marker you want to go to
    public int markerNum;
    

    void Start()
    {
        // THIS WOULD GRAB THE TIMELINE ON THIS OBJECT but I rather call a MASTER timeline!
        // playableDirector = GetComponent<PlayableDirector>();
        if (!isPlaying)
        {
            playableDirector.enabled = false;
        }

        

    }

    void Update()
    {
        pause = Time.timeScale == 0 ? true : false;
        var timelineAsset = playableDirector.playableAsset as TimelineAsset;
        var markers = timelineAsset.markerTrack.GetMarkers().ToArray();
        //Debug.Log(inputSystem.Skip);
        if(GetComponent<PlayableDirector>().state == PlayState.Playing)
        {
            isPlaying = true;
        }
        if (Input.GetKeyDown(KeyCode.Return) && !pause)
        {
            playableDirector.time = markers[markerNum].time;
        }


    }

    public void sumMarker()
    {
        markerNum++;
    }
}
