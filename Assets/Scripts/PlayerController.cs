using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        rb.AddForce(0, 0, 2000 * Time.deltaTime);
        score = 0;
        health = 5;
        scene = SceneManager.GetActiveScene();
        SetScoreText();
    }
    void Update()
    {
        if (health == 0)
        {
            //Debug.Log("Game Over!");
            WinLoseImage.gameObject.SetActive(true);
            WinLoseText.text = "Game Over!";
            WinLoseText.color = Color.white;
            WinLoseImage.color = Color.red;
            StartCoroutine(LoadScene(3));
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SceneManager.LoadScene("menu");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (speed <= 0)
        {
            if (Input.GetKey("w"))
                rb.AddForce(0, 0, 30);
            if (Input.GetKey("s"))
                rb.AddForce(0, 0, -30);
            if (Input.GetKey("a"))
                rb.AddForce(-30, 0, 0);
            if (Input.GetKey("d"))
                rb.AddForce(30, 0, 0);
        }
        else
        {
            if (Input.GetKey("w"))
                rb.AddForce(0, 0, 30 * speed);
            if (Input.GetKey("s"))
                rb.AddForce(0, 0, -30 * speed);
            if (Input.GetKey("a"))
                rb.AddForce(-30 * speed, 0, 0);
            if (Input.GetKey("d"))
                rb.AddForce(30 * speed, 0, 0);
        }

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Pickup")
        {
            other.gameObject.SetActive(false);
            score = score + 1;
            SetScoreText();
            //Debug.Log("Score: " + score);
        }
        if (other.gameObject.tag == "Trap")
        {
            health -= 1;
            SetHealthText();
            //Debug.Log("Health:" + health);
        }
        if (other.gameObject.tag == "Goal")
        {
            WinLoseImage.gameObject.SetActive(true);
            WinLoseText.text = "You Win!";
            WinLoseText.color = Color.black;
            WinLoseImage.color = Color.green;
            StartCoroutine(LoadScene(3));
        }
    }

    void SetScoreText()
    {
        scoreText.text = $"Score: {score}";
    }
    void SetHealthText()
    {
        healthText.text = $"Health: {health}";
    }
    IEnumerator LoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene(scene.name, LoadSceneMode.Single);
    }

    public Rigidbody rb;
    public float speed;
    private int score;
    public int health;
    private Scene scene;
    public Text scoreText;
    public Text healthText;
    public Text WinLoseText;
    public Image WinLoseImage;
}
