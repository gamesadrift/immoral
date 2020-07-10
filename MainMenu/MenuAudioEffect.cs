using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuAudioEffect : MonoBehaviour
{
    private static int countDown_0 = 3200;
    private static int countDown2_0 = 400;
    private int countDown = countDown_0;
    private int countDown2 = countDown2_0;
    private int half_cd2 = countDown2_0 / 2;
    private int quarter_cd2 = countDown2_0 / 4;

    void Update()
    {
        AudioSource AS = this.gameObject.GetComponent<AudioSource>();

        if (countDown != 0) countDown -= 1;
        else
        {
            float first_term = (float)countDown2 / (float)quarter_cd2;
            if (countDown2 > half_cd2) AS.pitch = first_term - 3.0f;
            else AS.pitch = -first_term + 1.0f;
 
            if (countDown2 != 0) countDown2 -= 1;
            else
            {
                countDown = countDown_0;
                countDown2 = countDown2_0;
                AS.pitch = 1;
            }
        }
    }
}
