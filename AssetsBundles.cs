/*
 AssetsBundles的创建和加载

 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetsBundles : MonoBehaviour {

	//创建AssetsBundles
	[MenuItem("Tools/BuildingAssetsBundles")]
	static void BuildAssetBundle()
	{
		string path = "Assets/AB";
		if (!Directory.Exists(path))
		{
			Directory.CreateDirectory(path);
		}
		BuildPipeline.BuildAssetBundles(path, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
		AssetDatabase.Refresh();
	}

	//加载AssetsBundles

	IEnumerator Load()
	{
		string CubePath = "file://" + Application.dataPath + "/AB/prefabs.ab";
		WWW w = new WWW(CubePath);
		yield return w;
		print("LoadIsDone");
		Cube = w.assetBundle.LoadAsset("Cube") as GameObject;
		Instantiate(Cube);
	}

	IEnumerator LoadMat()
	{
		//先Load被依赖的包
		//string MatPath = "file://" + Application.dataPath + "/AB/testab.ab";
		//WWW mw = new WWW(MatPath);
		//yield return mw;
		//mw.assetBundle.LoadAllAssets();
		//mw.assetBundle.LoadAllAssetsAsync();
		//StartCoroutine("Load");

		//直接Load总体依赖关系
		string MainManifestPath = "file://" + Application.dataPath + "/AB/AB";
		WWW mainW = new WWW(MainManifestPath);
		yield return mainW;
		AssetBundleManifest mf = mainW.assetBundle.LoadAsset("AssetBundleManifest") as AssetBundleManifest;
		List<string> manifest = mf.GetAllDependencies("prefabs.ab").ToList();
		foreach (var item in manifest)
		{
			string temp = "file://" + Application.dataPath + "/AB/" + item;
			WWW tW = new WWW(temp);
			yield return tW;
			tW.assetBundle.LoadAllAssets();
		}
		StartCoroutine(Load());
	}
}
