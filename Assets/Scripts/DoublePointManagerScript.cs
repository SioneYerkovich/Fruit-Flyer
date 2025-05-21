using UnityEngine;

public class DoublePointManagerScript : MonoBehaviour
{
        public static DoublePointManagerScript Instance;
        public float bonusTimer;
        public bool bonus = false;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            PointsControl();
        }

        private void Awake()
        {
            Instance = this;
        }

        public void ActivateBonus(float duration)
        {
            bonusTimer = duration;
            bonus = true;
        }

        public void EndBonus()
        {
            bonus = false;
            bonusTimer = 0f;
        }

        public void PointsControl()
        {
            if (bonus)
            {
                bonusTimer -= Time.deltaTime;
                if (bonusTimer <= 0f)
                {
                    EndBonus();
                }
            }
        }
}
