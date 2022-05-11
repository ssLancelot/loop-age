using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayMenuScript : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
