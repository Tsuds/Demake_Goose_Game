using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockedCamera : MonoBehaviour
{
	private Camera cam;
	public TriggerData[] camPos;
	public TriggerData[] triggerPos;
	private int currentTrig;

    // Start is called before the first frame update
    void Start()
    {
		cam = GetComponent<Camera>();
        for (int i = 0; i < triggerPos.Length; i++)
		{			
			triggerPos[i].orderData = i;
			camPos[i].orderData = i;
		}
		cam.transform.position = camPos[0].transform.position;

	}
	
	public void CameraUpdate(GameObject t)
	{
		int j = t.GetComponent<TriggerData>().orderData;
		if (j != currentTrig)
		{
			cam.transform.position = camPos[j].transform.position;
			currentTrig = j;
		}
	}
}
