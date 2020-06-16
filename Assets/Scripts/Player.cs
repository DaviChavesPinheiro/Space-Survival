using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // public SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Animator animator;
    public GameObject bullet_pref;
    public Transform gun;
    public int maxSpeed = 15;
    public int force = 250;
    public float rotation_speed = 3;
    private float lerpAngle;
    public float maxLinearDrag = 1;
    public float minLinearDrag = 0;
    public float maxAngularDrag = 1;
    public float minAngularDrag = 0;
    private bool moving = false;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        lerpAngle = 360 - transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        // print(360 - Mathf.Atan2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Mathf.Rad2Deg);
        if(Input.GetMouseButtonDown(0)){
            shoot();
        }
    }

    void FixedUpdate() {
        
        Vector2 input = new Vector2 (Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        float magnitude = input.magnitude;
        float angle = 360 - Mathf.Atan2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * Mathf.Rad2Deg;
        lerpAngle = Mathf.LerpAngle(lerpAngle, angle, rotation_speed * Time.fixedDeltaTime * magnitude);

        rb.MoveRotation(lerpAngle);

        rb.AddForce (input * force * Time.fixedDeltaTime);

        if (Mathf.Abs(rb.velocity.x) > maxSpeed || Mathf.Abs(rb.velocity.y) > maxSpeed) {
			rb.drag = 10;
		} else {
			if (magnitude != 0) {
				rb.drag = maxLinearDrag;
				rb.angularDrag = maxAngularDrag;
			} else {
				rb.drag = minLinearDrag;
				rb.angularDrag = minLinearDrag;
			}
		}

        if(magnitude != 0){
            animator.SetBool("moving", true);
        } else {
            animator.SetBool("moving", false);
        }
    }

    private void shoot(){
        GameObject bullet = Instantiate(bullet_pref, gun.position, gun.rotation) as GameObject;
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.up * bullet_pref.GetComponent<Bullet>().velocity;
        Destroy(bullet, bullet_pref.GetComponent<Bullet>().life_time);
    }
    
}
