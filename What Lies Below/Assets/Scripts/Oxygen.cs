using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OxygenManager : MonoBehaviour
{
    public Image OxygenBar;
    public float oxygenAmount = 100f;
    public float depletionRate = 5f; // Oxygen depletion per second

    void Start()
    {
        UpdateOxygenBar();
    }

    void Update()
    {
        // Deplete oxygen over time
        oxygenAmount -= depletionRate * Time.deltaTime;
        oxygenAmount = Mathf.Clamp(oxygenAmount, 0, 100);
        UpdateOxygenBar();

        // Reload scene if oxygen is depleted
        if (oxygenAmount <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        // Replenish oxygen when the player presses the Space key
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ReplenishOxygen(10);
        }
    }

    public void ReplenishOxygen(float amount)
    {
        oxygenAmount += amount;
        oxygenAmount = Mathf.Clamp(oxygenAmount, 0, 100);
        UpdateOxygenBar();
    }

    void UpdateOxygenBar()
    {
        if (OxygenBar != null)
        {
            OxygenBar.fillAmount = oxygenAmount / 100f;
        }

        else
        {
            Debug.LogWarning("OxygenBar image not assigned in the Inspector.");
        }
    }
}

