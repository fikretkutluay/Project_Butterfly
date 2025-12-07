using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Her yerden eriþim için

    [Header("Müzik Dosyalarý (WAV/OGG)")]
    public AudioClip findikLoop;      // Oyun baþý - Baba öncesi
    public AudioClip fatherDialogue;  // Baba ile konuþma
    public AudioClip mecnunLoop;      // Mecnun sahnesi
    public AudioClip badEnding;       // Kötü son (Paradoks)

    private AudioSource musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // --- DEÐÝÞEN KISIM BAÞLANGIÇ ---

            // Eskiden: musicSource = gameObject.AddComponent<AudioSource>();

            // Þimdi: Zaten eklediðin kaynaðý bulup kullanýyoruz
            musicSource = GetComponent<AudioSource>();

            // Eðer elle eklemeyi unuttuysan diye yine de garantiye alalým:
            if (musicSource == null)
                musicSource = gameObject.AddComponent<AudioSource>();

            // --- DEÐÝÞEN KISIM BÝTÝÞ ---

            musicSource.loop = true;
            musicSource.playOnAwake = false;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Oyun baþladýðýnda otomatik Fýndýk müziði çalsýn
        PlayMusic(findikLoop);
    }

    // Þarký Deðiþtirme Fonksiyonu
    public void PlayMusic(AudioClip clip)
    {
        // Eðer zaten ayný þarký çalýyorsa baþtan baþlatma
        if (musicSource.clip == clip) return;

        musicSource.Stop();
        musicSource.clip = clip;
        musicSource.Play();
    }
}