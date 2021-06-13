using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    [SerializeField]
    private float perception;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float horizontalClamp;
    private GameObject target;
    private static List<GameObject> ghosts = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        ghosts.Add(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 targetV = new Vector2(target.transform.position.x, target.transform.position.z);
        Vector2 myV = new Vector2(transform.position.x, transform.position.z);
        Vector2 delta = targetV - myV;
        float dist = delta.magnitude;

        if (dist > perception) return;

        myV += delta.normalized * Mathf.Max(speed, (speed / (dist + 0.01f)));
        myV.x = Mathf.Clamp(myV.x, targetV.x - horizontalClamp, targetV.x + horizontalClamp);

        transform.position = new Vector3(myV.x, transform.position.y, myV.y);
    }
}
