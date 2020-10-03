using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreAnimation : MonoBehaviour {

	[SerializeField] private GameObject matchParticles;

	private Manager manager;
	private ParticleSystem.Burst burst;
	private ParticleSystem particle;

	public void DestroyGameObject(){
	
		Object.Destroy(transform.parent.gameObject);
	
	}

	void Awake(){

		manager = GameObject.FindObjectOfType<Manager> ();

		if (manager.GetMatchSequence() <= 5) {
			SetBurstCount (5f);
		} else if (manager.GetMatchSequence() >= 6 && manager.GetMatchSequence() <= 15) {
			SetBurstCount (10f);
		} else if (manager.GetMatchSequence() >= 16) {
			SetBurstCount (15f);
		}

	}

	private void SetBurstCount (float count){

		burst = matchParticles.GetComponent<ParticleSystem> ().emission.GetBurst (0);
		burst.count = count;
		matchParticles.GetComponent<ParticleSystem> ().emission.SetBurst (0, burst);

	}

}
