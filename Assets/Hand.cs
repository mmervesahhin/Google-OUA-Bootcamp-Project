using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{
    private Transform m_transform;
    public Vector3 target;
    [SerializeField] private GameObject crosshair;
    [SerializeField] GameObject gun;

   

    private void LAMouse()
    {
        Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - m_transform.position;
        float angle = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;

        Quaternion rotation = Quaternion.AngleAxis(angle-90,Vector3.forward);
        if (target.x > transform.position.x)
        {
            // Hedef noktasý karakterin saðýnda olduðunda
            transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Y ekseni 0 derece rotasyon
            gun.transform.localScale = new Vector3(2.3f, 2.3f, 2.3f);
        }
        else
        {
            // Hedef noktasý karakterin solunda olduðunda
            transform.rotation = Quaternion.Euler(0f, 180f, 0f); // Y ekseni 180 derece rotasyon
            gun.transform.localScale = new Vector3(2.3f, -2.3f, 2.3f);
        }
        m_transform.rotation = rotation;

    }

    private void Update()
    {
        Cursor.visible = false;
        LAMouse();
    }
}
