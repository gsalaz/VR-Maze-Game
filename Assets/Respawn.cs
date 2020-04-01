using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Respawn : MonoBehaviour
{
    public AudioClip fall;
	[SerializeField] private Transform respawnPoint;
	public UnityEvent OnDie;
	

	void OnTriggerExit(Collider other) {

		if (other.gameObject.tag == "Player")
		{
			AudioSource.PlayClipAtPoint(fall, transform.position);
			other.transform.rotation = Quaternion.identity;
			other.GetComponent<Rigidbody>().velocity = new Vector3 (0,0,0);
			other.transform.position = respawnPoint.transform.position;
       
            OnDie.Invoke();
            
			
		}
	}
}
