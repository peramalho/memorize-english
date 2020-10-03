using UnityEngine;
using UnityEngine.UI;

public class LightningBolt : MonoBehaviour
{
    private int pos1 = -300;
    private int pos2 = -160;
    private int pos3 = -10;
    private int pos4 = 140;
    private int pos5 = 290;

    [SerializeField] int mana = 0;
    [SerializeField] private Sprite[] manaSprites;
    [SerializeField] private Sprite[] lightningSprites;
    [SerializeField] private GameObject particles;
    [SerializeField] private GameObject actualLightning;
    [SerializeField] private GameObject zeusAnimation;
    [SerializeField] private Canvas canvas;

    private Spawner spawner;
    private AudioManager audioManager;
    private ScoreManager scoreManager;
    private Slider timer;
    private Image actualImage;
    private Image panelEn;
    private Image panelPt;

    public void CastSpell()
    {
        if ((mana >= 100) && (mana <= 199))
        {
            mana -= 100;
            manaAnimation();

            GetComponentInChildren<Animator>().SetBool("ManaTrigger", false);
            actualLightning.GetComponent<Image>().overrideSprite = lightningSprites[0];

            Spell();

            //GameObject.Destroy(panelEn);
            //GameObject.Destroy(panelPt);

            audioManager.PlayLightningSound();
        }

        else if ((mana >= 200) && (mana <= 299))
        {
            mana -= 200;
            manaAnimation();

            actualLightning.GetComponent<Image>().overrideSprite = lightningSprites[0];
            GetComponentInChildren<Animator>().SetBool("ManaTrigger", false);

            Spell();
            Spell();

            //GameObject.Destroy(panelEn);
            //GameObject.Destroy(panelPt);

            audioManager.PlayLightningSound();
        }

        else if (mana >= 300)
        {
            mana -= 300;
            manaAnimation();

            actualLightning.GetComponent<Image>().overrideSprite = lightningSprites[0];
            GetComponentInChildren<Animator>().SetBool("ManaTrigger", false);

            Spell();
            Spell();
            Spell();

            //GameObject.Destroy(panelEn);
            //GameObject.Destroy(panelPt);

            audioManager.PlayLightningSound();
        }

    }

    private void CastZeusAnimation()
    {
        GameObject zeus1 = Instantiate(zeusAnimation) as GameObject;
        zeus1.transform.SetParent(canvas.transform, false);
        Vector3 pos1 = panelEn.transform.position;
        pos1 = pos1 + new Vector3(0, 75, 0);
        zeus1.transform.position = pos1;
        zeus1.transform.SetSiblingIndex(6);

        GameObject zeus2 = Instantiate(zeusAnimation) as GameObject;
        zeus2.transform.SetParent(canvas.transform, false);
        Vector3 pos2 = panelPt.transform.position;
        pos2 = pos2 + new Vector3(0, 75, 0);
        zeus2.transform.position = pos2;
        zeus2.transform.SetSiblingIndex(6);

    }

    private void Spell()
    {
        spawner = GameObject.FindObjectOfType<Spawner>();

        GameObject[] wordsObjects = GameObject.FindGameObjectsWithTag("Words");
        int index = 0;

        string[] wordPair = CheckWordAlive(index, wordsObjects);

        //Search the words in table
        GameObject wordObjectEn = SearchWord(wordPair[0]);
        GameObject wordObjectPt = SearchWord(wordPair[1]);


        //Destroy the words and get the panels for the score animation
        panelEn = DestroyWord(wordObjectEn);
        panelPt = DestroyWord(wordObjectPt);
        scoreManager.UpScore(panelEn, panelPt);

        spawner.IncreasePairsMatched();

        timer.GetComponent<Timer>().IncreaseTimer();

        wordObjectEn.GetComponent<Button>().enabled = false;
        wordObjectPt.GetComponent<Button>().enabled = false;

        //Lightning animation art
        CastZeusAnimation();

    }

    private string[] CheckWordAlive(int index, GameObject[] wordsObjects)
    {
        string[] wordPair = spawner.GetWordPair(index);

        foreach (GameObject wordObject in wordsObjects)
        {
            if (wordPair[0] == wordObject.GetComponentInChildren<Text>().text)
            {
                return wordPair;
            }
        }

        index++;
        return CheckWordAlive(index, wordsObjects);
    }

    private GameObject SearchWord(string word)
    {
        GameObject[] wordsObjects = GameObject.FindGameObjectsWithTag("Words");

       foreach(GameObject wordObject in wordsObjects)
       {
            if (word == wordObject.GetComponentInChildren<Text>().text)
            {
                return wordObject;
            }
       }
       return null;
    }

    private Image DestroyWord(GameObject wordObject)
    {
        Text text = wordObject.GetComponentInChildren<Text>();
        Image panel = wordObject.GetComponentInChildren<Image>();

        //Destroy text and hide panel
        //UnityEngine.Object.Destroy(text);
        text.text = "";
        panel.enabled = false;

        audioManager.PlayGemsSound();

        return panel;
    }

    private void RecoverMana()
    {
        if (mana < 300)
        {
            mana += 25;
            manaAnimation();
        }
    }

    private void manaAnimation()
    {
        if (mana <= 0)
        {
            actualImage.sprite = manaSprites[0];
            particles.GetComponent<RectTransform>().localPosition = new Vector3(0, pos1, 0);
        }
        if (mana == 25)
        {
            actualImage.sprite = manaSprites[1];
            particles.GetComponent<RectTransform>().localPosition = new Vector3(0, pos2, 0);
        }
        if (mana == 50)
        {
            actualImage.sprite = manaSprites[2];
            particles.GetComponent<RectTransform>().localPosition = new Vector3(0, pos3, 0);
        }
        if (mana == 75)
        {
            actualImage.sprite = manaSprites[3];
            particles.GetComponent<RectTransform>().localPosition = new Vector3(0, pos4, 0);
        }
        if (mana == 100)
        {
            actualImage.sprite = manaSprites[4];
            particles.GetComponent<RectTransform>().localPosition = new Vector3(0, pos5, 0);

            actualLightning.GetComponent<Image>().overrideSprite = lightningSprites[1];
            GetComponentInChildren<Animator>().SetBool("ManaTrigger", true);
        }
        if (mana == 125)
        {
            actualImage.sprite = manaSprites[5];
            particles.GetComponent<RectTransform>().localPosition = new Vector3(0, pos2, 0);
        }
        if (mana == 150)
        {
            actualImage.sprite = manaSprites[6];
            particles.GetComponent<RectTransform>().localPosition = new Vector3(0, pos3, 0);
        }
        if (mana == 175)
        {
            actualImage.sprite = manaSprites[7];
            particles.GetComponent<RectTransform>().localPosition = new Vector3(0, pos4, 0);
        }
        if (mana == 200)
        {
            actualImage.sprite = manaSprites[8];
            particles.GetComponent<RectTransform>().localPosition = new Vector3(0, pos5, 0);
        }
        if (mana == 225)
        {
            actualImage.sprite = manaSprites[9];
            particles.GetComponent<RectTransform>().localPosition = new Vector3(0, pos2, 0);
        }
        if (mana == 250)
        {
            actualImage.sprite = manaSprites[10];
            particles.GetComponent<RectTransform>().localPosition = new Vector3(0, pos3, 0);
        }
        if (mana == 275)
        {
            actualImage.sprite = manaSprites[11];
            particles.GetComponent<RectTransform>().localPosition = new Vector3(0, pos4, 0);
        }
        if (mana >= 300)
        {
            actualImage.sprite = manaSprites[12];
            particles.GetComponent<RectTransform>().localPosition = new Vector3(0, pos5, 0);
        }

    }

    void Awake()
    {
        Messenger.AddListener(GameEvent.MATCHED, RecoverMana);

    }

    void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
        timer = GameObject.FindObjectOfType<Slider>();
        actualImage = GetComponent<Image>();
    }

    void OnDestroy()
    {
        Messenger.RemoveListener(GameEvent.MATCHED, RecoverMana);
    }

}
