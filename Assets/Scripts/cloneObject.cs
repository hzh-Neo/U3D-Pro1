using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabitInit : MonoBehaviour
{
    [Header("数量")]
    public int ObjectNum = 25;

    public GameObject obj;

    [Header("方圆多大内生成")]
    public float moveArea = 100;

    public bool isRenderScale;

    private Vector3[] vectorDis = new Vector3[2];

    // Start is called before the first frame update
    void Start()
    {
        vectorDis[0] = transform.position - new Vector3(moveArea, 0, moveArea);
        vectorDis[1] = transform.position + new Vector3(moveArea, 0, moveArea);
        initObj();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void initObj()
    {
        for (int i = 0; i < ObjectNum; i++)
        {
            GameObject rabiit = newRabit();
            rabiit.transform.position = GenerateRandomVector3InBox(vectorDis[0], vectorDis[1]);
           
            if (isRenderScale)
            {
                float bigNum = Random.Range(0, 6);
                rabiit.transform.localScale = new Vector3(bigNum, bigNum, bigNum);
            }
            else
            {
                rabiit.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    Vector3 GenerateRandomVector3InBox(Vector3 c1, Vector3 c2)
    {
        float minX = Mathf.Min(c1.x, c2.x);
        float maxX = Mathf.Max(c1.x, c2.x);
        float minY = Mathf.Min(c1.y, c2.y);
        float maxY = Mathf.Max(c1.y, c2.y);
        float minZ = Mathf.Min(c1.z, c2.z);
        float maxZ = Mathf.Max(c1.z, c2.z);

        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);
        float z = Random.Range(minZ, maxZ);

        return new Vector3(x, 0, z);
    }

    private GameObject newRabit()
    {
        GameObject clonedRabit = Instantiate(obj, obj.transform.parent);
        return clonedRabit;
    }
}
