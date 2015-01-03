using UnityEngine;

public class UserInput : MonoBehaviour
{
	void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            if(MenuManager.Instance.currentPanel.activeSelf)
            {
                GameManager.Instance.Pause();
                MenuManager.Instance.CloseAllMenus();
            }
            else
            {
                GameManager.Instance.Pause();
                MenuManager.Instance.SwitchMenu("Options Panel");
            }
        }
    }
}