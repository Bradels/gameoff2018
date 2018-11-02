using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
	public Animator animator;
	public State state;
	public float walkSpeed = 5;
	// Use this for initialization
	void Start () {
		state = new Onground (gameObject);
		animator = gameObject.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		state.HandleInput(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"));
		Debug.Log (state.getState ());
	}

	public void changeState(State newState){
		state = newState;
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
	public Onground(GameObject body)
	{
		this.body = body;
		animator = body.gameObject.GetComponent<Animator> ();
		rigidbody2D = body.gameObject.GetComponent<Rigidbody2D> ();
		walkSpeed = body.GetComponent<Movement> ().walkSpeed;
	}
	public override void HandleInput (float hAxis, float vAxis)
	{
		Debug.Log("Hey");
		if (hAxis != 0) {
			animator.Play("Walk");
			rigidbody2D.AddForce (new Vector2 (hAxis * walkSpeed, 0));
		} else {
			animator.Play("Still");
		}
		//throw new System.NotImplementedException ();
	}

	public override string getState()
	{
		return "On Ground";
	} 
}