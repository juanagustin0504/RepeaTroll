using UnityEngine;
using System.Collections;

public class CameraCtrl : MonoBehaviour
{
    public GameObject A;
    Transform AT;

    private float xDistance, yDistance;

    void Start()
    {
        Vector2 distance = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));
        xDistance = distance.x;
        yDistance = distance.y;

        AT = A.transform;
    }
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, AT.position, 2f * Time.deltaTime);
        transform.Translate(0, 0, -10); //카메라를 원래 z축으로 이동

        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, -28f + xDistance, 28f - xDistance),
            Mathf.Clamp(transform.position.y, -16f + yDistance, 16f - yDistance),
            -10);
    }
}