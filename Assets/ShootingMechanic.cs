using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingMechanic : MonoBehaviour
{
    private Transform m_transform;
    [SerializeField] private GameObject crosshair;
    [SerializeField] GameObject hand;

    private void Start()
    {
        m_transform = this.transform;
    }

    private void LAMouse()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        //Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        Vector2 direction = new Vector2(mousePosition.x, mousePosition.y);
        crosshair.transform.position = new Vector2(direction.x, direction.y);
        hand.transform.up = direction;

        if (transform.position.x > direction.x)
        {
            transform.localScale = new Vector3(-2.9f, 2.9f, 2.9f);
        }
        else{
            transform.localScale = new Vector3(2.9f, 2.9f, 2.9f);
        }
        /*Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - m_transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotationR = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        Quaternion rotationL = Quaternion.AngleAxis(angle -90, Vector3.forward);

        crosshair.transform.position = new Vector2(direction.x, direction.y);
        if (direction.x >transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Y ekseni 0 derece rotasyon
            hand.transform.localScale = new Vector3(-2.3f, 2.3f, 2.3f);
            hand.transform.rotation = rotationR;
        }
        else
        {
            transform.rotation = Quaternion.Euler(0f,180f, 0f); // Y ekseni 180 derece rotasyon
            hand.transform.localScale = new Vector3(-2.3f, -2.3f, 2.3f);
            hand.transform.rotation = rotationL;
        } */  
    }
    private void Update()
    {
        Cursor.visible = false;    
        LAMouse();
    }
}
