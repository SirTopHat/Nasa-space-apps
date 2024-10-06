using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OrbitalRevolution : MonoBehaviour
{
    [SerializeField]
    public float speed = 80f;

    [SerializeField]
    public float distanceFromCenter = 300f;
    public Transform centralStar;
    public Button scoreButton;
    public Image revolvingStarImage;
    public RectTransform starRectTransform;

    public float maxSize = 1.5f;         // Maximum size at the center
    public float minSize = 1f;           // Minimum size at the limits
    public float hideSize = 0f;        // Size when fully hidden behind the central star

    public GameObject TaskCompletedCanvas;

    [SerializeField]
    public float hidingZoneWidth = 105f; // Width of the hiding zone

    private bool movingRight = true;
    private bool isScoringEnabled = true;
    private int goalScore = 3;

    private float leftLimit;
    private float rightLimit;

    public static OrbitalRevolution Instance;

    private int effectiveScoreCount = 0; 

    void Start()
    {
        // Set limits based on the central star's position
        leftLimit = centralStar.position.x - distanceFromCenter;
        rightLimit = centralStar.position.x + distanceFromCenter;

        // Start the star's position at the left limit
        transform.position = new Vector3(leftLimit, transform.position.y, transform.position.z);

        // Add button listener
        if (scoreButton != null)
        {
            scoreButton.onClick.AddListener(ScorePoint);
        }

        if (TaskCompletedCanvas != null)
        {
            TaskCompletedCanvas.SetActive(false);
        }
    }

    void Update()
    {
        MoveStar();
        AdjustStarSize();
    }

    void MoveStar()
    {
        float newPosX = transform.position.x + (movingRight ? speed * Time.deltaTime : -speed * Time.deltaTime);
        transform.position = new Vector3(newPosX, transform.position.y, transform.position.z);

        // Change direction when reaching limits
        if (newPosX >= rightLimit)
        {
            movingRight = false;
        }
        else if (newPosX <= leftLimit)
        {
            movingRight = true;
        }
    }

    void AdjustStarSize()
    {
        float distanceFromCentralStar = Mathf.Abs(transform.position.x - centralStar.position.x);
        float newScale = starRectTransform.localScale.x;

        if (movingRight)
        {
            if (transform.position.x < centralStar.position.x)
            {
                // Moving from left limit to center, size increases from 1 to 1.5
                float t = (transform.position.x - leftLimit) / (centralStar.position.x - leftLimit);
                newScale = Mathf.Lerp(minSize, maxSize, t);
            }
            else
            {
                // Moving from center to right limit, size decreases from 1.5 to 1
                float t = (transform.position.x - centralStar.position.x) / (rightLimit - centralStar.position.x);
                newScale = Mathf.Lerp(maxSize, minSize, t);
            }
            revolvingStarImage.enabled = true;
        }
        else
        {
            if (transform.position.x > centralStar.position.x + hidingZoneWidth)
            {
                // Moving from right limit to edge of hiding zone, size decreases from 1 to 0.1
                float t = (rightLimit - transform.position.x) / (rightLimit - (centralStar.position.x + hidingZoneWidth));
                newScale = Mathf.Lerp(minSize, hideSize, t);
                revolvingStarImage.enabled = true;
            }
            else if (distanceFromCentralStar < hidingZoneWidth)
            {
                // Inside hiding zone, size becomes 0.1 and hide the star
                newScale = hideSize;
                revolvingStarImage.enabled = false;
            }
            else if (transform.position.x < centralStar.position.x - hidingZoneWidth)
            {
                // Moving from left edge of hiding zone to left limit, size increases from 0.1 to 1
                float t = (centralStar.position.x - hidingZoneWidth - transform.position.x) / (centralStar.position.x - hidingZoneWidth - leftLimit);
                newScale = Mathf.Lerp(hideSize, minSize, t);
                revolvingStarImage.enabled = true;
            }
        }
        starRectTransform.localScale = new Vector3(newScale, newScale, 1f);
    }

    public void ScorePoint()
    {
        // Calculate the effective scoring area based on the central star's position
        float centralStarX = centralStar.position.x;
        float effectiveMinX = centralStarX - 25f;
        float effectiveMaxX = centralStarX + 25f;

        // Check if the star is within the effective scoring area and is moving right
        if (scoreButton != null && movingRight && isScoringEnabled && transform.position.x >= effectiveMinX && transform.position.x <= effectiveMaxX)
        {
            Debug.Log("Score!");
            effectiveScoreCount++; // Increment effective score count
            speed *= 1.8f; // Increase speed by 1.5x
            MiniGameManager.Instance.ScorePoint();

            // Check if the task is completed
            if (effectiveScoreCount >= goalScore)
            {
                TaskCompletedCanvas.SetActive(true);
                revolvingStarImage.enabled = false;
                scoreButton.gameObject.SetActive(false);
            }

            isScoringEnabled = false; 
            Invoke("EnableScoring", 2f); 
        }
    }


    void EnableScoring()
    {
        isScoringEnabled = true;
    }
}
