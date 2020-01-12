using UnityEngine;
using UnityEngine.UI;

public class PointCounter : MonoBehaviour
{
    public static int pointCount=0;
    Text text;
    void Start()
    {
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = pointCount.ToString();
    }
}
