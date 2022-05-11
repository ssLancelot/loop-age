using UnityEngine;

public class Parts : MonoBehaviour
{
    public int[] values;
    public float speed;
    float realRotation;

    public GameManager gm;

    private void Awake()
    {
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }
    void Update()
    {
        if (transform.rotation.eulerAngles.z != realRotation)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, realRotation), speed);
        }
    }

    void OnMouseDown()
    {
        if (gm.gh.puzzle._curValue == gm.gh.puzzle._winValue)
            return;

        int differance = -gm.gh.QuickSweep((int)transform.position.x, (int)transform.position.y);
        RotateParts();
        differance += gm.gh.QuickSweep((int)transform.position.x, (int)transform.position.y);
        gm.gh.puzzle._curValue += differance;

        if (gm.gh.puzzle._curValue == gm.gh.puzzle._winValue)
            gm.Win();
    }

    public void RotateParts()
    {
        realRotation += 90;
        gm._swipeCount++;
        gm._textCounter.text = gm._swipeCount.ToString();

        if (realRotation == 360)
            realRotation = 0;

        RotateValues();
    }

    public void RotateValues()
    {
        int aux = values[0];

        for (int i = 0; i < values.Length - 1; i++)
        {
            values[i] = values[i + 1];
        }
        values[3] = aux;
    }
}
