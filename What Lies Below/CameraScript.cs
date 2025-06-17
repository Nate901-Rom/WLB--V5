using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.BoolParameter;

public class CameraScript : MonoBehaviour

{
    public Sprite[] photos;
    public Image photoDisplay;
    public GameObject flashPanel;
    public AudioSource cameraSound;
    public float displayTime; short displayDuration;
    public float showTime = 2f;
    private float timer = 0f;
    private int currentPhoto = 0;
    private bool photoActive = false;
    private bool isNearObject = false;


    public void TakeFakePhoto()
    {
        if (!photoActive && isNearObject)
        {
            if (cameraSound) cameraSound.Play();

            if (flashPanel) flashPanel.SetActive(true);

            photoDisplay.sprite = photos[currentPhoto];
            photoDisplay.color = new Color(1, 1, 1, 1);

            photoActive = true;
            timer = showTime;

            currentPhoto = (currentPhoto + 1) % photos.Length;
        }
    }

    void Update()
    {
        if (photoActive)
        {
            timer -= Time.deltaTime;

            if (timer < showTime - 0.1f && flashPanel.activeSelf)
                flashPanel.SetActive(false);

            if (timer <= 0f)
            {
                photoDisplay.color = new Color(1, 1, 1, 0);
                photoActive = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PhotoTarget"))
            isNearObject = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PhotoTarget"))
            isNearObject = false;
    }
    void displayImage()
    {
        Destroy(gameObject, displayTime);
    }
    }