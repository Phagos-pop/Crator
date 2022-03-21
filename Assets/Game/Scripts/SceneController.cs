using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public string startPlaceTag;
    [SerializeField]
    private PlayerMove playerPrefab;
    [SerializeField]
    private GameObject panel;

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void InstatiatePlayer()
    {
        if (GameObject.FindGameObjectWithTag(startPlaceTag))
        {
            Instantiate(playerPrefab);
        }
        else
        {
            Debug.Log("There is no Start Platform on scene");
        }
    }

    public void Win()
    {
        panel.SetActive(true);
    }
}
