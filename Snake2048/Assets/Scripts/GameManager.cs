using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{ 
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Text scoreText;
    public Collider2D gridArea;
    public int score;
     // Start is called before the first frame update
    void Start()
    {
        int loopNum = Random.Range(4, 11);
        for(int i=0; i<loopNum; i++)
        {
            CreateCube();
        }
        scoreText.text = score.ToString();
    }

    private void Update()
    {
        scoreText.text = score.ToString();
    }
    private void CreateCube()
    {
        Bounds bounds = this.gridArea.bounds;

        // Pick a random position inside the bounds
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        

        // Round the values to ensure it aligns with the grid
        x = Mathf.Round(x);
        y = Mathf.Round(y);

        GameObject newCube = Instantiate(cubePrefab, this.transform);
        newCube.transform.position = new Vector2(x, y);
    }

    public void ReCreateCube()
    {
        int rand = Random.Range(1,3);
        for(int i=0; i<rand; i++)
        {
            CreateCube();
        }
    }
    
}
