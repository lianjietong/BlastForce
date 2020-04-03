using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneChanger : MonoBehaviour
{
    public Animator animator;
    private int sceneToLoad;

    // Start is called before the first frame update
    void Start()
    {
        // searches for SceneChanger animator
        GameObject temp = GameObject.Find("SceneChanger");
        if (temp != null)
        {
            // gets Animator
            animator = temp.GetComponent<Animator>();
        }
        else
        {
            Debug.Log("Animator not found");
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            FadeToScene(1);
        }
    }

    public void FadeToNextScene()
    {
        FadeToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToScene(int sceneIndex)
    {
        sceneToLoad = sceneIndex;
        animator.SetTrigger("FadeOut");
        StartCoroutine(WaitAndLoad(3));
    }

    IEnumerator WaitAndLoad(int timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        SceneManager.LoadScene(sceneToLoad);
    }
}
