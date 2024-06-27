using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.Timeline.AnimationPlayableAsset;

public class RabitMoveController : MonoBehaviour
{
    [Header("移动速度")]
    public float moveSpeed = 1;

    [Header("移动范围")]
    public float moveArea = 50;

    public float timeDelay = 5f;

    public Animator anim;

    public bool moving
    {
        get
        {
            return anim.GetBool("moving");
        }
        set
        {
            anim.SetBool("moving", value);
        }
    }

    public bool running
    {
        get
        {
            return anim.GetBool("running");
        }
        set
        {
            anim.SetBool("running", value);
        }
    }

    public bool death
    {
        get
        {
            return anim.GetBool("death");
        }
        set
        {
            anim.SetBool("death", value);
        }
    }

    public float groundDistance = 0.4f;

    public LayerMask groundMask;

    private Vector3[] vectorDis = new Vector3[2];

    private delegate void TimerCallback();

    private STimer timer;

    private bool isGrounded;

    private bool moveTimer = false;

    private bool moveOver = true;

    private Vector3 targetVect3;

    private void Start()
    {
        vectorDis[0] = transform.position - new Vector3(moveArea, 0, moveArea);
        vectorDis[1] = transform.position + new Vector3(moveArea, 0, moveArea);
        timer = gameObject.AddComponent<STimer>();
        targetVect3 = GenerateRandomVector3InBox(vectorDis[0], vectorDis[1]);
    }

    // Update is called once per frame
    void Update()
    {
        loopMove();

    }

    private void onListenFloor()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, groundMask);
        if (isGrounded)
        {
            if (transform.position.y < 0)
            {
             
            }
        }
    }

    private void loopMove()
    {
        if (!moveTimer)
        {
            moving = true;
            float rotateY = CalculateYRotationAngle(transform.position, targetVect3);
            rotateRabit(rotateY);
        }
        else
        {
            moving = false;
        }
        moveTo(targetVect3, () =>
        {
            if (!moveTimer)
            {
                moving = false;
                moveTimer = timer.StartTimer(timeDelay, () =>
                {
                    targetVect3 = GenerateRandomVector3InBox(vectorDis[0], vectorDis[1]);
                    float rotateY = CalculateYRotationAngle(transform.position, targetVect3);
                    rotateRabit(rotateY);
                    moveTimer = false;
                    loopMove();
                });
            }
        });
    }

    float CalculateYRotationAngle(Vector3 start, Vector3 end)
    {
        // 计算方向向量
        Vector3 direction = end - start;
        // 只考虑 x 和 z 分量
        Vector3 directionXZ = new Vector3(direction.x, 0, direction.z);

        // 计算 y 轴上的旋转角度
        float angle = Mathf.Atan2(directionXZ.x, directionXZ.z) * Mathf.Rad2Deg;

        return angle;
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

        return new Vector3(x, y, z);
    }

    void rotateRabit(float yRotationAngle)
    {
        Quaternion targetRotation = Quaternion.Euler(0, yRotationAngle, 0);
        transform.rotation = targetRotation;
    }

    int GenerateRandomAngle()
    {
        // 使用 Random.Range 生成 0 到 359 之间的随机整数
        int angle = Random.Range(0, 360);
        return angle;
    }

    private void moveTo(Vector3 targetPosition, TimerCallback callback)
    {

        Vector3 currentPosition = transform.position;
        Vector3 direction = (targetPosition - currentPosition).normalized; // 计算方向向量并归一化
        float distance = Vector3.Distance(currentPosition, targetPosition); // 计算当前距离到目标的距离

        // 计算当前帧移动的距离
        float step = moveSpeed * Time.deltaTime;

        // 如果当前帧移动的距离大于剩余的距离，则直接移动到目标位置
        if (step < distance)
        {
            // 按照方向移动一步
            transform.position = currentPosition + direction * step;
           
        }
        else
        {
            callback?.Invoke();
        }
    }
}
