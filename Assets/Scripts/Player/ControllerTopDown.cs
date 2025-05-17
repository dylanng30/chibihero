using UnityEngine;

public class ControllerTopDown : Entity
{
    private float leftRight, upDown;
    [SerializeField] float PSpeed;
    // Start is called before the first frame update

    void Awake()
    {
        this.SetComponents();
        _speed = PSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        leftRight = Input.GetAxis("Horizontal");
        upDown = Input.GetAxis("Vertical");
        this.Flip();
        if (this.GetRb().velocity != Vector2.zero)
            this.GetAnim().Play("Run");
        else
        {
            this.GetAnim().Play("Idle");
        }
    }
    private void FixedUpdate()
    {
        this.Move();
    }
    private void Move()
    {
        this.GetRb().velocity = new Vector2(leftRight * _speed, upDown * _speed);
    }
}
