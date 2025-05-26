using UnityEngine;
using UnityEngine.Events;

namespace MyFps {
    //적 체력을 제어
    public class EnemyHealth : MonoBehaviour, IDamagable {
        #region Variables
        //체력
        [SerializeField] private float maxHealth = 20f;
        float currentHealth;
        //사망시 이벤트
        public UnityAction OnDeathEvent;
        #endregion

        #region Properties
        public float GetHealth {
            get { return currentHealth; }
        }
        public bool IsDead {
            get { return currentHealth <= 0; }
        }
        public float GetMaxHealth {
            get { return maxHealth; }
        }
        #endregion
        #region Unity Event Method
        private void Start() {
            //초기화
            currentHealth = maxHealth;
        }
        #endregion
        #region Custom Method
        //체력 증감
        public void TakeDamage(float damage) {
            Debug.Log($"Enemy currentHealth: {currentHealth}");
            currentHealth = (currentHealth - damage < 0) ? 0 : currentHealth - damage;

            if(currentHealth <= 0) {
                currentHealth = 0;
                Die();
            }
        }
        //사망
        public void Die() {
            //사망시 이벤트 발생
            OnDeathEvent?.Invoke();
        }
        #endregion
    }
}