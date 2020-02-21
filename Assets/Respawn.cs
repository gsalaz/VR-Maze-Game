using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
	[SerializeField] private Transform player;
	[SerializeField] private Transform respawnPoint;

	void OnTriggerExit(Collider other) {
		player.transform.rotation = Quaternion.identity;
		player.transform.position = respawnPoint.transform.position;
	}
}
