using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraScript : MonoBehaviour
{
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray clickRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            // Does the ray intersect any objects excluding the player layer
            if (Physics.Raycast(clickRay, out hit))
            {
                Debug.DrawRay(transform.position, (hit.point - transform.position) * hit.distance, Color.yellow);
                OnClickScript clickScript = hit.collider.gameObject.GetComponent<OnClickScript>();
                if (clickScript)
                    clickScript.Clicked();
            }
        }
    }

}
