using UnityEngine;

public class CameraSet : MonoBehaviour
{
    [SerializeField] GameHandler _gh;

    void Start()
    {
        this.gameObject.transform.position = new Vector3(_gh.puzzle._width - 1, _gh.puzzle._height, -20) / 2;
    }
}
