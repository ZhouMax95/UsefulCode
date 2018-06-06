using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	private Transform mainCamera;
	private Transform player;
	private Transform playerShoulder;
	private Vector3 CaDistance;
	private UIManager UIMng;

	/// <summary>
	/// 当前场景名称
	/// </summary>
	[HideInInspector]
	public string currentScName;
	private bool isRotating = false;
	private float rotateSpeed = 2;
	private float scrollSpeed = 10;
	/// <summary>
	/// 相机远近
	/// </summary>
	private float distance;

	void Awake()
	{
		player = GameObject.Find("Player").GetComponent<Transform>();
		playerShoulder = player.transform.Find("Shoulder").GetComponent<Transform>();
		UIMng = GetComponent<UIManager>();
		mainCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
	}

	void Start()
	{
		mainCamera.LookAt(playerShoulder);
		CaDistance = mainCamera.position - playerShoulder.position;
	}

	void Update()
	{
		mainCamera.position = playerShoulder.position + CaDistance;
		CameraRotate();
		ScrollView();
		if (Input.GetMouseButton(0))
		{
			RayDetective();
		}
	}

	/// <summary>
	/// 相机缩放控制
	/// </summary>
	void ScrollView()
	{
		distance = CaDistance.magnitude;
		distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
		distance = Mathf.Clamp(distance, 4, 20);
		CaDistance= CaDistance.normalized * distance;
		
	}

	/// <summary>
	/// 相机旋转控制
	/// </summary>
	void CameraRotate()
	{
		if (Input.GetMouseButtonDown(1))
		{
			isRotating = true;
		}
		if (Input.GetMouseButtonUp(1)||!Input.GetMouseButton(1))
		{
			isRotating = false;
		}
		if (isRotating)
		{
			float mouse_x = Input.GetAxis("Mouse X");
			float mouse_y = Input.GetAxis("Mouse Y");

			Vector3 pos = mainCamera.position;
			Quaternion rot = mainCamera.rotation;

			mainCamera.RotateAround(playerShoulder.position, Vector3.up, mouse_x * rotateSpeed);
			mainCamera.RotateAround(playerShoulder.position, mainCamera.right, -mouse_y * rotateSpeed);


			float x = mainCamera.eulerAngles.x;
			if (x < 1 || x > 80)
			{
				mainCamera.position = pos;
				mainCamera.rotation = rot;
			}

		}
		CaDistance = mainCamera.position - playerShoulder.position;
	}

	/// <summary>
	/// 射线检测
	/// </summary>
	private void RayDetective()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.transform.tag == "Building")
			{
				switch (hit.transform.name)
				{
					case "银行":
						LoadScene("Bank");			
						break;
					case "证券":
						LoadScene("Stock");
						break;
					case "保险":
						LoadScene("Insurance");
						break;
					case "外汇市场":
						LoadScene("ForexMarket");
						break;
					case "衍生品市场":
						LoadScene("DerivativeMarket");
						break;
					case "信用评级":
						LoadScene("CreditRating");
						break;
					default:
						break;
				}
			}
		}
	}

	/// <summary>
	/// 加载场景
	/// </summary>
	/// <param name="SceneName">场景名称</param>
	public void LoadScene(string SceneName)
	{
		if (SceneName=="Street")
		{
			GameObject[] objs = FindObjectsOfType(typeof(GameObject)) as GameObject[];
			foreach (var item in objs)
			{
				if (item.GetComponent<DontDestroyOnLoad>() != null)
				{
					Destroy(item);
				}
			}
			currentScName = TranslateWord(SceneName);
			UIMng.ShowLittleMap();
			UIMng.HideMapBtn();
			UIMng.OnCloseMapBG();
			UIMng.ShowQuitBtn();
		}
		else
		{
			currentScName = TranslateWord(SceneName);
			UIMng.HideLittleMap();
			UIMng.ShowMapBtn();
			UIMng.OnCloseMapBG();
			UIMng.HideQuitBtn();
		}
		SceneManager.LoadScene(SceneName);
		UIMng.UpdateSceneName(currentScName);
	}

	/// <summary>
	/// 翻译词语
	/// </summary>
	public string TranslateWord(string name)
	{
		switch (name)
		{
			case "银行":
				return "Bank";
			case "证券":
				return "Stock";
			case "保险":
				return "Insurance";
			case "外汇市场":
				return "ForexMarket";
			case "衍生品市场":
				return "DerivativeMarket";
			case "信用评级":
				return "CreditRating";
			case "街道":
				return "Street";
			case "Street":
				return "街道";
			case "Bank":
				return "银行";
			case "Stock":
				return "证券";
			case "Insurance":
				return "保险";
			case "ForexMarket":
				return "外汇市场";
			case "DerivativeMarket":
				return "衍生品市场";
			case "CreditRating":
				return "信用评级";
			default:
				Debug.LogError("不存在的建筑");
				return "";
		}
	}

	/// <summary>
	/// 退出程序
	/// </summary>
	public void QuitApplication()
	{
		
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}
	
}
