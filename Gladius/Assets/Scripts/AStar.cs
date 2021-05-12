using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AStar
{
    /// <summary>
    /// Returns the best path as a List of Tiles
    /// </summary>
    public static List<Tile> Search(Map map, Tile start, Tile goal = null)
    {
        Dictionary<Tile, Tile> came_from = new Dictionary<Tile, Tile>();
        Dictionary<Tile, int> cost_so_far = new Dictionary<Tile, int>();

        List<Tile> path = new List<Tile>();

        Queue<Tile> frontier = new Queue<Tile>();
        frontier.Enqueue(start);

        came_from.Add(start, start);
        cost_so_far.Add(start, 0);

        Tile current = start;
        while (frontier.Count > 0)
        {
            current = frontier.Dequeue();
            if (current == goal) break; // Early exit

            foreach (Tile next in map.GetNeighbours(current))
            {
                int new_cost = cost_so_far[current] + next.TravelCost;
                if (!cost_so_far.ContainsKey(next) || new_cost < cost_so_far[next])
                {
                    cost_so_far[next] = new_cost;
                    came_from[next] = current;
                    frontier.Enqueue(next);
                }
            }
        }

        while (current != start)
        {
            path.Add(current);
            current = came_from[current];
        }
        path.Reverse();

        return path;
    }

    public static List<Tile> DisplayRange(Map map, Tile start, int maxCost)
    {
        Dictionary<Tile, Tile> came_from = new Dictionary<Tile, Tile>();
        Dictionary<Tile, int> cost_so_far = new Dictionary<Tile, int>();

        List<Tile> path = new List<Tile>();

        Queue<Tile> frontier = new Queue<Tile>();
        frontier.Enqueue(start);

        came_from.Add(start, start);
        cost_so_far.Add(start, 0);

        Tile current = start;
        while (frontier.Count > 0)
        {
            current = frontier.Dequeue();

            foreach (Tile next in map.GetNeighbours(current))
            {
                int new_cost = cost_so_far[current] + next.TravelCost;
                if (!cost_so_far.ContainsKey(next) || new_cost < cost_so_far[next])
                {
                    cost_so_far[next] = new_cost;
                    came_from[next] = current;
                    if (new_cost <= maxCost)
                    {
                        frontier.Enqueue(next);
                        if (!path.Contains(next))
                        {
                            path.Add(next);
                        }
                    }
                }
            }
        }

        return path;
    }

    public static int Heuristic(Tile a, Tile b)
    {
        return Mathf.Abs(a.Pos.x - b.Pos.x) + Mathf.Abs(a.Pos.y - b.Pos.y);
    }
}