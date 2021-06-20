using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerspectiveController : MonoBehaviour
{
    [SerializeField]
    private float minScaleSprite;
    [SerializeField]
    private float maxScaleSprite;
    [SerializeField]
    private float minScaleDecal;
    [SerializeField]
    private float maxScaleDecal;
    [SerializeField]
    private float farDistance;
    [SerializeField]
    private float nearDistance;
    [SerializeField]
    private float epsilon;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        GameObject[] sprites = GameObject.FindGameObjectsWithTag("Sprite");
        GameObject[] decals = GameObject.FindGameObjectsWithTag("Decal");
        Vector3 myPos = transform.position;
        float worldFar = myPos.z + farDistance;
        float worldNear = myPos.z - nearDistance;

        foreach (GameObject sprite in sprites)
        {
            Vector3 spritePos = sprite.transform.position;

            //scale
            Vector3 newScale = sprite.transform.localScale;
            float t = Mathf.InverseLerp(myPos.z + epsilon, worldNear, spritePos.z);
            newScale.x = newScale.y = Mathf.Lerp(minScaleSprite, maxScaleSprite, t);
            sprite.transform.localScale = newScale;

            if (spritePos.z < worldFar && spritePos.z > myPos.z + epsilon)
            {
                //far y
                Vector3 newPosition = spritePos;
                t = Mathf.InverseLerp(worldFar, myPos.z + epsilon, newPosition.z);
                newPosition.y = t * myPos.y;
                sprite.transform.position = newPosition;
            }
            if (spritePos.z < myPos.z - epsilon && spritePos.z > worldNear)
            {
                //near y
                Vector3 newPosition = spritePos;
                t = Mathf.InverseLerp(myPos.z - epsilon, worldNear, newPosition.z);
                newPosition.y = myPos.y - (4 * t * t * myPos.y);
                sprite.transform.position = newPosition;
            }
        }
        foreach (GameObject decal in decals)
        {
            Vector3 decalPos = decal.transform.position;

            //scale
            Vector3 newScale = decal.transform.localScale;
            float t = Mathf.InverseLerp(myPos.z + epsilon, worldNear, decalPos.z);
            newScale.x = Mathf.Lerp(minScaleDecal, maxScaleDecal, t);
            newScale.y = Mathf.Lerp(0, maxScaleDecal, t);
            decal.transform.localScale = newScale;

            if (decalPos.z < worldFar && decalPos.z > myPos.z + epsilon)
            {
                //far y
                Vector3 newPosition = decalPos;
                t = Mathf.InverseLerp(worldFar, myPos.z + epsilon, newPosition.z);
                newPosition.y = t * myPos.y - epsilon;
                decal.transform.position = newPosition;
            }
            if (decalPos.z < myPos.z - epsilon && decalPos.z > worldNear)
            {
                //near y
                Vector3 newPosition = decalPos;
                t = Mathf.InverseLerp(myPos.z - epsilon, worldNear, newPosition.z);
                newPosition.y = myPos.y - (4 * t * t * myPos.y) - epsilon;
                decal.transform.position = newPosition;
            }
        }
    }
}
