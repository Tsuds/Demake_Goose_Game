using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerData : MonoBehaviour
{
	public LockedCamera cam;
	public int orderData;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			cam.CameraUpdate(this.gameObject);
		}
	}

}
