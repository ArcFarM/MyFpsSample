using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MyFps
{
    //피스톨 제어 클래스
    public class PistolShoot : MonoBehaviour
    {
        #region Variable
        //발사 지점
        [SerializeField] Transform firePoint;

        //공격 대기 시간
        [SerializeField] float fireDelay = 1f;
        bool canFireFlag = true;

        //공격력과 사거리
        [SerializeField] float damage = 10f;
        [SerializeField] float range = 200f;

        //충격 강도 - 넉백 효과
        [SerializeField] float impactForce = 10f;

        //애니메이터 및 효과
        private Animator animator;
        [SerializeField] GameObject fireEffect;
        [SerializeField] AudioSource gun_as;
        string fire = "Fire";
        //적 피격 효과
        [SerializeField] GameObject hitImpactPrefab;
        
        #endregion

        #region Unity Event Method
        private void Start() {
            animator = GetComponent<Animator>();
        }

        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            float dist = PlayerCasting.distanceFromTarget;
            float gizmodist = (dist > range) ? range : dist;
            Gizmos.DrawRay(firePoint.position, firePoint.forward * gizmodist);
        }
        #endregion

        #region Custom Method

        public void FireAction() {
            if (canFireFlag) //발사 가능 상태일 때
            {
                StartCoroutine(Fire());
            }
        }

        IEnumerator Fire() {
            Debug.DrawRay(firePoint.position, firePoint.forward * range, Color.red, 1f);
            //연사 방지
            canFireFlag = false;
            if (PlayerManager.Instance.IsAmmoEmpty()) {
                Debug.Log("Not Enough Ammo");
                canFireFlag = true; //이 경우엔 난사해도 문제가 없다
                yield break;
            } //탄약이 없으면 발사 불가
            else {
                PlayerManager.Instance.UseAmmo(1); //탄약 사용
                //Debug.Log($"current ammo : {PlayerManager.Instance.Ammo}");
            }
                //효과 출력
                animator.SetTrigger(fire);
            gun_as.Play();
            fireEffect.SetActive(true);
            //사거리 만큼 raycast를 실시하여 충돌하면 적에게 피해를 준다
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range)) {
                //적에게 피해를 준다
                Debug.Log($"Ray hit: {hit.transform.name}, Ray hit Transform : {hit.transform}");
                //타격 효과 생성
                if (hitImpactPrefab) {
                    GameObject effectGo = Instantiate(hitImpactPrefab, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(effectGo, 2f);
                }
                //피격된 적 넉백
                if (hit.rigidbody) {
                    hit.rigidbody.AddForce(-hit.normal * impactForce, ForceMode.Impulse);
                }

                IDamagable enemy = hit.transform.GetComponent<IDamagable>();
                if (enemy != null) {
                    Debug.Log("Target Damaged");
                    enemy.TakeDamage(damage);
                }
            }
            yield return new WaitForSeconds(fireDelay);
            //효과 종료
            fireEffect.SetActive(false);
            canFireFlag = true;
        }
        #endregion
    }
}