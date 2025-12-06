using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    [Header("UI Referanslarý")]
    public GameObject pausePanel; // Açýlýp kapanacak olan Panel

    // ESC tuþunu dinlemek için
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; // Zamaný durdur (Mecnun donar)
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; // Zamaný devam ettir
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1f; // Çok önemli! Menüye dönerken zamaný düzeltmelisin.
        SceneManager.LoadScene(0); // 0 numaralý sahne (Ana Menü)
    }
}
