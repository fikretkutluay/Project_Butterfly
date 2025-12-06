using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro için
using UnityEngine.SceneManagement;
using System.Collections;

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager Instance;

    [Header("UI Referanslarý")]
    public GameObject choicePanel;      // Seçenek butonlarýnýn olduðu panel
    public Button optionButton1;        // Üstteki buton
    public Button optionButton2;        // Alttaki buton
    public TextMeshProUGUI subtitleText; // Ekranýn altýndaki altyazý yazýsý

    [Header("Diyalog Ayarlarý")]
    public float subtitleSpeed = 2f;    // Her cümlenin ekranda kalma süresi

    private void Awake()
    {
        if (Instance == null) Instance = this;

        // Baþlangýçta UI kapalý olsun
        if (choicePanel) choicePanel.SetActive(false);
        if (subtitleText) subtitleText.text = "";
    }

    // =================================================================
    // 1. BÖLÜM: FINDIK OKUL SEÇÝMÝ (Oyunun Baþý)
    // =================================================================

    public void TriggerFindikChoice()
    {
        choicePanel.SetActive(true);
        Time.timeScale = 0f; // Oyunu dondur

        // Buton 1: Okula Gir (HATA -> BAÞA DÖNER)
        SetupButton(optionButton1, "Okula Gir", () => {
            Time.timeScale = 1f;
            Debug.Log("Okula girildi -> Döngü baþa sarýyor...");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Sahneyi resetle
        });

        // Buton 2: Okula Girme / Ormandan Git (DOÐRU YOL)
        SetupButton(optionButton2, "Orman Yolundan Git", () => {
            Time.timeScale = 1f;
            choicePanel.SetActive(false);
            // Burada oyuncunun hareketini açabilirsin
        });
    }

    // =================================================================
    // 2. BÖLÜM: BABA ÝLE KARÞILAÞMA (Altyazý Sistemi)
    // =================================================================

    public void StartFatherDialogue()
    {
        // Baba ile karþýlaþýnca hareket dursun mu? Ýstersen burayý aç:
        // Time.timeScale = 0f; 

        string[] speeches = new string[] {
            "Mecnun: Fýndýk? Senin burada ne iþin var?",
            "Fýndýk: Baba... Sadece biraz deðiþiklik yapmak istedim.",
            "Mecnun: Biliyor musun... Ben de gençken bu yoldan gitmiþtim.",
            "Mecnun: Ve o gün hayatým deðiþmiþti...",
            "Mecnun: Gel sana hikayeyi anlatayým."
        };

        StartCoroutine(PlaySubtitles(speeches, true)); // true = Sonunda sahne deðiþsin
    }

    // =================================================================
    // 3. BÖLÜM: MECNUN SEÇÝMÝ (Geçmiþ Zaman)
    // =================================================================

    public void TriggerMecnunChoice()
    {
        choicePanel.SetActive(true);
        Time.timeScale = 0f;

        // Buton 1: Eve Git (PARADOKS -> KIZ YOK OLUR)
        SetupButton(optionButton1, "Doðrudan Eve Git", () => {
            Time.timeScale = 1f;
            StartCoroutine(TriggerParadoxEnding());
        });

        // Buton 2: Orman Yolundan Git (DOÐRU YOL -> LEYLA ÝLE TANIÞ)
        SetupButton(optionButton2, "Orman Yolundan Git", () => {
            Time.timeScale = 1f;
            choicePanel.SetActive(false);
            // Mecnun ormana girer, oyun devam eder...
        });
    }

    // =================================================================
    // YARDIMCI FONKSÝYONLAR
    // =================================================================

    // Altyazýlarý sýrayla oynatan sistem
    IEnumerator PlaySubtitles(string[] lines, bool loadNextSceneAtEnd)
    {
        choicePanel.SetActive(false); // Seçim ekraný varsa kapat

        foreach (string line in lines)
        {
            subtitleText.text = line;
            yield return new WaitForSeconds(subtitleSpeed); // Okuma süresi
        }

        subtitleText.text = ""; // Temizle

        if (loadNextSceneAtEnd)
        {
            // Buraya Mecnun sahnesinin numarasýný yaz (Örn: 2)
            SceneManager.LoadScene(2);
        }
    }

    // Kötü Son (Paradox) Senaryosu
    IEnumerator TriggerParadoxEnding()
    {
        // Ekraný karart veya dramatik bir müzik çal
        subtitleText.text = "Mecnun o gün eve gitti...";
        yield return new WaitForSeconds(3f);

        subtitleText.text = "Leyla ile hiç tanýþmadý...";
        yield return new WaitForSeconds(3f);

        subtitleText.text = "Ve Fýndýk hiç doðmadý.";
        yield return new WaitForSeconds(3f);

        // Fýndýk sahnesine (Mezarlýk kýsmýna) geri dönüp kýzýn olmadýðýný göstermek istersen:
        // SceneManager.LoadScene(1); 
        // Veya direkt Main Menu:
        SceneManager.LoadScene(0);
    }

    // Buton ayarlayýcý (Kod tekrarýný önlemek için)
    void SetupButton(Button btn, string text, UnityEngine.Events.UnityAction action)
    {
        btn.GetComponentInChildren<TextMeshProUGUI>().text = text;
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(action);
    }
}