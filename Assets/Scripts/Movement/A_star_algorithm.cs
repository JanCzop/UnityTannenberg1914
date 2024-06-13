using System;
using System.Collections.Generic;

public class A_star_movement
{
    public static List<Hex> Get_reachable_hexes(Hex startHex, float maxCost, Unit unit)
    {
        Dictionary<Hex, float> distances = new Dictionary<Hex, float>();
        List<Hex> reachableHexes = new List<Hex>();
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
                Hex neighborHex = edgeInfo.Value.Neighbour_hex;
                if (neighborHex == null || !Movement.Movement_posibility(currentHex, neighborHex))
                    continue;

                float cost = Movement.Calculate_movement_cost(unit, currentHex, neighborHex);
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
}
