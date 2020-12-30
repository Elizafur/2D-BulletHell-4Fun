using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;


public class MusicHandlerLevel01 : MonoBehaviour
{
    [Required]
    public List<AudioSource> song;

    private int currentSong = -1;
    private float songTime;
    
    // Start is called before the first frame update
    void Start()
    {
        songTime = 0f;
    }
    
    // Update is called once per frame
    void Update()
    {
        songTime -= Time.deltaTime;

        if (songTime < 0f)
        {
            ++currentSong;
            if (currentSong >= song.Count)
                currentSong = 0;
            PlayNext();
        }
    }

    void PlayNext()
    {
        AudioSource curSong = song[currentSong];
        songTime = curSong.clip.length;
        curSong.Play();
    }
}
