using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CitySceneManager : MonoBehaviour
{
    [Header("People")]
    [SerializeField] private Transform people1;
    [SerializeField] private Transform people2;
    [SerializeField] private Transform people3;
    [SerializeField] private Transform people4;
    [SerializeField] private float speed;
    [SerializeField] Animator [] anim;
    PlayerController playerController;
    public int collide = 0;

    [Header("Player")]
    [SerializeField] GameObject Player;
    string playerTag;

    [Header("Phone")]
    [SerializeField] GameObject phone;

    [Header("Enemy")]
    [SerializeField] GameObject enemy;
    [SerializeField] Transform enemyspawn;

    private void Start()
    {
        playerTag = Player.tag;
        //burada oyunun ilk 5 saniyesinde kontroller kapatýldý.   
        phone.SetActive(false);
        Invoke("Phone",1f);
        InputOff();
        Invoke("InputOn",5f);
    }

    private void Update()
    {
        people1.position = new Vector3(people1.position.x + Time.deltaTime * speed,
            people1.position.y, people1.position.z);

        people2.position = new Vector3(people2.position.x + Time.deltaTime * -speed,
            people2.position.y, people2.position.z);

        people3.position = new Vector3(people3.position.x + Time.deltaTime * -speed,
            people3.position.y, people3.position.z);

        people4.position = new Vector3(people4.position.x + Time.deltaTime * speed,
            people4.position.y, people4.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag) && collide==0 )
        {
            Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            Invoke("UnFreezPlayer",5f);
            phone.SetActive(false);
            StartCoroutine(SpawnEnemies());
            collide ++;
            for (int i = 0; i < 4; i++)
            {
                AudioManager.Instance.Play("Scream");
                anim[i].SetBool("run", true);
                speed = 10f;
            }
            Debug.Log("geldi");
        }
    }
    private void InputOff()
    {
        Player.GetComponent<PlayerController>().canPlay = false;
    }

    private void InputOn()
    {
        Player.GetComponent<PlayerController>().canPlay = true;
    }

    private void Phone()
    {
        AudioManager.Instance.Play("Phone");
        phone.SetActive(true);
    }

    private void UnFreezPlayer()
    {
        Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        Player.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    IEnumerator SpawnEnemies()
    {
        for (int i = 0; i < 5; i++)
        {
            EnemySpawn(); 

            yield return new WaitForSeconds(2f); // 2 saniye bekleme
        }
    }

    private void EnemySpawn()
    {
        Instantiate(enemy, enemyspawn.position,Quaternion.identity);
    }
}
