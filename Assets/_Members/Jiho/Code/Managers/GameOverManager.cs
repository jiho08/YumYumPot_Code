using System.Collections;
using Code.Core.Events;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.Managers
{
    public class GameOverManager : MonoBehaviour
    {
        [SerializeField] private Image fadeImage;
        [SerializeField] private float fadeDuration = 2f;

        private bool _isGameOver;

        private void Start()
        {
            fadeImage.gameObject.SetActive(false);
            
            Bus<PlayerDeadEvent>.Subscribe(GameOver);
        }

        private void OnDisable()
        {
            Bus<PlayerDeadEvent>.Unsubscribe(GameOver);
        }

        [ContextMenu("GameOver")]
        public void GameOver(PlayerDeadEvent evt)
        {
            if (_isGameOver)
                return;

            fadeImage.gameObject.SetActive(true);
            _isGameOver = true;

            StartCoroutine(FadeAndLoadScene());
        }
        
        private IEnumerator FadeAndLoadScene()
        {
            Color color = fadeImage.color;
            float time = 0f;

            while (time < fadeDuration)
            {
                time += Time.deltaTime;
                color.a = Mathf.Lerp(0f, 1f, time / fadeDuration);
                fadeImage.color = color;
                yield return null;
            }

            color.a = 1f;
            fadeImage.color = color;

            SceneManager.LoadScene(0);
        }
    }
}