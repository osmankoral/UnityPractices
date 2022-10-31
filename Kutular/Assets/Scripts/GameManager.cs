using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] cubePref;
    [SerializeField] private Transform cubeTransform;
    [SerializeField] private GameObject finishObject;

    public GameObject cube;
    private GameObject newCube;

    private bool isActive;
    private Vector3 poz;

    public int index_;
    public Vector3 a_;
    public bool isMerge;

    

    // Start is called before the first frame update
    void Start()
    {
        CreateCube();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0) && cube != null)
        {
            poz = Input.mousePosition;
            isActive = !isActive;
            finishObject.SetActive(false);
        }

        if(Input.GetMouseButtonUp(0) && isActive)
        {
            isActive = !isActive;
            cube.GetComponent<CubeManager>().Move();
            Invoke("CreateCube", 0.5f);
            StartCoroutine(DelayFinish());
        }

        if(isMerge)
        {
            NewCube();
        }

    }
    IEnumerator DelayFinish()
    {
        yield return new WaitForSeconds(0.5f);
        finishObject.SetActive(true);
    }

    private void FixedUpdate()
    {
        if(isActive)
        {
            Vector3 a = Input.mousePosition;
            Vector3 dir = a - poz;
            Vector3 c = cubeTransform.GetChild(cubeTransform.childCount - 1).transform.position;
            c.x = (dir.x / 200);// -1.3 1.2
            if(c.x < -1.3f || c.x >  1.2f)
            {
                if(c.x < -1.3f) c.x = -1.3f;
                else if(c.x >  1.2f) c.x = 1.2f;
            }
           
            cubeTransform.GetChild(cubeTransform.childCount - 1).transform.position = c;

        }
    }
    

    private void CreateCube()
    {
        cube = Instantiate(cubePref[Random.Range(0,2)], cubeTransform);
    }

    /*public void NewCube(int index, Vector3 a)
    {
        int index_ = index;
        Vector3 a_ = a;
        StartCoroutine(NewCubeCre(index_, a_));
    }
    IEnumerator NewCubeCre(int index, Vector3 a)
    {
        yield return new WaitForSeconds(0.2f);
        GameObject newCube = Instantiate(cubePref[index], a, transform.rotation);
    }*/

    private void NewCube()
    {
        isMerge = !isMerge;
        StartCoroutine(NewCubeCre());
        
    }
    IEnumerator NewCubeCre()
    {
        yield return new WaitForSeconds(0.1f);
        newCube = Instantiate(cubePref[index_], a_, transform.rotation);
        yield return new WaitForSeconds(0.05f);
        if(newCube != null)
            newCube.GetComponent<CubeManager>().Jump();
    }
    
}
