using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace MyFps {
    public class GunnerRobot : MonoBehaviour {
        #region Variables
        private Animator animator;
        NavMeshAgent agent;

        private RobotState robotState;
        private RobotState beforeState;
        public Transform thePlayer;
        Vector3 targetPos;

        [SerializeField] GameObject[] Waypoints;
        int curr_wpIndex = 0;
        [SerializeField] float idleTime = 2f;
        float idleTimer = 0f;
        [SerializeField] float detectingRange = 15f;

        EnemyHealth robotHealth;
        bool deadFlag = false;
        [SerializeField] private float moveSpeed = 5f;

        [SerializeField] private float attackRange = 10f;
        [SerializeField] float attackCooldown = 2f;
        float attackTimer = 0f;
        [SerializeField] private float attackDamage = 5f;

        private string enemyState = "MoveState";
        string attack = "Attack";

        // 코루틴 중복 방지
        bool isAttacking = false;

        #endregion

        #region Unity Event Method
        void Awake() {
            robotHealth = this.GetComponent<EnemyHealth>();
            robotHealth.OnDeathEvent += OnDeathEvent;
            agent = GetComponent<NavMeshAgent>();
        }
        private void Start() {
            animator = this.GetComponent<Animator>();
        }

        private void OnEnable() {
            ChangeState(RobotState.R_Idle);
        }
        private void OnDisable() {
            robotHealth.OnDeathEvent -= OnDeathEvent;
        }
        private void Update() {
            if (robotHealth.IsDead) {
                return;
            }

            targetPos = thePlayer.position;
            float dist = Vector3.Distance(thePlayer.position, transform.position);

            switch (robotState) {
                case RobotState.R_Idle:
                    if (idleTimer < idleTime) {
                        idleTimer += Time.deltaTime;
                    }
                    if (idleTimer >= idleTime) {
                        idleTimer = 0;
                        ChangeState(RobotState.R_Patrol);
                        Patrol();
                    }
                    // Idle에서 플레이어 감지
                    if (dist < attackRange) ChangeState(RobotState.R_Attack);
                        else if (dist < detectingRange) { ChangeState(RobotState.R_Chase); }
                    break;

                case RobotState.R_Patrol:
                    if (agent.remainingDistance <= 0.1f) ChangeState(RobotState.R_Idle);
                    // 순찰 중 플레이어 감지
                    if (dist < attackRange) ChangeState(RobotState.R_Attack);
                    else if (dist < detectingRange) { ChangeState(RobotState.R_Chase); }
                    break;

                case RobotState.R_Chase:
                    Chase();
                    // 추격 중 공격 사거리 진입
                    if (dist < attackRange) { ChangeState(RobotState.R_Attack); }
                    // 추격 중 범위 이탈
                    else if (dist > detectingRange + 5f) { ChangeState(RobotState.R_Idle); }
                    break;

                case RobotState.R_Attack:
                    // 공격 사거리 벗어나면 추격 또는 Idle로
                    if (dist > attackRange) {
                        isAttacking = false;
                        // 공격 범위 벗어나면 chase 또는 idle
                        if (dist < detectingRange) ChangeState(RobotState.R_Chase);
                        else ChangeState(RobotState.R_Idle);
                        break;
                    }
                    // 아직 공격 중이 아니라면 코루틴 시작
                    if (!isAttacking) {
                        StartCoroutine(CheckAttack());
                    }
                    break;

                case RobotState.R_Death:
                    break;
            }
        }
        #endregion

        #region Custom Method
        public void ChangeState(RobotState newState) {
            if (robotState == newState) {
                return;
            }
            beforeState = robotState;
            robotState = newState;
            if (robotState >= RobotState.R_Patrol) animator.SetInteger(enemyState, (int)RobotState.R_Walk);
            else animator.SetInteger(enemyState, (int)robotState);

            // 상태 진입시 필요한 초기화
            if (robotState != RobotState.R_Attack) {
                isAttacking = false;
                StopAllCoroutines();
            }
        }

        public void OnDeathEvent() {
            if (deadFlag) return;
            deadFlag = true;
            ChangeState(RobotState.R_Death);
            GetComponent<BoxCollider>().enabled = false;
            Destroy(this.gameObject, 10f);
        }

        void Patrol() {
            if (Waypoints.Length > 0) {
                Vector3 nextWp = Waypoints[curr_wpIndex].transform.position;
                agent.SetDestination(nextWp);
                curr_wpIndex = (curr_wpIndex + 1) % Waypoints.Length;
                //20% 확률로 무작위 지점으로 이동
                int dice = Random.Range(1, 101);
                if (dice % 5 == 0) curr_wpIndex = (int)Random.Range(0, Waypoints.Length);
            } else ChangeState(RobotState.R_Idle);
        }

        void Chase() {
            agent.SetDestination(targetPos);
            animator.SetLayerWeight(1, 1);
        }

        void Attack() {
            animator.SetInteger(enemyState, (int)RobotState.R_Idle);
            agent.velocity = Vector3.zero;
            agent.ResetPath();
            animator.SetTrigger(attack);
            // 여기서 데미지 처리 등 추가
        }

        IEnumerator CheckAttack() {
            isAttacking = true;
            Attack();
            yield return new WaitForSeconds(attackCooldown);
            isAttacking = false;
        }
        #endregion
    }
}