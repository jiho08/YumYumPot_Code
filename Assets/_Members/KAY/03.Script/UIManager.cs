using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject SettingUi;
    [SerializeField] private string NextSceneName;
    
    public void OnOff()
    {
        if (SettingUi.activeSelf)
        {
            SettingUi.SetActive(false);
            Time.timeScale = 1;
        }

        else
        {
            SettingUi.SetActive(true);  
            Time.timeScale = 0; 
        }
        
    }

    private void Start()
    {
        SettingUi.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void GameStart()
    {
        SceneManager.LoadScene(NextSceneName);  
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnOff();
        }
    }
}
