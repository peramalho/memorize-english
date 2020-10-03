using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	private AudioManager audioManager;

	public void StartButton(){
        SceneManager.LoadScene(1);
        audioManager.PlayButtonSound();

	}

	public void TryAgainButton(){
		SceneManager.LoadScene(1);
		audioManager.PlayButtonSound ();
	}

	public void MenuButton(){
		SceneManager.LoadScene(0);
		audioManager.PlayButtonSound ();
	}

	public void InfoButton(){
		SceneManager.LoadScene(3);
		audioManager.PlayButtonSound ();
	}

	public void ConfigButton(){
		SceneManager.LoadScene(4);
		audioManager.PlayButtonSound ();
	}

    public void CreditsButton()
    {
        SceneManager.LoadScene(5);
        audioManager.PlayButtonSound();
    }

    void Awake(){
	
		audioManager = GameObject.FindObjectOfType<AudioManager>();

	}
		
}