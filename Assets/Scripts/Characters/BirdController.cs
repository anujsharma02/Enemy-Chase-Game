using UnityEngine;
using System.Collections;

public class BirdController : MonoBehaviour
{

    [Header("Idle Animation (On Pig Head)")]
[SerializeField] private float idleBobHeight = 0.05f;
[SerializeField] private float idleBobSpeed = 2f;
[SerializeField] private float idleScaleAmount = 0.05f;
[SerializeField] private float idleScaleSpeed = 2f;

private bool isIdleOnPig = false;
private Vector3 idleBaseLocalPos;
private Vector3 idleBaseScale;



    [Header("Hop Settings")]
    [SerializeField] private float hopHeight = 0.4f;
    [SerializeField] private float hopSpeed = 8f;

    private Vector3 startPosition;
    private float moveDistance;

    private bool movementLocked;
    private Coroutine hopRoutine;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        startPosition = transform.position;
    }
private void Update()
{
    if (!isIdleOnPig)
        return;

    // Vertical bob
    float bobY = Mathf.Sin(Time.time * idleBobSpeed) * idleBobHeight;
    transform.localPosition = idleBaseLocalPos + Vector3.up * bobY;

    // Gentle breathing scale
    float scale = 1 + Mathf.Sin(Time.time * idleScaleSpeed) * idleScaleAmount;
    transform.localScale = idleBaseScale * scale;
}

    // Called once when game starts
    public void InitMovement(float meetX)
    {
        startPosition = transform.position;
        moveDistance = meetX - startPosition.x;
        movementLocked = false;
    }

    // Called on every tap
    public void OnTapMove(int currentTap, int totalTaps)
    {
        if (movementLocked) return;

        float t = (float)currentTap / totalTaps;

        transform.position = new Vector3(
            startPosition.x + moveDistance * t,
            startPosition.y,
            transform.position.z
        );

        if (hopRoutine != null)
            StopCoroutine(hopRoutine);

        hopRoutine = StartCoroutine(Hop());
        AudioManager.Instance.PlayBirdHop();
    }

    private IEnumerator Hop()
    {
        Vector3 start = transform.position;
        Vector3 up = start + Vector3.up * hopHeight;

        yield return Move(start, up);
        yield return Move(up, start);
    }

    private IEnumerator Move(Vector3 from, Vector3 to)
    {
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * hopSpeed;
            transform.position = Vector3.Lerp(from, to, t);
            yield return null;
        }
    }

    // Called when bird wins
    public void JumpOnPig(Transform pig)
    {
        movementLocked = true;
        StopAllCoroutines();

        transform.SetParent(null);

        SpriteRenderer pigSR = pig.GetComponent<SpriteRenderer>();

        float pigTopY = pigSR.bounds.max.y;
        float birdHalfY = spriteRenderer.bounds.extents.y;

        transform.position = new Vector3(
            pig.position.x,
            pigTopY + birdHalfY,
            transform.position.z
        );

        transform.SetParent(pig, true);
        // Start idle animation
idleBaseLocalPos = transform.localPosition;
idleBaseScale = transform.localScale;
isIdleOnPig = true;

    }
}
