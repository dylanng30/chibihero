using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform _player;
    private float smoothSpeed = 0.05f;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate()
    {
        if (_player == null)
            return;
        this.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, -10);
    }
}
