using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(NavMeshAgent), typeof(InteractDialogueOrObjects))]
public class PlayerController : MonoBehaviour
{
	//[SerializeField] private float speed = 5.0f;
	//[SerializeField] private float speedRotation = 5.0f;

	private static GameObject _moveToIndicatorInstance;

	[SerializeField]
	private float _feedbackPointerScale = 1f; // Scale the pointer

	[SerializeField]
	private GameObject _moveToIndicator; // Prefab of the pointer 3D

	[SerializeField]
	private GameObject mainCamera; // Main camera

	[SerializeField] 
	private DialogueUI dialogueUI;

	[SerializeField] 
	private LayerMask layerMask;

	public DialogueUI DialogueUI => dialogueUI;

	[SerializeField]
	private float angleRotate;

	[SerializeField] private float tempVectorOffset = 0.75f;

	[SerializeField]
	private float distance;

	bool onlyOnce = false;
	Vector3 groundVector; // Vector which the player will move using navMesh agent
	Vector3 selectedVector; // Vector which you click on, counts to interactable objects
	Rigidbody rb;
	Animator animator;
	public float speedRotate = 1;
	NavMeshAgent agent;
	RaycastHit hit;
	Camera m_Camera;
	NavMeshHit navHit;

	[HideInInspector]
	public bool isRotating = false;

	public static PlayerController currentPlayer;

	private void OnEnable()
	{
		DialogueUI.StopDialogue += StopInteractingDialogue;
	}

	private void OnDisable()
	{
		DialogueUI.StopDialogue -= StopInteractingDialogue;
	}

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();

		agent = GetComponent<NavMeshAgent>();

		agent.updateRotation = false;
		agent.isStopped = true;

		animator = GetComponent<Animator>();

		currentPlayer = this;

		m_Camera = mainCamera.GetComponent<Camera>();

		PreparePointer(_moveToIndicator, _feedbackPointerScale);
	}


	void Update()
	{
		
		Vector3 tempV = new Vector3(selectedVector.x, 0, selectedVector.z) - new Vector3(transform.position.x, 0, transform.position.z);
		float tempOffset = tempV.magnitude;
		if(MorwickController.MorwickCauch == true)
        {
			agent.isStopped = true;
        }
		if (tempOffset <= tempVectorOffset && onlyOnce)
		{
			agent.isStopped = true; 
			//transform.position = transform.position;

			//float angle = Mathf.Atan2(tempV.z, tempV.x) * Mathf.Rad2Deg -90;

			//Quaternion rotObj = Quaternion.AngleAxis(angle, Vector3.up);

			//Debug.Log("Rotation: " + tempV + " 2: " + angle + " True rotation: " + transform.rotation.eulerAngles);

			////transform.rotation = rotObj;
			//transform.eulerAngles = Vector3.up * angle;


			onlyOnce = false;
		}
			//Debug.DrawRay(transform.position, tempV, Color.green);

		Animators();

		if (dialogueUI.isOpen || MenuManager.pause || PlanetPuzle.Planets /*|| TimeLineManager.isPlaying*/) {
			/*rb.velocity = Vector3.zero;*/ 
			agent.isStopped = true; 

			return; 
		}
		Move();


	}

	public void RotateTo(Vector3 pos)
    {
		if(agent.isStopped == false)
        {
			if(isRotating)
			{
				StopAllCoroutines();
				StartCoroutine(RotationChar(pos - transform.position));
			}
			else
			{
				StartCoroutine(RotationChar(pos - transform.position));
			}
        }
    }

	IEnumerator RotationChar(Vector3 dir)
	{
		isRotating = true;
		Quaternion originalRotation = transform.rotation;
		Quaternion rotation = Quaternion.LookRotation(dir);
		float time = Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.y, rotation.eulerAngles.y) / speedRotate);
		//Debug.Log("Estoy corrutinante = " + Mathf.DeltaAngle(transform.rotation.eulerAngles.y, rotation.eulerAngles.y));
		//Provisional hasta que se arregle la rotacion
		//transform.rotation = rotation;
		transform.forward = dir;
		for (float timer = 0; timer < time; timer += Time.deltaTime)
		{
			//Debug.Log("fase");
			transform.rotation = Quaternion.Lerp(originalRotation, rotation, timer / time);
			yield return new WaitForEndOfFrame();
		}
		isRotating = false;

	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.yellow;

		Gizmos.DrawWireSphere(groundVector, tempVectorOffset);
		
		Gizmos.color = Color.red;

		Gizmos.DrawWireSphere(selectedVector, tempVectorOffset);

	}

	void Move()
	{
		
		if(agent.isStopped == false)
        {
			StartCoroutine(RotationChar(navHit.position - transform.position));
		}
		
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = m_Camera.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_Camera.nearClipPlane));
			if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask) && !EventSystem.current.IsPointerOverGameObject())
			{
				agent.isStopped = false;
				NavMesh.SamplePosition(hit.point, out navHit, 100, -1);





				if (!navHit.hit)
					return;

				var path = new NavMeshPath();
				if (!agent.CalculatePath(navHit.position, path) || path.status == NavMeshPathStatus.PathInvalid)
					return;

				ShowPointer(navHit.position);
				
				groundVector = navHit.position;

				selectedVector = hit.point;

				if (agent.isStopped) agent.isStopped = false;

				onlyOnce = true;

				agent.SetPath(path);
				





			}

			


		}

		//if(angleRotate > 5.0f)
		//{

		//}
		//else
		//{
		//    if(distance > 0.1f)
		//    {
		//        agent.Move(ray.position);
		//    }
		//    else
		//    {
		//        agent.isStopped = true;
		//    }
		//}

		//var dir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

		//rb.velocity = dir * speed;
	}

	void Animators()
	{
		// Animation to move
		animator?.SetFloat("velocity", Mathf.Abs(agent.velocity.x) + Mathf.Abs(agent.velocity.z));

		animator?.SetBool("talking", dialogueUI.isOpen ? true : false);
	}

	void StopInteractingDialogue()
	{
		GetComponent<InteractDialogueOrObjects>().StopInteract();
	}



	public void PreparePointer(GameObject prefabInstance, float scale)
	{
		if (prefabInstance == null) return;

		if (_moveToIndicatorInstance == null)
			_moveToIndicatorInstance = GameObject.Instantiate(prefabInstance);

		_moveToIndicatorInstance.SetActive(false);
		_moveToIndicatorInstance.transform.localScale = Vector3.one * scale;

		GameObject.DontDestroyOnLoad(_moveToIndicatorInstance);
	}

	public void ShowPointer(Vector3 position)
	{
		_moveToIndicatorInstance?.SetActive(false);
		if (_moveToIndicatorInstance != null) _moveToIndicatorInstance.transform.position = position;
		_moveToIndicatorInstance?.SetActive(true);
	}

}
