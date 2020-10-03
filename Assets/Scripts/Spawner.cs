using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{

    [SerializeField] private int wordParameter;
    [SerializeField] private int timeParameter;
    [SerializeField] private int starParameter;
    [SerializeField] private int tornadoParameter;
    [SerializeField] private int flashParameter;
    [SerializeField] private GameObject textButtonPrefab;
    [SerializeField] private GameObject tornadosLeftPrefab;
    [SerializeField] private GameObject tornadosRightPrefab;
    [SerializeField] private GameObject lifePrefab;

    private Manager manager;
    private GameObject canvas;
    private AudioManager audioManager;
    private Dictionary dictionary;
    private Text[] texts;
    private Slider timer;
    private ScoreManager scoreManager;
    private Text textAux;
    private Image panelAux;
    private GridLayoutGroup gridLayout;

    private int numPairs;
    private int[] parameters = new int[5];
    private string[] wordPair;
    private int rng;
    private int index;
    private int pairsMatched = 0;
    private int matchSequence;
    private bool isOtherSelected = false;
    private bool isEnSelected;
    private bool isWorking = true;
    private bool isMatched = false;
    private bool isDrawn = false;
    private List<string> enWords = new List<string>();
    private List<string> ptWords = new List<string>();
    private Color32 redColor = new Color32(255, 107, 107, 255);
    private Color32 greenColor = new Color32(0, 255, 50, 255);
    private float recoverLifeChance = 0.25f;
    private int maxLives = 4;

    void Start()
    {

        manager = GameObject.FindObjectOfType<Manager>();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        dictionary = GameObject.FindObjectOfType<Dictionary>();
        timer = GameObject.FindObjectOfType<Slider>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        gridLayout = GetComponent<GridLayoutGroup>();

        parameters = LevelParameters.GetParameters(manager.GetCurrentLevel());
        wordParameter = parameters[0];
        timeParameter = parameters[1];
        starParameter = parameters[2];
        tornadoParameter = parameters[3];
        flashParameter = parameters[4];

        ApplyWordParameter();
        ApplyTimeParameter();

        texts = GetComponentsInChildren<Text>();

        EnlistWords();

        FindSlot(enWords);
        FindSlot(ptWords);

        StartCoroutine(ApplyOtherParameters());

        Messenger.Broadcast(GameEvent.SPAWNED);
    }

    IEnumerator ApplyOtherParameters()
    {
        ApplyStarParameter();
        ApplyTornadoParameter();
        ApplyFlashParameter();
        //Add parameters calls here!

        yield return null;
    }

    void Update()
    {

        //WIN CONDITION
        if (pairsMatched == numPairs)
        {
            Object.Destroy(gameObject);

            manager.OnLevelCompleted();

            timer.GetComponent<Timer>().ResetTimer();

        }

        //LOSE CONDITION
        if (GameObject.FindGameObjectsWithTag("Lives").Length == 0 || timer.value <= 0)
        {

            dictionary.GetComponent<UnityAds>().TriggerAdsCount++; // +1 trigger for Ads. At 4, ads is launched

            OnGameLostAnimation();
            manager.OnGameLost();

        }

    }

    public void AddLife()
    {
        GameObject life = Instantiate(lifePrefab) as GameObject;
        life.transform.SetParent(GameObject.FindGameObjectWithTag("LivesBoard").transform, false);
        audioManager.PlayPotionSound();
    }

    public void OnSelectAnimation(Image panel)
    {
        panel.color = greenColor; //matchColor
    }

    public void OnGameLostAnimation()
    {
        audioManager.PlayLostSound();
    }

    private void ApplyWordParameter()
    {

        GameObject textButton;

        if (wordParameter == 4)
        {

            numPairs = wordParameter / 2;
            //gridLayout.cellSize = new Vector2 (300, 150);

            for (int i = 0; i < wordParameter; i++)
            {
                textButton = Instantiate(textButtonPrefab) as GameObject;
                textButton.transform.SetParent(this.transform, false);
                gridLayout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                gridLayout.constraintCount = 2;
            }

        }

        else if (wordParameter == 6)
        {

            numPairs = wordParameter / 2;
            //gridLayout.cellSize = new Vector2 (300, 150);

            for (int i = 0; i < wordParameter; i++)
            {
                textButton = Instantiate(textButtonPrefab) as GameObject;
                textButton.transform.SetParent(this.transform, false);
                gridLayout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                gridLayout.constraintCount = 2;
            }

        }

        else if (wordParameter == 8)
        {

            numPairs = wordParameter / 2;
            //gridLayout.cellSize = new Vector2 (300, 150);

            for (int i = 0; i < wordParameter; i++)
            {
                textButton = Instantiate(textButtonPrefab) as GameObject;
                textButton.transform.SetParent(this.transform, false);
                gridLayout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                gridLayout.constraintCount = 2;
            }

        }

        else if (wordParameter == 12)
        {

            numPairs = wordParameter / 2;
            //gridLayout.cellSize = new Vector2 (300, 150);

            for (int i = 0; i < wordParameter; i++)
            {
                textButton = Instantiate(textButtonPrefab) as GameObject;
                textButton.transform.SetParent(this.transform, false);
                gridLayout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                gridLayout.constraintCount = 3;
            }

        }

        else if (wordParameter == 16)
        {

            numPairs = wordParameter / 2;
            //gridLayout.cellSize = new Vector2 (300, 150);

            for (int i = 0; i < wordParameter; i++)
            {
                textButton = Instantiate(textButtonPrefab) as GameObject;
                textButton.transform.SetParent(this.transform, false);
                gridLayout.constraint = GridLayoutGroup.Constraint.FixedRowCount;
                gridLayout.constraintCount = 4;
            }

        }

    }

    private void ApplyTimeParameter()
    {

        timer.GetComponent<Timer>().SetLevelSeconds(timeParameter);

    }

    private void ApplyStarParameter()
    {

        // Apply Star Parameter

    }

    private void ApplyTornadoParameter()
    {

        if (tornadoParameter != 0)
        {
            for (int i = 1; i <= tornadoParameter; i++)
            {
                int spawnTime = Random.Range(1, timeParameter);
                StartCoroutine(SpawnTornado(spawnTime));
            }
        }

    }

    IEnumerator SpawnTornado(int spawnTime)
    {
        GameObject tornadosPrefab;
        GameObject tornados;

        float rng = Random.Range(0.00f, 1.00f);
        if (rng <= 0.50f)
        {
            tornadosPrefab = tornadosLeftPrefab;
        }
        else
        {
            tornadosPrefab = tornadosRightPrefab;
        }
        yield return new WaitForSeconds(0.8f * (float)spawnTime);
        tornados = Instantiate(tornadosPrefab) as GameObject;
        tornados.transform.SetParent(canvas.transform, false);

    }

    private void ApplyFlashParameter()
    {

        //Flash Parameter In Development

    }

    //Add Future Parameters here!!
    //Add Future Parameters here!!

    void Matched(Text text, Image panel)
    {
        manager.IncreaseMatchSequence();

        //Object.Destroy(text);
        //Object.Destroy(textAux);
        text.text = "";
        textAux.text = "";

        panel.enabled = false;
        panelAux.enabled = false;
        audioManager.PlayGemsSound();

        pairsMatched++;
        scoreManager.UpScore(panel, panelAux);

        GameObject.Destroy(panel);
        GameObject.Destroy(panelAux);

        //Increase time, last resort!
        timer.GetComponent<Timer>().IncreaseTimer();
    }

    void NotMatched(Image panel)
    {
        manager.ResetMatchSequence();

        panel.color = redColor;
        panelAux.color = redColor;
        audioManager.PlayDamageSound();
        //Messenger.Broadcast (GameEvent.NOT_MATCHED);

        Object.Destroy(GameObject.FindGameObjectWithTag("Lives"));

    }

    public void SendWord(Text text, Image panel)
    {
        //Não existe outro texto selecionado
        if (isOtherSelected == false)
        {

            for (int i = 0; i < numPairs; i++)
            {

                if (text.text == enWords[i])
                {

                    isOtherSelected = true;
                    isEnSelected = true;
                    index = i;
                    textAux = text;
                    panelAux = panel;
                    isWorking = false;

                }
            }

            for (int i = 0; i < numPairs; i++)
            {

                if (text.text == ptWords[i] && isWorking == true)
                {

                    isOtherSelected = true;
                    isEnSelected = false;
                    index = i;
                    textAux = text;
                    panelAux = panel;

                }
            }

            isWorking = true;
        }
        //Existe outro texto selecionado
        else if (isOtherSelected == true)
        {

            //A primeira palavra selecionada é inglesa
            if (isEnSelected == true)
            {

                for (int i = 0; i < numPairs; i++)
                {

                    if (text.text == ptWords[i] && index == i)
                    {
                        //Score a point
                        isMatched = true;
                        Matched(text, panel);
                        Messenger.Broadcast(GameEvent.MATCHED);

                        float random = Random.Range(0.00f, 1.00f);

                        //20% chance to recover a life
                        if (random <= recoverLifeChance && GameObject.FindGameObjectsWithTag("Lives").Length < maxLives) AddLife();

                    }

                    else if (text.text == enWords[i] && index == i)
                    {

                        isMatched = false;
                        isDrawn = true;

                    }

                }

                if (isDrawn)
                {
                    panel.color = redColor;
                }

                if (!isMatched && !isDrawn)
                {

                    NotMatched(panel);

                }

                //Messenger.Broadcast (GameEvent.NOT_MATCHED);

                isOtherSelected = false;
                isEnSelected = false;
                isMatched = false;
                isDrawn = false;
            }

            //A primeira palavra selecionada é portuguesa
            else if (isEnSelected == false)
            {

                for (int i = 0; i < numPairs; i++)
                {

                    if (text.text == enWords[i] && index == i)
                    {
                        //Score a point
                        isMatched = true;
                        Matched(text, panel);
                        Messenger.Broadcast(GameEvent.MATCHED);

                        float random = Random.Range(0.00f, 1.00f);
                        //20% chance to recover a life
                        if (random <= recoverLifeChance && GameObject.FindGameObjectsWithTag("Lives").Length < maxLives) AddLife();

                        }

                    else if (text.text == ptWords[i] && index == i)
                    {

                        isMatched = false;
                        isDrawn = true;

                    }

                }

                if (isDrawn)
                {
                    panel.color = redColor;
                }

                if (!isMatched && !isDrawn)
                {

                    NotMatched(panel);

                }

                //Messenger.Broadcast (GameEvent.NOT_MATCHED);

                isOtherSelected = false;
                isEnSelected = false;
                isMatched = false;
                isDrawn = false;
            }
        }
    }

    //Isolate the word binary into two lists
    void EnlistWords()
    {
        for (int i = 0; i < numPairs; i++)
        {
            wordPair = dictionary.GetWordPairs();
            enWords.Add(wordPair[0]);
            ptWords.Add(wordPair[1]);
        }
    }

    //Find an empty slot for the word list
    void FindSlot(List<string> list)
    {

        int slot;
        foreach (string word in list)
        {

            slot = CheckSlot();
            texts[slot].text = word;

        }
    }

    //Check if the actual slot is empty	
    int CheckSlot()
    {
        int slot;
        rng = Random.Range(0, numPairs * 2);

        if (texts[rng].text == "")
        {

            slot = rng;
            return slot;

        }
        else
        {

            slot = CheckSlot();
            return slot;

        }
    }

    public int GetNumPairs()
    {
        return numPairs;
    }

    public string[] GetRandomPair()
    {
        int rng = Random.Range(0, numPairs);
  
        string[] randomPair = new string[] { enWords[rng], ptWords[rng] };
        return randomPair;
    }

    public string[] GetWordPair(int i)
    {
        string[] randomPair = new string[] { enWords[i], ptWords[i] };
        return randomPair;
    }

    public void IncreasePairsMatched()
    {
        pairsMatched++;
    }

}