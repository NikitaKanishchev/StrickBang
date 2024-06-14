using HealthBar;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
    public class PlayerXP : MonoBehaviour
    {
        public int level;
        public float currentXp;
        public float requiredXp;

        private float lerpTimer;
        private float delayTimer;

        [Header("UI")] 
        public Image frontXpBar;
        public Image backXpBar;

        public TextMeshProUGUI levelText;
        public TextMeshProUGUI XpText;

        [Header("Multipliers")]
        [Range(1f, 300)]
        public float additionalMultiplier = 300;
        [Range(2f, 4)]
        public float powerMultiplier = 2;    
        [Range(7f, 14f)]
        public float divisionMultiplier = 7;    

        private void Start()
        {
            frontXpBar.fillAmount = currentXp / requiredXp;
            backXpBar.fillAmount = currentXp / requiredXp;
            requiredXp = CalculateRequiredXp();
            levelText.text = "Level" + level;
        }

        private void Update()
        {
            UpdateXpUI();
            if (Input.GetKeyDown(KeyCode.Z))
            {
                GainExperienceFlatRate(20);
            }

            if (currentXp > requiredXp)
            {
                LevelUp();
            }
        }

        public void UpdateXpUI()
        {
            float xpFraction = currentXp / requiredXp;
            float FXP = frontXpBar.fillAmount;
            if (FXP < xpFraction)
            {
                delayTimer += Time.deltaTime;
                backXpBar.fillAmount = xpFraction;
                if (delayTimer > 0.3)
                {
                    lerpTimer += Time.deltaTime;
                    float percentComplete = lerpTimer / 4;
                    frontXpBar.fillAmount = Mathf.Lerp(FXP, backXpBar.fillAmount, percentComplete);
                }
            }

            XpText.text = currentXp + "/" + requiredXp;
        }

        public void GainExperienceFlatRate(float xpGained)
        {
            currentXp += xpGained;
            lerpTimer = 0f;
        }

        public void GainExperienceFlatRate(float xpGained, int passedLevel)
        {
            if (passedLevel < level)
            {
                float multiplier = 1 + (level - passedLevel) * 0.01f;
                currentXp += xpGained * multiplier;
            }
            else
            {
                currentXp += xpGained;
            }
            lerpTimer = 0f;
            delayTimer = 0f;
        }
        
        public void LevelUp()
        {
            level++;
            frontXpBar.fillAmount = 0f;
            backXpBar.fillAmount = 0f;
            currentXp = Mathf.RoundToInt(currentXp - requiredXp);
            GetComponent<PlayerHealth>().IncreaseHealth(level);
            requiredXp = CalculateRequiredXp();
            levelText.text = "Level" + level;
        }
        
        private int CalculateRequiredXp()
        {
            int solveForRquiredXp = 0;
            for (int levelCycle = 1; levelCycle <= level; levelCycle++)
            {
                solveForRquiredXp += (int)Mathf.
                    Floor(levelCycle + additionalMultiplier * Mathf.
                        Pow(powerMultiplier, levelCycle / divisionMultiplier));
            }
            return solveForRquiredXp / 4;
        }
    }