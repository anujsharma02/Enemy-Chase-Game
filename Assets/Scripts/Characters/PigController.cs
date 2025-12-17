using UnityEngine;

public class PigController : MonoBehaviour
{
    private Vector3 startPosition;
    private float moveDistance;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void InitMovement(float meetX)
    {
        startPosition = transform.position;
        moveDistance = startPosition.x - meetX;
    }

    public void OnTapMove(int currentTap, int totalTaps)
    {
        float t = (float)currentTap / totalTaps;

        transform.position = new Vector3(
            startPosition.x - moveDistance * t,
            transform.position.y,
            transform.position.z
        );
    }

    // Pig defeated when bird wins
    public void Defeated()
    {
        spriteRenderer.color = Color.gray;
        transform.localScale = new Vector3(1.2f, 0.6f, 1);
        AudioManager.Instance.PlayPigDefeated();
    }

    // Pig escapes when time runs out
    public void Escape()
    {
        transform.localScale = Vector3.one * 1.1f;
        spriteRenderer.color = Color.white;
    }
}
