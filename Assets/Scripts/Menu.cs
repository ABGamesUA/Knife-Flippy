using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {
	public GameObject ui;
	public GameObject uiInfo;
	public Text comboText;
	public AudioSource click;
	void Start(){
		comboText.text = "Your BEST Combo " + PlayerPrefs.GetInt("bestCombo").ToString();
	}

	public void Play(){
		SoundManager.instance.PlaySingle();
		StartCoroutine(LoadScene());	
	}

	public void Exit(){
		Application.Quit();
	}

	public void Info()
	{
		SoundManager.instance.PlaySingle();
		ui.SetActive(!ui.activeSelf);
		uiInfo.SetActive(!uiInfo.activeSelf);
	}
	
	IEnumerator LoadScene()
	{
		yield return new WaitForSeconds(1f);
		SceneManager.LoadScene("Main");
	}
	
}
