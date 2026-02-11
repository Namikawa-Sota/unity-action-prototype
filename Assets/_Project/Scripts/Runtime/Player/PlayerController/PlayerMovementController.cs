using System;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private Player _player;

    public Action OnAvoidFinished;
    public bool IsAvoidMoving = false;

    public void MoveInCameraSpace(Vector2 direction)
    {
        Vector3 cameraForward = _player.CameraTransform.forward.normalized;
        Vector3 cameraRight = _player.CameraTransform.right.normalized;

        cameraForward.y = 0f;
        cameraRight.y = 0f;

        Vector3 moveVector = cameraForward * direction.y + cameraRight * direction.x;

        Move(moveVector, this._player.RunSpeed);
    }

    public void Move(Vector3 moveVector, float speed)
    {
        if (moveVector.sqrMagnitude < 0.01f)
        {
            return;
        }

        // 移動処理
        this._player.CharaController.Move(moveVector.normalized * speed * Time.deltaTime);

        // 向き
        Quaternion targetRotation = Quaternion.LookRotation(moveVector);
        this._player.transform.rotation = Quaternion.Slerp(this._player.transform.rotation, targetRotation, this._player.RotationSpeed * Time.deltaTime);
    }

    public void Update()
    {
        if (IsAvoidMoving)
        {
            Move(_player.transform.forward, this._player.RunSpeed * 2);
        }
    }

    public void OnAvoidFinish()
    {
        OnAvoidFinished?.Invoke();
    }

    public void OnAvoidMovingStart()
    {
        IsAvoidMoving = true;
    }

    public void OnAvoidMovingFinish()
    {
        IsAvoidMoving = false;
    }
}
