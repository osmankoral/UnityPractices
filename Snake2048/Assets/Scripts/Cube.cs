using UnityEngine;

public class Cube : MonoBehaviour
{
    //public Collider2D gridArea;
    [SerializeField] private Sprite[] sprites;
    private string tagName;

    private void Start()
    {
        RandomizeSprite();
    }


    public void RandomizeSprite()
    {
        int index = Random.Range(0, sprites.Length);
        
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[index];

        switch(index)
        {
            case 0:
                gameObject.tag = "Cube2";;
                break;
            case 1:
                gameObject.tag = "Cube4";
                break;
            case 2:
                gameObject.tag = "Cube8";
                break;
            case 3:
                gameObject.tag = "Cube16";
                break;
            case 4:
                gameObject.tag = "Cube32";
                break;
            case 5:
                gameObject.tag = "Cube64";
                break;
        }
    }

}
