using UnityEngine;
using System.Collections;


// from http://savalishunitytutorials.com/updatedtouchmovement.cs
public class TouchCameraControl : MonoBehaviour
{
	public float moveSensitivityX = 1.0f;
	public float moveSensitivityY = 1.0f;
	public bool updateZoomSensitivity = true;
	public float orthoZoomSpeed = 0.05f;
	public float minZoom = 1.0f;
	public float maxZoom = 20.0f;
	public bool invertMoveX = false;
	public bool invertMoveY = false;
	public float mapWidth = 60.0f;
	public float mapHeight = 40.0f;
	
	public float inertiaDuration = 1.0f;
	
	public Camera mainCamera;
	
	private float minX, maxX, minY, maxY;
	private float horizontalExtent, verticalExtent;
	
	private float scrollVelocity = 0.0f;
	private float timeTouchPhaseEnded;
	private Vector2 scrollDirection = Vector2.zero;
	
	void Start ()
	{
		// maxZoom = 0.5f * (mapWidth / mainCamera.aspect);
		
		// if (mapWidth > mapHeight)
		// 	maxZoom = 0.5f * mapHeight;
		
		if (mainCamera.orthographicSize > maxZoom)
			mainCamera.orthographicSize = maxZoom;
		
		CalculateLevelBounds ();
	}
	
	void Update ()
	{
		if (updateZoomSensitivity) {
			moveSensitivityX = mainCamera.orthographicSize / 5.0f;
			moveSensitivityY = mainCamera.orthographicSize / 5.0f;
		}
		
		Touch[] touches = Input.touches;
		
		if (touches.Length < 1) {
			//if the camera is currently scrolling
			if (scrollVelocity != 0.0f) {
				//slow down over time
				float t = (Time.time - timeTouchPhaseEnded) / inertiaDuration;
				float frameVelocity = Mathf.Lerp (scrollVelocity, 0.0f, t);
				mainCamera.transform.position += -(Vector3)scrollDirection.normalized * (frameVelocity * 0.05f) * Time.deltaTime;
				
				if (t >= 1.0f)
					scrollVelocity = 0.0f;
			}
		}
		
		if (touches.Length > 0) {
			// Single touch choose

			//Single touch (move)
			if (touches.Length == 1) {
				if (touches [0].phase == TouchPhase.Began) {
					scrollVelocity = 0.0f;
				} else if (touches [0].phase == TouchPhase.Moved) {
					Vector2 delta = touches [0].deltaPosition;
					
					float positionX = delta.x * moveSensitivityX * Time.deltaTime;
					positionX = invertMoveX ? positionX : positionX * -1;
					
					float positionY = delta.y * moveSensitivityY * Time.deltaTime;
					positionY = invertMoveY ? positionY : positionY * -1;
					
					mainCamera.transform.position += new Vector3 (positionX, positionY, 0);
					
					scrollDirection = touches [0].deltaPosition.normalized;
					scrollVelocity = touches [0].deltaPosition.magnitude / touches [0].deltaTime;
					
					
					if (scrollVelocity <= 100)
						scrollVelocity = 0;
				} else if (touches [0].phase == TouchPhase.Ended) {
					timeTouchPhaseEnded = Time.time;
				}
			}
			
			
			//Double touch (zoom)
			if (touches.Length == 2) {
				Vector2 cameraViewsize = new Vector2 (mainCamera.pixelWidth, mainCamera.pixelHeight);
				
				Touch touchOne = touches [0];
				Touch touchTwo = touches [1];
				
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;
				Vector2 touchTwoPrevPos = touchTwo.position - touchTwo.deltaPosition;
				
				float prevTouchDeltaMag = (touchOnePrevPos - touchTwoPrevPos).magnitude;
				float touchDeltaMag = (touchOne.position - touchTwo.position).magnitude;
				
				float deltaMagDiff = prevTouchDeltaMag - touchDeltaMag;
				
				mainCamera.transform.position += mainCamera.transform.TransformDirection ((touchOnePrevPos + touchTwoPrevPos - cameraViewsize) * mainCamera.orthographicSize / cameraViewsize.y);
				
				mainCamera.orthographicSize += deltaMagDiff * orthoZoomSpeed;
				mainCamera.orthographicSize = Mathf.Clamp (mainCamera.orthographicSize, minZoom, maxZoom) - 0.001f;
				
				mainCamera.transform.position -= mainCamera.transform.TransformDirection ((touchOne.position + touchTwo.position - cameraViewsize) * mainCamera.orthographicSize / cameraViewsize.y);
				
				CalculateLevelBounds ();
			}
		}
	}
	
	void CalculateLevelBounds ()
	{
		verticalExtent = mainCamera.orthographicSize;
		horizontalExtent = mainCamera.orthographicSize * Screen.width / Screen.height;
		minX = horizontalExtent - mapWidth / 2.0f;
		maxX = mapWidth / 2.0f - horizontalExtent;
		minY = verticalExtent - mapHeight / 2.0f;
		maxY = mapHeight / 2.0f - verticalExtent;
	}
	
	void LateUpdate ()
	{
		Vector3 limitedCameraPosition = mainCamera.transform.position;
		limitedCameraPosition.x = Mathf.Clamp (limitedCameraPosition.x, minX, maxX);
		limitedCameraPosition.y = Mathf.Clamp (limitedCameraPosition.y, minY, maxY);
		mainCamera.transform.position = limitedCameraPosition;
	}
	
	void OnDrawGizmos ()
	{
		Gizmos.DrawWireCube (Vector3.zero, new Vector3 (mapWidth, mapHeight, 0));
	}
}