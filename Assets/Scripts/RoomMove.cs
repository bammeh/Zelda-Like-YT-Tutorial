using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMove : MonoBehaviour
{
    public Vector2 cameraChange; // how much to shift camera
    public Vector3 playerChange; // how much will i shift player
    private CameraMovement cam;
    public bool needText;
    public string placeName;
    public GameObject text;
    public Text placeText;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger) // doesnt do double trigger
        {
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            other.transform.position += playerChange;
            if (needText)
            {
                StartCoroutine(placeNameCo());
            }
        }

    }

    private IEnumerator placeNameCo()
    {
        text.SetActive(true); // set place name object active
        placeText.text = placeName; // Set text on object to place name
        yield return new WaitForSeconds(4); // Wait 4 seconds
        text.SetActive(false); //set place name object inactive.
    }
}
