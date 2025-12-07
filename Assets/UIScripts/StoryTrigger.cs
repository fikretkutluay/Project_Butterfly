using UnityEngine;
using UnityEngine.Events; // Bunu eklemeyi unutma!

public class StoryTrigger : MonoBehaviour
{
    [Header("Ne Çalýþsýn?")]
    public UnityEvent onTriggerEnter; // Inspector'da fonksiyon seçmeni saðlayan sihirli satýr

    private bool hasTriggered = false; // Sadece bir kere çalýþsýn diye

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Kutunun içine giren þey "Player" etiketli mi?
        // Ve daha önce bu kutu çalýþtý mý?
        if (other.CompareTag("Player") && !hasTriggered)
        {
            hasTriggered = true; // Artýk çalýþtý olarak iþaretle
            onTriggerEnter.Invoke(); // Inspector'da seçtiðin görevi yap!
        }
    }
}