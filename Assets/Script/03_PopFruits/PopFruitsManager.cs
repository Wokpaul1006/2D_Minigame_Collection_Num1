using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PopFruitsManager : MonoBehaviour
{
    //No. of Game" #02
    //Rule:
    //Player pop the fruit fall from the spawner, if miss more tha 5 fruist, game end.

    //Common zone
    [HideInInspector] SceneSC sceneMN = new SceneSC();
    [HideInInspector] PauseSC pausePnl;
    [SerializeField] Text scoreTxt;
    [SerializeField] Text lvlTxt;

    //Specific Zone
    [SerializeField] SpawnerSC spawner;
    [SerializeField] Transform parent;
    [SerializeField] Text lostFruits;
    [SerializeField] List<GameObject> fruits = new List<GameObject>();
    [HideInInspector] public int missedFruits;

    private int rand, level, countSeconds, lvlMilestone, curScore;
    private float timeToSpawn;
    private Vector3 spawnerTrans;
    private void Start()
    {
        SetUpStart();
        StartCoroutine(CountingClock());

        pausePnl = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
    }
    private void UpdateTextScore()
    {
        scoreTxt.text = curScore.ToString();
    }
    private void UpdateMissFruitText()
    {
        lostFruits.text = missedFruits.ToString();
    }
    void SetUpStart()
    {
        level = 1;
        timeToSpawn = 10;
        lvlMilestone = 10;
        curScore = 0;
        spawner.SpeedUp(level);

        lvlTxt.text = level.ToString();
        UpdateTextScore();
        UpdateMissFruitText();

        pausePnl = GameObject.Find("CAN_Pause").GetComponent<PauseSC>();
    }
    private void RandFruitToSpawn() => rand = Random.Range(0, fruits.Count);
    private void GetSpawnerPos() => spawnerTrans = spawner.transform.position;
    private void OnSpawnFruits()
    {
        RandFruitToSpawn();
        GetSpawnerPos();
        Instantiate(fruits[rand], spawnerTrans, Quaternion.identity, parent);
    }
    private IEnumerator CountingClock()
    {
        yield return new WaitForSeconds(1f);
        countSeconds++;
        if(countSeconds == lvlMilestone)
        {
            lvlMilestone = countSeconds + 10;
            OnLevelUp();
        }
        StartCoroutine(WaitoSpawn(timeToSpawn));
        StartCoroutine(CountingClock());
    }
    IEnumerator WaitoSpawn(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        OnSpawnFruits();
    }
    private void OnLevelUp()
    {
        level++;
        timeToSpawn = (timeToSpawn * level)/10;
        spawner.SpeedUp(level);

        lvlTxt.text = level.ToString();
    }
    public void CountMiss()
    {
        missedFruits++;
        UpdateMissFruitText();
        if(missedFruits >= 10)
        {
            //pausePnl.ShowPanel(true);
        }
    }
    public void CountSocre() 
    {
        print("coutn score");
        curScore++;
        UpdateTextScore(); 
    }
    public void CounTest()
    {
        print("test");
    }

    public void ToHome() => sceneMN.LoadScene(1, true);
}
