using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainWheelScpit : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform sticksHolder;
    public GameObject wheelStick;
    public float wheelOffset = 0.47f;

    public int sticksAmount;

    void Start()
    {
        float angleOffset = Mathf.PI * 2 / sticksAmount;
        for (int i = 0; i < sticksAmount; i++)
            Instantiate(wheelStick, new Vector3(Mathf.Cos(i * angleOffset) * wheelOffset, 0, Mathf.Sin(i * angleOffset) * wheelOffset), Quaternion.Euler(new Vector3(0, -90, -90)), sticksHolder);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
