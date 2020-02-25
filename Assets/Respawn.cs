using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
	//[SerializeField] private AudioSource fall;
	[SerializeField] private Transform respawnPoint;
	

	void OnTriggerExit(Collider other) {

		if (other.gameObject.tag == "Player")
		{
			//fall.Play();
			other.transform.rotation = Quaternion.identity;
			other.GetComponent<Rigidbody>().velocity = new Vector3 (0,0,0);
			other.transform.position = respawnPoint.transform.position;
			
		}
	}
}
