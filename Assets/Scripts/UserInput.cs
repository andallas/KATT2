using UnityEngine;

public class UserInput : MonoBehaviour
{
    private Menu menu;

    void Start()
    {
        menu = GameObject.Find("~UIManager").GetComponent<Menu>();
    }

	void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(menu.currentPanel.activeSelf)
            {
                GameManager.Instance.Pause();
                menu.CloseAllMenus();
            }
            else
            {
                GameManager.Instance.Pause();
                menu.SwitchMenu("Options Panel");
            }
        }
    }
}