using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] float defauldDistance = 0f;
    [SerializeField] float damage = 5;
    public Transform laserFirePoint;
    public LineRenderer lineeRenderer;
    Transform m_transform;

    PlayerController player;

    [SerializeField] bool laserOn;
    //açýk olacaðý zamaný ayarlar.
    [SerializeField] float onTime = 5f;

    //kapalý olacakðý zamaný ayarlar. 5'in üzerine ne kadar kapalý kalacaðý yani bu durumda 10-5 = 5sn kapalý kalacak.
    [SerializeField] float offTime = 10f;
    private float timer = 0f;

    private bool isDrawing = false;

    private void Awake()
    {
        m_transform = GetComponent<Transform>();
        player = new PlayerController();
    }


    private void Update()
    {
        //ShootLaser();
        timer += Time.deltaTime;

        if (timer <= onTime)
        {
            isDrawing = true; 
            ShootLaser();
        }

        if (timer> onTime) 
        {
            isDrawing = false; 
            lineeRenderer.enabled = false; 

            if (timer >= offTime)
            {
                timer = 0f;
            }
        }

    }

    private void ShootLaser()
    {
        RaycastHit2D hit = Physics2D.Raycast(laserFirePoint.position, laserFirePoint.transform.right);

        if (hit.collider != null)
        {
            Draw2DRay(laserFirePoint.position, hit.point);

            if (hit.collider.CompareTag("Player"))
            {
                Transform hitPlayer = hit.transform;

                PlayerController playerController = hitPlayer.GetComponent<PlayerController>();

                if (playerController !=null)
                {
                    playerController.TakeDamage(damage);
                    AudioManager.Instance.Play("Laser");
                }
            }
        }
        else
        {
            Draw2DRay(laserFirePoint.position, laserFirePoint.position + laserFirePoint.transform.right * defauldDistance);
        }
    }


    /* private void ShootLaser()
     {
             if (Physics2D.Raycast(m_transform.position, laserFirePoint.transform.right))
             {
                 RaycastHit2D hit = Physics2D.Raycast(laserFirePoint.position, laserFirePoint.transform.right);
                 Draw2DRay(laserFirePoint.position, hit.point);

                 if (hit.collider.CompareTag("Player"))
                 {
                     player.TakeDamage(damage);
                 }
             }
            else
             {
                 Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * defauldDistance);
             }
     }*/

    /* private void ShootLaserZero()
     {
         if (Physics2D.Raycast(m_transform.position, laserFirePoint.transform.right))
         {
             RaycastHit2D hit = Physics2D.Raycast(laserFirePoint.position, laserFirePoint.transform.right);
             Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * 0.01f);

             if (hit.collider.CompareTag("Player"))
             {
                 player.TakeDamage(damage);
             }
         }
        /* else
         {
             Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * defauldDistance);
         }
     }*/

    /*   private IEnumerator LaserGun()
       {
           ShootLaser();
           yield return new WaitForSeconds(2f);
           ShootLaserZero();
       }*/

    /*private IEnumerator ShootLaser()
    {
        yield return new WaitForSeconds(2f);
        if (Physics2D.Raycast(m_transform.position, laserFirePoint.transform.right))
        {
            RaycastHit2D hit = Physics2D.Raycast(laserFirePoint.position, laserFirePoint.transform.right);
            Draw2DRay(laserFirePoint.position, hit.point);

            if (hit.collider.CompareTag("Player"))
            {
                player.TakeDamage(damage);
            }
        }
        else
        {
            Draw2DRay(laserFirePoint.position, laserFirePoint.transform.right * defauldDistance);
        }
    }*/

    private void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        if (!isDrawing) // Çizim bitmiþse çizgiyi devre dýþý býrak
        {
            lineeRenderer.enabled = false;
            return;
        }

        lineeRenderer.enabled = true;
        lineeRenderer.SetPosition(0, startPos);
        lineeRenderer.SetPosition(1, endPos);
    }

    /*private void Draw2DRay(Vector2 startPoint, Vector2 endPoint)
    {
        lineeRenderer.SetPosition(0,startPoint);
        lineeRenderer.SetPosition(1, endPoint);
    }*/

}
