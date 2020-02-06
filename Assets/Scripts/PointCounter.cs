using UnityEngine;
using UnityEngine.UI;

public class PointCounter : MonoBehaviour
{
    [HideInInspector]
    public int pointCount;
    public Text text;

    void Start()
    {
        pointCount = 0;
    }

    void Update()
    {
        text.text = pointCount.ToString();
    }
}
