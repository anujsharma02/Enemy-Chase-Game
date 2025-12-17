using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private float resetX = -20f;
    [SerializeField] private float startX = 20f;

    void Update()
    {
        if (!GameManager.Instance.IsGameStarted || GameManager.Instance.IsgameEnded)
            return;

        transform.Translate(Vector3.left * speed * Time.deltaTime);

        if (transform.position.x <= resetX)
        {
            transform.position = new Vector3(startX, transform.position.y, 0);
        }
    }
}
