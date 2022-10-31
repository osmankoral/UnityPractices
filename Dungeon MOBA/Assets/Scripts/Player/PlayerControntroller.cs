using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerControntroller : MonoBehaviour
{
    [SerializeField ] private Camera playerCamera;
    

    private Rigidbody rb;
    private Animator anim;

    private Vector3 targetPos;
    private float rotateVelocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        targetPos = transform.position;
    }
    void Update()
    {
        CameraMovement();
        PlayerPositionControl();
        if(Input.GetKeyDown(KeyCode.F))
        {
            CutTree();
        }
    }

    private void PlayerPositionControl()
    {
        if (Input.GetButton("Fire2"))
        {
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit))
            {
 
                Vector3 b = raycastHit.point;
                b.y = transform.position.y; targetPos = b;

            }
        }
        if (transform.position.x != targetPos.x && transform.position.z != targetPos.z)
        {
            PlayerLookRotation();
            PlayerMove();
            anim.SetBool("Run", true);
        }
        else anim.SetBool("Run", false);
    }

    void PlayerMove()
    {
        rb.position = Vector3.MoveTowards(transform.position, targetPos, 0.12f);
    }
    private void PlayerLookRotation()
    {
        Quaternion rotationLook = Quaternion.LookRotation(targetPos - transform.position);
        float rotationY = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationLook.eulerAngles.y, ref rotateVelocity, 0.1f * (Time.deltaTime * 5));
        transform.eulerAngles = new Vector3(0, rotationY, 0);
    }
    private void CameraMovement()
    {
        Vector3 tempCamVec = transform.position;
        tempCamVec += new Vector3(0, 5, -4);
        Vector3 lastCamVec = playerCamera.transform.position;
        playerCamera.transform.position = Vector3.Slerp(lastCamVec, tempCamVec, 0.5f);
    }

    private void CutTree()
    {
        anim.SetTrigger("Tree");
        anim.SetTrigger("Tree2");
    }
}
