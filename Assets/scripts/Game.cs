using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Game : MonoBehaviour
{
    public static Game Singletone;
    
    public int score = 100;

    [SerializeField] private GameObject destroyParticle;
    [SerializeField] private GameObject textPopUp;
    [SerializeField] private Text scoreText;
    [SerializeField] private float timeFactor = 1; // Чем больше - тем меньше времени между спавном

    private int winScore = 300, loseScore = 0;
    
    private void Start()
    {
        Singletone = this;
        scoreText.text = $"{score}";
        
        StartCoroutine(SpawnCycle());
    }

    IEnumerator SpawnCycle()
    {
        float scoreToSecond = score / 100;
        print($"Time: ({scoreToSecond} - {scoreToSecond / 10}) * {timeFactor} = {(scoreToSecond - scoreToSecond / 10) * timeFactor}");
        yield return new WaitForSeconds((scoreToSecond - scoreToSecond / 10) * timeFactor);
        SpawnVirus.Singletone.Spawn();
        StartCoroutine(SpawnCycle());
    }

    public void VirusDestroy(Transform spawnPoint)
    {
        int scorePlus = Random.Range(5, 11);

        score += scorePlus;
        scoreText.text = $"{score}";
        CheckScore();
        
        Instantiate(destroyParticle, spawnPoint.position, Quaternion.identity); // Particle
        GameObject newPopUp = Instantiate(textPopUp, spawnPoint.position, Quaternion.Euler(0, 0, Random.Range(-20, 21))); // PopUp
        newPopUp.transform.GetChild(0).GetComponent<Text>().text = $"+{scorePlus}!";
        Destroy(newPopUp, 1f);
    }

    public void VirusEscape() // When player not destroy virus
    {
        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y, 0);
        int scorePlus = Random.Range(5, 11);

        score -= scorePlus;
        scoreText.text = $"{score}";
        CheckScore();
        
        GameObject newPopUp = Instantiate(textPopUp, spawnPoint, Quaternion.Euler(0, 0, Random.Range(-20, 21))); // PopUp
        newPopUp.transform.GetChild(0).GetComponent<Text>().text = $"-{scorePlus}!!!";
        Destroy(newPopUp, 1f);
    }

    public void CheckScore()
    {
        if (score >= winScore)
        {
            // Win
        }
        
        if (score <= loseScore)
        {
            // Lose
        }
    }
}