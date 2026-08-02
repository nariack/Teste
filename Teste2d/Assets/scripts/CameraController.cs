using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;

    public float smoothTime = 0.5f;
    public float verticalDeadZone = 3.5f;

    private Vector3 velocity;

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = transform.position;

        // Sempre acompanha o jogador no eixo X
        desiredPosition.x = target.position.x;

        float topLimit = Camera.main.orthographicSize - 10f;

if (target.position.y > transform.position.y + topLimit)
{
    desiredPosition.y = target.position.y - topLimit;
}
else if (target.position.y < transform.position.y - topLimit)
{
    desiredPosition.y = target.position.y + topLimit;
}


        // Mantém a profundidade
        desiredPosition.z = transform.position.z;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref velocity,
            smoothTime
        );
    }
}