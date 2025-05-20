using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Assign your bullet prefab in the Inspector
    public Transform firePoint;     // Assign the fire point (e.g., the tip of the gun)
    public float bulletSpeed = 10f; // Adjust the bullet speed as needed

    void Update()
    {
        // Check if the 'V' key is pressed
        if (Input.GetKeyDown(KeyCode.V))
        {
            ShootAtMouse();
        }
    }

    void ShootAtMouse()
    {
        // Ensure bulletPrefab and firePoint are assigned
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("Missing bulletPrefab or firePoint reference.");
            return;
        }

        // Get the mouse position in world space
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0f; // Set z to 0 for 2D

        // Calculate the direction from the firePoint to the mouse position
        Vector3 direction = (mousePos - firePoint.position).normalized;

        // Instantiate the bullet at the firePoint position
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        // Rotate the bullet to face the direction
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Get the Rigidbody2D component from the bullet
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Set the velocity of the bullet to move it towards the mouse position
            rb.linearVelocity = direction * bulletSpeed;
        }
        else
        {
            Debug.LogWarning("The bullet prefab lacks a Rigidbody2D component.");
        }
    }
}