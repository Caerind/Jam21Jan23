using System.Collections.Generic;
using UnityEngine;

public abstract class WaveFunctionCollapse2D
{
    protected System.Random randomGenerator;
    protected int _width;
    protected int _height;
    protected List<int>[,] possibilitiesMap = null;
    protected bool initialized = false;
    protected bool collapsed = false;
    protected List<Vector2Int> initList = new List<Vector2Int>();

    public int width => _width;
    public int height => _height;

    public WaveFunctionCollapse2D(int width, int height, int seed = 0)
    {
        _width = width;
        _height = height;

        randomGenerator = new System.Random(seed);

        int possibilityCount = GetPossibilityCount();
        int[] allPossibilities = new int[possibilityCount];
        for (int i = 0; i < possibilityCount; ++i)
        {
            allPossibilities[i] = i;
        }

        possibilitiesMap = new List<int>[width, height];
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                possibilitiesMap[i, j] = new List<int>(allPossibilities);
            }
        }
    }

    protected abstract int GetPossibilityCount();
    protected abstract int GetNeighborLinkCount();
    protected abstract Vector2Int GetNeighborCoordsFromNeighborLink(Vector2Int coords, int neighborLink);
    protected abstract int[] GetPossibles(int possibility, int neighborLink);

    public void Initialize()
    {
        foreach (Vector2Int p in initList)
        {
            Propagate(p);
        }
        initList.Clear();
        initList = null;
        initialized = true;
    }

    public bool IsCollapsed()
    {
        return collapsed;
    }

    public void Iterate()
    {
        UnityEngine.Assertions.Assert.IsTrue(initialized);
        UnityEngine.Assertions.Assert.IsFalse(collapsed);

        Vector2Int coords = GetNextCoords();
        if (coords.x >= 0 && coords.x < width && coords.y >= 0 && coords.y < height)
        {
            int possibility = SelectPossibility(coords);

            possibilitiesMap[coords.x, coords.y].Clear();
            possibilitiesMap[coords.x, coords.y].Add(possibility);

            Propagate(coords);
        }
        else
        {
            collapsed = true;
        }
    }

    public bool HasValidOutput()
    {
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                if (possibilitiesMap[i, j].Count != 1)
                {
                    return false;
                }
            }
        }
        return true;
    }

    public int[,] GetOutput()
    {
        int[,] output = new int[width, height];
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                output[i, j] = (possibilitiesMap[i, j].Count == 1) ? possibilitiesMap[i, j][0] : -1;
            }
        }
        return output;
    }

    protected virtual Vector2Int GetNextCoords()
    {
        List<Vector2Int> bestMatches = new List<Vector2Int>();
        int bestScore = int.MaxValue;
        for (int i = 0; i < width; ++i)
        {
            for (int j = 0; j < height; ++j)
            {
                int score = possibilitiesMap[i, j].Count;
                if (score > 1)
                {
                    if (score < bestScore)
                    {
                        bestMatches.Clear();
                        bestMatches.Add(new Vector2Int(i, j));
                        bestScore = score;
                    }
                    else if (score == bestScore)
                    {
                        bestMatches.Add(new Vector2Int(i, j));
                    }
                }
            }
        }

        if (bestMatches.Count > 0)
        {
            return bestMatches[randomGenerator.Next(bestMatches.Count)];
        }
        else
        {
            return new Vector2Int(-1, -1);
        }
    }

    private void Propagate(Vector2Int updatedCoords)
    {
        Stack<Vector2Int> stack = new Stack<Vector2Int>();
        stack.Push(updatedCoords);
        while (stack.Count > 0)
        {
            Vector2Int coords = stack.Pop();
            List<int> possibilities = possibilitiesMap[coords.x, coords.y];
            for (int neighborLink = 0; neighborLink < GetNeighborLinkCount(); ++neighborLink)
            {
                Vector2Int neighborCoords = GetNeighborCoordsFromNeighborLink(coords, neighborLink);
                if (neighborCoords.x >= 0 && neighborCoords.x < width && neighborCoords.y >= 0 && neighborCoords.y < height)
                {
                    int[] neighborPossibilities = possibilitiesMap[neighborCoords.x, neighborCoords.y].ToArray();
                    if (neighborPossibilities.Length <= 1)
                        continue;

                    List<int> possibles = new List<int>();
                    foreach (int possibility in possibilities)
                    {
                        possibles.AddRange(GetPossibles(possibility, neighborLink));
                    }

                    if (possibles.Count == 0)
                    {
                        continue;
                    }

                    foreach (int adjacentPossibility in neighborPossibilities)
                    {
                        if (!possibles.Contains(adjacentPossibility))
                        {
                            if (possibilitiesMap[neighborCoords.x, neighborCoords.y].Remove(adjacentPossibility))
                            {
                                if (!stack.Contains(neighborCoords))
                                {
                                    stack.Push(neighborCoords);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    protected virtual int SelectPossibility(Vector2Int coords)
    {
        List<int> possibilities = possibilitiesMap[coords.x, coords.y];
        return possibilities[randomGenerator.Next(possibilities.Count)];
    }

    public bool RemovePossibilityAtCoords(int possibility, Vector2Int coords)
    {
        UnityEngine.Assertions.Assert.IsFalse(initialized);
        if (!initList.Contains(coords))
        {
            initList.Add(coords);
        }
        return possibilitiesMap[coords.x, coords.y].Remove(possibility);
    }

    public List<int> GetPossibilitiesAt(Vector2Int coords)
    {
        return possibilitiesMap[coords.x, coords.y];
    }
}
