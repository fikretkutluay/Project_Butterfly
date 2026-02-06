using UnityEngine;
using UnityEngine.SceneManagement;

public class OutroManager : MonoBehaviour
{
    [Header("Sahne Ayarlarý")]
    public int mecnunSceneIndex = 2; // Mecnun'un sahnesinin numarasý (Build Settings'ten bak)
    public int mainMenuIndex = 0;    // Ana menü numarasý

    // "Tekrar Dene" Butonu Ýçin
    public void TryAgain()
    {
        // Zamaný normalleþtirelim (Eðer durduysa)
        Time.timeScale = 1f;

        // Müziði Mecnun'un sahne müziðine geri döndürelim
        if (AudioManager.Instance)
        {
            AudioManager.Instance.PlayMusic(AudioManager.Instance.mecnunLoop);
        }

        // Mecnun sahnesini baþtan yükle
        SceneManager.LoadScene(mecnunSceneIndex);
    }

    // "Ana Menü" Butonu Ýçin
    public void ReturnToMenu()
    {
        Time.timeScale = 1f;

        // Ana menü müziðine (Fýndýk Loop) dönelim
        if (AudioManager.Instance)
        {
            AudioManager.Instance.PlayMusic(AudioManager.Instance.findikLoop);
        }

        SceneManager.LoadScene(mainMenuIndex);
    }
}