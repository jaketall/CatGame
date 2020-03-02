using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointerController : MonoBehaviour
{
    public Material dotMaterial;
    public float range;
    private GameObject dot;
    private PowerController.Powers powers;
    private bool isLaserOn;

    public void setLaser(bool state)
    {
        isLaserOn = state;
        dot.SetActive(isLaserOn);
    }

    // Start is called before the first frame update
    void Start()
    {
        powers = this.gameObject.GetComponent<PowerController>().powers;
        dot = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        Destroy(dot.GetComponent<Collider>());
        dot.transform.localScale = new Vector3(1, .1f, 1);
        dot.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(powers.laserPointer && !isLaserOn)
            setLaser(true);
        else if(isLaserOn && !powers.laserPointer)
            setLaser(false);
        
        if(isLaserOn)
        {
            float angle = transform.eulerAngles.y;
            dot.transform.position = new Vector3(
                transform.position.x + transform.forward.x*range,
                2,
                transform.position.z + transform.forward.z*range
                );

            Debug.Log("catX: " + transform.position.x
                + ", dot.x: " + dot.transform.position.x);
        }
            
    }
}
