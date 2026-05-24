using System.Collections;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float duration = 0.2f;
    [SerializeField] private float intensity = 0.1f;

    private Vector3 offSet;

    public static CameraFollow Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void TriggerShake()
    {
        StartCoroutine(CoroutineShake());
    }

   private  void LateUpdate()
    {
        transform.position = new Vector3(
            playerTransform.position.x,
            playerTransform.position.y,
            transform.position.z
            ) + offSet;
    }

    private IEnumerator CoroutineShake()
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
 