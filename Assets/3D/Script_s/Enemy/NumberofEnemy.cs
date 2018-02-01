using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberofEnemy : MonoBehaviour {
    private int _enemynumber = 4;

    public RandomEnemy random;
    public Sprite[] numimage;
    public List<int> number = new List<int>();

    public int EnemyNumber
    {
        get { return _enemynumber; }
        set
        {
            _enemynumber = value;
            var objs = GameObject.FindGameObjectsWithTag("EnemyNumber");
            foreach (var obj in objs)
            {
                if (0 <= obj.name.LastIndexOf("Clone"))
                {
                    Destroy(obj);
                }
            }
            View(_enemynumber);
        }
    }

    void View(int score)
    {
        var digit = score;
        number = new List<int>();
        while (digit != 0)
        {
            score = digit % 10;
            digit = digit / 10;
            number.Add(score);
        }

        GameObject.Find("NumberofEnemy").GetComponent<Image>().sprite = numimage[number[0]];
        for (int i = 1; i < number.Count; i++)
        {
            RectTransform numberimage = (RectTransform)Instantiate(GameObject.Find("NumberofEnemy")).transform;
            numberimage.SetParent(this.transform, false);
            numberimage.localPosition = new Vector2(
                numberimage.localPosition.x - numberimage.sizeDelta.x * i / 4,
                numberimage.localPosition.y);
            numberimage.GetComponent<Image>().sprite = numimage[number[i]];
        }
    }

    void Start()
    {
        //_enemynumber = random.CreateEnemy;
        View(_enemynumber);
    }


    void Update()
    {
    }
}
