using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int CurrentLevel;
    private Item[] items;

    public GameObject Entity;
    public GameObject Player;

    public GameObject GoToPos;
    public GameObject GoToAway;

    public float TimeBeforeSpawn = 30f;
    public float TimeBeforeLeave = 5f;

    private float initialTimeBeforeSpawn;
    private Coroutine checkRoutine;

    private Vector3[] ItemPositionsAtStart;
    public bool isThere = false;

    public AudioSource audioSource;
    public AudioClip footstepSound;
    private float LevelTime = 0f;
    void Awake()
    {
        items = FindObjectsByType<Item>(FindObjectsInactive.Include, FindObjectsSortMode.None);
        initialTimeBeforeSpawn = TimeBeforeSpawn;
    }

    void Start()
    {
        UpdateItemsForLevel();
        GetItemPositions();

        if (GoToAway != null) EntityAtPosition(GoToAway.transform);

        checkRoutine = StartCoroutine(CheckOnRoom(GoToPos, GoToAway));
    }

    void GetItemPositions()
    {
        ItemPositionsAtStart = new Vector3[items.Length];
        for (int i = 0; i < items.Length; i++)
        {
            ItemPositionsAtStart[i] = items[i].transform.position;
            Debug.Log($"Item {i} position: {ItemPositionsAtStart[i]}");
        }
    }

    public void UpdateItemsForLevel()
    {
        foreach (var item in items)
        {
            bool shouldBeActive = false;

            int idx = CurrentLevel;
            if (idx >= 0 && idx < item.activeForLevels.Length)
            {
                shouldBeActive = item.activeForLevels[idx];
            }

            item.gameObject.SetActive(shouldBeActive);
        }
    }

    public MessageManager messageManager;
    public string[] hints;
    private bool hintSentForLevel = false;
    public void EndingLevel()
    {
        CurrentLevel++;
        UpdateItemsForLevel();
        LevelTime = 0f;
        Player.GetComponent<PlayerController>().moveSpeed = Player.GetComponent<PlayerController>().originalMoveSpeed;
        Player.GetComponent<PlayerController>().holdingHand.Open();
    }

    public AudioClip dieSound;
    public void LostLevel()
    {
        FindObjectOfType<TakeDrugEffects>().TakeDrug("You were caught...");
        Player.GetComponent<AudioSource>().PlayOneShot(dieSound);

        if (items == null || ItemPositionsAtStart == null) return;

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null) continue;
            items[i].transform.position = ItemPositionsAtStart[i];
        }

        UpdateItemsForLevel();

        TimeBeforeSpawn = initialTimeBeforeSpawn;

        if (GoToAway != null) EntityAtPosition(GoToAway.transform);
        isThere = false;

        if (checkRoutine != null) StopCoroutine(checkRoutine);
        checkRoutine = StartCoroutine(CheckOnRoom(GoToPos, GoToAway));
    }

    public void EntityAtPosition(Transform Pos)
    {
        var tranpos = Pos.position;
        Entity.transform.position = new Vector3(tranpos.x, tranpos.y + 1, tranpos.z);
        print(Pos);
    }

    public IEnumerator CheckOnRoom(GameObject GoTo, GameObject GoToOff)
    {
        float waitTime = TimeBeforeSpawn;

        for (int i = 0; i < 5; i++)
        {
            if (waitTime > 5f)
            {
                yield return new WaitForSeconds(waitTime - 5f);
                if (audioSource != null && footstepSound != null && isThere == false)
                    audioSource.PlayOneShot(footstepSound);
                yield return new WaitForSeconds(5f);
            }
            else
            {
                yield return new WaitForSeconds(waitTime);
            }

            EntityAtPosition(GoTo.transform);
            isThere = true;

            yield return new WaitForSeconds(TimeBeforeLeave);

            EntityAtPosition(GoToOff.transform);
            isThere = false;

            waitTime -= 5f;
        }

        if (waitTime <= 0f)
        {
            LostLevel();
        }
    }

    public float giveHintAfter;
    public SleepSystem sleepSystem;
    void Update()
    {
        LevelTime += Time.deltaTime;
        if (LevelTime >= giveHintAfter && !hintSentForLevel)
        {
            messageManager.SendDialogue(hints[CurrentLevel]);
            hintSentForLevel = true;
        }

        if (isThere)
        {
            Entity.transform.LookAt(Player.transform);
            Entity.transform.localEulerAngles = new Vector3(
                0,
                Entity.transform.localEulerAngles.y,
                0
            );

            if (!sleepSystem.isSleeping)
            {
                LostLevel();
            }
        }
    }
}

