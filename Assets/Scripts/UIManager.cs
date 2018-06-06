using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Transform mapBG;
	public Transform mapBtn;
	public Transform littleMap;
	public Transform quitUI;
	public Transform quitBtn;
	public Text sceneNameText;
	private GameManager gameMng;


	void Awake()
	{
		gameMng = GetComponent<GameManager>();
	}



	/// <summary>
	/// 打开地图
	/// </summary>
	public void OnMapBtn()
	{
		mapBG.gameObject.SetActive(true);
	}

	/// <summary>
	/// 关闭地图
	/// </summary>
	public void OnCloseMapBG()
	{
		mapBG.gameObject.SetActive(false);
	}

	/// <summary>
	/// 显示地图按钮
	/// </summary>
	public void ShowMapBtn()
	{
		mapBtn.gameObject.SetActive(true);
		if (littleMap.gameObject.activeSelf==true)
		{
			littleMap.gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// 隐藏地图按钮
	/// </summary>
	public void HideMapBtn()
	{
		mapBtn.gameObject.SetActive(false);	
	}

	/// <summary>
	/// 显示小地图
	/// </summary>
	public void ShowLittleMap()
	{
		littleMap.gameObject.SetActive(true);
		if (mapBtn.gameObject.activeSelf==true)
		{
			mapBtn.gameObject.SetActive(false);
		}
	}

	/// <summary>
	/// 隐藏小地图
	/// </summary>
	public void HideLittleMap()
	{
		littleMap.gameObject.SetActive(false);
	}

	/// <summary>
	/// 进入其他场景按钮
	/// </summary>
	public void OnOtherSceneBtn()
	{
		var ClickButton = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject;
		Text text = ClickButton.transform.Find("Text").GetComponent<Text>();
		gameMng.LoadScene(gameMng.TranslateWord(text.text));
	}

	/// <summary>
	/// 更新场景标题名
	/// </summary>
	public void UpdateSceneName(string name)
	{
		sceneNameText.text = name;
	}

	/// <summary>
	/// 显示退出确认界面
	/// </summary>
	public void ShowQuitUI()
	{
		quitUI.gameObject.SetActive(true);
	}

	/// <summary>
	/// 隐藏退出确认界面
	/// </summary>
	public void HideQuitUI()
	{
		quitUI.gameObject.SetActive(false);
	}

	/// <summary>
	/// 显示退出按钮
	/// </summary>
	public void ShowQuitBtn()
	{
		quitBtn.gameObject.SetActive(true);
	}

	/// <summary>
	/// 隐藏退出按钮
	/// </summary>
	public void HideQuitBtn()
	{
		quitBtn.gameObject.SetActive(false);
	}
}
