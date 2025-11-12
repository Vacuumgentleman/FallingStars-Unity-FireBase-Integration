using UnityEngine;

public class FallingObject : MonoBehaviour
{
    [SerializeField] private float fallSpeed = 5f;
    private const float destroyThresholdY = -10f;

    private void Update()
    {
        MoveDown();
        CheckOutOfBounds();
    }

    private void MoveDown()
    {
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    private void CheckOutOfBounds()
    {
        if (transform.position.y < destroyThresholdY)
        {
            Destroy(gameObject);
        }
    }
}
