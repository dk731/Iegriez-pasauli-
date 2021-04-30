using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    public Camera myCamera;

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        Ray clickRay = myCamera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(clickRay, out hit))
        {
            Debug.DrawRay(transform.position, (hit.point - transform.position) * hit.distance, Color.yellow);
            OnClickScript clickScript = hit.collider.gameObject.GetComponent<OnClickScript>();
            if (clickScript)
            {
                if (Input.GetMouseButtonUp(0))
                    clickScript.Clicked();

                clickScript.OnHover();
            }

        }
    }

}
