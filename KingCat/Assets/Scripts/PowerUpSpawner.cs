using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public string tagToFollow;
    private GameObject[] targets;
    
    public Color mouseLightColor;
    public Color mouseTintColor;
    public Color mouseSparkleColor;
    public float mouseShellSize;
    public float mousePartSize;

    public Color clawLightColor;
    public Color clawTintColor;
    public Color clawSparkleColor;
    public float clawShellSize;
    public float clawPartSize;

    public Color laserLightColor;
    public Color laserTintColor;
    public Color laserSparkleColor;
    public float laserShellSize;
    public float laserPartSize;

    public Color shieldLightColor;
    public Color shieldTintColor;
    public Color shieldSparkleColor;
    public float shieldShellSize;
    public float shieldPartSize;

    public Color shoesLightColor;
    public Color shoesTintColor;
    public Color shoesSparkleColor;
    public float shoesShellSize;
    public float shoesPartSize;

    public Color targetLightColor;
    public Color targetTintColor;
    public Color targetSparkleColor;
    public float targetShellSize;
    public float targetPartSize;

    public Color whistleLightColor;
    public Color whistleTintColor;
    public Color whistleSparkleColor;
    public float whistleShellSize;
    public float whistlePartSize;


    GameObject spawnPowerUp(Vector3 position, float partSize, float shellSize, Color lightColor, Color tintColor, Color sparkleColor)
    {
        GameObject powerUp = Instantiate(powerUpPrefab);
		targets = GameObject.FindGameObjectsWithTag(tagToFollow);
        powerUp.AddComponent<CollectMe>().targets=targets;

        //update transform
        powerUp.transform.position = position;
        powerUp.transform.localScale = Vector3.one;
        powerUp.transform.eulerAngles = Vector3.zero;

        //update transforms of each child
        foreach (Transform child in powerUp.transform)
        {
            child.position = position;
            child.eulerAngles = Vector3.zero;

            if(child.gameObject.name == "Icon")
            {
                var sampleM = child.gameObject.GetComponent<Renderer>();
                sampleM.material = Instantiate(sampleM.material);
                sampleM.sharedMaterial.SetColor("_Color", sparkleColor);

                child.localScale = Vector3.one * partSize;
            }
            else if(child.gameObject.name == "Light Emitter")
            {
                //Now change the light color                                                      
                Light l = child.GetComponent<Light>();
                l.color = lightColor;
            }
            else if(child.gameObject.name == "Tint Sphere")
            {
                child.localScale = Vector3.one * shellSize;

                var sampleM = child.gameObject.GetComponent<Renderer>();
                sampleM.sharedMaterial = Instantiate(sampleM.sharedMaterial);
                sampleM.sharedMaterial.SetColor("_TintColor", tintColor);
                
            }
            else if(child.gameObject.name == "Foggy Sphere")
            {
                child.localScale = Vector3.one * shellSize*0.9f;
            }
        }

        powerUp.SetActive(true);
        return powerUp;
    }

    
    //make functions for other scripts to call
    GameObject spawnMousePowerUp(Vector3 position)
    {
        return spawnPowerUp(position, mousePartSize, mouseShellSize, mouseLightColor, mouseTintColor, mouseSparkleColor);
    }
    GameObject spawnClawPowerUp(Vector3 position)
    {
        return spawnPowerUp(position, clawPartSize, clawShellSize, clawLightColor, clawTintColor, clawSparkleColor);
    }
    GameObject spawnLaserPowerUp(Vector3 position)
    {
        return spawnPowerUp(position, laserPartSize, laserShellSize, laserLightColor, laserTintColor, laserSparkleColor);
    }
    GameObject spawnShieldPowerUp(Vector3 position)
    {
        return spawnPowerUp(position, shieldPartSize, shieldShellSize, shieldLightColor, shieldTintColor, shieldSparkleColor);
    }
    GameObject spawnShoesPowerUp(Vector3 position)
    {
        return spawnPowerUp(position, shoesPartSize, shoesShellSize, shoesLightColor, shoesTintColor, shoesSparkleColor);
    }
    GameObject spawnTargetPowerUp(Vector3 position)
    {
        return spawnPowerUp(position, targetPartSize, targetShellSize, targetLightColor, targetTintColor, targetSparkleColor);
    }
    GameObject spawnWhistlePowerUp(Vector3 position)
    {
        return spawnPowerUp(position, whistlePartSize, whistleShellSize, whistleLightColor, whistleTintColor, whistleSparkleColor);
    }


    // Start is called before the first frame update
    void Start()
    {
        float start = -50;
        spawnMousePowerUp(new Vector3(start, 5, 0));
        start += 5;
        spawnWhistlePowerUp(new Vector3(start, 5, 0));
        start += 5;
        spawnClawPowerUp(new Vector3(start, 5, 0));
        start += 5;
        spawnLaserPowerUp(new Vector3(start, 5, 0));
        start += 5;
        spawnShieldPowerUp(new Vector3(start, 5, 0));
        start += 5;
        spawnShoesPowerUp(new Vector3(start, 5, 0));
        start += 5;
        spawnTargetPowerUp(new Vector3(start, 5, 0));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
