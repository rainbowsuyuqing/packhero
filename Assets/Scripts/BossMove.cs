using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public float horizontalRange = 3f;          // 左右移动的距离
    public float horizontalSpeed = 2f;          // 左右移动的速度
    public float horizontalDuration = 6f;       //左右移动时间
    public float downDistance = 2f;             // 向下移动的距离
    public float downSpeed = 2f;                // 向下移动的速度
    public float upSpeed = 2f;                  // 向上移动的速度
    public float verticalDuration = 2f;               // 向上移动的时长
    public float idleDuration = 2f;             // 停留时间

    private Vector2 startPosition;              // 初始位置
    private Vector2 targetPosition;             // 目标位置
    private Vector2 verticalStart;
    private float timeElapsed;                  // 时间流逝
    private bool isMovingLeft = true;            // 是否向左移动

    public enum BossState { Horizontal, Down, Idle, Up };
    public BossState currentState;             // Boss当前状态

    private void Start()
    {
        // 获取初始位置
        startPosition = transform.position;

        // 设置目标位置
        //targetPosition = new Vector2(
        //    isMovingLeft ? startPosition.x - horizontalRange : startPosition.x + horizontalRange,
        //    startPosition.y - downDistance
           
        //);

        // 设置初始状态为水平移动
        currentState = BossState.Horizontal;
    }

    private void Update()
    {
        // 根据当前状态进行不同的行为
        switch (currentState)
        {
            case BossState.Horizontal:
                MoveLeftAndRight();
                break;

            case BossState.Down:
                MoveDown();
                break;

            case BossState.Idle:
                Idle();
                break;

            case BossState.Up:
                MoveUp();
                break;
        }
    }

    // 左右移动
    private void MoveLeftAndRight()
    {
        // 计算目标位置
        targetPosition = new Vector2(
            isMovingLeft ? startPosition.x - horizontalRange : startPosition.x + horizontalRange,
            startPosition.y
        );

        // 计算移动距离和速度
        float movementDistance = horizontalRange * 2f;
        float movementSpeed = horizontalSpeed;

        float lerpRatio = timeElapsed / horizontalDuration;
        //计算位置
        //float pingPong = Mathf.PingPong(timeElapsed * movementSpeed, movementDistance);
        Vector2 newPosition = Vector2.Lerp(startPosition, targetPosition, lerpRatio);

        // 移动
        transform.position = newPosition;

        // 更新时间
        timeElapsed += Time.deltaTime;

        // 如果时间已到，切换到下降状态
        if (timeElapsed >= horizontalDuration)
        {
            verticalStart = transform.position;
            currentState = BossState.Down;
            timeElapsed = 0f;
        }
    }

    // 向下移动
    private void MoveDown()
    {
        targetPosition = new Vector2(verticalStart.x, startPosition.y - downDistance);

        // 计算位置
        float lerpRatio = timeElapsed / verticalDuration;
        Vector2 newPosition = Vector2.Lerp(verticalStart ,targetPosition,lerpRatio);

        // 移动
        transform.position = newPosition;

        // 更新时间
        timeElapsed += Time.deltaTime;

        // 如果时间已到，切换到停留状态
        if (lerpRatio >= 1f)
        {
            currentState = BossState.Idle;
            timeElapsed = 0f;
        }
    }

    // 停留
    private void Idle()
    {
        // 更新时间
        timeElapsed += Time.deltaTime;

        // 如果时间已到，切换到向上移动状态
        if (timeElapsed >= idleDuration)
        {
            currentState = BossState.Up;
            timeElapsed = 0f;
        }
    }

    // 向上移动
    private void MoveUp()
    {
        // 计算位置
        float lerpRatio = timeElapsed / verticalDuration;
        Vector2 newPosition = Vector2.Lerp(targetPosition, verticalStart, lerpRatio);

        // 移动
        transform.position = newPosition;

        // 更新时间
        timeElapsed += Time.deltaTime;

        // 如果时间已到，切换到水平移动状态
        if (lerpRatio >= 1f)
        {
            currentState = BossState.Horizontal;
            timeElapsed = 0f;

            // 切换方向
            isMovingLeft = !isMovingLeft;
            startPosition = transform.position;
           
        }
    }


}

