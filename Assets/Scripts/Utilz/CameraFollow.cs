using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _player;
    private SpriteRenderer _background;
    private GameObject bgObject;
    private float smoothSpeed = 0.1f;
    public float mapLeft, mapRight, mapBottom, mapTop;

    [SerializeField] float Xoffset, Yoffset;//X = 18, Y= 10;

    private void Start()
    {
        bgObject = GameObject.FindGameObjectWithTag("Background");
        _background = bgObject.GetComponent<SpriteRenderer>();
    }

    private void LateUpdate()
    {
        if (_player == null)
            _player = GameObject.FindGameObjectWithTag("Player").transform;
        else
            UpdatePos();

    }

    private void UpdatePos()
    {
        float Xrange = _background.bounds.size.x - Xoffset;
        float Yrange = _background.bounds.size.y - Yoffset;

        mapLeft = -Xrange / 2;
        mapRight = Xrange / 2;
        mapBottom = -Yrange / 2;
        mapTop = Yrange / 2;

        Vector3 desiredPosition = _player.position;

        desiredPosition.x = Mathf.Clamp(desiredPosition.x, mapLeft, mapRight);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, mapBottom, mapTop);
        desiredPosition.z = -10;

        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
    }
}