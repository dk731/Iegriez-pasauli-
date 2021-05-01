using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables
{
    public static int gameScore = 0;
    public static Dictionary<string, Sprite> iconSprites = new Dictionary<string, Sprite>();
    public static List<Mesh> randMeshList;
    public static bool activeForClick = false;
    public static List<int> genUniqueNumbers(int min, int max, int count)
    {
        List<int> returnList = new List<int>();
        if (max - min < count)
            return returnList;

        for (int i = 0; i < count; i++)
        {
            int randNum = Random.Range(min, max);
            while (returnList.Contains(randNum))
                randNum = randNum + 1 < max ? randNum + 1 : 0;
            returnList.Add(randNum);
        }

        return returnList;
    }

    public static IEnumerator FocusOn(Transform foc, Transform target, float time)
    {
        yield return new WaitForSeconds(1.0f);

        Vector3 targetDirection = (target.position - foc.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
        float animationStep = 0.01f;
        float animationTime = time;
        float currentAnimationTime = 0.0f;
        float rotationSpeed = Quaternion.Angle(target.rotation, targetRotation) / animationTime * animationStep;

        while (currentAnimationTime <= animationTime)
        {
            Camera.main.transform.rotation = Quaternion.RotateTowards(foc.rotation, targetRotation, rotationSpeed);
            currentAnimationTime += animationStep;
            yield return new WaitForSeconds(animationStep);
        }
    }
}
