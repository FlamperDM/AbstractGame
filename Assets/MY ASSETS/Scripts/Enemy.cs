using UnityEngine;

namespace AbstractGame.ThirdPerson
{
    public class Enemy : MonoBehaviour
    {
        public float speed = 20;

        void Update()
        {
            Vector3 dir = GameMaster.Player.transform.localPosition - transform.localPosition;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.localRotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }

        void FixedUpdate()
        {
            transform.position = Vector2.MoveTowards(transform.position, GameMaster.Player.transform.position, speed * Time.fixedDeltaTime);
        }

        public void Death()
        {
            GameMaster.CountKill();
            transform.localScale = Vector3.one;
            ObjectsPool.Spawn(GameMaster.enemyFX, GameMaster.idPool_enemyDeathFX, transform.position, transform.rotation).GetComponent<AudioSource>().Play();
            ObjectsPool.Despawn(GameMaster.idPool_enemies, gameObject);
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
            {
                other.GetComponent<EnergyPlayer>().HitHealth();
            }
        }
    }
}