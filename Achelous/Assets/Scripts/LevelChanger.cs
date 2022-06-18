using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    public Animator animator;

    public int levelToLoad;

    // Update is called once per frame
    // void Update()
    // {
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         FadeToNextLevel();
    //     }
    // }

    void OnTriggerEnter(Collider target)
    {
        if(target.tag == "Player")
            {
                FadeToNextLevel();
            }
    }


    public void FadeToNextLevel () 
    {
         FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToLevel (int levelIndex) 
    {
        animator.SetTrigger("FadeOut");
    }

    public void OnFadeComplete ()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}

