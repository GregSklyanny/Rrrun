using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] [Range(1f, 5f)] private float _angularSpeed = 1f;

    [SerializeField] private Transform _target;

    private float _angleY;

    // Start is called before the first frame update
    private void Start()
    {
        _angleY = transform.rotation.y;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z)) _angleY -= _angularSpeed;
        if (Input.GetKey(KeyCode.X)) _angleY += _angularSpeed;

        transform.position = _target.transform.position;
        transform.rotation = Quaternion.Euler(0,_angleY,0);
    }
}
