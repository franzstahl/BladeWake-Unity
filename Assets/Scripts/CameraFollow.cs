using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   [SerializeField] private Transform playerTransform;


    void LateUpdate()
    {
        transform.position = new Vector3(
            playerTransform.position.x,
            playerTransform.position.y,
            transform.position.z
            );


    }
}
