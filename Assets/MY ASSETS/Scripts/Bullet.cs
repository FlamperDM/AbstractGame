using UnityEngine;

namespace AbstractGame.ThirdPerson
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 20;

        void Update()
        {
            if (transform.localPosition.y > 15)
            {
                DisableObj();
            }
        }

        void FixedUpdate()
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector2(0, 30), speed * Time.fixedDeltaTime);
        }

        void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "Enemy")
            {
                ObjectsPool.Spawn(GameMaster.bulletFX, GameMaster.idPool_bulletFX, transform.position, transform.rotation).GetComponent<AudioSource>().Play();
                collision.GetComponent<Enemy>().Death();
                DisableObj();
            }
        }

        void DisableObj()
        {
            ObjectsPool.Despawn(GameMaster.idPool_bullet, gameObject);
        }
    }
}
