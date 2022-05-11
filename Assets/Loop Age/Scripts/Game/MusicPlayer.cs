using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    static MusicPlayer _instance { get; set; }
    AudioSource _audioSource;

    void Awake()
    {
        AwakePreferance();
    }
    public void Play()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }
    public void StopMusic() => _audioSource.Stop();
    void AwakePreferance()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(transform.gameObject);
            _audioSource = GetComponent<AudioSource>();
            Play();
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
