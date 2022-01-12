using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Transform> containers;
    private Animator animator1, animator2, animator3, animator;
    void Start()
    {
        // animator1 = containers[0].GetComponent<Animator>(); 
        // animator2 = containers[1].GetComponent<Animator>();
        // animator3 = containers[2].GetComponent<Animator>();
        animator = GetComponent<Animator>();
    }

    public void StartGame()
    {
        foreach (Transform t in containers)
        {
            t.GetComponent<Animator>().enabled = true;
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
        if (index1 == 0 && index2 == 1 || index1 == 1 && index2 == 0)
        {
            animator1.Play("Container anim1");
            animator2.Play("Container anim4");
        }
        else if (index1 == 0 && index2 == 2 || index1 == 2 && index2 == 0)
        {
            animator1.Play("Container anim5");
            animator3.Play("Container anim6");
        }
        else if (index1 == 1 && index2 == 2 || index1 == 2 && index2 == 1)
        {
            animator2.Play("Container anim2");
            animator3.Play("Container anim3");
        }
        print($"{index1} swapped with {index2}");
    }
}
