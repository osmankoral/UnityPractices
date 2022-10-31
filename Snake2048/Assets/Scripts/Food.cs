using UnityEngine;

public class Food : MonoBehaviour
{
    public Collider2D gridArea;

        [SerializeField] private Sprite[] sprites;
    private float needTime;

    private void Awake()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
        gameObject.tag = "Untagged";
    }
    private void Start()
    {
        RandomizePosition();
        needTime = Random.Range(20f, 80f);
    }

    public void RandomizePosition()
    {
        Bounds bounds = this.gridArea.bounds;

        // Pick a random position inside the bounds
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        // Round the values to ensure it aligns with the grid
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        this.transform.position = new Vector2(x, y);
    }
    private void Update()
    {
        needTime -= Time.deltaTime;
        if(needTime <= 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            gameObject.tag = "Food";
            needTime = Random.Range(4f, 15f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
        gameObject.tag = "Untagged";
        RandomizePosition();
    }
}
