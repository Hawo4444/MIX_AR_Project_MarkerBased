using UnityEngine.UI;
using UnityEngine;

public class PlayerPickup : MonoBehaviour
{
    [SerializeField] private GameObject[] SpawnLocations;
    [SerializeField] private GameObject lapCounterText;

    private AudioSource collectSound;
    //private Rigidbody rb;
    private int currentIndex = 0;
    private int lapCounter = 0;
 
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        this.MoveToLocation(0);
        collectSound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0f, 0f, 100f) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            collectSound.Play();
            this.MoveToLocation(currentIndex + 1);
        }
    }

    private void MoveToLocation(int index)
    {
        if (index < 0 || index >= SpawnLocations.Length)
        {
            lapCounter++;
            currentIndex = 0;
            lapCounterText.GetComponent<Text>().text = "Laps: " + lapCounter;
            return;
        }

        currentIndex = index;
        this.transform.position = SpawnLocations[index].transform.position;
    }

    public void ResetLaps()
    {
        lapCounter = 0;
    }
}
