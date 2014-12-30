using UnityEngine;

public class UserInput : MonoBehaviour
{
    public GameObject menuObject;
    private Menu menu;

    void Start()
    {
        menu = menuObject.GetComponent<Menu>();
    }

	void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(menu.GetCurrentPanel().activeSelf)
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