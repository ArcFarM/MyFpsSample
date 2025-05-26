using UnityEngine;
using System.Collections;
using NUnit.Framework.Constraints;

namespace MyFps
{
    //적 등장 트리거 연출
    public class CJumpScareTrigger : MonoBehaviour
    {
        #region Variables
        //문열리는 애니메이션
        public Animator animator;

        //문열리는 사운드
        public AudioSource doorBang;

        //적 오브젝트
        public GameObject enemy;

        //적 등장 사운드
        public AudioSource jumpScare;

        //배경음
        public AudioSource bgm;

        //애니메이션 파라미터
        private string isOpen = "IsOpen";
        #endregion

        #region Unity Event Method
        private void Start() {
            this.gameObject.SetActive(false); //시작 시 트리거 비활성화
        }
        private void OnTriggerEnter(Collider other)
        {
            //플레이어 체크
            if(other.tag == "Player")
            {
                //트리거 해제
                this.GetComponent<BoxCollider>().enabled = false;
                StartCoroutine(SequencePlayer());
            }
        }
        #endregion

        #region Custom Method
        //트리거 연출 구현
        IEnumerator SequencePlayer()
        {
            //기본 배경음 정지
            bgm.Stop();

            //문이 열린다
            animator.SetBool(isOpen, true);

            //문이 열리는 사운드
            doorBang.Play();

            //적 등장 
            enemy.SetActive(true);

            //1초 딜레이
            yield return new WaitForSeconds(1f);

            //적 등장 사운드 플레이
            jumpScare.Play();

            //로봇의 상태가 걷기 상태로 변경
            Robot robot = enemy.GetComponent<Robot>();
            if(robot)
            {
                robot.ChangeState(RobotState.R_Walk);
            }

            //배경음 전환을 위한 로봇 감시
            StartCoroutine(CheckEnemy(robot));
        }

        IEnumerator CheckEnemy(Robot r) {
            Debug.Log("Check sTart");
            while (!r.GetComponent<EnemyHealth>().IsDead) {
                yield return new WaitForSeconds(0.1f);
            }
            if (r.GetComponent<EnemyHealth>().IsDead) {
                //로봇이 죽었을 때 배경음 재생
                Debug.Log("Check End");
                bgm.Play();
            }
        }
        #endregion
    }
}

