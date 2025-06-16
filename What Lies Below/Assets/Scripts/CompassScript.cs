using UnityEngine;

public class CompassScript : MonoBehaviour
{
    public RectTransform compassNeedle;
    public Transform player;
    public Transform target;

    void Update()
    {
        Vector2 direction = target.position - player.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        compassNeedle.localEulerAngles = new Vector3(0, 0, -angle);
    }
}