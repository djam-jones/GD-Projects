using UnityEngine;
using System.Collections;

[System.Serializable]
public class CameraRotation : MonoBehaviour {

	//private Movement _playerMovement;
	private GameObject _target;
	private Vector3 _offset;

	private float _distanceX = 1.3f;
	private float _distanceY = 1.4f;
	private float _distanceZ = 7.5f;

	private float _xRotationSpeed = 130f;
	private float _yRotationSpeed = 75f;

	private float _yMinLimit = -15;
	private float _yMaxLimit = 90;

	private bool _cursorToggle = false;

	Quaternion rotation;
	Vector3 position;
	float sensitivity = 0.04f;
	float x;
	float y;

	[HideInInspector]
	public static CameraRotation instance;

	void Awake()
	{
		instance = this;

		_target = GameObject.FindGameObjectWithTag(Tags.PlayerTag);
		//_playerMovement = GetComponentInParent<Movement>();
	}

	void Start()
	{
		_offset = _target.transform.position - transform.position;
		Cursor.visible = false;
	}

	void LateUpdate()
	{
		UpdateRotation();
		if(Input.GetKeyDown(KeyCode.Period))
		{
			ToggleRotation();
		}
	}

	void UpdateRotation()
	{
		if(_target && !_cursorToggle)
		{
			x += Input.GetAxis("Mouse X") * _xRotationSpeed * sensitivity;
			y -= Input.GetAxis("Mouse Y") * _yRotationSpeed * sensitivity;
			
			y = Mathf.Clamp(y, _yMinLimit, _yMaxLimit);
			
			rotation = Quaternion.Euler(y, x, 0);
			position = rotation * new Vector3(_distanceX, _distanceY, -_distanceZ) + _target.transform.position;
			
			transform.position = position;
			transform.rotation = rotation;

			Cursor.lockState = CursorLockMode.Locked;
		}
		else
		{
			Cursor.lockState = CursorLockMode.Confined;
		}
		//		else if(playerMovement.moving)
		//		{
		//			transform.rotation = Quaternion.Slerp(transform.rotation, startRotation, Time.time * lerpTime);
		//			transform.position = Vector3.Lerp(transform.position, startPosition, Time.time * lerpTime);
		//
		//			float desiredAngle = target.transform.eulerAngles.y;
		//			rotation = Quaternion.Euler(0, desiredAngle, 0);
		//			transform.position = target.transform.position - (rotation * offset);
		//			transform.LookAt(target.transform);
		//
		//			y += Input.GetAxis("Mouse Y") * sensitivity;
		//			y = Mathf.Clamp(y, yMinLimit, yMaxLimit);
		//
		//			transform.localRotation *= Quaternion.AngleAxis(y, Vector3.left);
		//		}
	}
	
	public void ToggleRotation()
	{
		if(!_cursorToggle)
		{
			SetCursorState = true;
			_cursorToggle = true;
		}
		else if(_cursorToggle)
		{
			SetCursorState = false;
			_cursorToggle = false;
		}
	}

	private bool SetCursorState
	{
		set 
		{
			Cursor.visible = value;
		}
		get
		{
			return SetCursorState;
		}
	}
}