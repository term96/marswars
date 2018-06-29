using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBarrackController : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
    public GameObject prefab;
    private Vector3 base_pos;
    // Use this for initialization
    void Start()
    {
        base_pos = GameObject.FindGameObjectWithTag("EnemyBarrack").transform.position;
        InvokeRepeating("generateUnit", 3f, 10f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Update is called once per frame
    void generateUnit()
    {
        GameObject obj = Instantiate(prefab, new Vector3(base_pos.x + 5, base_pos.y, base_pos.z), Quaternion.identity) as GameObject;
        if (obj.tag == "")
        { }
    }
}
