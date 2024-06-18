using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Combination : MonoBehaviour
{
    public TextMeshProUGUI combinationText;
    public GameObject[] numberDisplay;
    public int[] possibleNumbers;
    public Transform[] possibleLocations;
    [SerializeField] private List <int> correctNumbers = new List<int>();
    [SerializeField] private List <int> correctCombination = new List<int>();
    [SerializeField] private GameObject greenLight;
    [SerializeField] private LockedDoor lockedDoor;
    [SerializeField] private AudioSource keypadSound;
    [SerializeField] private AudioSource[] unlockSound;
    private List <int> inputCombination = new List<int>();
    private int numInputs = 0;
    public int numAttempts; // private
    public bool codeCracked = false;
    void Start()
    {
        combinationText.text = "";
        greenLight.SetActive(false);
        RandomizeNumbers();
    }

    public void RandomizeNumbers()
    {
        correctNumbers.Clear();
        while (correctNumbers.Count < 4)
        {
           int num = RandomNumber(possibleNumbers.Length);
           if (!correctNumbers.Contains(num)) 
           {
                correctNumbers.Add(num);
           }
        }
        RandomizeLocations(correctNumbers);
        RandomCombination(correctNumbers);
    }

    public int RandomNumber(int size)
    {
        return Random.Range(0, size);
    }

    public void RandomizeLocations(List<int> numList)
    {
        HashSet<int> usedPositions = new HashSet<int>(); // track used positions
        for (int i = 0; i < numList.Count; i++)
        {
            int numToDisplay = 0;
            switch (numList[i])
            {
                case 0:
                    Debug.Log("0/");
                    numToDisplay = 0;
                    break;
                case 1:
                    Debug.Log("1/");
                    numToDisplay = 1;
                    break;
                case 2:
                    Debug.Log("2/");
                    numToDisplay = 2;
                    break;
                case 3:
                    Debug.Log("3/");
                    numToDisplay = 3;
                    break;
                case 4:
                    Debug.Log("4/");
                    numToDisplay = 4;
                    break;
                case 5:
                    Debug.Log("5/");
                    numToDisplay = 5;
                    break;
                case 6:
                    Debug.Log("6/");
                    numToDisplay = 6;
                    break;
                case 7:
                    Debug.Log("7/");
                    numToDisplay = 7;
                    break;
                case 8:
                    Debug.Log("8/");
                    numToDisplay = 8;
                    break;
                case 9:
                    Debug.Log("9/");
                    numToDisplay = 9;
                    break;
                default:
                    Debug.Log("ERROR");
                    numToDisplay = 0;
                    break;
            }
            
            int num;
            do
            {
                num = Random.Range(0, possibleLocations.Length);
            } 
            while (usedPositions.Contains(num));
            usedPositions.Add(num);
    
            numberDisplay[numToDisplay].transform.position = possibleLocations[num].position;
            numberDisplay[numToDisplay].transform.rotation = possibleLocations[num].rotation;
            numberDisplay[numToDisplay].SetActive(true);
        }
    }

    public void RandomCombination(List<int> numList)
    {
        List<int> tempList = new List<int>(numList);
        int count = numList.Count;

        while (count > 1)
        {
            count--;
            int i = Random.Range(0, count + 1);
            int temp = tempList[i];
            tempList[i] = tempList[count];
            tempList[count] = temp;
        }
        correctCombination = new List<int>(tempList);
    }



    public void GetInput(int num)
    {
        if (numInputs < 4)
        {
            keypadSound.Play();
            inputCombination.Add(num);
            combinationText.text += num.ToString();
            numInputs++;

            if (numInputs == 4)
            {
                CombinationHint();
                CheckCombination();
            }
        }
        else
        {
           CheckCombination();
        }
    }

    public void CheckCombination()
    {
        if (inputCombination.Count != correctCombination.Count)
        {
            return;
            unlockSound[0].Play();
        }

        for (int i = 0; i < inputCombination.Count; i++)
        {
            if (inputCombination[i] != correctCombination[i] && numAttempts < 4) 
            {
                inputCombination.Clear();
                numInputs = 0;
                combinationText.text = "";
                unlockSound[0].Play();
                return;
            }
            if (inputCombination[i] != correctCombination[i] && numAttempts >= 4 && numAttempts < 7) // Hint after 4 attempts
            {
                inputCombination.Clear();
                inputCombination.Add(correctCombination[0]);
                numInputs = 1;
                combinationText.text = correctCombination[0].ToString();
                unlockSound[0].Play();
                return;
            } 
            if (inputCombination[i] != correctCombination[i] && numAttempts >= 7) // Hint after 7 attempts
            {
                inputCombination.Clear();
                inputCombination.Add(correctCombination[0]);
                inputCombination.Add(correctCombination[1]);
                numInputs = 2;
                combinationText.text = correctCombination[0].ToString() + correctCombination[1].ToString();
                unlockSound[0].Play();
                return;
            } 
        }
        greenLight.SetActive(true);
        unlockSound[1].Play();
        StartCoroutine(HideCombination());
    }

    public void CombinationHint()
    {
        if (inputCombination.Count != correctCombination.Count)
        {
            return;
        }
        for (int i = 0; i < inputCombination.Count; i++)
        {
            if (!correctCombination.Contains(inputCombination[i]))
            {
                return;
            }
        }
        numAttempts++;
    }


    private IEnumerator HideCombination()
    {
        yield return new WaitForSeconds(1f);
        lockedDoor.UnlockDoor();
        codeCracked = true;

    }



}
