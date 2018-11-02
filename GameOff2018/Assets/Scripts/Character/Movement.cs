using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public Animator animator;
	public State state;
	public float walkSpeed = 5;
    public float stepLength = 0.512f;
    public Vector3 stepLocation;
    public bool canMove = true;
	// Use this for initialization
	void Start () {
		state = new Onground (gameObject);
		animator = gameObject.GetComponent<Animator> ();
        stepLocation = new Vector3(stepLength, 0, 0);
}
	
	// Update is called once per frame
	void Update () {
        if (canMove)
        {
            state.HandleInput(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
		Debug.Log (canMove);
	}

	public void changeState(State newState){
		state = newState;
	}

    public void UpdateCanMove(float x)
    {
        if(x != 0){ canMove = true; }
        else { canMove = false; }
    }
}

/*public class State
{
	protected GameObject body;
	public State()
	{

	}
	public State(GameObject body)
	{
		this.body = body;
		body.GetComponent<Movement> ().changeState (new Onground (body));
	}
	public virtual void HandleInput(float hAxis,float vAxis)
	{

	}

	public virtual string getState()
	{
		return "State";
	} 
}
*/
public abstract class State
{
	
	public abstract void HandleInput (float hAxis, float vAxis);

	public abstract string getState ();
}

public class Onground : State
{
	protected GameObject body;
	private Animator animator;
	private Rigidbody2D rigidbody2D;
	private float walkSpeed;
    private Movement movement;
    public Onground(GameObject body)
	{
		this.body = body;
		animator = body.gameObject.GetComponent<Animator> ();
		rigidbody2D = body.gameObject.GetComponent<Rigidbody2D> ();
        movement = body.GetComponent<Movement>();
        walkSpeed = movement.walkSpeed;
	}
	public override void HandleInput (float hAxis, float vAxis)
	{
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            body.transform.Translate(movement.stepLocation);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            body.transform.Translate(movement.stepLocation * -1);
        }
        animator.Play("Walk");
            //rigidbody2D.AddForce (new Vector2 (hAxis * walkSpeed, 0))
		//throw new System.NotImplementedException ();
	}

	public override string getState()
	{
		return "On Ground";
	} 
}