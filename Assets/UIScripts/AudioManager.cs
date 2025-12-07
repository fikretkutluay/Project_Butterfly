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
        // Singleton Yapýsý (Sahneler arasý yok olmamasý için)
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Bu obje sahne deðiþince silinmez!

            // Audio Source'u otomatik ekleyelim
            musicSource = gameObject.AddComponent<AudioSource>();
            musicSource.loop = true; // Müzikler hep döngüde olsun
            musicSource.playOnAwake = false;
        }
        else
        {
            Destroy(gameObject); // Eðer 2. bir DJ oluþursa onu yok et
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