using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameHandler gh;
    public TextMeshProUGUI _textCounter;
    public int _swipeCount;
    [SerializeField] GameObject _winPanel;

    private void Awake()
    {
        gh = GameObject.Find("GameHandler").GetComponent<GameHandler>();

    }

    void Start()
    {
        _winPanel.SetActive(false);
        gh.puzzle._width = Random.Range(4, 7);
        gh.puzzle._height = Random.Range(4, 7);
        gh.GeneratePuzzle();

        foreach (var part in GameObject.FindGameObjectsWithTag("Parts"))
        {

            gh.puzzle._parts[(int)part.transform.position.x, (int)part.transform.position.y] = part.GetComponent<Parts>();

        }
        gh.puzzle._winValue = GetWinValue();
        gh.Shuffle();

        gh.puzzle._curValue = gh.Sweep();

        _swipeCount = 0;
        _textCounter.text = _swipeCount.ToString();
    }
    int GetWinValue()
    {
        int winValue = 0;
        foreach (var parts in gh.puzzle._parts)
        {
            foreach (var j in parts.values)
            {
                winValue += j;
            }
        }
        winValue /= 2;

        return winValue;
    }

    public void Win()
    {
        _winPanel.SetActive(true);
    }
    public void StartLevel()
    {
        Destroy(gh);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void BacktoMenu()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}