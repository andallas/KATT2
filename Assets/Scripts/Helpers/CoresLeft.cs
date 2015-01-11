using UnityEngine;
using UnityEngine.UI;

public class CoresLeft : MonoBehaviour
{
    private int coresLeft;
    private Text coresText;

    void Start()
    {
        coresLeft = GameManager.Instance.cores;
        coresText = gameObject.GetComponent<Text>();
        coresText.text = coresLeft.ToString();
    }

    void Update()
    {
        if (coresLeft != GameManager.Instance.cores)
        {
            coresLeft = GameManager.Instance.cores;
            coresText.text = coresLeft.ToString();
        }
    }
}