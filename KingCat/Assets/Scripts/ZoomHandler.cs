using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZoomHandler : MonoBehaviour
{
	public float minHeight;
	public float maxHeight;
	public int numSteps; //number of levels to zoom to between min and max height; inclusive
	public float deadzone;
	public float zoomTime; //time it takes to zoom between levels
	public string tagToFollow;

   private Transform camTransform;
	private int currStep; //index in array of zoomSteps, below
	private float[] zoomSteps; //calculated heights of each zoom level
	private int isZooming; //-1 means zooming in, 0 means not zooming, +1 means zooming out
	private GameObject[] targets;
	private float camRadius; //Notice: calculated using verticle FOV, usually smaller than horiz, so no worries.
	private float innerCamRadius; //used for checking if should zoom in
   private float startTime; //used as checkmark for starting a zoom

	// Start is called before the first frame update
   void Start()
   {
      zoomSteps = new float[numSteps];
		int step = (int)((maxHeight - minHeight)/numSteps);
		for(int i=0;i<numSteps;i++)
			zoomSteps[i] = step*i + minHeight;
		camTransform = this.gameObject.transform;

		targets = GameObject.FindGameObjectsWithTag(tagToFollow);

		//tan(camera.fieldOfView) = x/z --> x = z*tan()
		camRadius = camTransform.position.y * Mathf.Tan(this.gameObject.GetComponent<Camera>().fieldOfView);

		currStep = 0;
		startTime = 0; //should be overwritten as soon as a new zoom level is requested.
   }

   // Update is called once per frame
   void Update()
   {
		//center camera first
      float minX, maxX, minZ, maxZ;

      minX = maxX = targets[0].transform.position.x;
  		minZ = maxZ = targets[0].transform.position.z;
  		for(int i=1;i<targets.Length;i++)
  		{
  			minX = Math.Min(minX, targets[i].transform.position.x);
  			maxX = Math.Max(maxX, targets[i].transform.position.x);
  			minZ = Math.Min(minZ, targets[i].transform.position.z);
  			maxZ = Math.Max(maxZ, targets[i].transform.position.z);
  		}

  		camTransform.position = new Vector3((maxX-minX)/2+minX, zoomSteps[currStep], (maxZ - minZ) / 2+minZ);
		Debug.Log("Camera radius: " + camRadius);

  		if(isZooming != 0)
  		{
  			Vector3 targVec;
  			targVec = new Vector3(camTransform.position.x, zoomSteps[currStep + isZooming], camTransform.position.z);

 			float fracComplete = (Time.time - startTime) / zoomTime;
  			camTransform.position = Vector3.Slerp(camTransform.position, targVec, fracComplete);
  			if(fracComplete >= 1)
  			{
  				currStep += isZooming;
  				isZooming = 0;
  				camRadius = zoomSteps[currStep] * Mathf.Tan(this.gameObject.GetComponent<Camera>().fieldOfView);
				if(currStep>0)
				{
					innerCamRadius = zoomSteps[currStep-1] * Mathf.Tan(this.gameObject.GetComponent<Camera>().fieldOfView);
				}
  			}
  		}
  		else
  		{
  			//if a player outside deadzone, AKA need to zoom out
  			if ((currStep < numSteps-1) 
			&& ((minX < camTransform.position.x - camRadius * (1-deadzone) 
				|| minZ < camTransform.position.z - camRadius * (1-deadzone))
  			|| (maxX > camTransform.position.x + camRadius * (1-deadzone) 
				|| maxZ > camTransform.position.z + camRadius * (1-deadzone))))
  			{
  				isZooming = 1;
  				startTime = Time.time;
  			}
  			//else if all players inside deadzone*2, AKA need to zoom in
			else if ((currStep > 0)
			&& ((minX > camTransform.position.x - innerCamRadius * (1-deadzone)
			&& minZ > camTransform.position.z - innerCamRadius * (1-deadzone))
  			&& (maxX < camTransform.position.x + innerCamRadius * (1-deadzone) 
			&& maxZ < camTransform.position.z + innerCamRadius * (1-deadzone))))
  			{
  				isZooming = -1;
  				startTime = Time.time;
  			}
  		}
   }
}

