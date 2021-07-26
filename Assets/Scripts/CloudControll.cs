using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudControll : MonoBehaviour
{

    public float speed;
    private float lifeTime = 5f;
    public float deadTime = 1f;
    private float xPosition;
    public float yPosUp;
    public float yPosDown;

    // Start is called before the first frame update
    void Start()
    {
        xPosition = gameObject.transform.position.x;   
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime);

        lifeTime -= deadTime * Time.deltaTime;
        if(lifeTime <= 0)
        {
            Instantiate(gameObject, new Vector3(xPosition, Random.Range(yPosUp, yPosDown), 0), Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
