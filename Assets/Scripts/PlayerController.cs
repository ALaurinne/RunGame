using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float jumpForce = 2f;
    [SerializeField] Rigidbody2D rb2d;
    [SerializeField] Collider2D collider2d;
    [SerializeField] Animator animator;
    [SerializeField] LayerMask ground;

    [SerializeField] HudController hudController;
    
    private State state = State.idle; 

    private enum State {idle, run, jump, fall};
    bool canMove = false;

    Vector3 initialPosition;

    void OnEnable() {
        initialPosition = transform.position;
    }

    public void StartGame(){
        animator.SetTrigger("restart");
        canMove = true;
        transform.position = initialPosition;
        hudController.gameObject.SetActive(false);
    }

    void Update(){
        if(canMove){

#if UNITY_EDITOR
            if(Input.anyKeyDown && state != State.run){
                 Run();
            }

            if(Input.anyKeyDown && state == State.run && collider2d.IsTouchingLayers(ground)){
                Jump();
            }
#else
            if(state == State.idle && Input.touchCount > 0){
                Run();
            }

            if(state == State.run && collider2d.IsTouchingLayers(ground)){
                for (int i = 0; i < Input.touchCount; i++){
                    Jump();
                }
            }

#endif   
            UpdateState();
        }

        if(Input.GetMouseButtonDown(1)) StartGame();
        animator.SetInteger("state",(int)state);
    }

    void Jump(){
        Vector2 atas = new Vector2 (0,1);
        rb2d.AddForce(atas * jumpForce, ForceMode2D.Impulse);
    }

    void Run(){
        rb2d.velocity = new Vector2 (speed, rb2d.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            hudController.gameObject.SetActive(true);
            canMove = false;
            animator.SetTrigger("dead");
            hudController.LostGame();
        }
        
        if (collision.gameObject.tag == "End")
        {
            hudController.gameObject.SetActive(true);
            canMove = false;
            state = State.idle;
            hudController.WinGame();
        }
    }

    void UpdateState(){

        switch (state){
            case State.idle:
                if(rb2d.velocity.x >= 1) state = State.run;
                if(rb2d.velocity.y > 0) state = State.jump;
                break;
            case State.run:
                if(rb2d.velocity.y > 0) state = State.jump;
                if(rb2d.velocity.x < 1f) state = State.idle;
                break;
            case State.jump:
                if(rb2d.velocity.y < 1) state = State.fall;
                break;
            case State.fall:
                if(collider2d.IsTouchingLayers(ground)) {
                    state = State.run;
                    Run();
                }
                break;
            default:
                state = State.idle;
                break;
        }
    }
}