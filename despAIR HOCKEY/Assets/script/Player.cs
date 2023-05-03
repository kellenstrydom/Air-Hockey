using UnityEngine;
public class Player
{
    private int score;
    private int collectibles;

    public Player()
    {
        score = 0;
        collectibles = 0;
    }
    public int getScore()
    {
        return score;
    }

    public void Goal()
    {
        ++score;
    }

    public void losePoint()
    {
        if (score != 0 )--score;
    }

    public int getCollectibles()
    {
        return collectibles;
    }

    public void Collect()
    {
        collectibles++;

        if (collectibles >= 3)
        {
            collectibles = 0;
            Goal();
        }
    }
    
    
}
