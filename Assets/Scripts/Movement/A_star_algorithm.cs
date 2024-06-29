using System;
using System.Collections.Generic;

public class A_star_movement
{
    public static List<Hex> Get_reachable_hexes(Hex startHex, float maxCost, Unit.Unit_type type)
    {
        Dictionary<Hex, float> distances = new();
        List<Hex> reachableHexes = new();
        Priority_Queue<Tuple<float, Hex>> priorityQueue = new Priority_Queue<Tuple<float, Hex>>();

        distances[startHex] = 0f;
        priorityQueue.Enqueue(Tuple.Create(0f, startHex), 0f);

        while (priorityQueue.Count > 0)
        {
            var (currentDistance, currentHex) = priorityQueue.Dequeue();

            if (currentDistance > maxCost)
                continue;

            reachableHexes.Add(currentHex);

            foreach (Hex.Edge_info edgeInfo in currentHex.Edges)
            {
                Hex neighborHex = edgeInfo.Hex;
                if (neighborHex == null || !Movement.Movement_posibility(currentHex, neighborHex))
                    continue;

                float cost = Movement.Calculate_movement_cost(type, currentHex, neighborHex);
                float tentativeDistance = currentDistance + cost;

                if (tentativeDistance <= maxCost)
                {
                    if (!distances.ContainsKey(neighborHex) || tentativeDistance < distances[neighborHex])
                    {
                        distances[neighborHex] = tentativeDistance;
                        priorityQueue.Enqueue(Tuple.Create(tentativeDistance, neighborHex), tentativeDistance);
                    }
                }
            }
        }

        return reachableHexes;
    }

    public static List<Hex> Get_hexes_in_range(Hex startHex, int range)
    {
    List<Hex> hexesInRange = new();
    Queue<(Hex hex, int currentRange)> queue = new();
    HashSet<Hex> visited = new();

    hexesInRange.Add(startHex);
    queue.Enqueue((startHex, range));
    visited.Add(startHex);

    while (queue.Count > 0)
    {
        var (currentHex, currentRange) = queue.Dequeue();
        if (currentRange > 0)
        {
            foreach (var edge in currentHex.Edges)
            {
                Hex neighbourHex = edge.Hex;
                if (neighbourHex != null && !visited.Contains(neighbourHex) &&
                     Movement.Is_edge_passable(edge.Value) && Movement.Is_hex_enterable(neighbourHex.Terrain))
                {
                    hexesInRange.Add(neighbourHex);
                    queue.Enqueue((neighbourHex, currentRange - 1));
                    visited.Add(neighbourHex);
                }
            }
        }
    }

    return hexesInRange;
}
}
