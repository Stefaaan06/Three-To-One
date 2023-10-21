using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class playerManager : MonoBehaviour
    {   
        public Slider slider;
        public Rigidbody[] rb;
        public GameObject[] players;
        public int JumpForce = 100;
        public int bigJumpForce = 100;
        public ParticleSystem[] loadupParticles;
        public cameraShake shaker;

        public float loadupShakeStrenght = 3f;
        public float jumpShakeStrength = 5f;

        public AudioSource powerUp;

        public GameObject deathScreen;
        public GameObject finishScreen;
        
        public int pEnd = 0;
        public int lvl;
        public int pJump;

        public TMP_Text points;
        public AudioSource jump;
        
        public uiWipe deathWipe;
        public uiWipe finishWipe;
        private void Start()
        {
            Time.timeScale = 1f;
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        void FixedUpdate()
        {
            if (Input.GetKey(KeyCode.Space))
            {
                slider.value = slider.value += 10;
                shaker.ShakeOnce(loadupShakeStrenght / 2, loadupShakeStrenght, null, Camera.current);
                foreach (ParticleSystem particle in loadupParticles)
                {
                    if (particle != null)
                    {
                        particle.Play();
                    }
                    if (!powerUp.isPlaying)
                    {
                        powerUp.pitch = Time.timeScale + Random.Range(0.1f, 0.4f);
                        powerUp.Play();
                    }
                }
            }
            else 
            {
                if (slider.value == slider.maxValue)
                {
                    foreach (Rigidbody rb in rb)
                    {
                        if (rb != null)
                        {
                            rb.AddForce(Vector3.up * JumpForce);
                            jump.pitch = Time.timeScale + Random.Range(0.1f, 0.4f);
                            jump.Play();
                            slider.value = slider.minValue;
                        }
                        shaker.ShakeOnce(jumpShakeStrength / 2, jumpShakeStrength, null, Camera.current);
                    }
                }
                slider.value = slider.value -= 100;
                
                if (powerUp.isPlaying)
                {
                    powerUp.Stop();
                }
                
            }
        }

        private bool _dead = false;

        public void fullDie()
        {
            deathScreen.SetActive(true);
            deathWipe.StartWipeIn();
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            _dead = true;
        }

        public float shrinkDuration = 3f; // Duration in seconds
        private float  _originalTimeScale = 1f;
        private float _timer = 0f;
        private bool _finish = false;
        private int _endValue = 0;
        private void Update()
        {
            if (_dead)
            {
                if (_timer < shrinkDuration)
                {
                    finishScreen.SetActive(false);
                    _timer += Time.deltaTime;
                    float t = Mathf.Clamp01(_timer / shrinkDuration);
                    Time.timeScale = Mathf.Lerp(_originalTimeScale, 0f, t);
                }
                return;
            }
            _endValue = 0;
            foreach (GameObject g in players)
            {
                if (g != null)
                {
                    _endValue += 6;
                }
            }

            if (_endValue == 0)
            {
                fullDie();
            }
            if (pEnd >= _endValue && _endValue != 0)
            {
                _finish = true;
            }
            if (_finish)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                if (!finishScreen.activeSelf)
                {
                    finishScreen.SetActive(true);
                    finishWipe.StartWipeIn();   
                }
                if (_timer < shrinkDuration)
                {
                    _timer += Time.deltaTime;
                    float t = Mathf.Clamp01(_timer / shrinkDuration);
                    Time.timeScale = Mathf.Lerp(_originalTimeScale, 0f, t);
                    switch (_endValue)
                    {
                        case 6:
                            points.text = "Points: 1/3";
                            break;
                        case 12:
                            points.text = "Points: 2/3";
                            break;
                        case 18:
                            points.text = "Points: 3/3";
                            break;
                    }
                    setValues();
                }
            }

            if (pJump >= _endValue)
            {
                foreach (Rigidbody rb in rb)
                {
                    if (rb != null)
                    {
                        rb.AddForce(Vector3.up * bigJumpForce);
                        jump.Play();
                    }
                }
            }
        }

        void setValues()
        {
            int value;
            switch (lvl)
            {
                case 1:
                    value = PlayerPrefs.GetInt("l1p");
                    switch (_endValue)
                    {
                        case 6:
                            if (value < 2)
                            {
                                PlayerPrefs.SetInt("l1p", 1);
                            }
                            break;
                        case 12:
                            if (value < 3)
                            {
                                PlayerPrefs.SetInt("l1p", 2);
                            }
                            break;
                        case 18:
                            PlayerPrefs.SetInt("l1p", 3);
                            break;
                    }
                    break;
                case 2:
                    value = PlayerPrefs.GetInt("l2p");
                    switch (_endValue)
                    {
                        case 6:
                            if (value < 2)
                            {
                                PlayerPrefs.SetInt("l2p", 1);
                            }
                            break;
                        case 12:
                            if (value < 3)
                            {
                                PlayerPrefs.SetInt("l2p", 2);
                            }
                            break;
                        case 18:
                            PlayerPrefs.SetInt("l2p", 3);
                            break;
                    }
                    break;
                case 3:
                    value = PlayerPrefs.GetInt("l3p");
                    switch (_endValue)
                    {
                        case 6:
                            if (value < 2)
                            {
                                PlayerPrefs.SetInt("l3p", 1);
                            }
                            break;
                        case 12:
                            if (value < 3)
                            {
                                PlayerPrefs.SetInt("l3p", 2);
                            }
                            break;
                        case 18:
                            PlayerPrefs.SetInt("l3p", 3);
                            break;
                    }
                    break;
                case 4:
                    value = PlayerPrefs.GetInt("l4p");
                    switch (_endValue)
                    {
                        case 6:
                            if (value < 2)
                            {
                                PlayerPrefs.SetInt("l4p", 1);
                            }
                            break;
                        case 12:
                            if (value < 3)
                            {
                                PlayerPrefs.SetInt("l4p", 2);
                            }
                            break;
                        case 18:
                            PlayerPrefs.SetInt("l4p", 3);
                            break;
                    }
                    break;
                case 5:
                    value = PlayerPrefs.GetInt("l5p");
                    switch (_endValue)
                    {
                        case 6:
                            if (value < 2)
                            {
                                PlayerPrefs.SetInt("l5p", 1);
                            }
                            break;
                        case 12:
                            if (value < 3)
                            {
                                PlayerPrefs.SetInt("l5p", 2);
                            }
                            break;
                        case 18:
                            PlayerPrefs.SetInt("l5p", 3);
                            break;
                    }
                    break;
                case 6:
                    value = PlayerPrefs.GetInt("l6p");
                    switch (_endValue)
                    {
                        case 6:
                            if (value < 2)
                            {
                                PlayerPrefs.SetInt("l6p", 1);
                            }
                            break;
                        case 12:
                            if (value < 3)
                            {
                                PlayerPrefs.SetInt("l6p", 2);
                            }
                            break;
                        case 18:
                            PlayerPrefs.SetInt("l6p", 3);
                            break;
                    }
                    break;
                case 7:
                    value = PlayerPrefs.GetInt("l7p");
                    switch (_endValue)
                    {
                        case 6:
                            if (value < 2)
                            {
                                PlayerPrefs.SetInt("l7p", 1);
                            }
                            break;
                        case 12:
                            if (value < 3)
                            {
                                PlayerPrefs.SetInt("l7p", 2);
                            }
                            break;
                        case 18:
                            PlayerPrefs.SetInt("l7p", 3);
                            break;
                    }
                    break;
                case 8:
                    value = PlayerPrefs.GetInt("l8p");
                    switch (_endValue)
                    {
                        case 6:
                            if (value < 2)
                            {
                                PlayerPrefs.SetInt("l8p", 1);
                            }
                            break;
                        case 12:
                            if (value < 3)
                            {
                                PlayerPrefs.SetInt("l8p", 2);
                            }
                            break;
                        case 18:
                            PlayerPrefs.SetInt("l8p", 3);
                            break;
                    }
                    break;
                case 9:
                    value = PlayerPrefs.GetInt("l9p");
                    switch (_endValue)
                    {
                        case 6:
                            if (value < 2)
                            {
                                PlayerPrefs.SetInt("l9p", 1);
                            }
                            break;
                        case 12:
                            if (value < 3)
                            {
                                PlayerPrefs.SetInt("l9p", 2);
                            }
                            break;
                        case 18:
                            PlayerPrefs.SetInt("l9p", 3);
                            break;
                    }
                    break;
                case 10:
                    value = PlayerPrefs.GetInt("l10p");
                    switch (_endValue)
                    {
                        case 6:
                            if (value < 2)
                            {
                                PlayerPrefs.SetInt("l10p", 1);
                            }
                            break;
                        case 12:
                            if (value < 3)
                            {
                                PlayerPrefs.SetInt("l10p", 2);
                            }
                            break;
                        case 18:
                            PlayerPrefs.SetInt("l10p", 3);
                            break;
                    }
                    break;
            }
        }
    }
