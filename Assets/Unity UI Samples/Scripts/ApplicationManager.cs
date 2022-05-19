//============================================================
// ApplicationManager.cs
//======================================================================
// 開発履歴
//
// 2022/05/01 author 竹尾：他アセットの既存スクリプトを改造
// 
//
//======================================================================
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ApplicationManager : MonoBehaviour
{

	private AsyncOperation async;
	public GameObject LoadingUi;
	public GameObject LoadingRoom;
	public Slider Slider;

	public void Quit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#else
		Application.Quit();
#endif
	}

	public void NextScene()
	{
		SceneManager.LoadScene("Game");
	}

	public void LoadNextScene()
	{
		LoadingUi.SetActive(true);
		LoadingRoom.SetActive(true);
		StartCoroutine(LoadScene());
	}

	IEnumerator LoadScene()
	{
		async = SceneManager.LoadSceneAsync("Game");

		while (!async.isDone)
		{
			Slider.value = async.progress;
			yield return null;
		}
	}
}
