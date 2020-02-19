using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ZoomHandler : MonoBehaviour
{
	public float minHeight;
	public float maxHeight;
	public float percentZoomedOut;
	public string tagToFollow;

	private GameObject[] targets;
	private Transform camTransform;
	private float fovVert;
	private float fovHoriz; //calc'd by using fovVert and Aspect Ratio of screen (Screen.width/Screen.height)

	// Start is called before the first frame update
	void Start()
	{
		camTransform = this.gameObject.transform;

		fovVert = this.gameObject.GetComponent<Camera>().fieldOfView;
		fovHoriz = fovVert * this.gameObject.GetComponent<Camera>().aspect; 
		camTransform.position = new Vector3(camTransform.position.x, minHeight, camTransform.position.z); //set camera to minHeight
		
		targets = GameObject.FindGameObjectsWithTag(tagToFollow);
	}

	// Update is called once per frame
	void Update()
	{
		//update camera fovs
		float aspect = this.gameObject.GetComponent<Camera>().aspect; 
		fovVert = this.gameObject.GetComponent<Camera>().fieldOfView;
		//fovHoriz = fovVert * aspect;
		//credit to user andeeeee :: https://forum.unity.com/threads/how-to-calculate-horizontal-field-of-view.16114/
		fovHoriz = (float)(2 * Math.Atan(Mathf.Tan(fovVert * Mathf.Deg2Rad / 2) * aspect) * Mathf.Rad2Deg);

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
		//get cat distance from center of camera
		float catRadX = (maxX-minX)/2;
		float catRadZ = (maxZ-minZ)/2;
		
		//Formula for calc'ing appropriate zoom height (y):
		//tan(fov) = x/y --> x = y*tan() --> y = x/tan(fov)
		float zoomForX = catRadX*(1+percentZoomedOut/100)/Mathf.Tan(fovHoriz);
		float zoomForZ = catRadZ*(1+percentZoomedOut/100)/Mathf.Tan(fovVert);

		camTransform.position = Vector3.Slerp(
				camTransform.position, //current position
				new Vector3( //target position
					catRadX+minX, //center between cats, X
					Mathf.Clamp( //choose a zoom level
						Math.Max(zoomForX, zoomForZ),
						minHeight, 
						maxHeight), 
					catRadZ+minZ), //center between cats, Y
				.1f); //percent complete with zoom
		return;
		
		/*
		if(catRadX/aspect > catRadZ) //cats are closer to horizontal edge
		{

			camTransform.position = Vector3.Slerp(
					camTransform.position, //current position
					new Vector3( //target position
						catRadX+minX, //center between cats, X
						Mathf.Clamp( //choose a zoom level
							catRadX*(1+percentZoomedOut/100)/Mathf.Tan(fovHoriz), 
							minHeight, 
							maxHeight), 
						catRadZ+minZ), //center between cats, Y
					.1f); //percent complete with zoom
		}
		else //vertical edge
		{
			camTransform.position = Vector3.Slerp(
					camTransform.position, //current position
					new Vector3( //target position
						catRadX+minX, //center between cats, X
						Mathf.Clamp( //choose a zoom level
							catRadZ*(1+percentZoomedOut/100)/Mathf.Tan(fovVert), 
							minHeight, 
							maxHeight), 
						catRadZ+minZ), //center between cats, Y
					.1f); //percent complete with zoom
		}
		*/
		//return;

		//welp it looks like the 'levels' were kind of totally unnecessary
		/*

		//camTransform.position = new Vector3((maxX-minX)/2+minX, radiiValues[currRadiusIndex], (maxZ - minZ) / 2+minZ);
		camTransform.position = new Vector3(catRadX+minX, radiiValues[currRadiusIndex]/Mathf.Tan(fovVert), catRadZ+minZ);
		//Debug.Log("In no-zoom state, inCamV: "+innerCamRadiusVert+", catDistZ: "+(maxZ-minZ)
		//+ "\tinCamH: "+innerCamRadiusHoriz+", catDistX: "+(maxX-minX));
		Debug.Log("currRad=("+camRadiusHoriz+","+camRadiusVert+"), catRad=("+catRadX+","+catRadZ+")"
				+ ", innerCam=("+innerCamRadiusHoriz+","+innerCamRadiusVert+")"
				+ ", minX="+minX+",maxX="+maxX+",minZ="+minZ+",maxZ="+maxZ);
		if(isZooming != 0)
		{
			Vector3 targVec;
			targVec = new Vector3(camTransform.position.x, radiiValues[currRadiusIndex + isZooming]/Mathf.Tan(fovVert), camTransform.position.z);
		
			float fracComplete = (Time.time - startTime) / zoomTime;
			camTransform.position = Vector3.Slerp(camTransform.position, targVec, fracComplete);
			if(fracComplete >= 1)
			{
				currRadiusIndex += isZooming;
				isZooming = 0;
				camRadiusVert = radiiValues[currRadiusIndex];
				camRadiusHoriz = camRadiusVert * this.gameObject.GetComponent<Camera>().aspect;
				if(currRadiusIndex>0)
				{
					innerCamRadiusVert = radiiValues[currRadiusIndex-1];
					innerCamRadiusHoriz = innerCamRadiusVert * this.gameObject.GetComponent<Camera>().aspect;
				}
			}
		}
		else //not currently zooming .. should we be?
		{
		
			//	//if a player outside zoomOutBuffer, AKA need to zoom out
			//	if ((currRadiusIndex < numRadiiValues - 1) //if another zoom out level is available
			//	&& ((minX < camTransform.position.x - camRadiusVert * (1-zoomOutBuffer) 
			//		|| minZ < camTransform.position.z - camRadiusVert * (1-zoomOutBuffer))
			//	|| (maxX > camTransform.position.x + camRadiusVert * (1-zoomOutBuffer) 
			//		|| maxZ > camTransform.position.z + camRadiusVert * (1-zoomOutBuffer))))
			//	{
			//		isZooming = 1;
			//		startTime = Time.time;
			//	}
			//	//else if all players inside an inner zoomInBuffer, AKA need to zoom in
			//	else if ((currRadiusIndex > 0) //if another zoom in level is available
			//	&& ((minX > camTransform.position.x - innerCamRadiusHoriz * (1-zoomInBuffer)
			//	&& minZ > camTransform.position.z - innerCamRadiusVert * (1-zoomInBuffer))
			//	&& (maxX < camTransform.position.x + innerCamRadiusHoriz * (1-zoomInBuffer) 
			//	&& maxZ < camTransform.position.z + innerCamRadiusVert * (1-zoomInBuffer))))
			//	{
			//		isZooming = -1;
			//		startTime = Time.time;
			//	}
			
			//zoom out when:
			if((currRadiusIndex < numRadiiValues-1) && (catRadX>camRadiusHoriz*(1-zoomOutBuffer) || catRadZ>camRadiusVert*(1-zoomOutBuffer)))
			{
				isZooming = 1;
				startTime = Time.time;
			}
			//zoom in when:
			if((currRadiusIndex > 0) && (catRadX<camRadiusHoriz*(1-zoomInBuffer) && catRadZ<camRadiusVert*(1-zoomInBuffer)))
			{
				isZooming = -1;
				startTime = Time.time;
			}
		}
		*/
	}
}

