using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour
{
    Rigidbody rigidBody;
    AudioSource audioSource;


    [SerializeField] float rcsThrust = 200f;
    [SerializeField] float mainThrust = 1200f;
    [SerializeField] float levelLoadDelay = 1.5f;
    [SerializeField] float deadLoadDelay = 1.75f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip death;

    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem deathParticles;

    enum State { Alive, Dying, Transcending}
    State state = State.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; } // Ignore collision when Dead

        switch (collision.gameObject.tag)
        {
            case "Friendly":
                // Friendly do nothing
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartDeathSequence();
                break;
        }
    }

    private void StartSuccessSequence()
    {
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        mainEngineParticles.Stop();
        successParticles.Play();
        Invoke("LoadNextLevel", levelLoadDelay); // Time for Won is 1.5f second
    }

    private void StartDeathSequence()
    {
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(death);
        mainEngineParticles.Stop();
        mainEngineParticles.Play();
        deathParticles.Play();
        Invoke("LoadFirstLevel", deadLoadDelay); // Time for Respawn is 1.75f second
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0; // Loop backto start
        }
        SceneManager.LoadScene(nextSceneIndex);
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0); // TODO: Allow for more than 2 levels
    }

    private void RespondToThrustInput()
    {
        if (Input.GetKey(KeyCode.Space)) // can thrust while rotating
        {
            ApplyThrust();
        }
        else if (Input.GetKey(KeyCode.W))
        {
            ApplyThrust();
        }
        else
        {
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticles.Play();

    }

    private void RespondToRotateInput()
    {
        rigidBody.freezeRotation = true; // Bug fix by turning on and off freezeRotation, here we on
        // Debug.Log("It's turn on");
        float rotationThisFrame = rcsThrust * Time.deltaTime; // Multiplying rocket Thrust Rotation speed by Time.deltaTime

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame); // Rotation left, by using result of Multiplied rocket speed by Time.deltaTime
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame); // Rotation right, by using result of Multiplied rocket speed by Time.deltaTime
        }

        rigidBody.freezeRotation = false; // Bug fix by turning on and off freezeRotation
        // Debug.Log("It's turn off");

    }

}
 