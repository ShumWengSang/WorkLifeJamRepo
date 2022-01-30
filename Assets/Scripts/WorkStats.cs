using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkStats : MonoBehaviour
{
    public List<GameObject> topRowStars = new List<GameObject>();
    public List<GameObject> bottomRowStars = new List<GameObject>();

    public void ShowStars(int earned, int total)
    {
        //half
        if(total > 4)
        {
            var half_plus_one = (total%2==0)?(total / 2) :(total / 2) + 1;
            var half = (total / 2);

            foreach (var star in topRowStars)
            {
                if(half_plus_one > 0)
                {
                    star.SetActive(true);

                    var starScript = star.GetComponent<WorkStar>();
                    if(starScript != null)
                    {
                        if(earned > 0)
                        {
                            starScript.FilledIn();
                            earned--;
                        }
                        else
                        {
                            starScript.Missing();
                        }
                    }

                    half_plus_one--;
                } else
                {
                    star.SetActive(false);
                }
            }
            foreach (var star in bottomRowStars)
            {
                if (half > 0)
                {
                    star.SetActive(true);

                    var starScript = star.GetComponent<WorkStar>();
                    if (starScript != null)
                    {
                        if (earned > 0)
                        {
                            starScript.FilledIn();
                            earned--;
                        }
                        else
                        {
                            starScript.Missing();
                        }
                    }

                    half--;
                }
                else
                {
                    star.SetActive(false);
                }
            }
        }
        else
        {
            foreach (var star in topRowStars)
            {
                if (total > 0)
                {
                    star.SetActive(true);

                    var starScript = star.GetComponent<WorkStar>();
                    if (starScript != null)
                    {
                        if (earned > 0)
                        {
                            starScript.FilledIn();
                            earned--;
                        }
                        else
                        {
                            starScript.Missing();
                        }
                    }

                    total--;
                }
                else
                {
                    star.SetActive(false);
                }
            }
            foreach (var star in bottomRowStars)
            {
                {
                    star.SetActive(false);
                }
            }
        }
    }
}
