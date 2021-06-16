using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ComponentType
{
    public string description = "";
    public Sprite[] sprites;
    public string[] texts;
}

public class Game : MonoBehaviour
{
    public Action<string> OnUpdateFindTextEvent;

    [SerializeField] ComponentType[] _componentTypes;

    [SerializeField] ObjectPool _objectPool;

    [SerializeField] GameLevel gameLevel;

    [SerializeField] FindPanel _findPanel;

    [SerializeField] FadeInOut _fadeInOut;

    private Dictionary<int, string> dictionaryData;
    
    public float firstCoordX = -4f;
    public float firstCoordY = 4f;
    public float stepCoord = 4f;
    private float currentCoordX;
    private float currentCoordY;
    private int _countCells = 3;
    public int CountCells => _countCells;
    private int _levelIndex;
    private int _maxLevelIndex;
    private int _indexComponenType;
    private int _selectedIndex;

    private List<int> _intRandomList;

    private List<int> _shuffledCellValues;

    private static System.Random rng = new System.Random();

    public bool gameOver = false;

    private bool firstLaunch = true;

    private void Awake()
    {
        OnUpdateFindTextEvent += _findPanel.OnUpdateFindTextEvent;
    }

    private void Start()
    {
        Init();
        UpdateLevel();
        FirstLaunch();
    }

    /// <summary>
    /// waiting for ours to be ready for execution
    /// </summary>
    public IEnumerator GameRestart(Action action, float duration)
    {
        yield return new WaitForSeconds(duration);
        GameReset();

        action.Invoke();
    }

    /// <summary>
    /// check the values for matches, if the values match, then update the interface and look for a new value. 
    /// If the values do not match, play the animation.
    /// </summary>
    public void MatchCheck(Action onIncorrectCell, string text)
    {
        if (text == dictionaryData[_shuffledCellValues[_selectedIndex]] && !gameOver)
        {
            _selectedIndex++;
            onIncorrectCell.Invoke();
            if (_selectedIndex < _countCells)
            {
                OnUpdateFindTextEvent.Invoke(dictionaryData[_shuffledCellValues[_selectedIndex]]);
            }
            else
            {
                NextLevel();
            }
        }
        else if (text != dictionaryData[_shuffledCellValues[_selectedIndex]])
        {
            onIncorrectCell.Invoke();
        }
    }

    /// <summary>
    /// we reset the game to the first level
    /// </summary>
    private void GameReset()
    {
        gameLevel = GameLevel.EASY;
        gameOver = false;
        Init();
        UpdateLevel();
    }

    /// <summary>
    /// animate all blocks when you first load the game
    /// </summary>
    private void FirstLaunch()
    {
        foreach (GameObject obj in _objectPool._allObjectCells)
        {
            obj.GetComponent<Cell>().StartAnimation();
        }

        firstLaunch = false;
    }

    /// <summary>
    /// the next level checks if there is a next level, if not, 
    /// then the game is over, we call the reset panel, otherwise go to the next level
    /// </summary>
    private void NextLevel()
    {        
        _levelIndex++;
        if (_levelIndex < _maxLevelIndex)
        {
            gameLevel = (GameLevel)Enum.GetValues(typeof(GameLevel)).GetValue(_levelIndex);
        }
        else
        {
            gameOver = true;
            _fadeInOut.gameObject.SetActive(true);
            return;
        }

        UpdateLevel();
    }

    /// <summary>
    /// when updating the level, we will set new blocks, 
    /// turn on all the necessary blocks and fill in all the necessary data
    /// </summary>
    public void UpdateLevel()
    {
        currentCoordX = firstCoordX;
        currentCoordY = firstCoordY;
        _selectedIndex = 0;
        _indexComponenType = GetRandomComponentType();
        _countCells = GetCountCells();
        _shuffledCellValues = new List<int>();
        _intRandomList = new List<int>();
        FillListAndShuffle(_componentTypes[_indexComponenType].sprites.Length, _intRandomList);
        FillListAndShuffle(_countCells, _shuffledCellValues);
        _objectPool.Reset();
        
        for (int i = 0; i < _countCells; i++)
        {  
            if (i % 3 == 0 && i != 0)
            {
                currentCoordY += (-stepCoord);
                currentCoordX = firstCoordX;
            }

            GameObject obj = _objectPool._allObjectCells[i];
            obj.transform.position = new Vector2(currentCoordX, currentCoordY);
            Cell cell = obj.GetComponent<Cell>();
            cell.Init(_componentTypes[_indexComponenType].sprites[_intRandomList[i]], _componentTypes[_indexComponenType].texts[_intRandomList[i]], this);
            obj.SetActive(true);
            if (!firstLaunch)
            {
                cell.transform.localScale = new Vector3(1, 1, 1);
            }

            currentCoordX += stepCoord;
        }

        FillDictionary();  
        OnUpdateFindTextEvent.Invoke(dictionaryData[_shuffledCellValues[_selectedIndex]]);
    }

    #region Init Data
    private int GetRandomComponentType()
    {
        return UnityEngine.Random.Range(0, _componentTypes.Length);
    }

    private int GetCountCells()
    {
        switch (gameLevel)
        {
            case GameLevel.EASY:
                return 3;
            case GameLevel.MIDDLE:
                return 6;
            case GameLevel.HARD:
                return 9;
        }

        return 3;
    }

    private void Init()
    {
        _levelIndex = (int) gameLevel;
        _maxLevelIndex = Enum.GetValues(typeof(GameLevel)).Length;
        currentCoordX = firstCoordX;
        currentCoordY = firstCoordY;
    }

    private void FillDictionary()
    {
        dictionaryData = new Dictionary<int, string>();

        for (int i = 0; i < _countCells; i++)
        {
            dictionaryData.Add(i, _componentTypes[_indexComponenType].texts[_intRandomList[i]]);
        }
    }

    private void FillListAndShuffle(int maxIndex, List<int> list)
    {
        for (int i = 0; i < maxIndex; i++)
        {
            list.Add(i);
        }
        Shuffle(list);
    }

    #endregion

    /// <summary>
    /// shuffle the required arrays
    /// </summary>
    private void Shuffle(List<int> list)
    {
        int n = list.Count;

        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            int value = list[k];
            list[k] = list[n];
            list[n] = value; 
        }
    }
}

// GAME LEVEL
public enum GameLevel
{
    EASY,
    MIDDLE,
    HARD
}