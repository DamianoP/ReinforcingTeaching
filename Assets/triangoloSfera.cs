using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triangoloSfera : MonoBehaviour
{
	private GameObject figure;
    private float z = 0.0001f;
    private int increment=1;
    private float transformSpeed=0.6f;
    // Start is called before the first frame update

    void Start()
    {
    	figure= gameObject;
        
    }

    // Update is called once per frame
    void Update()
    {
    	if(increment==1)
    		z+=Time.deltaTime*transformSpeed;
    	else
    		z-=Time.deltaTime*transformSpeed;
    	if(z>2)
    		increment=0;
    	if(z<0)	
    		increment=1;
        transform.localScale = new Vector3(2f, 2f, z);
    }
}
