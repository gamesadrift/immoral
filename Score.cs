using UnityEngine;

public class Score : MonoBehaviour
{
    public GameObject floatingText;

    public int good;
    public int bad;

    Character goodGuy;
    Character badGuy;

    int factorGood;
    int factorBad;

    object lockGood = new object();
    object lockBad = new object();

    // Start is called before the first frame update
    void Start()
    {
        good = 0;
        bad = 0;
        goodGuy = GameObject.Find("GoodGuy").GetComponent<Character>();
        badGuy = GameObject.Find("BadGuy").GetComponent<Character>();
        factorGood = 1;
        factorBad = 1;
    }

    // Update is called once per frame
    void Update()
    {
        factorGood = goodGuy.DoubleMoral ? 2 : 1;
        factorBad = badGuy.DoubleMoral ? 2 : 1;
    }

    public void GoodPoints(int value, Vector3 position)
    {
        int score = value * factorGood;
        ShowFloatingScore(true, score, position);
        lock(lockGood) good += score;
    }

    public void BadPoints(int value, Vector3 position)
    {
        int score = value * factorBad;
        ShowFloatingScore(false, score, position);
        lock (lockGood) bad += score;
    }

    void ShowFloatingScore(bool good, int value, Vector3 position)
    {
        GameObject text = Instantiate(floatingText, position, Quaternion.identity, transform);
        TextMesh mesh = text.GetComponent<TextMesh>();
        mesh.text = "+" + value;
        FloatingText ft = text.GetComponent<FloatingText>();
        if (good)
        {
            mesh.color = ft.goodColor;
            ft.goodSound.Play();
        }
        else
        {
            mesh.color = ft.badColor;
            ft.badSound.Play();
        }   
    }
}
