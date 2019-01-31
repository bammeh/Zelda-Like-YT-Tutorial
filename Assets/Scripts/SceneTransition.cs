using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public Vector2 playerPosition; 
    public VectorValue playerStorage; // position player should spawn at.
    public GameObject fadeInPanel;
    public GameObject fadeOutPanel;
    public float fadeWait; //minimum loading time.

    void Awake() // Gets called before start()
    {
        if (fadeInPanel != null) // Did we assign a fade for this scene?
        {
            GameObject panel = Instantiate(fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);//Kill it after 1 sec.
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            playerStorage.initialValue = playerPosition;
            StartCoroutine(FadeCo());
            //SceneManager.LoadScene(sceneToLoad);
        }
    }

    public IEnumerator FadeCo()
    {
        if (fadeOutPanel != null) // Did we assign a fade for this scene?
        {
            Instantiate(fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(fadeWait);
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneToLoad); //load scene while loading fade panel.
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }
}
