using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingObjects_script : MonoBehaviour
{
    private int num = 2;
    public Object plane;
    public Object barrel;
    public Object house;
    private Vector3 dir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

   
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("collision");
        print("collision");
        Destroy(other.gameObject);
        num++;
        dir = new Vector3(num*100,0,0);
        GameObject.Instantiate(plane, dir, Quaternion.identity);

        dir = new Vector3(num * 100, 11.8f, 15f);
        GameObject.Instantiate(house, dir, Quaternion.Euler(270, 0, 0));

        dir = new Vector3(num * 100, 11.8f, -15f);
        GameObject.Instantiate(house, dir, Quaternion.Euler(270, 180, 0));

        for (int i = 0; i <= 4; i++) {


            dir = new Vector3(num * 100 + 20*i -50 , 0, Random.Range(-11f,11f));

            GameObject.Instantiate(barrel,dir, Quaternion.Euler(-90,0,0));
        }
        

    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
