using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    Vector2 moveDir;
    public float speed = 10;
    Rigidbody rb;
    float h, v;
    Vector3 inputVector;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        h = DampenValue(h, moveDir.x);
        v = DampenValue(v, moveDir.y);

        inputVector = new Vector3(h * speed, rb.velocity.y, v * speed);
        transform.LookAt(transform.position + new Vector3(inputVector.x, 0, inputVector.z));
        rb.velocity = inputVector;
        Debug.DrawRay(transform.position, transform.forward * 7, Color.green);
    }

    public void MovePlayer(InputAction.CallbackContext ctx)
    {
        Debug.Log(ctx.ReadValue<Vector2>());
        moveDir = ctx.ReadValue<Vector2>();
    }

    float DampenValue(float ReadValue, float moveDir)
    {
        ReadValue = Mathf.MoveTowards(ReadValue, moveDir, Time.deltaTime * 2);
        return ReadValue = Mathf.Clamp(ReadValue, -1, 1);
    }
}
