using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartScript : MonoBehaviour
{
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _restartButton2;
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Start()
    {
        _restartButton.onClick.AddListener(RestartLevel);
        _restartButton2.onClick.AddListener(RestartLevel);
    }
}
