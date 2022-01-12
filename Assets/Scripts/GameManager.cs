using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private List<Transform> containers;
    private Animator animator1, animator2, animator3, coinAnimator;
    private int coinIndex = 1;
    private GameObject btnEasy, btnMedium, btnHard;
    private float difficulty = 0.5f;
    private int score;
    void Start()
    {
         animator1 = containers[0].GetComponent<Animator>(); 
         animator2 = containers[1].GetComponent<Animator>();
         animator3 = containers[2].GetComponent<Animator>();
         coinAnimator = GameObject.Find("Coin").GetComponent<Animator>();
         btnEasy = GameObject.Find("Easy");
         btnMedium = GameObject.Find("Medium");
         btnHard = GameObject.Find("Hard");
    }

    public void StartGame()
    {
        foreach (Transform t in containers)
        {
            t.GetComponent<Animator>().enabled = true;
        }
        Destroy(GameObject.Find("Play"));
        Destroy(btnEasy);
        Destroy(btnMedium);
        Destroy(btnHard);
        StartCoroutine(Shuffle());
    }

    public void ResetGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    IEnumerator Shuffle()
    {
        GameObject.Find("NapLeft").GetComponent<Button>().enabled = false;
        GameObject.Find("NapMiddle").GetComponent<Button>().enabled = false;
        GameObject.Find("NapRight").GetComponent<Button>().enabled = false;
        int randomRange = Random.Range(3, 10);
        for (int i = 0; i < randomRange; i++)
        {
            yield return new WaitForSeconds(difficulty);
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
        GameObject.Find("NapLeft").GetComponent<Button>().enabled = true;
        GameObject.Find("NapMiddle").GetComponent<Button>().enabled = true;
        GameObject.Find("NapRight").GetComponent<Button>().enabled = true;
    }

    void Swap(int index1, int index2)
    {
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

    public void SelectDifficultyEasy()
    {
        difficulty = 0.7f;
        var colors = btnMedium.GetComponent<Button> ().colors;
        colors.normalColor = Color.white;
        btnMedium.GetComponent<Button> ().colors = colors;
    }

    public void SelectDifficultyMedium()
    {
        animator1.speed = 1.3f;
        animator2.speed = 1.3f;
        animator3.speed = 1.3f;
        coinAnimator.speed = 1.3f;
    }

    public void SelectDifficultyHard()
    {
        difficulty = 0.3f;
        animator1.speed = 1.5f;
        animator2.speed = 1.5f;
        animator3.speed = 1.5f;
        coinAnimator.speed = 1.5f;
        var colors = btnMedium.GetComponent<Button> ().colors;
        colors.normalColor = Color.white;
        btnMedium.GetComponent<Button> ().colors = colors;
    }

    public void SelectLeftNap()
    {
        ShowNaps();
        if (coinIndex == 0)
        {
            GetComponent<AudioSource>().Play();
            score++;
            GameObject.Find("ScoreCounter").GetComponent<Text>().text = score.ToString();
            StartCoroutine(Wait(1f));
        }
        else
        {
            GetComponents<AudioSource>()[1].Play();
            StartCoroutine(WaitBeforeMenu(1.5f));
        }
    }
    
    public void SelectMiddleNap()
    {
        ShowNaps();
        if (coinIndex == 1)
        {
            GetComponent<AudioSource>().Play();
            score++;
            GameObject.Find("ScoreCounter").GetComponent<Text>().text = score.ToString();
            StartCoroutine(Wait(1f));
        }
        else
        {
            GetComponents<AudioSource>()[1].Play();
            StartCoroutine(WaitBeforeMenu((1.5f)));
        }
    }
    
    public void SelectRightNap()
    {
        ShowNaps();
        if (coinIndex == 2)
        {
            GetComponent<AudioSource>().Play();
            score++;
            GameObject.Find("ScoreCounter").GetComponent<Text>().text = score.ToString();
            StartCoroutine(Wait(1f));
        }
        else
        {
            GetComponents<AudioSource>()[1].Play();
            StartCoroutine(WaitBeforeMenu(1.5f));
        }
    }
    IEnumerator WaitBeforeMenu(float time)
    {
        yield return new WaitForSeconds(time);
        ResetGame();
    }

    IEnumerator Wait(float time)
    {
        yield return new WaitForSeconds(time);
        StartCoroutine(Shuffle());
    }

    private void ShowNaps()
    {
        animator1.Play("Container UpLeft");
        animator2.Play("Container UpMiddle");
        animator3.Play("Container UpRight");
    }
}
