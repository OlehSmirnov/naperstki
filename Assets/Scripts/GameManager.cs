using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Transform> containers;
    private Animator animator1, animator2, animator3, coinAnimator;
    private int coinIndex;
    void Start()
    {
         animator1 = containers[0].GetComponent<Animator>(); 
         animator2 = containers[1].GetComponent<Animator>();
         animator3 = containers[2].GetComponent<Animator>();
         coinAnimator = GameObject.Find("Coin").GetComponent<Animator>();
         coinIndex = 1;
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
        for (int i = 0; i < 10; i++)
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
        print("Coin index before swap:" + coinIndex);
        if (index1 == 0 && index2 == 1 || index1 == 1 && index2 == 0)
        {
            animator1.CrossFadeInFixedTime("Container anim1", 0);
            animator2.CrossFadeInFixedTime("Container anim4", 0);
            animator3.Play("Container3 Idle");
            if (coinIndex == 1)
            {
                coinAnimator.CrossFadeInFixedTime("Coin MoveLeft", 0);
                coinIndex = 0;
            }
            else if (coinIndex == 0)
            {
                coinAnimator.CrossFadeInFixedTime("Coin LeftRight", 0);
                coinIndex = 1;
            }
        }
        else if (index1 == 0 && index2 == 2 || index1 == 2 && index2 == 0)
        {
            animator1.CrossFadeInFixedTime("Container anim5", 0);
            animator2.Play("Container2 Idle");
            animator3.CrossFadeInFixedTime("Container anim6", 0);
            if (coinIndex == 2)
            {
                coinAnimator.CrossFadeInFixedTime("Coin MoveLeft2", 0);
                coinIndex = 0;
            }
            else if (coinIndex == 0)
            {
                coinAnimator.CrossFadeInFixedTime("Coin MoveRight2", 0);
                coinIndex = 2;
            }
        }
        else if (index1 == 1 && index2 == 2 || index1 == 2 && index2 == 1)
        {
            animator1.Play("Container1 Idle");
            animator2.CrossFadeInFixedTime("Container anim2", 0);
            animator3.CrossFadeInFixedTime("Container anim3", 0);
            if (coinIndex == 2)
            {
                coinAnimator.CrossFadeInFixedTime("Coin RightLeft", 0);
                coinIndex = 1;
            }
            else if (coinIndex == 1)
            {
                coinAnimator.CrossFadeInFixedTime("Coin MoveRight", 0);
                coinIndex = 2;
            }
        }
    }
}
