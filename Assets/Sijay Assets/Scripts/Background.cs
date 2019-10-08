using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public GameObject player;
    private Vector3 player_orig;
    private Vector3 orig;
    public float scale;

    // Start is called before the first frame update
    void Start()
    {
        orig = transform.position;
        player_orig = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = orig - new Vector3((player_orig.x - player.transform.position.x) / scale, 0, 0);
    }
}
