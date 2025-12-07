using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager Instance;

    [Header("UI Referanslarý")]
    public GameObject choicePanel;       // Seçim Butonlarý Paneli
    public Button optionButton1;         // Üst Buton
    public Button optionButton2;         // Alt Buton
    public TextMeshProUGUI subtitleText; // Altyazý Text'i

    [Header("Ayarlar")]
    public float subtitleSpeed = 2.5f;   // Yazýlarýn ekranda kalma süresi (2.5 saniye ideal)

    private void Awake()
    {
        if (Instance == null) Instance = this;

        if (choicePanel) choicePanel.SetActive(false);
        if (subtitleText) subtitleText.text = "";
    }

    // =================================================================
    // 1. OLAY: FINDIK OKUL SEÇÝMÝ (Fýndýk Okula Gelince)
    // =================================================================
    public void TriggerFindikChoice()
    {
        choicePanel.SetActive(true);
        Time.timeScale = 0f; // Oyunu durdur

        // Seçenek 1: Okula Gir (Yanlýþ Yol)
        SetupButton(optionButton1, "Okula Gir", () => {
            Time.timeScale = 1f;
            Debug.Log("Okula girildi -> Döngü baþa sarýyor...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Reset
        });

        // Seçenek 2: Ormana Git (Doðru Yol)
        SetupButton(optionButton2, "Orman Yolundan Git", () => {
            Time.timeScale = 1f;
            choicePanel.SetActive(false);
            // Panel kapanýr, Fýndýk yürümeye devam eder
        });
    }

    // =================================================================
    // 2. OLAY: BABA ÝLE KONUÞMA (Fýndýk Babayý Görünce)
    // =================================================================
    public void StartFatherDialogue()
    {
        // Konuþma metinlerini BURAYA yazýyorsun (Güvenli ve Sabit)
        string[] speeches = new string[] {
            "Mecnun: Fýndýk? Senin burada ne iþin var?",
            "Fýndýk: Baba... Sadece biraz deðiþiklik yapmak istedim.",
            "Mecnun: Biliyor musun... Ben de gençken bu yoldan gitmiþtim.",
            "Mecnun: Ve o gün hayatým deðiþmiþti...",
            "Mecnun: Gel sana hikayeyi anlatayým."
        };

        // Ýkinci parametre 'true' olduðu için konuþma bitince Mecnun sahnesini açar
        StartCoroutine(PlaySubtitles(speeches, true));
    }

    // =================================================================
    // 3. OLAY: MECNUN SEÇÝMÝ (Mecnun Okuldan Çýkýnca)
    // =================================================================
    public void TriggerMecnunChoice()
    {
        choicePanel.SetActive(true);
        Time.timeScale = 0f;

        // Seçenek 1: Eve Git (Yanlýþ Yol - Paradoks)
        SetupButton(optionButton1, "Doðrudan Eve Git", () => {
            Time.timeScale = 1f;
            StartCoroutine(TriggerParadoxEnding());
        });

        // Seçenek 2: Ormana Git (Doðru Yol - Leyla'ya Koþ)
        SetupButton(optionButton2, "Orman Yolundan Git", () => {
            Time.timeScale = 1f;
            choicePanel.SetActive(false);
            // Mecnun ormana dalar, oyun devam eder
        });
    }

    // =================================================================
    // ARKA PLAN SÝSTEMLERÝ (Dokunmana Gerek Yok)
    // =================================================================

    IEnumerator PlaySubtitles(string[] lines, bool loadNextScene)
    {
        choicePanel.SetActive(false);

        foreach (string line in lines)
        {
            subtitleText.text = line;
            yield return new WaitForSeconds(subtitleSpeed);
        }

        subtitleText.text = "";

        if (loadNextScene)
        {
            // Build Settings'de Mecnun Sahnesi kaç numaraysa onu yaz (Örn: 2)
            SceneManager.LoadScene(2);
        }
    }

    IEnumerator TriggerParadoxEnding()
    {
        subtitleText.text = "Mecnun o gün eve gitti...";
        yield return new WaitForSeconds(3f);

        subtitleText.text = "Leyla ile hiç tanýþmadý...";
        yield return new WaitForSeconds(3f);

        subtitleText.text = "Ve Fýndýk hiç doðmadý.";
        yield return new WaitForSeconds(3f);

        // outroya dön
        SceneManager.LoadScene(3);
    }

    void SetupButton(Button btn, string text, UnityEngine.Events.UnityAction action)
    {
        btn.GetComponentInChildren<TextMeshProUGUI>().text = text;
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(action);
    }
}