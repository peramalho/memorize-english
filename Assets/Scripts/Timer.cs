using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

	[SerializeField] private int levelSeconds;
	[SerializeField] private GameObject panel;
	[SerializeField] private AudioClip tictacSound;
	[SerializeField] private AudioSource audioSource;

	private float initialTime;
	private float timeElapsed;
	private float timeLeft;
	private Slider slider;

	// Use this for initialization
	void Start () {
		slider = GetComponent<Slider> ();
		initialTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (slider.value > 0) {
			timeElapsed = Time.time - initialTime;
			slider.value = 1 - timeElapsed / levelSeconds;
		}

		if (slider.value < 0.20f) {
		
			panel.GetComponent<Image>().color = new Color32 (255, 22, 22, 255);

			if (!audioSource.isPlaying) {
				audioSource.clip = tictacSound;
				audioSource.Play ();
			}
		}

	}

	public void ResetTimer(){
	
		slider.value = 1;
		audioSource.Stop ();
		initialTime = Time.time;
		panel.GetComponent<Image>().color = new Color32 (252, 255, 55, 255);

	}

    public void IncreaseTimer()
    {
        initialTime += 1f;
    }

	public void SetLevelSeconds (int levelSeconds){

		this.levelSeconds = levelSeconds;

	}

}
