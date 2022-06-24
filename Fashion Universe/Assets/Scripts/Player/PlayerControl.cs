using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public static PlayerControl instance;

    private Animator anim;
    public DynamicJoystick dynJoy;
    [SerializeField] private float speed;
    [SerializeField] private float turnSpeed;

    private void Awake()
    {
        instance = this;

        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        SetAnimations();
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            Movement();
        }       
    }

    private void SetAnimations()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("Walk", true);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            anim.SetBool("Walk", false);
        }
    }

    public void Movement()
    {
        float hor = dynJoy.Horizontal;
        float ver = dynJoy.Vertical;
        Vector3 addedPos = new Vector3(hor * speed * Time.deltaTime, 0, ver * speed * Time.deltaTime);
        transform.position += addedPos;

        Vector3 dir = Vector3.forward * ver + Vector3.right * hor;
        if (dir!=Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), turnSpeed * Time.deltaTime);
        }
    }
}
