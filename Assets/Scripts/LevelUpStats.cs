using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpStats : MonoBehaviour
{
    public int level = 1;
    public float Experience { get; private set; }
    public Text tvLevel;
    public Image imExpBar;

    public static int ExpNeedToLvlUp(int currentLevel)
    {
        if (currentLevel == 0)
            return 0;

        return (currentLevel * currentLevel + currentLevel) * 5;
    }

    public void SetExperience(float exp)
    {
        Experience += exp;

        float expNeeded = ExpNeedToLvlUp(level);
        float previousExperience = ExpNeedToLvlUp(level - 1);

        //Level up with Exp
        if (Experience >= expNeeded)
        {
            LevelUp();
            expNeeded = ExpNeedToLvlUp(level);
            previousExperience = ExpNeedToLvlUp(level - 1);
        }

        //Fill Exp Bar Image with Exp
        imExpBar.fillAmount = (Experience - previousExperience) / (expNeeded - previousExperience);

        //Reset the Fillbar
        if (imExpBar.fillAmount == 1)
        {
            imExpBar.fillAmount = 0;
        }
    }

    public void LevelUp()
    {
        level++;
        tvLevel.text = level.ToString("");
    }
}