using MyFps;
using UnityEngine;

public class ClashSound : MonoBehaviour
{
    #region Variables
    Rigidbody rb;
    Collider col;
    float threshold = 1f; //충돌 속도 임계점
    #endregion

    #region Properties
    #endregion

    #region Unity Event Method
    private void Start() {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }
    private void OnCollisionEnter(Collision collision) {
         //충돌 시 상대 속도 계산
        if (col != null && rb != null) {
            //두 물체 간 상대 속도가 임계점 이상이고, Ground 태그에 속하는 오브젝트와 충돌했을 때 소리 재생
            if (collision.relativeVelocity.magnitude > threshold && collision.gameObject.CompareTag("Ground")) {
                AudioManager.Instance.Play("Clash"); //AudioManager를 통해 소리 재생
            }
        }
    }
    #endregion

    #region Custom Method
    #endregion
}
