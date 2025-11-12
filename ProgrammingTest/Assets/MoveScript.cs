using UnityEngine;
using TMPro;  // für TextMeshPro

public class MoveScript : MonoBehaviour
{
    public float speed = 5f;
    public Color hitColor = Color.green;   
    public float colorResetTime = 0.5f;

    public TextMeshProUGUI scoreText;   // UI Text
    private int score = 0;

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime, Space.World);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Gate"))
        {
            // Score erhöhen
            score++;
            UpdateScoreUI();

            // Gate-Farbe ändern
            Renderer rend = collision.gameObject.GetComponent<Renderer>();
            if (rend != null)
            {
                Color originalColor = rend.material.color;
                rend.material.color = hitColor;
                StartCoroutine(ResetColor(rend, originalColor, colorResetTime));
            }
        }
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
    }

    private System.Collections.IEnumerator ResetColor(Renderer rend, Color originalColor, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (rend != null)
            rend.material.color = originalColor;
    }
}
