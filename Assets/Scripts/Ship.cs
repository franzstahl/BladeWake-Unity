using System.Collections;
using TMPro;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ship : MonoBehaviour
{
    [SerializeField] private TMP_Text message;
    [SerializeField] private CanvasGroup fadePanel;
    private bool _playerNearby;
    private float _fadeDuration = 5f;
    private bool _isLoading = false;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerNearby)
        {

            StartCoroutine(FadeAndLoadWin());

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            message.gameObject.SetActive(true);
            _playerNearby = true;

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isLoading) return;
        if (collision.CompareTag("Player"))
        {
            message.gameObject.SetActive(false);
            _playerNearby = false;
        }
    }

    private IEnumerator FadeAndLoadWin() // Coroutine to fade in the screen and then load the win scene
    {
        _isLoading = true;
        while (fadePanel.alpha < 1f)
        {
            fadePanel.alpha += Time.deltaTime / _fadeDuration; // Increment alpha based on time and fade duration
            yield return null;
        }

        fadePanel.alpha = 1f; // Ensure alpha is fully opaque at the end of the fade
        SceneManager.LoadScene("Win");

    }
}
