using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Scroll : MonoBehaviour
{
    public float speed = 1f;
    public Vector2 direction = new Vector2(-1, 0);
    public bool isLinkedToCamera = false;
    public bool isLooping = false;
    public bool isRandom = false;

    private List<Transform> backgroundPart;

    void Start()
    {
        if (isLooping)
        {
            backgroundPart = new List<Transform>();
 
            for(int i = 0; i < transform.childCount; i++)
            {
                Transform child = transform.GetChild(i);

                if(child.renderer != null)
                {
                    backgroundPart.Add(child);
                }
            }

            backgroundPart = backgroundPart.OrderBy(t => t.position.x).ToList();
        }
    }

    void Update()
    {
        if (!GameManager.Instance.isPaused)
        {
            Vector2 movement = new Vector2(speed * direction.x, speed * direction.y);
            movement = Vector2.ClampMagnitude(movement, speed);
            movement *= Time.deltaTime;
            transform.Translate(movement);

            if (isLinkedToCamera && GameManager.Instance.levelActive)
            {
                Camera.main.transform.Translate(movement);
            }

            if (isLooping)
            {
                if (isRandom)
                {
                    Transform firstChild = backgroundPart.FirstOrDefault();

                    if (firstChild != null &&
                        firstChild.position.x < Camera.main.transform.position.x &&
                        !firstChild.renderer.IsVisibleFrom(Camera.main))
                    {
                        Vector3 screenSize = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
                        float widthOffset = Random.Range(0, screenSize.x);
                        float heightOffset = Random.Range(0, screenSize.y);

                        firstChild.position = new Vector3(screenSize.x + widthOffset * 10, heightOffset * 2 - screenSize.y, firstChild.position.z);

                        backgroundPart.Remove(firstChild);
                        backgroundPart.Add(firstChild);
                    }
                }
                else
                {
                    Transform firstChild = backgroundPart.FirstOrDefault();

                    if (firstChild != null &&
                        firstChild.position.x < Camera.main.transform.position.x &&
                        !firstChild.renderer.IsVisibleFrom(Camera.main))
                    {
                        Transform lastChild = backgroundPart.LastOrDefault();
                        Vector3 lastPosition = lastChild.transform.position;
                        Vector3 lastSize = (lastChild.renderer.bounds.max - lastChild.renderer.bounds.min);

                        firstChild.position = new Vector3(lastPosition.x + lastSize.x, firstChild.position.y, firstChild.position.z);

                        backgroundPart.Remove(firstChild);
                        backgroundPart.Add(firstChild);
                    }
                }
            }
        }
    }
}