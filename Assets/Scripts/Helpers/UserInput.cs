using UnityEngine;

public class UserInput : MonoBehaviour
{
	void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(MenuManager.Instance.currentPanel.activeSelf)
            {
                MenuManager.Instance.CloseAllMenus();

                if (GameManager.Instance.currentLevel == 1)
                {
                    MenuManager.Instance.SwitchMenu("Main Panel");
                }
            }
            else
            {
                GameManager.Instance.Pause();
                MenuManager.Instance.SwitchMenu("Options Panel");
            }
        }
    }
}