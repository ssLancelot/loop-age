using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject[] PartsPrefabs;

    [System.Serializable]
    public class Puzzle
    {
        public int _winValue;
        public int _curValue;

        public int _width;
        public int _height;
        public Parts[,] _parts;
    }
    public Puzzle puzzle;
    internal void GeneratePuzzle()
    {
        puzzle._parts = new Parts[puzzle._width, puzzle._height];
        for (int h = 0; h < puzzle._height; h++)
        {
            int[] auxValues = { 0, 0, 0, 0 };
            for (int w = 0; w < puzzle._width; w++)
            {
                if (w == 0)
                    auxValues[3] = 0;
                else
                    auxValues[3] = puzzle._parts[w - 1, h].values[1];

                if (w == puzzle._width - 1)
                    auxValues[1] = 0;
                else
                    auxValues[1] = Random.Range(0, 2);

                if (h == 0)
                    auxValues[2] = 0;
                else
                    auxValues[2] = puzzle._parts[w, h - 1].values[0];

                if (h == puzzle._height - 1)
                    auxValues[0] = 0;
                else
                    auxValues[0] = Random.Range(0, 2);

                int valueSum = auxValues[0] + auxValues[1] + auxValues[2] + auxValues[3];

                if (valueSum == 2 && auxValues[0] != auxValues[2])
                    valueSum = 5;
                GameObject go = Instantiate(PartsPrefabs[valueSum], new Vector3(w, h, 0), Quaternion.identity);

                while (go.GetComponent<Parts>().values[0] != auxValues[0] ||
                      go.GetComponent<Parts>().values[1] != auxValues[1] ||
                      go.GetComponent<Parts>().values[2] != auxValues[2] ||
                      go.GetComponent<Parts>().values[3] != auxValues[3])

                {
                    go.GetComponent<Parts>().RotateParts();
                }
                puzzle._parts[w, h] = go.GetComponent<Parts>();
            }
        }
    }

    public void Shuffle()
    {
        foreach (var parts in puzzle._parts)
        {
            int k = Random.Range(0, 4);
            for (int i = 0; i < k; i++)
                parts.RotateParts();
        }
    }

    public int Sweep()
    {
        int value = 0;

        for (int h = 0; h < puzzle._height; h++)
        {
            for (int w = 0; w < puzzle._width; w++)
            {
                //compares top
                if (h != puzzle._height - 1)
                    if (puzzle._parts[w, h].values[0] == 1 && puzzle._parts[w, h + 1].values[2] == 1)
                        value++;
                //compare right
                if (w != puzzle._width - 1)
                    if (puzzle._parts[w, h].values[1] == 1 && puzzle._parts[w + 1, h].values[3] == 1)
                        value++;
            }
        }

        return value;
    }

    public int QuickSweep(int w, int h)
    {
        int value = 0;
        //compares top
        if (h != puzzle._height - 1)
            if (puzzle._parts[w, h].values[0] == 1 && puzzle._parts[w, h + 1].values[2] == 1)
                value++;
        //compare right
        if (w != puzzle._width - 1)
            if (puzzle._parts[w, h].values[1] == 1 && puzzle._parts[w + 1, h].values[3] == 1)
                value++;

        //compare left
        if (w != 0)
            if (puzzle._parts[w, h].values[3] == 1 && puzzle._parts[w - 1, h].values[1] == 1)
                value++;

        //compare bottom
        if (h != 0)
            if (puzzle._parts[w, h].values[2] == 1 && puzzle._parts[w, h - 1].values[0] == 1)
                value++;

        return value;
    }
}