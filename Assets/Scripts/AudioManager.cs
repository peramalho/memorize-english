using UnityEngine;

public class AudioManager : MonoBehaviour {

	[SerializeField] private AudioClip gemsSound;
    [SerializeField] private AudioClip lightningSound;
    [SerializeField] private AudioClip potionSound;
    [SerializeField] private AudioClip damageSound;
	[SerializeField] private AudioClip lostSound;
	[SerializeField] private AudioClip buttonSound;
    [SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioSource musicSource;

	public void PlayGemsSound (){
		audioSource.PlayOneShot (gemsSound);
	}

    public void PlayLightningSound()
    {
        audioSource.PlayOneShot(lightningSound);
    }

    public void PlayPotionSound()
    {
        audioSource.PlayOneShot(potionSound);
    }

    public void PlayDamageSound (){
		audioSource.clip = damageSound;
		audioSource.Play ();
	}	

	public void PlayLostSound (){
		audioSource.clip = lostSound;
		audioSource.Play ();
	}	

	public void PlayButtonSound (){
		audioSource.PlayOneShot (buttonSound);
	}

    public void PlayBackgroundMusic (){
		musicSource.clip = Resources.Load("Music/backgroundMusic") as AudioClip;
		musicSource.Play ();
	} 

	public void MuteSound (){
		musicSource.volume = 0f;
	}

    public void UnMuteSound (){
		musicSource.volume = 0.12f;
    }

    void Awake() { //Singleton
		int numGameSessions = FindObjectsOfType<AudioManager>().Length;
		if (numGameSessions > 1)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}  //Singleton

	void Start (){
		PlayBackgroundMusic ();
	}
}
