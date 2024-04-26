using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExperience : MonoBehaviour
{
    public int currentLevel = 1;
    public int currentExperience = 0;
    public int experienceToNextLevel = 50;

    public void GainExperience(int amount)
    {
        currentExperience += amount;
        if (currentExperience >= experienceToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        currentExperience -= experienceToNextLevel;
        experienceToNextLevel = CalculateExperienceForNextLevel();
        // Aquí podrías implementar el aumento de estadísticas o habilidades del jugador
    }

    private int CalculateExperienceForNextLevel()
    {
        // Aquí podrías definir una fórmula para calcular la experiencia necesaria para el siguiente nivel
        return experienceToNextLevel * 2;
    }
}
