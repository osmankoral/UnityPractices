using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Color[] colors;
    private List<Transform> _segments = new List<Transform>();
    private List<int> _cubes = new List<int>();
    private List<int> cubes = new List<int>();
    public Transform segmentPrefab;
    public Vector2 direction = Vector2.right;
    private int initialSize = 5;
    private float moveDelay = 0.15f;
    private float moveDelayed;
    private int movePoint;
    private bool isFirst;
    private int score;
    GameManager gameManager;
    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
    }
    private void Start()
    {
        ColorSet();
        moveDelayed = moveDelay;
        ResetState();
    }
    private void ColorSet()
    {
        /*colors[0] = new Color(241,241,241);
        colors[1] = new Color(253,15,15);
        colors[2] = new Color(59,255,13);
        colors[3] = new Color(250,71,3);
        colors[4] = new Color(55,109,248);
        colors[5] = new Color(243,26,162);
        colors[6] = new Color(3,226,253);
        colors[7] = new Color(8,11,255);
        colors[8] = new Color(104,229,105);
        colors[9] = new Color(217,175,9);
        colors[10] = new Color(40,128,43);
        colors[11] = new Color(255,117,0);
        colors[12] = new Color(90,12,108);
        colors[13] = new Color(31,255,0);*/
    }

    private void Update()
    {
        // Only allow turning up or down while moving in the x-axis
        if (this.direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow) || movePoint == 1)
            {
                this.direction = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow) || movePoint == 2)
            {
                this.direction = Vector2.down;
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (this.direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || movePoint == 3)
            {
                this.direction = Vector2.right;
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || movePoint == 4)
            {
                this.direction = Vector2.left;
            }
        }
        ScoreManager();
    }

    public void TouchPoint(int _movePoint)
    {
        movePoint = _movePoint;
    }

    private void FixedUpdate()
    {
        moveDelayed -= Time.fixedDeltaTime;
        if (moveDelayed <= 0)
        {
            // Set each segment's position to be the same as the one it follows. We
            // must do this in reverse order so the position is set to the previous
            // position, otherwise they will all be stacked on top of each other.
            for (int i = _segments.Count - 1; i > 0; i--)
            {
                _segments[i].position = _segments[i - 1].position;
            }

            // Move the snake in the direction it is facing
            // Round the values to ensure it aligns to the grid
            float x = Mathf.Round(this.transform.position.x) + this.direction.x;
            float y = Mathf.Round(this.transform.position.y) + this.direction.y;

            this.transform.position = new Vector2(x, y);
            moveDelayed = moveDelay;
        }

    }

    public void Grow()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        segment.position = _segments[_segments.Count - 1].position;

        _segments.Add(segment);
        cubes.Add(0);
        _cubes.Add(0);
    }
    public void UpdateGrow()
    {
        /*for(int i=_cubes.Count-1; i>1;i--)
        {
            _cubes[i] = _cubes[i-1];
        }
        _cubes[1]=0;*/
        for (int i = cubes.Count - 1; i > 1; i--)
        {
            cubes[i] = cubes[i - 1];
        }
        cubes[1] = 0;
        for (int j = _segments.Count - 1; j > 1; j--)
        {
            //_segments[j].gameObject.GetComponent<SpriteRenderer>().color = _segments[j - 1].gameObject.GetComponent<SpriteRenderer>().color;
            _segments[j].gameObject.GetComponent<SpriteRenderer>().sprite = _segments[j - 1].gameObject.GetComponent<SpriteRenderer>().sprite;
        }
        //_segments[1].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        _segments[1].gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
    }

    public void ResetState()
    {
        isFirst = true;
        this.direction = Vector2.right;
        this.transform.position = Vector3.zero;

        // Start at 1 to skip destroying the head
        for (int i = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }

        // Clear the list but add back this as the head
        _segments.Clear();
        cubes.Clear();
        _cubes.Clear();
        _segments.Add(this.transform);
        cubes.Add(0);
        _cubes.Add(0);

        // -1 since the head is already in the list
        for (int i = 0; i < this.initialSize - 1; i++)
        {
            Grow();
        }
    }

    private void CubeListCreate(int _cubesNumber)
    {
        for (int _i = 1; _i < cubes.Count; _i++)
        {
            if (cubes[_i] == 0 || cubes[_i] == _cubesNumber)
            {
                if (cubes[_i] == 0)
                {
                    cubes[_i] = _cubesNumber;

                    if (cubes[_i + 1] == 0 || cubes[_i + 1] == _cubesNumber)
                    {
                        if (_i != cubes.Count - 1)
                        {
                            cubes[_i] = 0;
                        }
                    }

                }
                else if (cubes[_i] == _cubesNumber)
                {
                    _cubesNumber += 1;
                    cubes[_i] = _cubesNumber;
                    int limit = _i + 1;
                    if (limit < cubes.Count && cubes[limit] == _cubesNumber)
                    {
                        cubes[_i] = 0;
                    }
                }
            }
            else if (cubes[_i] != _cubesNumber)
                break;
        }
    }

    private void CubeRest()
    {
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            /*if (cubes[i] == 0)
                //_segments[i].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            else
               // _segments[i].gameObject.GetComponent<SpriteRenderer>().color = Color.red;*/

            _segments[i].gameObject.GetComponent<SpriteRenderer>().sprite = sprites[cubes[i]];

        }

    }

    private void CubeManager(int _index)
    {
        StartCoroutine(CubeAnimationDelay(_index));
    }
    IEnumerator CubeAnimationDelay(int _cubesNumber)
    {
        for (int i = 0; i < cubes.Count; i++)
        {
            _cubes[i] = cubes[i];
        }
        CubeListCreate(_cubesNumber);
        for (int _i = 1; _i < _cubes.Count; _i++)
        {
            if (_cubes[_i] == 0 || _cubes[_i] == _cubesNumber)
            {
                if (_cubes[_i] == 0)
                {
                    // _segments[_i].gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    _segments[_i].gameObject.GetComponent<SpriteRenderer>().sprite = sprites[_cubesNumber];
                    _cubes[_i] = _cubesNumber;

                    if (_cubes[_i + 1] == 0 || _cubes[_i + 1] == _cubesNumber)
                    {
                        if (_i != _cubes.Count - 1)
                        {
                            yield return new WaitForSeconds(0.5f);
                            //_segments[_i].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                            _segments[_i].gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
                            _cubes[_i] = 0;
                        }
                        else if (_i == _cubes.Count - 1)
                        {
                            yield return new WaitForSeconds(0.5f);
                            //_segments[_i].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                            _segments[_i].gameObject.GetComponent<SpriteRenderer>().sprite = sprites[_cubesNumber];
                            _cubes[_i] = _cubesNumber;
                        }
                    }

                }
                else if (_cubes[_i] == _cubesNumber)
                {
                    _cubesNumber += 1;
                    _cubes[_i] = _cubesNumber;
                    //_segments[_i].gameObject.GetComponent<SpriteRenderer>().color = Color.red;
                    _segments[_i].gameObject.GetComponent<SpriteRenderer>().sprite = sprites[_cubesNumber];
                    int limit = _i + 1;
                    if (limit < _cubes.Count && _cubes[limit] == _cubesNumber)
                    {
                        yield return new WaitForSeconds(0.1f);
                        // _segments[_i].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                        _segments[_i].gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
                        _cubes[_i] = 0;
                    }
                }
            }
            else if (_cubes[_i] != _cubesNumber)
                break;
        }

    }
    private void UpdateBomb()
    {

        StartCoroutine(BombDelay());
    }
    IEnumerator BombDelay()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            int currentValue = _cubes[i];

            _segments[i].gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            _segments[i].gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
            yield return new WaitForSeconds(0.1f);
            _segments[i].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            _segments[i].gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
            _cubes[i] = 0;
            cubes[i] = 0;
            if (currentValue != 0)
                break;
        }
    }
    private void UpdateVisibleBomb()
    {

        StartCoroutine(VisibleBombDelay());
    }
    IEnumerator VisibleBombDelay()
    {
        for (int i = 1; i < _segments.Count; i++)
        {
            int currentValue = _cubes[i];

            _segments[i].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            _segments[i].gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
            yield return new WaitForSeconds(0.1f);
            _segments[i].gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            _segments[i].gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
            _cubes[i] = 0;
            cubes[i] = 0;
            if (currentValue != 0)
                break;
        }
    }

    private void KarsıDuvar() 
    {
        Vector2 a = _segments[0].position;
        if(direction.x == 0 && direction.y != 0) {a.y =(a.y- 1f) * -1; _segments[0].position = a;}
        if(direction.x != 0 && direction.y == 0) {a.x =(a.x+11f) * -1; _segments[0].position = a;}
 

    }
    private void ScoreManager()
    {
        int _score = 0;
        for(int i = 1; i<cubes.Count;i++)
        {
            int _cube = 0;
            switch(cubes[i])
            {
                case 0: _cube = 0; break;
                case 1: _cube = 2; break;
                case 2: _cube = 4; break;
                case 3: _cube = 8; break;
                case 4: _cube = 16; break;
                case 5: _cube = 32; break;
                case 6: _cube = 64; break;
                case 7: _cube = 128; break;
                case 8: _cube = 256; break;
                case 9: _cube = 512; break;
                case 10: _cube = 1024; break;
                case 11: _cube = 2048; break;
                case 12: _cube = 4096; break;
                case 13: _cube = 8128; break;
            }
            _score += _cube;
        }
        score = _score;
        gameManager.score = score;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Food")
        {
            CubeRest();
            Grow();
            UpdateGrow();
        }
        else if (other.tag == "Wall")
        {
            KarsıDuvar();
        }
        else if (other.tag == "Obstacle")
        {
            ResetState();
        }
        else if (other.tag == "Finish")
        {
            CubeRest();
            UpdateBomb();
        }
        else if (other.tag == "Cube2")
        {
            CubeRest();
            CubeManager(1);
            Destroy(other.gameObject);
            gameManager.ReCreateCube();
            /*if (isFirst)
            {
                UpdateVisibleBomb();
                CubeManager(1);
                isFirst = false;
                CubeRest();
            }*/
        }
        else if (other.tag == "Cube4")
        {
            CubeRest();
            CubeManager(2);
            Destroy(other.gameObject);
            gameManager.ReCreateCube();
            /*if (isFirst)
            {
                UpdateVisibleBomb();
                CubeManager(2);
                isFirst = false;
                CubeRest();
            }*/
        }
        else if (other.tag == "Cube8")
        {
            CubeRest();
            CubeManager(3);
            Destroy(other.gameObject);
            gameManager.ReCreateCube();
            /*if (isFirst)
            {
                UpdateVisibleBomb();
                CubeManager(3);
                isFirst = false;
                CubeRest();
            }*/
        }
        else if (other.tag == "Cube16")
        {
            CubeRest();
            CubeManager(4);
            Destroy(other.gameObject);
            gameManager.ReCreateCube();
            /*if (isFirst)
            {
                UpdateVisibleBomb();
                CubeManager(4);
                isFirst = false;
                CubeRest();
            }*/
        }
        else if (other.tag == "Cube32")
        {
            CubeRest();
            CubeManager(5);
            Destroy(other.gameObject);
            gameManager.ReCreateCube();
            /*if (isFirst)
            {
                UpdateVisibleBomb();
                CubeManager(5);
                isFirst = false;
                CubeRest();
            }*/
        }
        else if (other.tag == "Cube64")
        {
            CubeRest();
            CubeManager(6);
            Destroy(other.gameObject);
            gameManager.ReCreateCube();
            /*if (isFirst)
            {
                UpdateVisibleBomb();
                CubeManager(6);
                isFirst = false;
                CubeRest();
            }*/
        }
        else if (other.tag == "Cube128")
        {
            CubeRest();
            CubeManager(7);
            Destroy(other.gameObject);
            gameManager.ReCreateCube();
            /*if (isFirst)
            {
                UpdateVisibleBomb();
                CubeManager(7);
                isFirst = false;
                CubeRest();
            }*/
        }

    }




}
