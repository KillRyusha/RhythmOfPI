using UnityEngine;

public class CurrentSongInfoSingleton : MonoBehaviour
{
    private const int MAX_SONG_LENGTH = 60;
    private const float MOVE_SPEED_MULTIPLY = 0.85f;

    public static CurrentSongInfoSingleton Instance { get; private set; }

    public string SongTitle { get; private set; }
    public int SongDuration { get; private set; }
    public int NotesAmount { get; private set; }
    public string SongTags { get; private set; }  
    public int Level { get; private set; }  
    public AudioClip SongAudioClip { get; private set; }
    public Sprite SongSptite { get; set; }


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetCurrentSong(string title, int duration, string tags, int level, AudioClip audioClip)
    {
        SongTitle = title;
        SongDuration = duration > MAX_SONG_LENGTH ? MAX_SONG_LENGTH : duration;
        NotesAmount = (int)(SongDuration * MOVE_SPEED_MULTIPLY);
        SongTags = tags;
        Level = level;
        SongAudioClip = audioClip;
    }

    public void ClearCurrentSong()
    {
        SongTitle = string.Empty;
        SongDuration = 0;
        SongTags = string.Empty;
        SongAudioClip = null;
    }
}
