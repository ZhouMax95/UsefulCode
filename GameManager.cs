/*
 第一人称，第三人称相机控制
*/
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.PostProcessing;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour {

	#region dontDestoryOnLoad
	private Transform mainCamera;
	private Transform player;
	public Transform eventSystem;
	public Transform canvas;
	public Transform gameManager;
	#endregion
	private Transform playerShoulder;
	private Vector3 CaDistance;
	private Vector3 originCaDistance;
	private UIManager UIMng;
	private bool isThirdPerson = true;
	private float horizontal, vertical;

	public PostProcessingProfile securityProfile;

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
		mainCamera = GameObject.Find("Main Camera").GetComponent<Transform>();
		player = GameObject.Find("Player").GetComponent<Transform>();
		playerShoulder = player.transform.Find("Shoulder").GetComponent<Transform>();
		UIMng = GetComponent<UIManager>();
		
	}

	void Start()
	{
		
		mainCamera.LookAt(playerShoulder);
		CaDistance = mainCamera.position - playerShoulder.position;
		originCaDistance = CaDistance;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			RayDetective();
		}
	}

	void LateUpdate()
	{
		
		ScrollView();
		horizontal = CrossPlatformInputManager.GetAxis("Horizontal");
		vertical = CrossPlatformInputManager.GetAxis("Vertical");
		if (horizontal != 0 || vertical != 0)
		{
			mainCamera.position = playerShoulder.position + CaDistance;
		}
		CameraRotate();
	}

	/// <summary>
	/// 相机缩放控制
	/// </summary>
	void ScrollView()
	{
		if (isThirdPerson&& Input.GetAxis("Mouse ScrollWheel") * scrollSpeed!=0)
		{
			distance = CaDistance.magnitude;
			distance -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
			distance = Mathf.Clamp(distance, 3, 12);//相机缩放距离
			CaDistance = CaDistance.normalized * distance;
			mainCamera.position = playerShoulder.position + CaDistance;
		}		
	}

	/// <summary>
	/// 相机旋转控制
	/// </summary>
	void CameraRotate()
	{
		if (isThirdPerson)
		{
			mainCamera.LookAt(playerShoulder);
		}
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
			if (isThirdPerson)
			{
				if (x > 80 && x < 355)
				{
					mainCamera.position = pos;
					mainCamera.rotation = rot;
				}
			}
			else
			{
				if (x > 80 && x < 320)
				{
					mainCamera.position = pos;
					mainCamera.rotation = rot;
				}
			}

			CaDistance = mainCamera.position - playerShoulder.position;
		}
		
	}

	/// <summary>
	/// 射线检测（左键点击）
	/// </summary>
	private void RayDetective()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit))
		{
			if (hit.transform.tag == "Building"||hit.transform.tag=="Web")
			{
				switch (hit.transform.name)
				{	
					//进入不同建筑
					case "银行":
						LoadScene(TranslateWord("银行"));			
						break;
					case "证券":
						LoadScene(TranslateWord("证券"));
						break;
					case "保险":
						LoadScene(TranslateWord("保险"));
						break;
					case "外汇市场":
						LoadScene(TranslateWord("外汇市场"));
						break;
					case "衍生品市场":
						LoadScene(TranslateWord("衍生品市场"));
						break;
					case "信用评级":
						LoadScene(TranslateWord("信用评级"));
						break;
					//弹出不同页面
					case "证券市场认知":
						Debug.LogWarning("弹出证券市场认知页面");
						Application.ExternalCall("SayHello", "弹出证券市场认知页面");
						break;
					case "证券产品认知":
						Debug.LogWarning("弹出证券产品认知页面");
						Application.ExternalCall("SayHello", "弹出证券产品认知页面");
						break;
					case "证券信息":
						Debug.LogWarning("弹出证券信息页面");
						Application.ExternalCall("SayHello", "弹出证券信息页面");
						break;
					case "证券技术形态分析":
						Debug.LogWarning("弹出证券技术形态分析页面");
						Application.ExternalCall("SayHello", "弹出证券技术形态分析页面");
						break;
					case "证券技术指标分析":
						Debug.LogWarning("弹出证券技术指标分析页面");
						Application.ExternalCall("SayHello", "弹出证券技术指标分析页面");
						break;
					case "组合管理":
						Debug.LogWarning("弹出组合管理页面");
						Application.ExternalCall("SayHello", "弹出组合管理页面");
						break;
					case "华泰证券模拟交易系统":
						Debug.LogWarning("弹出华泰证券模拟交易系统页面");
						Application.ExternalCall("SayHello", "华泰证券模拟交易系统");
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
			SceneManager.LoadScene(SceneName);
			Destroy(player.gameObject);
			Destroy(mainCamera.gameObject);
			Destroy(canvas.gameObject);
			Destroy(gameManager.gameObject);
			Destroy(eventSystem.gameObject);

			//GameObject[] objs = FindObjectsOfType(typeof(GameObject)) as GameObject[];
			//foreach (var item in objs)
			//{
			//	if (item.GetComponent<DontDestroyOnLoad>() != null)
			//	{
			//		Destroy(item.gameObject);
			//	}
			//}

			PostProcessingBehaviour ppb = mainCamera.gameObject.GetComponent<PostProcessingBehaviour>();
			if (ppb != null)
			{
				Destroy(ppb);
			}
			currentScName = TranslateWord(SceneName);
			UIMng.ShowLittleMap();
			UIMng.HideMapBtn();
			UIMng.OnCloseMapBG();
			UIMng.ShowQuitBtn();
			return;
			
		}
		else if (SceneName=="Stock")
		{
			UIMng.ClickFirstPersonBtn();
			player.position = new Vector3(-2, 0.2f, -17);
			mainCamera.rotation = Quaternion.Euler(new Vector3(1.2f, 2f, 0));
			currentScName = TranslateWord(SceneName);
			PostProcessingBehaviour temp = mainCamera.gameObject.GetComponent<PostProcessingBehaviour>();
			if (temp==null)
			{
				PostProcessingBehaviour ppb = mainCamera.gameObject.AddComponent<PostProcessingBehaviour>();
				ppb.profile = securityProfile;
			}	
			UIMng.HideLittleMap();
			UIMng.ShowMapBtn();
			UIMng.OnCloseMapBG();
			UIMng.HideQuitBtn();
			
		}
		else
		{
			player.position = Vector3.zero;
			currentScName = TranslateWord(SceneName);
			PostProcessingBehaviour ppb = mainCamera.gameObject.GetComponent<PostProcessingBehaviour>();
			if (ppb != null)
			{
				Destroy(ppb);
			}
			UIMng.HideLittleMap();
			UIMng.ShowMapBtn();
			UIMng.OnCloseMapBG();
			UIMng.HideQuitBtn();
			
		}
		SceneManager.LoadScene(SceneName);
		UIMng.UpdateSceneName(currentScName);
		mainCamera.position = playerShoulder.position + CaDistance;
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

	public void ThirdPersonToFirst()
	{
		CaDistance = new Vector3(0, 0, 0);
		player.Find("EthanBody").gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
		player.Find("EthanGlasses").gameObject.GetComponent<SkinnedMeshRenderer>().enabled = false;
		isThirdPerson = false;
		mainCamera.position = playerShoulder.position + CaDistance;
	}

	public void FirstPersonToThird()
	{
		CaDistance = originCaDistance;
		player.Find("EthanBody").gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
		player.Find("EthanGlasses").gameObject.GetComponent<SkinnedMeshRenderer>().enabled = true;
		isThirdPerson = true;
		mainCamera.position = playerShoulder.position + CaDistance;
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
