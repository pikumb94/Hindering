using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoulder : MonoBehaviour
{

    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GameObject.FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mouse_position = Input.mousePosition;

        float camera2me = transform.position.z - _camera.transform.position.z;

        Vector3 mouse = _camera.ScreenToWorldPoint(new Vector3(mouse_position.x, mouse_position.y, camera2me));

        Vector3 direction = (Vector2)mouse - (Vector2)transform.position;

        float rotationZ = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotationZ);
    }
}
