using UnityEngine;
public class Lyskaster : MonoBehaviour
{
    public float speed = 50.0f;
    public float minRotation = 10.0f;
    public float maxRotation = 60.0f;
    public Quaternion initialRotation;
    public float degsToRotate;
    public int direction;
    public Vector3 euler;
    // Start is called before the first frame update
    void Start()
    {
        initialRotation = transform.rotation;
        degsToRotate = Random.Range(10.0f, 60.0f);
        direction = getNonZero();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(speed*Time.deltaTime * direction,0,0);
        euler = transform.rotation.eulerAngles;
        if (Mathf.Abs(euler.x - initialRotation.eulerAngles.x) > degsToRotate)
        {
            //reset the rotation phase
            degsToRotate = Random.Range(10.0f, 60.0f);
            direction = getNonZero();
            initialRotation = transform.rotation;
        }
    }
    int getNonZero()
    {
        int dir = Random.Range(-1, 2);
        while (dir == 0)
        {
            dir = Random.Range(-1, 2);
        }
        return dir;
    }
}