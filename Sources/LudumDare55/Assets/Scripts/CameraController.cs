using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// how the camera moves around with the mouse and stuff. handles 1st person and 3rd person.

public class CameraController : MonoBehaviour {


	public float Sensitivity {
		get { return sensitivity; }
		set { sensitivity = value; }
	}
	[Range(0.1f, 9f)][SerializeField] float sensitivity = 2f;
	[Tooltip("Limits vertical camera rotation. Prevents the flipping that happens when rotation goes above 90.")]
	[Range(0f, 90f)][SerializeField] float yRotationLimit = 88f;

	public Vector2 rotation = Vector2.zero;
	const string xAxis = "Mouse X";
	const string yAxis = "Mouse Y";
	public bool FlipY;

	public Camera Cam;
	public bool ThirdPerson;
	public float CameraDistance;
	public float GeometryPadding;

	public float FOV = 90;
	public float ZoomTimer;

	public float CamLerp;

	public float DiffX;
	public float DiffY;

	public Transform PlayerVis;
	public PlayerController PMov;

	void Start(){
	}

	void Update(){
		if (Global.Dataholder.Pausemenu.IsPaused) {
			return;
		}


		if (!PMov.InCutscene) {
			
		DiffX = Input.GetAxis (xAxis) * sensitivity * Mathf.Lerp (1f, 0.1f, ZoomTimer);
		DiffY = Input.GetAxis (yAxis) * sensitivity * Mathf.Lerp (1f, 0.1f, ZoomTimer);
		rotation.x += DiffX;
		rotation.y += DiffY;
		rotation.y = Mathf.Clamp (rotation.y, -yRotationLimit, yRotationLimit);
		var xQuat = Quaternion.AngleAxis (rotation.x, Vector3.up);
			var yQuat = Quaternion.AngleAxis (Super.Dataholder.InvertMouse ? -rotation.y : rotation.y, Vector3.left);
		transform.localRotation = xQuat * yQuat; //Quaternions seem to rotate more consistently than EulerAngles. Sensitivity seemed to change slightly at certain degrees using Euler. transform.localEulerAngles = new Vector3(-rotation.y, rotation.x, 0);

		/*
		if (Input.GetKeyDown (KeyCode.Tab)) {
			ThirdPerson = !ThirdPerson;
		}
		*/

		Vector3 FirstPersonCameraPos = Vector3.zero;
		Vector3 BackItUp = -transform.forward;
		int layerMask = 1 << 9; //layer 9 is CameraBlockers
		RaycastHit Hit;
		float camDist = CameraDistance;
		if (Physics.Raycast (transform.position, BackItUp, out Hit, CameraDistance, layerMask)) {
			camDist = Hit.distance - GeometryPadding;
			if (camDist < 0) {
				camDist = 0;
			}
		}


		float camspeed = 10;
		if (ThirdPerson) {
			CamLerp += Time.deltaTime * camspeed;
		} else {
			CamLerp -= Time.deltaTime * camspeed;
		}
		CamLerp = Mathf.Clamp01 (CamLerp);

		Vector3 ThirdPersonCameraPos = new Vector3 (0, camDist / 4, -camDist);
		
		Vector3 CamPos = new Vector3 (0, Mathf.Lerp (FirstPersonCameraPos.y, ThirdPersonCameraPos.y, CamLerp), Mathf.Lerp (FirstPersonCameraPos.z, ThirdPersonCameraPos.z, CamLerp));

		Cam.transform.localPosition = CamPos;


		float zoomSpeed = 10;
		if (Input.GetKey (KeyCode.F)) {
			ZoomTimer += Time.deltaTime * zoomSpeed;
		} else {
			ZoomTimer -= Time.deltaTime * zoomSpeed;
		}
		ZoomTimer = Mathf.Clamp01 (ZoomTimer);
		Cam.fieldOfView = Mathf.Lerp (FOV, 10, ZoomTimer);

			float PlayerAngle = 15;
			/*
		if ((transform.eulerAngles.y > PlayerVis.eulerAngles.y + PlayerAngle && transform.eulerAngles.y > PlayerAngle) || (transform.eulerAngles.y < PlayerVis.eulerAngles.y + PlayerAngle && transform.eulerAngles.y <= PlayerAngle)) {
			PlayerVis.eulerAngles = new Vector3 (PlayerVis.eulerAngles.x, transform.eulerAngles.y - PlayerAngle, PlayerVis.eulerAngles.z);
		}
		if ((transform.eulerAngles.y < PlayerVis.eulerAngles.y - PlayerAngle && transform.eulerAngles.y < 360- PlayerAngle) || (transform.eulerAngles.y > PlayerVis.eulerAngles.y - PlayerAngle && transform.eulerAngles.y >= 360- PlayerAngle)) {
			PlayerVis.eulerAngles = new Vector3 (PlayerVis.eulerAngles.x, transform.eulerAngles.y + PlayerAngle, PlayerVis.eulerAngles.z);
		}*/

			PlayerVis.eulerAngles = new Vector3 (PlayerVis.eulerAngles.x, transform.eulerAngles.y + (Global.Dataholder.Pmov.SpeedboostTimer * Global.Dataholder.Pmov.SpeedboostTimer) * 360 * 5, PlayerVis.eulerAngles.z);
		}
	}
}
