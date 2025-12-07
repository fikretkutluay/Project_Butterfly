using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Slider'a eriþmek için gerekli

public class MainMenuManager : MonoBehaviour
{
    [Header("Paneller")]
    public GameObject mainMenuPanel; // Ana menü paneli
    public GameObject settingsPanel; // Ayarlar paneli (Inspector'dan sürükle)

    [Header("Ayarlar")]
    public Slider volumeSlider; // Ses slider'ý (Inspector'dan sürükle - opsiyonel)

    public void Start()
    {
        mainMenuPanel.SetActive(true);
        // Oyun açýldýðýnda ses seviyesini slider'ýn mevcut deðeriyle eþleyelim
        if (volumeSlider != null)
        {
            volumeSlider.value = AudioListener.volume;
        }
    }

    // 1. OYNA BUTONU
    public void PlayGame()
    {
        // File -> Build Settings'de 1. sýradaki sahneyi açar
        SceneManager.LoadScene(1);
    }

    // 2. AYARLAR BUTONU (Ana menüdeki buton)
    public void OpenSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    // 3. GERÝ DÖN BUTONU (Ayarlar panelinin içindeki buton)
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
        mainMenuPanel.SetActive(true);
    }

    // 4. SES AYARI (Slider'a baðlanacak)
    // Slider min deðeri 0, max deðeri 1 olmalý
    public void SetVolume(float volume)
    {
        AudioListener.volume = volume; // Global ses seviyesini deðiþtirir
    }

    // 5. ÇIKIÞ BUTONU
    public void QuitGame()
    {
        Debug.Log("Oyundan çýkýlýyor...");
        // Bu kýsým normal oyuncular için (.exe)
        Application.Quit();

        // Bu kýsým sadece SENÝN için (Unity Editör'de çalýþýrken Play modunu durdurur)
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}