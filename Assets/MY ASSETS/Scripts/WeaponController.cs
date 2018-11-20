using UnityEngine;
using System.Collections;

namespace AbstractGame.ThirdPerson
{
    public class WeaponController : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public float radiusPower = 3.5f;
        public float speed = 20;
        public float fireRate = 0.1f;

        Coroutine shooting;

        bool shoot;

        EnergyPlayer energyPlayer;

        Vector3 scalePower;

        public AudioSource powerSong;

        public AudioSource bulletSong;

        void Awake()
        {
            energyPlayer = GetComponentInParent<EnergyPlayer>();
            scalePower = Vector3.one * 1.3f;
        }

        public void Shoot(bool shoot)
        {

            this.shoot = shoot;
            if (shoot && gameObject.activeInHierarchy)
            {
                shooting = StartCoroutine(Shoot());
            }
            else
            {
                if (shooting != null) StopCoroutine(shooting);
            }
        }

        IEnumerator Shoot()
        {
            while (shoot)
            {
                GameObject bullet = ObjectsPool.Spawn(bulletPrefab, GameMaster.idPool_bullet, transform.position, transform.rotation, transform);
                bullet.transform.localPosition = new Vector2(bullet.transform.localPosition.x, bullet.transform.localPosition.y + 0.8f);
                bullet.GetComponent<Bullet>().speed = speed;

                bulletSong.Play();
                yield return new WaitForSeconds(fireRate);
            }
            yield return null;
        }


        public void UsePower()
        {
            if (energyPlayer.available && gameObject.activeInHierarchy)
            {
                energyPlayer.UseMagic();

                GameMaster.Player.transform.LeanScale(scalePower, .1f).setOnComplete(() =>
                {
                    GameMaster.Player.transform.LeanScale(Vector3.one, .1f);
                });

                transform.LeanScale(scalePower, .1f).setOnComplete(() =>
                {
                    transform.LeanScale(Vector3.one, .1f);
                    HitEnemies();
                });
            }
        }

        void HitEnemies()
        {
            GameMaster.powerFX.Play();
            powerSong.Play();
            Collider2D[] enemies;
            LeanTween.value(0, radiusPower, .37f).setOnUpdate((float value) =>
            {
                enemies = Physics2D.OverlapCircleAll(transform.position, value, 1 << LayerMask.NameToLayer("Enemy"));
                foreach (Collider2D enemy in enemies)
                {
                    if (enemy.tag == "Enemy")
                    {
                        enemy.gameObject.GetComponent<Enemy>().Death();
                    }
                }
            });
        }

    }
}