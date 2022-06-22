using UnityEngine;
using UnityEngine.UI;

public class Abilities : MonoBehaviour
{
    [Header("Ability 1")] [SerializeField] private Image abilityImage1;
    [SerializeField] private float cooldown1 = 5;
    [SerializeField] private KeyCode ability1;
    private bool _isCooldown1;

    [Header("Ability 2")] [SerializeField] private Image abilityImage2;
    [SerializeField] private float cooldown2 = 10;
    [SerializeField] private KeyCode ability2;
    private bool _isCooldown2;


    // Start is called before the first frame update
    void Start()
    {
        abilityImage1.fillAmount = 0;
        abilityImage2.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Ability1();
        Ability2();
    }

    private void Ability2()
    {
        if (Input.GetKey(ability2) && !_isCooldown2)
        {
            _isCooldown2 = true;
            abilityImage2.fillAmount = 1;
        }

        if (_isCooldown2)
        {
            abilityImage2.fillAmount -= 1 / cooldown2 * Time.deltaTime;

            if (abilityImage2.fillAmount <= 0)
            {
                abilityImage2.fillAmount = 0;
                _isCooldown2 = false;
            }
        }
    }

    private void Ability1()
    {
        if (Input.GetKey(ability1) && !_isCooldown1)
        {
            _isCooldown1 = true;
            abilityImage1.fillAmount = 1;
        }

        if (_isCooldown1)
        {
            abilityImage1.fillAmount -= 1 / cooldown1 * Time.deltaTime;

            if (abilityImage1.fillAmount <= 0)
            {
                abilityImage1.fillAmount = 0;
                _isCooldown1 = false;
            }
        }
    }
}