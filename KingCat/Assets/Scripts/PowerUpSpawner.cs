using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PowerUpSpawner : MonoBehaviour
{
    public GameObject powerUpPrefab;
    public string tagToFollow;
    private GameObject[] targets;

    public float timeBetweenSpawns;
    private float timeToSpawn=0;

    public int powerUpCount = 0;
    public int maxPowerUpCount;
    
    [Serializable]
    public class Area
    {
        public string name;
        public float minX;
        public float maxX;
        public float minZ;
        public float maxZ;
    }
    public List<Area> areas;

    public Color speedBoostLightColor;
    public Color speedBoostTintColor;
    public Color speedBoostSparkleColor;
    public float speedBoostShellSize;
    public float speedBoostPartSize;

    public Color stunBoostLightColor;
    public Color stunBoostTintColor;
    public Color stunBoostSparkleColor;
    public float stunBoostShellSize;
    public float stunBoostPartSize;

    public Color laserLightColor;
    public Color laserTintColor;
    public Color laserSparkleColor;
    public float laserShellSize;
    public float laserPartSize;

    public Color stunImmunityLightColor;
    public Color stunImmunityTintColor;
    public Color stunImmunitySparkleColor;
    public float stunImmunityShellSize;
    public float stunImmunityPartSize;

    public Color bootsLightColor;
    public Color bootsTintColor;
    public Color bootsSparkleColor;
    public float bootsShellSize;
    public float bootsPartSize;

    public Color hairballBoostLightColor;
    public Color hairballBoostTintColor;
    public Color hairballBoostSparkleColor;
    public float hairballBoostShellSize;
    public float hairballBoostPartSize;

    public Color whistleLightColor;
    public Color whistleTintColor;
    public Color whistleSparkleColor;
    public float whistleShellSize;
    public float whistlePartSize;

    private List<Func<Vector3, GameObject>> funcs;

    GameObject spawnPowerUp(Vector3 position, float partSize, float shellSize, Color lightColor, Color tintColor, Color sparkleColor, string type)
    {
        powerUpCount++;
        GameObject powerUp = Instantiate(powerUpPrefab);
		targets = GameObject.FindGameObjectsWithTag(tagToFollow);
        var collectMe = powerUp.AddComponent<CollectMe>();
        collectMe.targets=targets;
        collectMe.type=type;
        collectMe.spawner = this;

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
    GameObject spawnSpeedBoostPowerUp(Vector3 position)
    {
        return spawnPowerUp(position, speedBoostPartSize, speedBoostShellSize, speedBoostLightColor, speedBoostTintColor, speedBoostSparkleColor, "speedBoost");
    }
    GameObject spawnStunBoostPowerUp(Vector3 position)
    {
        return spawnPowerUp(position, stunBoostPartSize, stunBoostShellSize, stunBoostLightColor, stunBoostTintColor, stunBoostSparkleColor, "stunBoost");
    }
    GameObject spawnLaserPowerUp(Vector3 position)
    {
        return spawnPowerUp(position, laserPartSize, laserShellSize, laserLightColor, laserTintColor, laserSparkleColor, "laserPointer");
    }
    GameObject spawnStunImmunityPowerUp(Vector3 position)
    {
        return spawnPowerUp(position, stunImmunityPartSize, stunImmunityShellSize, stunImmunityLightColor, stunImmunityTintColor, stunImmunitySparkleColor, "stunImmunity");
    }
    GameObject spawnBootsPowerUp(Vector3 position)
    {
        return spawnPowerUp(position, bootsPartSize, bootsShellSize, bootsLightColor, bootsTintColor, bootsSparkleColor, "boots");
    }
    GameObject spawnHairballBoostPowerUp(Vector3 position)
    {
        return spawnPowerUp(position, hairballBoostPartSize, hairballBoostShellSize, hairballBoostLightColor, hairballBoostTintColor, hairballBoostSparkleColor,"hairballBoost");
    }
    GameObject spawnWhistlePowerUp(Vector3 position)
    {
        return spawnPowerUp(position, whistlePartSize, whistleShellSize, whistleLightColor, whistleTintColor, whistleSparkleColor, "whistle");
    }

    GameObject spawnRandomPowerUp(Vector3 position)
    {
        return funcs[UnityEngine.Random.Range(0, funcs.Count)](position);
    }

    // Start is called before the first frame update
    void Start()
    {
        //float start = -50;
        //spawnSpeedBoostPowerUp(new Vector3(start, 5, 0));
        //start += 5;
        //spawnWhistlePowerUp(new Vector3(start, 5, 0));
        //start += 5;
        //spawnStunBoostPowerUp(new Vector3(start, 5, 0));
        //start += 5;
        //spawnLaserPowerUp(new Vector3(start, 5, 0));
        //start += 5;
        //spawnStunImmunityPowerUp(new Vector3(start, 5, 0));
        //start += 5;
        //spawnBootsPowerUp(new Vector3(start, 5, 0));
        //start += 5;
        //spawnHairballBoostPowerUp(new Vector3(start, 5, 0));

        funcs = new List<Func<Vector3, GameObject>>();
        funcs.Add(spawnSpeedBoostPowerUp);
        funcs.Add(spawnSpeedBoostPowerUp);
        funcs.Add(spawnWhistlePowerUp);
        funcs.Add(spawnStunBoostPowerUp);
        funcs.Add(spawnLaserPowerUp);
        funcs.Add(spawnStunImmunityPowerUp);
        funcs.Add(spawnBootsPowerUp);
        funcs.Add(spawnHairballBoostPowerUp);
    }

    // Update is called once per frame
    void Update()
    {
        if(powerUpCount >= maxPowerUpCount)
            return;

        timeToSpawn += Time.deltaTime;
        if(timeToSpawn >= timeBetweenSpawns)
        {
            Vector3 position = new Vector3();

            Area targetArea = areas[UnityEngine.Random.Range(0,areas.Count)];
            Collider[] nearbyObjects;
            while(true)
            {
                position.x = UnityEngine.Random.Range(targetArea.minX, targetArea.maxX);
                position.y = 5;
                position.z = UnityEngine.Random.Range(targetArea.minZ, targetArea.maxZ);

                nearbyObjects = Physics.OverlapSphere(position, 10); //probably shouldn't harcode this radius...
                //Debug.Log(nearbyObjects.Length);
                //foreach (Collider c in nearbyObjects)
                //    Debug.Log(c.gameObject.name);
                if(nearbyObjects.Length == 1) //only touches 'Plane' (floor) -- not walls, players, obstacles, etc
                    break;
            }

            spawnRandomPowerUp(position);
            timeToSpawn = 0;
        }
    }
}
