using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public float horizontalRange = 3f;          // �����ƶ��ľ���
    public float horizontalSpeed = 2f;          // �����ƶ����ٶ�
    public float horizontalDuration = 6f;       //�����ƶ�ʱ��
    public float downDistance = 2f;             // �����ƶ��ľ���
    public float downSpeed = 2f;                // �����ƶ����ٶ�
    public float upSpeed = 2f;                  // �����ƶ����ٶ�
    public float verticalDuration = 2f;               // �����ƶ���ʱ��
    public float idleDuration = 2f;             // ͣ��ʱ��

    private Vector2 startPosition;              // ��ʼλ��
    private Vector2 targetPosition;             // Ŀ��λ��
    private Vector2 verticalStart;
    private float timeElapsed;                  // ʱ������
    private bool isMovingLeft = true;            // �Ƿ������ƶ�

    public enum BossState { Horizontal, Down, Idle, Up };
    public BossState currentState;             // Boss��ǰ״̬

    private void Start()
    {
        // ��ȡ��ʼλ��
        startPosition = transform.position;

        // ����Ŀ��λ��
        //targetPosition = new Vector2(
        //    isMovingLeft ? startPosition.x - horizontalRange : startPosition.x + horizontalRange,
        //    startPosition.y - downDistance
           
        //);

        // ���ó�ʼ״̬Ϊˮƽ�ƶ�
        currentState = BossState.Horizontal;
    }

    private void Update()
    {
        // ���ݵ�ǰ״̬���в�ͬ����Ϊ
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

    // �����ƶ�
    private void MoveLeftAndRight()
    {
        // ����Ŀ��λ��
        targetPosition = new Vector2(
            isMovingLeft ? startPosition.x - horizontalRange : startPosition.x + horizontalRange,
            startPosition.y
        );

        // �����ƶ�������ٶ�
        float movementDistance = horizontalRange * 2f;
        float movementSpeed = horizontalSpeed;

        float lerpRatio = timeElapsed / horizontalDuration;
        //����λ��
        //float pingPong = Mathf.PingPong(timeElapsed * movementSpeed, movementDistance);
        Vector2 newPosition = Vector2.Lerp(startPosition, targetPosition, lerpRatio);

        // �ƶ�
        transform.position = newPosition;

        // ����ʱ��
        timeElapsed += Time.deltaTime;

        // ���ʱ���ѵ����л����½�״̬
        if (timeElapsed >= horizontalDuration)
        {
            verticalStart = transform.position;
            currentState = BossState.Down;
            timeElapsed = 0f;
        }
    }

    // �����ƶ�
    private void MoveDown()
    {
        targetPosition = new Vector2(verticalStart.x, startPosition.y - downDistance);

        // ����λ��
        float lerpRatio = timeElapsed / verticalDuration;
        Vector2 newPosition = Vector2.Lerp(verticalStart ,targetPosition,lerpRatio);

        // �ƶ�
        transform.position = newPosition;

        // ����ʱ��
        timeElapsed += Time.deltaTime;

        // ���ʱ���ѵ����л���ͣ��״̬
        if (lerpRatio >= 1f)
        {
            currentState = BossState.Idle;
            timeElapsed = 0f;
        }
    }

    // ͣ��
    private void Idle()
    {
        // ����ʱ��
        timeElapsed += Time.deltaTime;

        // ���ʱ���ѵ����л��������ƶ�״̬
        if (timeElapsed >= idleDuration)
        {
            currentState = BossState.Up;
            timeElapsed = 0f;
        }
    }

    // �����ƶ�
    private void MoveUp()
    {
        // ����λ��
        float lerpRatio = timeElapsed / verticalDuration;
        Vector2 newPosition = Vector2.Lerp(targetPosition, verticalStart, lerpRatio);

        // �ƶ�
        transform.position = newPosition;

        // ����ʱ��
        timeElapsed += Time.deltaTime;

        // ���ʱ���ѵ����л���ˮƽ�ƶ�״̬
        if (lerpRatio >= 1f)
        {
            currentState = BossState.Horizontal;
            timeElapsed = 0f;

            // �л�����
            isMovingLeft = !isMovingLeft;
            startPosition = transform.position;
           
        }
    }


}

