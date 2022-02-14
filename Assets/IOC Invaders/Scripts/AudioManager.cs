using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip shootingClip;
    [SerializeField] [Range(0f, 1f)] private float shootingVolume = 1f;

    [SerializeField] private AudioClip damageClip;
    [SerializeField] [Range(0f, 1f)] private float damageVolume = 1f;
    
    [SerializeField] private AudioClip hoverClip;
    [SerializeField] [Range(0f, 1f)] private float hoverVolume = 1f;
    
    [SerializeField] private AudioClip selectClip;
    [SerializeField] [Range(0f, 1f)] private float selectVolume = 1f;

    private static AudioManager _instance;

    private AudioSource _audioSource;

    public static AudioManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            _audioSource = GetComponent<AudioSource>();
            DontDestroyOnLoad(gameObject);
        }
    }


    public void PlayTrack(AudioClip clip, float volume = 1f)
    {
        if (_audioSource.clip != clip)
        {
            _audioSource.clip = clip;
            _audioSource.Play();
        }

        _audioSource.volume = volume;
    }

    public void StopTrack()
    {
        _audioSource.Stop();
        _audioSource.clip = null;
    }

    public void PlayHoverClip()
    {
        PlayClip(hoverClip, hoverVolume);
    }
    
    public void PlaySelectClip()
    {
        PlayClip(selectClip, selectVolume);
    }
    
    public void PlayShootingClip()
    {
        PlayClip(shootingClip, shootingVolume);
    }

    public void PlayDamageClip()
    {
        PlayClip(damageClip, damageVolume);
    }

    public void PlayClip(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
        {
            Vector3 cameraPos = Camera.main.transform.position;
            AudioSource.PlayClipAtPoint(clip, cameraPos, volume);
        }
    }
}