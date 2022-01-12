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
        StartCoroutine(Shuffle());
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    IEnumerator Shuffle()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            int randomContainer1 = Random.Range(0, 3);
            int randomContainer2;
            if (randomContainer1 == 0)
            {
                randomContainer2 = Random.Range(1, 3);
                Swap(0, randomContainer2);
                
            }
            else if (randomContainer1 == 2)
            {
                randomContainer2 = Random.Range(0, 2);
                Swap(2, randomContainer2);
            }
            else
            {
                randomContainer2 = Random.Range(0, 2);
                if (randomContainer2 == 0)
                    Swap(1, 0);
                else
                    Swap(1, 2);
            }
            
        }
    }

    void Swap(int index1, int index2)
    {
        (containers[index1], containers[index2]) = (containers[index2], containers[index1]);
        print($"{index1} swapped with {index2}");
    }
}
