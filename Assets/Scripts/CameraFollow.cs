using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Takip Ayarlarý")]
    public Transform target; // Takip edilecek karakter (Fýndýk veya Mecnun)
    [Range(0, 1)]
    public float smoothSpeed = 0.125f; // 0.1 = Yumuþak, 1 = Çok Sert
    public Vector3 offset = new Vector3(0, 1, -10); // Kameranýn karakterden ne kadar uzakta duracaðý

    void Start()
    {
        // Eðer Inspector'dan hedef seçmeyi unuttuysan,
        // otomatik olarak "Player" etiketli objeyi bulur.
        if (target == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                target = player.transform;
            }
        }
    }

    void LateUpdate()
    {
        if (target == null) return;

        // Hedef pozisyon (Karakterin yeri + Offset)
        Vector3 desiredPosition = target.position + offset;

        // Lerp fonksiyonu ile yumuþak geçiþ
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        // Kamerayý taþý
        transform.position = smoothedPosition;
    }
}
