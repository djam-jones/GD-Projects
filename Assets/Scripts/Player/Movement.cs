using UnityEngine;
using System.Collections;

[System.Serializable]
public class Movement : MonoBehaviour {

	Camera _camera;

	private float _speed = 10f;
	private float _rotationSpeed = 600f;
	private Vector3 _moveDirection;
	private Vector3 _jumpDirection = new Vector3(0, 6, 0);
	private Rigidbody _rigidbody;

	[HideInInspector]
	public bool moving;

	[HideInInspector]
	public bool grounded = true;

	void Awake()
	{
		_camera = GetComponentInChildren<Camera>();
		_rigidbody = GetComponent<Rigidbody>();

		float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		_moveDirection = new Vector3(x, 0, z);
	}

	void Update()
	{
		PlayerMovement();
		UpdateRotation();
	}
	
	void UpdateRotation ()
	{
		float hor = Input.GetAxis("Horizontal");
		float ver = Input.GetAxis("Vertical");

		Vector3 target = ver * _camera.transform.forward + hor * _camera.transform.right;
		target.y = 0;

		if(target != Vector3.zero)
		{
			_moveDirection = Vector3.RotateTowards(_moveDirection, target, _rotationSpeed * Mathf.Deg2Rad * Time.deltaTime, 1000);
			_moveDirection = _moveDirection.normalized;
			transform.localRotation = Quaternion.LookRotation(_moveDirection);
		}
	}

	void PlayerMovement()
	{
		//Walking
		if(Input.GetKey(KeyCode.W))
			transform.Translate(Vector3.forward * _speed * Time.deltaTime);
		else if(Input.GetKey(KeyCode.S))
			transform.Translate(Vector3.forward * _speed * Time.deltaTime);

		if(Input.GetKey(KeyCode.A))
			transform.Translate(Vector3.forward * _speed * Time.deltaTime);
		else if(Input.GetKey(KeyCode.D))
			transform.Translate(Vector3.forward * _speed * Time.deltaTime);

		//Jumping
		if(Input.GetKeyDown(KeyCode.Space) && grounded)
		{
			_rigidbody.velocity = _jumpDirection;
			grounded = false;
		}


	}

	void OnCollisionEnter(Collision c)
	{
		if(c.transform.tag == Tags.GroundTag)
		{
			grounded = true;
		}
	}

	void OnCollisionExit(Collision c)
	{
		if(c.transform.tag == Tags.GroundTag)
		{
			grounded = false;
		}
	}
}