using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition instance;
    Animator sceneTransition;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        sceneTransition = GetComponent<Animator>();
    }

    public IEnumerator TransitionToScene(string nextScene)
    {
        sceneTransition.SetTrigger("Transition");
        yield return new WaitForSeconds(1f);

        if (nextScene == "End Scene")
        {
            Cursor.lockState = CursorLockMode.None;
        }

        SceneManager.LoadScene(nextScene);
    }
}
