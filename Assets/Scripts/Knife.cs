using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class Knife : MonoBehaviour {
	public Rigidbody rb;
	public Text comboText;
	public GameObject ui;
	private bool isOnMenu = false;
	bool isOPnAir;

	public float force = 5f;
	public float tourge = 20f;

	private int combo;

	private float timeWWSF;

	private Vector2 startSwipe;
	private Vector2 endSwipe;

	public UnityEvent fail;	
	public UnityEvent inWood;
	public AudioSource slash;

	// Use this for initialization
	void Start () {
		this.enabled = true;
		combo = -1;
		Time.timeScale = 1f;
	}
	
	// Update is called once per frame
	void Update () {		 
		if(!rb.isKinematic)return;
		if(Input.GetMouseButtonDown(0)){
			startSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
		}
		if(Input.GetMouseButtonUp(0)){
			endSwipe = Camera.main.ScreenToViewportPoint(Input.mousePosition);
			Swipe();
		}
		comboText.text = combo.ToString();
	}

	void Swipe(){
		if(startSwipe.y > endSwipe.y) return;
		if(startSwipe == endSwipe) return;
		slash.Play();
		isOPnAir = true;
		rb.isKinematic = false;
		timeWWSF = Time.time;
		Vector2 swipe = endSwipe - startSwipe;
		rb.AddForce(swipe * force, ForceMode.Impulse);
		rb.AddTorque(0f, 0f, (endSwipe.x > startSwipe.x)
							?tourge * force
							:-tourge * force,
		 ForceMode.Impulse);
	}

	
	void OnTriggerEnter(Collider col)
	{
		if(col.tag == "Wood")
		{
			isOPnAir = false;
			slash.Stop();
			inWood.Invoke();		
			rb.isKinematic = true;
			combo++;
			if(combo > PlayerPrefs.GetInt("bestCombo")){
				PlayerPrefs.SetInt("bestCombo", combo);
			}
		}
		else StartCoroutine(LoadCRScene());
	}

	void OnCollisionEnter()
	{	
			
		float timeInAir = Time.time - timeWWSF;
		if(!rb.isKinematic && timeInAir >= .05f){		
			isOPnAir = false;
			slash.Stop();
			StartCoroutine(LoadCRScene());			
		}			
	}

	IEnumerator LoadCRScene()
	{	fail.Invoke();	
		yield return new WaitForSeconds(1f);
		Restart();
	}
	
	void Restart(){
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}	

	public void Menu(){
		SoundManager.instance.PlaySingle();
		Time.timeScale = 1f;
		SceneManager.LoadScene("Menu");
	}

	public void Toggle(){
		isOnMenu = !isOnMenu;
		if(isOnMenu && isOPnAir) slash.Stop();
		else if(!isOnMenu && isOPnAir) slash.Play();		
		SoundManager.instance.PlaySingle();		
		ui.SetActive(!ui.activeSelf);
		if(ui.activeSelf){
			Time.timeScale = 0f;
		} else Time.timeScale = 1f;
		this.enabled = !this.enabled;
	}
}
