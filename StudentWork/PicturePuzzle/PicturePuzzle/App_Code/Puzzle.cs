using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Puzzle
/// </summary>
public class Puzzle
{
    int[] pictures;
    const int NUM_PIECES = 12;

	public Puzzle()
	{
        pictures = new int[NUM_PIECES]  { 10, 12, 9, 1, 8, 2, 7, 3, 6, 4, 5, 11 };
	}

    public Puzzle(int[] p)
    {
        pictures = p;
    }

    public int[] Pictures
    {
        get { return pictures; }
    }

    public void move(string source, string destination)
    {
        int src = int.Parse(source), dst = int.Parse(destination);
        int temp = pictures[dst];
        pictures[dst] = pictures[src];
        pictures[src] = temp;
        temp = 0;
    }


    public bool isFinished()
    {
        bool finished = true;

        for (int i = 0; i < NUM_PIECES - 1; i++)
            if (pictures[i] != pictures[i + 1] - 1)
                finished = false;

        return finished;
    }

}