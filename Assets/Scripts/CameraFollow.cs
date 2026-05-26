using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private float intensity = 0.27f;

    private Vector3 offSet;

    public static CameraFollow Instance;

    private void Awake()
    {
        Instance = this; // Singleton to allow other scripts trigger the shake effect
    }

    public void TriggerShake()
    {
        StartCoroutine(CoroutineShake());
    }

   private  void LateUpdate() // Ensure the camera follows after the player has moved
    {
        transform.position = new Vector3(
            playerTransform.position.x,
            playerTransform.position.y,
            transform.position.z
            ) + offSet;
    }

    private IEnumerator CoroutineShake() // Handle the shake effect by applying a random offset
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            Vector3 randomOffset = Random.insideUnitCircle * intensity;
            offSet = randomOffset;
            yield return null;
        }

        offSet = Vector3.zero;

    }
 
}
 