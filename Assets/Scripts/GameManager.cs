using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Transform> containers;
    void Start()
    {
        
    }

    public void StartGame()
    {
        foreach (Transform t in containers)
        {
            Vector2 container = t.position;
            t.position = new Vector2(container.x, -1.1f);
        }
        Destroy(GameObject.Find("Play"));
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Gameplay");
    }
}
