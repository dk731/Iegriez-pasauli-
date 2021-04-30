using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class EarthScript : MonoBehaviour
{
    public float sensivity;

    public MapQuestionHolder myQuestions;

    private bool active = true;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (active && Input.GetAxis("Fire1") != 0)
        {
            transform.Rotate(Camera.main.transform.up, -Input.GetAxis("Mouse X") * sensivity * Time.deltaTime, Space.World);
            transform.Rotate(-Camera.main.transform.right, -Input.GetAxis("Mouse Y") * sensivity * Time.deltaTime, Space.World);
        }
    }

    public IEnumerator StartEarth()
    {
        yield return new WaitForSeconds(0.5f);

        foreach (FieldInfo fi in myQuestions.GetType().GetFields())
            foreach (MapQuestion mq in (List<MapQuestion>)fi.GetValue(myQuestions))
                transform.Find(mq.locationName).GetComponent<MapLocationScript>().SetupQuestion(mq, GlobalVariables.iconSprites[fi.Name]);

        Vector3 targetDirection = (transform.position - Camera.main.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        float animationStep = 0.01f;
        float animationTime = 1.0f;
        float currentAnimationTime = 0.0f;
        float rotationSpeed = Quaternion.Angle(transform.rotation, targetRotation) / animationTime * animationStep;

        while (currentAnimationTime <= animationTime)
        {
            Camera.main.transform.rotation = Quaternion.RotateTowards(Camera.main.transform.rotation, targetRotation, rotationSpeed);
            currentAnimationTime += animationStep;
            yield return new WaitForSeconds(animationStep);
        }

        Debug.Log("Start Earth");

        active = true;

    }
        
}
