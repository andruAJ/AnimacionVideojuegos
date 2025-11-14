using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioSource audioAtmosphere;
    [SerializeField] private AudioSource audioSfx;

    [SerializeField] private AudioClip[] arrayAtmosphere;

    [SerializeField] public AudioClip gameOver;
    [SerializeField] public AudioClip sword;
    [SerializeField] public AudioClip collectPowrUp;
    private int currentClipIndex = default;

    private void Awake() {
        if (Instance != null) {
            Debug.LogError("There are many Audio Managers");
            Destroy(this);
            return;
        }
        Instance = this;
    }
    private void Start() {
        StartCoroutine(PlayMusic());
    }

    IEnumerator PlayMusic() {
        while (true) {
            audioAtmosphere.clip = arrayAtmosphere[currentClipIndex];
            audioAtmosphere.Play();
            yield return new WaitForSeconds(audioAtmosphere.clip.length);
            currentClipIndex = (currentClipIndex + 1) % arrayAtmosphere.Length;
        }
    }
    public void PlaySFX(AudioClip clip) {
        audioSfx.clip = clip;
        audioSfx.Play();
    }
}
